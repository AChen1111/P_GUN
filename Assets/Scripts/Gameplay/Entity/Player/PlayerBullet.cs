using System.Collections;
using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    public class PlayerBullet : MonoBehaviour, Game.Pooling.IPoolable {
        public Vector2 dir;
        public float speed = 15f;
        public Rigidbody2D rb;
        public int damage;
        [SerializeField] private float lifeTime = 3f;
        private bool hasHit = false;
        [SerializeField]private AudioPlay _audioPlay;
        private Coroutine autoRecycleCoroutine;

        /// <summary>
        /// 初始化运行时依赖.
        /// </summary>
        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
            _audioPlay = GetComponent<AudioPlay>();
            gameObject.layer = LayerMask.NameToLayer("PlayerBullet");
        }

        /// <summary>
        /// 每次从对象池取出子弹时调用，重置上一轮使用留下的方向、伤害、命中状态和生命周期
        /// </summary>
        public void Init(Vector2 shootDir, int bulletDamage,int bulletSpeed) {
            dir = shootDir;
            damage = bulletDamage;
            speed = bulletSpeed;
        }

        /// <summary>
        /// 从对象池取出子弹时调用
        /// </summary>
        public void OnSpawnFromPool() {
            hasHit = false;
            _audioPlay?.Clear();
            autoRecycleCoroutine = StartCoroutine(AutoRecycleIfNotHit());

            IEnumerator AutoRecycleIfNotHit()
            {
                yield return new WaitForSeconds(lifeTime);
                autoRecycleCoroutine = null;
                if (!hasHit)
                {
                    hasHit = true;
                    PlayerBulletPool.Instance.Release(this);
                }
            }
}

        /// <summary>
        /// 回收子弹时调用
        /// </summary>
        public void OnRecycleToPool() {
            hasHit = true;
            StopAutoRecycleCoroutine();
            StopMove();

            void StopAutoRecycleCoroutine()
            {
                if (autoRecycleCoroutine == null)
                    return;
                StopCoroutine(autoRecycleCoroutine);
                autoRecycleCoroutine = null;
            }

            void StopMove()
            {
                rb.velocity = Vector2.zero;
            }
}

        ///<summary>
        ///碰撞检测
        ///</summary>
        ///<param name="other">碰撞对象</param>
        private void OnCollisionEnter2D(Collision2D other) {
            HandleHit(other.gameObject);
        }

        /// <summary>
        /// 处理 2D 触发进入事件.
        /// </summary>
        private void OnTriggerEnter2D(Collider2D other) {
            HandleHit(other.gameObject);
        }



        ///<summary>
        ///固定更新
        ///</summary>
        private void FixedUpdate() {
            rb.velocity = dir * speed;
        }


        /// <summary>
        /// 处理碰撞
        /// </summary>
        /// <param name="target"></param>
        private void HandleHit(GameObject target) {
            if(hasHit || target == null) return;

            if(target.CompareTag("Enemy")) {
                hasHit = true;

                PlaySelfHitSound();
                var finalDamage = Global.player != null ? Global.player.CalculateBulletDamage(damage) : damage;
                DamageInfo damageInfo = new DamageInfo(finalDamage, dir);

                target.GetComponent<EnemyBase>()?.Hurt(damageInfo);
                PlayerBulletPool.Instance.Release(this);
                return;
            }

            var wallLayer = LayerMask.NameToLayer("Wall");
            var isWall = target.CompareTag("Wall") || target.layer == wallLayer;
            if(isWall) {
                hasHit = true;
                // 墙体可能没有音效组件,缺少时只回收子弹.
                target.GetComponent<AudioPlay>()?.Play();
                PlayerBulletPool.Instance.Release(this);
            }

            void PlaySelfHitSound()
            {
                if (_audioPlay == null)
                {
                    _audioPlay = GetComponent<AudioPlay>();
                }

                var clip = _audioPlay?.GetNextClip();
                if (clip == null)
                    return;
                // 子弹会立刻回收到对象池,命中音效交给全局音源播放.
                if (GlobalAudioPlay.Instance != null)
                {
                    GlobalAudioPlay.Instance.PlayOneShot(clip);
                    return;
                }

                AudioSource.PlayClipAtPoint(clip, transform.position);
            }
}

    }
}
