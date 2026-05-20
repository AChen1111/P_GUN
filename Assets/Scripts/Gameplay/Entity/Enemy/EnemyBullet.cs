using System.Collections;
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
        private bool hasHit;
        private Coroutine autoRecycleCoroutine;

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

            StopAutoRecycleCoroutine();
            autoRecycleCoroutine = StartCoroutine(AutoRecycleIfNotHit());

            if (rb == null) {
                rb = GetComponent<Rigidbody2D>();
            }

            IEnumerator AutoRecycleIfNotHit()
            {
                yield return new WaitForSeconds(lifeTime);
                autoRecycleCoroutine = null;
                if (!hasHit && gameObject != null)
                {
                    Recycle();
                }
            }
}

        /// <summary>
        /// 执行 OnSpawnFromPool 逻辑.
        /// </summary>
        public void OnSpawnFromPool() {
            hasHit = false;

            if (rb == null) {
                rb = GetComponent<Rigidbody2D>();
            }
        }

        /// <summary>
        /// 执行 OnRecycleToPool 逻辑.
        /// </summary>
        public void OnRecycleToPool() {
            hasHit = true;
            StopAutoRecycleCoroutine();
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

        /// <summary>
        /// 执行固定帧物理更新逻辑.
        /// </summary>
        private void FixedUpdate() {
            if (rb == null) return;
            rb.velocity = dir * speed;
        }

        /// <summary>
        /// 执行 HandleHit 逻辑.
        /// </summary>
        private void HandleHit(GameObject target) {
            if (hasHit || target == null) return;

            if (target.CompareTag("Player")) {
                target.GetComponent<Player>()?.Hurt(new DamageInfo(damage, dir));
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
        /// 敌人子弹结束生命周期时归还对象池，而不是 Destroy。
        /// </summary>
        private void Recycle() {
            hasHit = true;
            StopAutoRecycleCoroutine();
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
            StopAutoRecycleCoroutine();
            StopMove();
        }

        /// <summary>
        /// 执行 StopAutoRecycleCoroutine 逻辑.
        /// </summary>
        private void StopAutoRecycleCoroutine() {
            if (autoRecycleCoroutine == null) return;

            StopCoroutine(autoRecycleCoroutine);
            autoRecycleCoroutine = null;
        }
    }
}
