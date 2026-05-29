using UnityEngine;
using System.Collections.Generic;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
    public class EnemyBullet : MonoBehaviour, Game.Pooling.IPoolable {
        public Vector2 dir;
        public float speed = 10f;
        public Rigidbody2D rb;
        [SerializeField] private float lifeTime = 3f;
        [SerializeField] private int damage = 1;
        [Tooltip("命中后给玩家附加的 Buff id, -1 表示不附加.")]
        [SerializeField] private int hitBuffId = -1;
        private bool hasHit;
        private float lifeTimer;

        [Header("击中玩家音效")]
        public List<AudioClip> hitSoundsOnPlayer = new List<AudioClip>();
        private AudioClip hitSoundOnPlayer => hitSoundsOnPlayer[Random.Range(0, hitSoundsOnPlayer.Count)];
        [Header("击中墙壁音效")]
        public List<AudioClip> hitSoundsOnWall = new List<AudioClip>();
        private AudioClip hitSoundOnWall => hitSoundsOnWall[Random.Range(0, hitSoundsOnWall.Count)];

        /// <summary>
        /// 初始化运行时依赖.
        /// </summary>
        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            gameObject.layer = LayerMask.NameToLayer("EnemyBullet");
        }

        /// <summary>
        /// 每次从对象池取出敌人子弹时调用，重置方向、命中状态和生命周期。
        /// </summary>
        public void Init(Vector2 shootDir, int bulletDamage = 1) {
            dir = shootDir;
            damage = Mathf.Max(0, bulletDamage);
            hasHit = false;
            lifeTimer = 0f;

            if (rb == null) {
                rb = GetComponent<Rigidbody2D>();
            }
}
        public void OnSpawnFromPool() {
            hasHit = false;
            lifeTimer = 0f;

            if (rb == null) {
                rb = GetComponent<Rigidbody2D>();
            }
        }
        public void OnRecycleToPool() {
            hasHit = true;
            StopMove();
        }

        /// <summary>
        /// 处理 2D 碰撞进入事件.
        /// </summary>
        private void OnCollisionEnter2D(Collision2D other) {
            HandleHit(other.gameObject);
        }

        /// <summary>
        /// 处理 2D 触发进入事件.
        /// </summary>
        private void OnTriggerEnter2D(Collider2D other) {
            LogHit("Trigger", other.gameObject);
            HandleHit(other.gameObject);

            void LogHit(string hitType, GameObject target)
            {
                if (target == null)
                    return;
                var layerName = LayerMask.LayerToName(target.layer);
                var parentName = target.transform.parent != null ? target.transform.parent.name : "null";
                Debug.Log($"[EnemyBullet] {hitType} hit name={target.name}, tag={target.tag}, layer={target.layer}({layerName}), parent={parentName}");
            }
}

        /// <summary>
        /// 重置编辑器默认配置.
        /// </summary>
        private void Reset() {
            gameObject.layer = LayerMask.NameToLayer("EnemyBullet");
        }
        private void Update() {
            if (hasHit) return;

            // 子弹寿命使用敌人局部时间, 避免子弹时间中射程被真实时间提前截断.
            lifeTimer += GameplayTime.EnemyDeltaTime;
            if (lifeTimer >= lifeTime)
            {
                Recycle();
            }
        }
        private void FixedUpdate() {
            if (rb == null) return;
            // 敌人子弹使用敌人局部时间倍率, 玩家子弹和玩家移动不受影响.
            rb.velocity = dir * speed * GameplayTime.EnemyTimeScale;
        }
        private void HandleHit(GameObject target) {
            if (hasHit || target == null) return;

            if (target.CompareTag("Player")) {
                var player = target.GetComponent<Player>();
                var isDamageApplied = player != null && player.Hurt(new DamageInfo(damage, dir));
                if (isDamageApplied) {
                    TryApplyHitBuff(player);
                }
                hasHit = true;

                var audioSource = target.GetComponent<AudioSource>();
                if (audioSource != null && hitSoundsOnPlayer.Count > 0) {
                    audioSource.PlayOneShot(hitSoundOnPlayer);
                }

                Recycle();
                return;
            }

            var wallLayer = LayerMask.NameToLayer("Wall");
            var isWall = target.CompareTag("Wall") || (wallLayer != -1 && target.layer == wallLayer);
            if (isWall) {
                hasHit = true;
                if (hitSoundsOnWall.Count > 0) {
                    GlobalAudioPlay.Instance.PlayerAudioSourceByClip(hitSoundOnWall);
                }
                Recycle();
            }
        }

        /// <summary>
        /// 命中 Buff 是子弹 prefab 配置, -1 表示本次命中不附加 Buff.
        /// </summary>
        private void TryApplyHitBuff(Player player) {
            if (hitBuffId == -1 || player == null) return;

            var manager = player.buffManager != null ? player.buffManager : player.GetComponent<BuffManager>();
            if (manager == null) return;

            manager.AddBuffById(hitBuffId, this);
        }

        /// <summary>
        /// 敌人子弹结束生命周期时归还对象池，而不是 Destroy。
        /// </summary>
        private void Recycle() {
            hasHit = true;
            StopMove();
            EnemyBulletPool.Instance.Release(this);
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
