using System;
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
    /// 大型敌人: 环形弹幕与追踪点射两段循环, 走位由行为大脑决定.
    /// </summary>
    public class EnemyBig : EnemyBase, IEnemyAttack
    {
        private enum BossPhase
        {
            Radial,
            Aimed
        }

        [Header("攻击资源")]
        [SerializeField] private EnemyBullet bulletPrefab;
        [SerializeField] private List<AudioClip> shootSounds = new List<AudioClip>();

        [Header("动画参数")]
        [SerializeField] private string runBoolParameterName = "IsRun";

        [Header("环形弹幕段")]
        [SerializeField] private float radialStateDuration = 2f;
        [SerializeField] private float radialBurstInterval = 0.75f;
        [SerializeField] private int radialBulletCount = 12;
        [SerializeField] private float radialBulletSpawnDistance = 0.6f;

        [Header("追踪点射段")]
        [SerializeField] private float chaseStateDuration = 4f;
        [SerializeField] private float aimedShotInterval = 1.2f;
        [SerializeField] private float aimedBulletSpawnDistance = 0.6f;

        [Header("旧走位参数, 待手感核对后删除")]
        [SerializeField] private float playerSafeDistance = 3f;

        private BossPhase phase;
        private float phaseTimer;
        private float radialBurstTimer;
        private float aimedShotTimer;
        private float nextAttackTime;

        protected override WeaponType WeaponType => WeaponType.Gun;
        protected override IEnemyAttack AttackModule => this;

        protected override void OnInit()
        {
            if (bulletPrefab == null)
                throw new InvalidOperationException($"{nameof(EnemyBig)} requires {nameof(bulletPrefab)} on prefab.");

            phase = BossPhase.Radial;
            phaseTimer = 0f;
            radialBurstTimer = 0f;
            aimedShotTimer = 0f;
            nextAttackTime = 0f;
            SetRunAnimation(false);
        }

        /// <summary>
        /// 玩家位于攻击视锥内且冷却结束才进入两段循环.
        /// </summary>
        public bool CanAttack(EnemyAttackContext context)
        {
            return context.IsPlayerInAttackCone
                && context.DistanceToPlayer <= BrainAttackRange
                && EnemyTime >= nextAttackTime;
        }

        public void BeginAttack(EnemyAttackContext context)
        {
            phase = BossPhase.Radial;
            phaseTimer = 0f;
            radialBurstTimer = 0f;
            aimedShotTimer = 0f;
            nextAttackTime = EnemyTime + BrainAttackInterval;
            SetRunAnimation(false);
            ShootRadialBurst();
        }

        public void TickAttack(EnemyAttackContext context, float enemyDeltaTime)
        {
            phaseTimer += enemyDeltaTime;
            if (phase == BossPhase.Radial)
            {
                TickRadialPhase(enemyDeltaTime);
                return;
            }

            TickAimedPhase(context, enemyDeltaTime);
        }

        public void EndAttack()
        {
            phase = BossPhase.Radial;
            phaseTimer = 0f;
            radialBurstTimer = 0f;
            aimedShotTimer = 0f;
            SetRunAnimation(false);
        }

        /// <summary>
        /// 环形弹幕段停步, 点射段允许行为大脑继续走位.
        /// </summary>
        public bool LocksMovement => phase == BossPhase.Radial;

        public float AttackLockDuration => Mathf.Max(0.01f, radialStateDuration) + Mathf.Max(0.01f, chaseStateDuration);

        protected override void OnDead()
        {
            SetRunAnimation(false);
            base.OnDead();
        }

        /// <summary>
        /// 环形段: 原地按间隔向四周释放弹幕, 到时切换点射段.
        /// </summary>
        private void TickRadialPhase(float enemyDeltaTime)
        {
            radialBurstTimer += enemyDeltaTime;
            if (radialBurstTimer >= Mathf.Max(0.01f, radialBurstInterval))
            {
                radialBurstTimer = 0f;
                ShootRadialBurst();
            }

            if (phaseTimer >= radialStateDuration)
            {
                phase = BossPhase.Aimed;
                phaseTimer = 0f;
                aimedShotTimer = 0f;
                SetRunAnimation(true);
            }
        }

        /// <summary>
        /// 点射段: 玩家在攻击视锥内时按间隔点射, 离开视锥后继续移动追击.
        /// </summary>
        private void TickAimedPhase(EnemyAttackContext context, float enemyDeltaTime)
        {
            aimedShotTimer += enemyDeltaTime;
            if (aimedShotTimer < Mathf.Max(0.01f, aimedShotInterval))
            {
                return;
            }

            aimedShotTimer = 0f;
            if (!context.IsPlayerInAttackCone || context.DirectionToPlayer.sqrMagnitude <= 0.0001f)
            {
                return;
            }

            ShootAtPlayer(context.DirectionToPlayer);
        }

        private void ShootRadialBurst()
        {
            var count = Mathf.Max(1, radialBulletCount);
            var angleStep = 360f / count;

            for (var i = 0; i < count; i++)
            {
                var angle = angleStep * i * Mathf.Deg2Rad;
                var direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
                var spawnPosition = transform.position + (Vector3)(direction * radialBulletSpawnDistance);
                WeaponManager.Instance.SpawnEnemyBullet(bulletPrefab, spawnPosition, direction, AttackDamage);
            }

            PlayShootSound();
        }

        private void ShootAtPlayer(Vector2 direction)
        {
            var spawnPosition = transform.position + (Vector3)(direction * aimedBulletSpawnDistance);
            WeaponManager.Instance.SpawnEnemyBullet(bulletPrefab, spawnPosition, direction, AttackDamage);
            PlayShootSound();
        }

        private void SetRunAnimation(bool isRun)
        {
            if (Animator == null || string.IsNullOrEmpty(runBoolParameterName))
                return;

            foreach (var parameter in Animator.parameters)
            {
                if (parameter.type == AnimatorControllerParameterType.Bool && parameter.name == runBoolParameterName)
                {
                    Animator.SetBool(runBoolParameterName, isRun);
                    return;
                }
            }
        }

        private void PlayShootSound()
        {
            if (AudioSource == null || shootSounds == null || shootSounds.Count == 0)
                return;

            AudioSource.PlayOneShot(shootSounds[UnityEngine.Random.Range(0, shootSounds.Count)]);
        }
    }
}
