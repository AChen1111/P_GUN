using UnityEngine.Serialization;
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
    /// 蝙蝠远程敌人: 进入射程且冷却结束后停步, 延迟向玩家发射扇形弹.
    /// </summary>
    public class EnemyBat : EnemyBase, IEnemyAttack
    {
        [Header("攻击资源")]
        // 配置保存短名, 资源来自阶段预加载缓存.

        [SerializeField, AddressableKey(AddressableAssetKind.Prefab)] public string bulletPrefabKey = string.Empty;
        private EnemyBullet bulletPrefab => AddressableAssetAccess.Component<EnemyBullet>(bulletPrefabKey);
        // 配置保存短名, 资源来自阶段预加载缓存.

        [SerializeField, AddressableKey(AddressableAssetKind.AudioClip)] public List<string> shootSoundsKeys = new List<string>();
        private List<AudioClip> resolvedShootSounds;
        private List<AudioClip> shootSounds => resolvedShootSounds ?? (resolvedShootSounds = AddressableAssetAccess.List<AudioClip>(shootSoundsKeys));

        [Header("攻击参数")]
        [SerializeField] private float attackShootDelay = 0.15f;
        [SerializeField] private float attackLockDuration = 0.35f;
        [SerializeField] private float bulletSpawnDistance = 0.5f;
        [SerializeField] private int bulletCount = 5;
        [SerializeField] private float bulletSpreadStepAngle = 10f;

        [Header("旧走位参数, 待手感核对后删除")]
        [SerializeField] private float followBeforeAttackTime = 1.5f;
        [SerializeField] private float playerSafeDistance = 3f;

        private float attackTimer;
        private bool hasShot;
        private float nextAttackTime;

        protected override WeaponType WeaponType => WeaponType.Gun;
        protected override IEnemyAttack AttackModule => this;

        protected override void OnInit()
        {
            attackTimer = 0f;
            hasShot = false;
            nextAttackTime = 0f;
        }

        /// <summary>
        /// 玩家位于攻击视锥内且冷却结束才出手.
        /// </summary>
        public bool CanAttack(EnemyAttackContext context)
        {
            return context.IsPlayerInAttackCone
                && context.DistanceToPlayer <= BrainAttackRange
                && EnemyTime >= nextAttackTime;
        }

        public void BeginAttack(EnemyAttackContext context)
        {
            attackTimer = 0f;
            hasShot = false;
            nextAttackTime = EnemyTime + BrainAttackInterval;
            PlayAttackAnimation();
        }

        public void TickAttack(EnemyAttackContext context, float enemyDeltaTime)
        {
            attackTimer += enemyDeltaTime;
            if (hasShot || attackTimer < attackShootDelay) return;

            hasShot = true;
            // 玩家离开攻击视锥或攻击路线被墙挡住时不发射.
            if (!context.IsPlayerInAttackCone) return;
            ShootFan(context.DirectionToPlayer);
        }

        public void EndAttack()
        {
            attackTimer = 0f;
            hasShot = false;
        }

        /// <summary>
        /// 扇形弹攻击要求停步.
        /// </summary>
        public bool LocksMovement => true;

        public float AttackLockDuration => Mathf.Max(0.01f, attackLockDuration);

        private void ShootFan(Vector2 baseDirection)
        {
            if (bulletPrefab == null || baseDirection.sqrMagnitude <= 0.0001f) return;

            var spawnPosition = transform.position + (Vector3)(baseDirection * bulletSpawnDistance);
            // 以瞄准方向为中心均匀展开扇形弹, 避免多发子弹挤成一束.
            var baseAngle = Mathf.Atan2(baseDirection.y, baseDirection.x) * Mathf.Rad2Deg;
            var count = Mathf.Max(1, bulletCount);
            for (var i = 0; i < count; i++)
            {
                var bulletAngle = baseAngle + (i - (count - 1) * 0.5f) * bulletSpreadStepAngle;
                var rad = bulletAngle * Mathf.Deg2Rad;
                var bulletDirection = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
                WeaponManager.Instance.SpawnEnemyBullet(bulletPrefab, spawnPosition, bulletDirection, AttackDamage);
            }

            PlayShootSound();
        }

        private void PlayShootSound()
        {
            audioPlay.Play();
        }
    }
}
