using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Presentation;

namespace Game.Gameplay
{
    /// <summary>
    /// 敌人子弹.
    /// C# 只保留敌人局部时间的刚体速度写入和对象池重置, 命中与存活规则在预制体的 LuaComponet 模块里.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
    public class EnemyBullet : MonoBehaviour, Game.Pooling.IPoolable {
        public Vector2 dir;
        public float speed = 10f;
        public Rigidbody2D rb;

        [Header("子弹数据 id, 对应 BulletData.lua")]
        [SerializeField] private string bulletId;

        [Header("击中玩家音效")]
        public List<AudioClip> hitSoundsOnPlayer = new List<AudioClip>();
        private AudioClip hitSoundOnPlayer => hitSoundsOnPlayer.Count > 0 ? hitSoundsOnPlayer[Random.Range(0, hitSoundsOnPlayer.Count)] : null;
        [Header("击中墙壁音效")]
        public List<AudioClip> hitSoundsOnWall = new List<AudioClip>();
        private AudioClip hitSoundOnWall => hitSoundsOnWall.Count > 0 ? hitSoundsOnWall[Random.Range(0, hitSoundsOnWall.Count)] : null;

        private int damage = 1;
        private int hitBuffId = -1;
        private bool hasHit;
        private LuaComponet bulletLua;

        /// <summary>
        /// 初始化运行时依赖.
        /// </summary>
        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            gameObject.layer = LayerMask.NameToLayer("EnemyBullet");
            bulletLua = GetComponent<LuaComponet>();
        }

        /// <summary>
        /// 重置编辑器默认配置.
        /// </summary>
        private void Reset() {
            gameObject.layer = LayerMask.NameToLayer("EnemyBullet");
        }

        /// <summary>
        /// 从对象池取出后由 WeaponManager 调用, 写入本次发射参数并通知 Lua 模块.
        /// </summary>
        public void Init(Vector2 shootDir, int bulletDamage = 1) {
            if (bulletLua == null)
            {
                throw new InvalidOperationException($"{nameof(EnemyBullet)} 预制体缺少 LuaComponet, 无法初始化弹道规则.");
            }

            if (string.IsNullOrWhiteSpace(bulletId))
            {
                throw new InvalidOperationException($"{nameof(EnemyBullet)} 未配置 bulletId, 无法读取 BulletData.");
            }

            var config = LuaDataRuntime.GetBulletConfig(bulletId);
            dir = shootDir;
            damage = Mathf.Max(0, bulletDamage);
            hitBuffId = config.HitBuffId;

            bulletLua.SetLuaField("bullet", this);
            bulletLua.SetLuaField("bulletId", bulletId);
            bulletLua.SetLuaField("dir", dir);
            bulletLua.SetLuaField("damage", damage);
            bulletLua.SetLuaField("speed", speed);
            bulletLua.SetLuaField("hitBuffId", hitBuffId);
            bulletLua.SetLuaField("lifeTime", config.LifeTime);
            bulletLua.CallLuaFunction("OnSpawn");
        }

        /// <summary>
        /// 从对象池取出子弹时调用.
        /// </summary>
        public void OnSpawnFromPool() {
            hasHit = false;
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
        /// 逐帧驱动 Lua 模块, 存活计时使用敌人局部时间, 由模块自己累计.
        /// </summary>
        private void Update() {
            if (hasHit) return;
            bulletLua?.CallLuaFunction("OnMove", GameplayTime.EnemyDeltaTime);
        }

        private void FixedUpdate() {
            if (hasHit) return;
            // 敌人子弹使用敌人局部时间倍率, 玩家子弹和玩家移动不受影响.
            rb.velocity = dir * speed * GameplayTime.EnemyTimeScale;
        }

        /// <summary>
        /// 处理 2D 碰撞进入事件, 命中规则转发给 Lua 模块.
        /// </summary>
        private void OnCollisionEnter2D(Collision2D other) {
            HandleHit(other.gameObject);
        }

        /// <summary>
        /// 处理 2D 触发进入事件, 命中规则转发给 Lua 模块.
        /// </summary>
        private void OnTriggerEnter2D(Collider2D other) {
            HandleHit(other.gameObject);
        }

        private void HandleHit(GameObject target) {
            if (hasHit || target == null) return;

            if (bulletLua == null)
            {
                throw new InvalidOperationException($"{nameof(EnemyBullet)} 预制体缺少 LuaComponet, 无法处理命中.");
            }

            bulletLua.SetLuaField("hitTarget", target);
            bulletLua.CallLuaFunction("OnHit");
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
        /// 修改弹道, 供特殊弹道的 Lua 模块调用.
        /// </summary>
        public void SetTrajectory(Vector2 newDir, float newSpeed) {
            dir = newDir;
            speed = newSpeed;
        }

        /// <summary>
        /// 对玩家结算本次命中, 含命中 Buff, 音效和回池, 供 Lua 模块调用.
        /// </summary>
        public void ApplyPlayerHit(GameObject target) {
            if (hasHit || target == null) return;
            hasHit = true;

            var player = target.GetComponent<Player>();
            var isDamageApplied = player != null && player.Hurt(new DamageInfo(damage, dir));
            if (isDamageApplied)
            {
                TryApplyHitBuff(player);
            }

            var audioSource = target.GetComponent<AudioSource>();
            if (audioSource != null && hitSoundsOnPlayer.Count > 0)
            {
                audioSource.PlayOneShot(hitSoundOnPlayer);
            }

            Recycle();
        }

        /// <summary>
        /// 命中墙壁时的表现与回池, 供 Lua 模块调用.
        /// </summary>
        public void ApplyWallHit(GameObject target) {
            if (hasHit) return;
            hasHit = true;

            if (hitSoundsOnWall.Count > 0 && GlobalAudioPlay.Instance != null)
            {
                GlobalAudioPlay.Instance.PlayerAudioSourceByClip(hitSoundOnWall);
            }

            Recycle();
        }

        /// <summary>
        /// 命中 Buff 的 id 来自 BulletData, -1 表示本次命中不附加 Buff.
        /// </summary>
        private void TryApplyHitBuff(Player player) {
            if (hitBuffId == -1 || player == null) return;

            var manager = player.buffManager != null ? player.buffManager : player.GetComponent<BuffManager>();
            if (manager == null) return;

            manager.AddBuffById(hitBuffId, this);
        }

        /// <summary>
        /// 结束生命周期并经 WeaponManager 归还对象池, 而不是 Destroy.
        /// </summary>
        public void Recycle() {
            if (hasHit) return;
            hasHit = true;
            StopMove();
            WeaponManager.Instance.ReleaseEnemyBullet(this);
        }

        /// <summary>
        /// 清掉刚体速度，避免回收后再次启用时继承旧速度。
        /// </summary>
        public void StopMove() {
            if (rb != null) {
                rb.velocity = Vector2.zero;
            }
        }

        /// <summary>
        /// 注销禁用时需要的监听.
        /// </summary>
        private void OnDisable() {
            StopMove();
        }
    }
}