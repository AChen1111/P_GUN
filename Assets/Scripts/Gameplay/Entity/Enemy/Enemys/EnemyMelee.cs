using System;
using UnityEngine;
using QFramework;

namespace Game.Gameplay
{
    /// <summary>
    /// 近战敌人, 通过前方检测器发现玩家后播放攻击动画并造成伤害.
    /// </summary>
    public class EnemyMelee : EnemyBase
    {
        [Header("攻击组件")]
        [SerializeField] private MeleeAttackDetector attackDetector;

        [Header("攻击参数")]
        [SerializeField] private float attackCooldown = 1f;
        [SerializeField] private float attackLockDuration = 1.25f;

        [Header("检测器默认配置")]
        [SerializeField] private Vector2 detectorLocalOffset = new Vector2(0.75f, 0f);
        [SerializeField] private Vector2 detectorSize = new Vector2(0.8f, 0.8f);

        private Player cachedTarget;
        private float attackTimer;
        private float nextAttackTime;
        private bool hasAppliedDamage;
        private bool isAttacking;

        protected override WeaponType WeaponType => WeaponType.Melee;
        protected override void OnInit()
        {
            ResolveComponents();
            OwnerFightRoom = FightRoom.currentFightRoom;
            nextAttackTime = 0f;
            cachedTarget = null;
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
        protected override void RegisterFSM(FSM<EnemyState> fsm)
        {
            fsm.State(EnemyState.Follow)
                .OnUpdate(DoFollow);

            fsm.State(EnemyState.Attack)
                .OnEnter(BeginAttack)
                .OnUpdate(UpdateAttack)
                .OnExit(EndAttack);

            fsm.StartState(EnemyState.Follow);

            void EndAttack()
            {
                attackTimer = 0f;
                hasAppliedDamage = false;
                isAttacking = false;
            }

            void UpdateAttack()
            {
                attackTimer += EnemyDeltaTime;
                if (attackTimer >= attackLockDuration)
                {
                    FSM.ChangeState(EnemyState.Follow);
                }
            }

            void BeginAttack()
            {
                attackTimer = 0f;
                hasAppliedDamage = false;
                isAttacking = true;
                nextAttackTime = EnemyTime + attackCooldown;
                StopVelocity();
                base.PlayAttackAnimation();
            }

            void DoFollow()
            {
                if (IsDead)
                {
                    StopVelocity();
                    SetAnimatorSpeed(0f);
                    return;
                }

                if (FollowPlayerWithBodySpace(out var dir))
                {
                    FaceDirection(dir);
                }
            }

    void FaceDirection(Vector2 dir)
    {
        if (Sr == null)
            return;
        if (dir.x < 0f)
            Sr.flipX = true;
        else if (dir.x > 0f)
            Sr.flipX = false;
        UpdateDetectorDirection();
    }

    void UpdateDetectorDirection()
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

        /// <summary>
        /// 检测器发现玩家时调用, 统一由敌人本体控制攻击节奏.
        /// </summary>
        /// <param name="player">被检测到的玩家.</param>
        public void RequestAttack(Player player)
        {
            if (IsDead || player == null) return;

            cachedTarget = player;
            if (isAttacking) return;
            if (EnemyTime < nextAttackTime) return;

            FSM.ChangeState(EnemyState.Attack);
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
        private void StopVelocity()
        {
            if (Rb != null)
            {
                Rb.velocity = Vector2.zero;
            }
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
    }
}
