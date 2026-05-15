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
        [SerializeField] private Animator animator;
        [SerializeField] private MeleeAttackDetector attackDetector;

        [Header("攻击参数")]
        [SerializeField] private int attackDamage = 1;
        [SerializeField] private float attackCooldown = 1f;
        [SerializeField] private float attackHitDelay = 0.15f;
        [SerializeField] private float attackLockDuration = 0.35f;
        [SerializeField] private string attackTriggerName = "Attack";

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
        }

        protected override void OnHurt(DamageInfo damageInfo)
        {
            var damage = damageInfo == null ? 0 : damageInfo.Damage;
            ApplyDamage(damage);
        }

        protected override void OnDead()
        {
            if (Rb != null) Rb.velocity = Vector2.zero;
            FightRoom.NotifyEnemyDefeated(this);
            EnemyPool.Instance.Release(this);
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
            if (Time.time < nextAttackTime) return;

            FSM.ChangeState(EnemyState.Attack);
        }

        private void Reset()
        {
            EnsureDetector();
        }

        private void OnValidate()
        {
            if (attackDetector == null)
            {
                attackDetector = GetComponentInChildren<MeleeAttackDetector>();
            }

            ConfigureDetector();
        }

        private void ResolveComponents()
        {
            if (animator == null)
            {
                animator = GetComponentInChildren<Animator>();
            }

            if (attackDetector == null)
            {
                attackDetector = GetComponentInChildren<MeleeAttackDetector>();
            }

            if (attackDetector != null)
            {
                attackDetector.Init(this);
                ConfigureDetector();
            }
        }

        private void DoFollow()
        {
            if (IsDead || Global.player == null)
            {
                StopVelocity();
                return;
            }

            var dir = (Global.player.transform.position - transform.position).normalized;
            FaceDirection(dir);

            if (Rb != null)
            {
                Rb.velocity = dir * MoveSpeed;
            }
        }

        private void BeginAttack()
        {
            attackTimer = 0f;
            hasAppliedDamage = false;
            isAttacking = true;
            nextAttackTime = Time.time + attackCooldown;
            StopVelocity();
            PlayAttackAnimation();
        }

        private void UpdateAttack()
        {
            attackTimer += Time.deltaTime;

            if (!hasAppliedDamage && attackTimer >= attackHitDelay)
            {
                hasAppliedDamage = true;
                ApplyDamageToPlayer();
            }

            if (attackTimer >= attackLockDuration)
            {
                FSM.ChangeState(EnemyState.Follow);
            }
        }

        private void EndAttack()
        {
            attackTimer = 0f;
            hasAppliedDamage = false;
            isAttacking = false;
        }

        private void ApplyDamageToPlayer()
        {
            var target = cachedTarget != null ? cachedTarget : Global.player;
            if (target == null) return;

            var sourceDirection = (target.transform.position - transform.position).normalized;
            target.Hurt(new DamageInfo(attackDamage, sourceDirection));
        }

        private void PlayAttackAnimation()
        {
            if (animator == null || string.IsNullOrEmpty(attackTriggerName)) return;

            // 只在动画器存在对应 Trigger 时播放, 避免控制器未配置时报错.
            if (!HasTrigger(animator, attackTriggerName)) return;

            animator.SetTrigger(attackTriggerName);
        }

        private static bool HasTrigger(Animator targetAnimator, string triggerName)
        {
            foreach (var parameter in targetAnimator.parameters)
            {
                if (parameter.type == AnimatorControllerParameterType.Trigger && parameter.name == triggerName)
                {
                    return true;
                }
            }

            return false;
        }

        private void FaceDirection(Vector2 dir)
        {
            if (Sr == null) return;

            if (dir.x < 0f) Sr.flipX = true;
            else if (dir.x > 0f) Sr.flipX = false;
        }

        private void StopVelocity()
        {
            if (Rb != null)
            {
                Rb.velocity = Vector2.zero;
            }
        }

        private void EnsureDetector()
        {
            if (attackDetector == null)
            {
                attackDetector = GetComponentInChildren<MeleeAttackDetector>();
            }

            if (attackDetector == null)
            {
                var detectorObject = new GameObject("MeleeAttackDetector");
                detectorObject.transform.SetParent(transform, false);
                detectorObject.AddComponent<BoxCollider2D>();
                attackDetector = detectorObject.AddComponent<MeleeAttackDetector>();
            }

            attackDetector.Init(this);
            ConfigureDetector();
        }

        private void ConfigureDetector()
        {
            if (attackDetector == null) return;

            // 检测器放在敌人前方, 美术朝向为右时默认在 X 正方向.
            attackDetector.transform.localPosition = detectorLocalOffset;

            var boxCollider = attackDetector.GetComponent<BoxCollider2D>();
            if (boxCollider != null)
            {
                boxCollider.isTrigger = true;
                boxCollider.size = detectorSize;
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
