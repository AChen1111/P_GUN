using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    /// <summary>
    /// 远程敌人: 玩家位于攻击视锥内时按间隔点射, 攻击期间不停步.
    /// </summary>
    public class EnemyA : EnemyBase, IEnemyAttack
    {
        [Header("攻击资源")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private List<AudioClip> shootSounds = new();

        [Header("攻击参数")]
        [SerializeField] private float shootInterval = 0.2f;

        [Header("旧走位参数, 待手感核对后删除")]
        [SerializeField] private float followDuration = 1f;
        [SerializeField] private float attackDuration = 0.1f;

        private ShootDuration shootDuration;
        private bool hasFired;

        protected override WeaponType WeaponType => WeaponType.Gun;
        protected override IEnemyAttack AttackModule => this;

        protected override void OnInit()
        {
            // 远程敌人的射击间隔使用敌人局部时钟, 玩家武器仍保留正常时钟.
            shootDuration = new ShootDuration(shootInterval, () => EnemyTime);
        }

        /// <summary>
        /// 射程与视线由行为大脑判定, 间隔沿用敌人局部时钟.
        /// </summary>
        public bool CanAttack(EnemyAttackContext context)
        {
            return context.IsPlayerInAttackCone
                && context.DistanceToPlayer <= BrainAttackRange
                && shootDuration != null
                && shootDuration.CanShoot;
        }

        public void BeginAttack(EnemyAttackContext context)
        {
            hasFired = false;
            shootDuration.RecordShootTime();
        }

        public void TickAttack(EnemyAttackContext context, float enemyDeltaTime)
        {
            if (hasFired) return;

            hasFired = true;
            // 玩家离开攻击视锥或攻击路线被墙挡住时不开枪.
            if (!context.IsPlayerInAttackCone) return;
            Fire(context.DirectionToPlayer);
        }

        public void EndAttack()
        {
            hasFired = false;
        }

        /// <summary>
        /// 攻击时不强制停步, 走位继续由行为大脑驱动.
        /// </summary>
        public bool LocksMovement => false;

        public float AttackLockDuration => Mathf.Max(0.01f, attackDuration);

        private void Fire(Vector2 direction)
        {
            if (bulletPrefab == null || direction.sqrMagnitude <= 0.0001f) return;

            var spawnPosition = transform.position + (Vector3)(direction * 0.5f);
            WeaponManager.Instance.SpawnEnemyBullet(bulletPrefab, spawnPosition, direction, AttackDamage);
            PlayShootSound();
        }

        private void PlayShootSound()
        {
            if (AudioSource != null && shootSounds != null && shootSounds.Count > 0)
            {
                AudioSource.PlayOneShot(shootSounds[UnityEngine.Random.Range(0, shootSounds.Count)]);
            }
        }
    }
}
