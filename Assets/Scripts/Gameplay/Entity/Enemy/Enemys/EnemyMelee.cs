using System;
using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    /// <summary>
    /// 近战敌人: 前方检测器碰到玩家且冷却结束后停步, 由攻击动画事件结算伤害.
    /// </summary>
    public class EnemyMelee : EnemyBase, IEnemyAttack
    {
        [Header("攻击组件")]
        [SerializeField] private MeleeAttackDetector attackDetector;

        [Header("攻击参数")]
        [SerializeField] private float attackCooldown = 1f;
        [SerializeField] private float attackLockDuration = 1.25f;

        [Header("检测器默认配置")]
        [SerializeField] private Vector2 detectorLocalOffset = new Vector2(0.75f, 0f);
        [SerializeField] private Vector2 detectorSize = new Vector2(0.8f, 0.8f);

        private float attackTimer;
        private float nextAttackTime;
        private bool hasAppliedDamage;
        private bool isAttacking;

        protected override WeaponType WeaponType => WeaponType.Melee;
        protected override IEnemyAttack AttackModule => this;

        protected override void OnInit()
        {
            ResolveComponents();
            nextAttackTime = 0f;
            hasAppliedDamage = false;
            isAttacking = false;

            void ResolveComponents()
            {
                if (attackDetector == null)
                {
                    attackDetector = GetComponentInChildren<MeleeAttackDetector>();
                }

                if (attackDetector == null)
                {
                    throw new InvalidOperationException($"{nameof(EnemyMelee)} requires {nameof(MeleeAttackDetector)} on prefab.");
                }

                attackDetector.Init(this);
                ConfigureDetector(false);
            }
}

        /// <summary>
        /// 近战以检测器命中为出手条件, 不要求视线, 冷却沿用敌人局部时钟.
        /// </summary>
        public bool CanAttack(EnemyAttackContext context)
        {
            if (attackDetector == null || EnemyTime < nextAttackTime) return false;
            return attackDetector.TryGetPlayerInRange(out _);
        }

        public void BeginAttack(EnemyAttackContext context)
        {
            attackTimer = 0f;
            hasAppliedDamage = false;
            isAttacking = true;
            nextAttackTime = EnemyTime + attackCooldown;
            PlayAttackAnimation();
        }

        public void TickAttack(EnemyAttackContext context, float enemyDeltaTime)
        {
            attackTimer += enemyDeltaTime;
        }

        public void EndAttack()
        {
            attackTimer = 0f;
            hasAppliedDamage = false;
            isAttacking = false;
        }

        /// <summary>
        /// 近战攻击要求停步.
        /// </summary>
        public bool LocksMovement => true;

        public float AttackLockDuration => Mathf.Max(0.01f, attackLockDuration);

        /// <summary>
        /// 面朝变化后翻转检测盒位置.
        /// </summary>
        protected override void OnFacingChanged()
        {
            UpdateDetectorDirection();
        }

        /// <summary>
        /// 检测器发现玩家时调用, 保留给检测器的既有回调.
        /// </summary>
        public void RequestAttack(Player player)
        {
            if (IsDead || player == null) return;
        }

        /// <summary>
        /// 攻击动画最后一帧调用, 由 Animation Event 精确结算近战伤害.
        /// </summary>
        public void ApplyDamageOnAttackLastFrame()
        {
            if (IsDead || !isAttacking || hasAppliedDamage) return;

            hasAppliedDamage = true;
            ApplyDamageToPlayer();

            void ApplyDamageToPlayer()
            {
                if (attackDetector == null || !attackDetector.TryGetPlayerInRange(out var target))
                    return;
                if (target == null)
                    return;
                var sourceDirection = (target.transform.position - transform.position).normalized;
                target.Hurt(new DamageInfo(AttackDamage, sourceDirection));
            }
}

        /// <summary>
        /// 重置编辑器默认配置.
        /// </summary>
        private void Reset()
        {
            EnsureDetector(true);

            void EnsureDetector(bool applyDefaultShape)
            {
                if (attackDetector == null)
                {
                    attackDetector = GetComponentInChildren<MeleeAttackDetector>();
                }

                if (attackDetector == null)
                {
                    return;
                }

                attackDetector.Init(this);
                ConfigureDetector(applyDefaultShape);
            }
}

        /// <summary>
        /// 校验编辑器配置变化.
        /// </summary>
        private void OnValidate()
        {
            if (attackDetector == null)
            {
                attackDetector = GetComponentInChildren<MeleeAttackDetector>();
            }

            ConfigureDetector(false);
        }

        private void ConfigureDetector(bool applyDefaultShape)
        {
            if (attackDetector == null) return;

            if (applyDefaultShape)
            {
                // 新建检测器时才写入默认形状, 已配置的 prefab 保留手动调整.
                attackDetector.transform.localPosition = detectorLocalOffset;
            }

            var boxCollider = attackDetector.GetComponent<BoxCollider2D>();
            if (boxCollider != null)
            {
                boxCollider.isTrigger = true;
                if (applyDefaultShape)
                {
                    boxCollider.size = detectorSize;
                }
            }
            else
            {
                var detectorCollider = attackDetector.GetComponent<Collider2D>();
                if (detectorCollider != null)
                {
                    detectorCollider.isTrigger = true;
                }
            }
        }

        private void UpdateDetectorDirection()
        {
            if (attackDetector == null || Sr == null)
                return;
            var facingSign = Sr.flipX ? -1f : 1f;
            var localPosition = attackDetector.transform.localPosition;
            // 翻转时只镜像当前手动配置的位置, 不再用默认偏移覆盖 prefab.
            localPosition.x = Mathf.Abs(localPosition.x) * facingSign;
            attackDetector.transform.localPosition = localPosition;
        }
    }
}