using Game.Pooling;
using System;
using UnityEngine;
using Game.Core;
using Game.Presentation;

namespace Game.Gameplay
{
    /// <summary>
    /// 玩家子弹.
    /// C# 只保留刚体速度写入和对象池重置, 命中与存活规则在预制体的 LuaComponet 模块里.
    /// </summary>
    public class PlayerBullet : MonoBehaviour, Game.Pooling.IPoolable {
        public Vector2 dir;
        public float speed = 15f;
        public Rigidbody2D rb;
        public int damage;

        [Header("子弹数据 id, 对应 BulletData.lua")]
        [SerializeField] private string bulletId;
        [SerializeField] private AudioPlay _audioPlay;

        private bool hasHit = false;
        private LuaBehaviourHost bulletLua;

        /// <summary>
        /// 初始化运行时依赖.
        /// </summary>
        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
            _audioPlay = GetComponent<AudioPlay>();
            bulletLua = GetComponent<LuaBehaviourHost>();
            ConfigureHitColliders();
            gameObject.layer = LayerMask.NameToLayer("PlayerBullet");
        }

        /// <summary>
        /// 玩家子弹只需要触发命中, 禁用物理碰撞避免把敌人击退.
        /// </summary>
        private void ConfigureHitColliders()
        {
            var colliders = GetComponents<Collider2D>();
            foreach (var hitCollider in colliders)
            {
                hitCollider.isTrigger = true;
            }
        }

        /// <summary>
        /// 从对象池取出后由 WeaponManager 调用, 写入本次发射参数并通知 Lua 模块.
        /// </summary>
        public void Init(Vector2 shootDir, int bulletDamage,int bulletSpeed) {
            if (bulletLua == null)
            {
                throw new System.Exception($"{nameof(PlayerBullet)} 预制体缺少 LuaComponet, 无法初始化弹道规则.");
            }

            if (string.IsNullOrWhiteSpace(bulletId))
            {
                throw new System.Exception($"{nameof(PlayerBullet)} 未配置 bulletId, 无法读取 BulletData.");
            }

            var config = LuaDataRuntime.GetBulletConfig(bulletId);
            dir = shootDir;
            damage = bulletDamage;
            speed = bulletSpeed;

            bulletLua.SetLuaField("bullet", this);
            bulletLua.SetLuaField("bulletId", bulletId);
            bulletLua.SetLuaField("dir", dir);
            bulletLua.SetLuaField("damage", damage);
            bulletLua.SetLuaField("speed", speed);
            bulletLua.SetLuaField("lifeTime", config.LifeTime);
            bulletLua.CallLuaFunction("OnSpawn");
        }

        /// <summary>
        /// 从对象池取出子弹时调用.
        /// </summary>
        public void OnSpawnFromPool() {
            hasHit = false;
            _audioPlay?.Clear();
            if (bulletLua != null)
            {
                bulletLua.SetLuaField("lifeTimer", 0f);
                bulletLua.SetLuaField("hitTarget", null);
                bulletLua.CallLuaFunction("OnSpawnFromPool");
            }
        }

        /// <summary>
        /// 回收子弹时调用.
        /// </summary>
        public void OnRecycleToPool() {
            hasHit = true;
            StopMove();
            if (bulletLua != null)
            {
                bulletLua.SetLuaField("lifeTimer", 0f);
                bulletLua.SetLuaField("hitTarget", null);
                bulletLua.CallLuaFunction("OnRecycle");
            }
        }

        /// <summary>
        /// 逐帧驱动 Lua 模块, 存活计时由模块自己累计.
        /// </summary>
        private void Update() {
            if (hasHit) return;
            bulletLua?.CallLuaFunction("OnMove", Time.deltaTime);
        }

        private void FixedUpdate() {
            if (hasHit) return;
            // 玩家子弹使用正常时间, 不受子弹时间影响.
            rb.linearVelocity = dir * speed;
        }

        /// <summary>
        /// 碰撞检测, 命中规则转发给 Lua 模块.
        /// </summary>
        private void OnCollisionEnter2D(Collision2D other) {
            HandleHit(other.gameObject);
        }

        /// <summary>
        /// 触发命中, 命中规则转发给 Lua 模块.
        /// </summary>
        private void OnTriggerEnter2D(Collider2D other) {
            HandleHit(other.gameObject);
        }

        private void HandleHit(GameObject target) {
            if (hasHit || target == null) return;

            if (bulletLua == null)
            {
                throw new System.Exception($"{nameof(PlayerBullet)} 预制体缺少 LuaComponet, 无法处理命中.");
            }

            bulletLua.SetLuaField("hitTarget", target);
            bulletLua.CallLuaFunction("OnHit");
        }

        /// <summary>
        /// 修改弹道, 供特殊弹道的 Lua 模块调用.
        /// </summary>
        public void SetTrajectory(Vector2 newDir, float newSpeed) {
            dir = newDir;
            speed = newSpeed;
        }

        /// <summary>
        /// 获取当前伤害, 供 Lua 模块结算使用.
        /// </summary>
        public int GetDamage() {
            return damage;
        }

        /// <summary>
        /// 获取飞行方向.
        /// </summary>
        public Vector2 GetDir() {
            return dir;
        }

        /// <summary>
        /// 结束生命周期并归还对象池, 供 Lua 模块调用.
        /// </summary>
        public void Recycle() {
            if (hasHit) return;
            hasHit = true;
            StopMove();
            WeaponManager.Instance.ReleasePlayerBullet(this);
        }

        /// <summary>
        /// 对敌人结算本次命中伤害, 含命中音效和回池, 供 Lua 模块调用.
        /// </summary>
        public void ApplyEnemyDamage(GameObject target) {
            if (hasHit || target == null) return;
            hasHit = true;

            PlaySelfHitSound();
            var finalDamage = PlayerRegistry.Current != null ? PlayerRegistry.Current.CalculateBulletDamage(damage) : damage;
            var damageInfo = new DamageInfo(finalDamage, dir);
            target.GetComponent<EnemyBase>()?.Hurt(damageInfo);
            WeaponManager.Instance.ReleasePlayerBullet(this);

            void PlaySelfHitSound()
            {
                var clip = _audioPlay?.GetNextClip();
                if (clip == null)
                {
                    return;
                }

                // 子弹会立刻回收到对象池, 命中音效交给全局音源播放.
                if (GlobalAudioPlay.Instance != null)
                {
                    GlobalAudioPlay.Instance.PlayOneShot(clip);
                    return;
                }

                AudioSource.PlayClipAtPoint(clip, transform.position);
            }
        }

        /// <summary>
        /// 命中墙壁时的表现与回池, 供 Lua 模块调用.
        /// </summary>
        public void ApplyWallHit(GameObject target) {
            if (hasHit) return;
            hasHit = true;

            // 墙体可能没有音效组件, 缺少时只回收子弹.
            target.GetComponent<AudioPlay>()?.Play();
            WeaponManager.Instance.ReleasePlayerBullet(this);
        }

        /// <summary>
        /// 清掉刚体速度, 避免回收后再次启用时继承旧速度.
        /// </summary>
        private void StopMove() {
            rb.linearVelocity = Vector2.zero;
        }
    }
}