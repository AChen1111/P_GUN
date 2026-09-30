using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Slime 持续追击玩家, 进入小圆形范围后停步变红, 延时自爆并结算一次伤害.
    /// </summary>
    public sealed class EnemySlime : EnemyBase, IEnemyAttack
    {
        [Header("自爆参数")]
        [SerializeField, Min(0.1f)] private float explosionRadius = 1.75f;
        [SerializeField, Min(0.1f)] private float fuseDuration = 1.2f;
        [SerializeField] private Material warningMaterial;
        [SerializeField] private LineRenderer explosionWarningRing;
        private readonly Vector3[] warningCircle = new Vector3[48];
        private static readonly int WarningProgressId = Shader.PropertyToID("_WarningProgress");
        private Material normalMaterial;
        private MaterialPropertyBlock warningProperties;
        private float fuseTimer;
        private bool armed;
        private bool exploded;
        private Color normalColor;

        protected override WeaponType WeaponType => WeaponType.Melee;
        protected override IEnemyAttack AttackModule => this;
        public bool LocksMovement => true;
        // 自爆会结束生命, 大脑不会在引信计时结束前退回追击.
        public float AttackLockDuration => fuseDuration + 0.1f;
        protected override void OnInit()
        {
            Animator.enabled = true;
            Sr.enabled = true;
            if (normalMaterial == null) normalMaterial = Sr.sharedMaterial;
            Sr.sharedMaterial = normalMaterial;
            Sr.SetPropertyBlock(null);
            warningProperties ??= new MaterialPropertyBlock();
            explosionWarningRing.enabled = false;
            fuseTimer = 0f;
            armed = false;
            exploded = false;
            normalColor = Sr.color;
        }

        public bool CanAttack(EnemyAttackContext context)
        {
            var player = PlayerRegistry.Current;
            // 圆形触发没有朝向限制, 墙后玩家不会隔墙引爆 Slime.
            return !armed && player != null && context.DistanceToPlayer <= BrainAttackRange &&
                Physics2D.Linecast(transform.position, player.transform.position, LayerMask.GetMask("Wall")).collider == null;
        }

        public void BeginAttack(EnemyAttackContext context)
        {
            armed = true;
            fuseTimer = 0f;
            // 停止移动动画, 避免 Animator 覆盖自爆颜色反馈.
            Animator.enabled = false;
            if (warningMaterial == null) throw new System.InvalidOperationException("Slime 未配置自爆预警材质.");
            Sr.sharedMaterial = warningMaterial;
            explosionWarningRing.enabled = true;
            DrawExplosionWarning();
        }

        public void TickAttack(EnemyAttackContext context, float enemyDeltaTime)
        {
            if (!armed || exploded || IsDead) return;
            fuseTimer += enemyDeltaTime;
            warningProperties.SetFloat(WarningProgressId, Mathf.Clamp01(fuseTimer / fuseDuration));
            Sr.SetPropertyBlock(warningProperties);
            DrawExplosionWarning();
            if (fuseTimer < fuseDuration) return;
            exploded = true;
            var player = PlayerRegistry.Current;
            if (player != null && Vector2.Distance(transform.position, player.transform.position) <= explosionRadius &&
                Physics2D.Linecast(transform.position, player.transform.position, LayerMask.GetMask("Wall")).collider == null)
            {
                player.Hurt(new DamageInfo(1, (player.transform.position - transform.position).normalized));
            }
            // 使用既有死亡链路结算波次和掉落, 隐藏精灵表现自爆消失.
            Dead();
            Sr.enabled = false;
        }

        public void EndAttack()
        {
            armed = false;
            fuseTimer = 0f;
            Animator.enabled = true;
            Sr.enabled = true;
            Sr.color = normalColor;
            Sr.sharedMaterial = normalMaterial;
            Sr.SetPropertyBlock(null);
            explosionWarningRing.enabled = false;
        }

        protected override void OnDead()
        {
            Animator.enabled = true;
            Sr.sharedMaterial = normalMaterial;
            Sr.SetPropertyBlock(null);
            explosionWarningRing.enabled = false;
            base.OnDead();
        }

        private void DrawExplosionWarning()
        {
            // 使用世界坐标绘制实际伤害半径, 不受 Slime 预制体缩放影响.
            for (var i = 0; i < warningCircle.Length; i++)
            {
                var angle = i * Mathf.PI * 2f / warningCircle.Length;
                warningCircle[i] = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * explosionRadius;
            }
            explosionWarningRing.positionCount = warningCircle.Length;
            explosionWarningRing.SetPositions(warningCircle);
        }

        protected override void DrawAttackRangeGizmos()
        {
            // 黄色圆为触发范围, 红色圆为爆炸范围, 仅在 Scene 显示.
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, BrainAttackRange);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}
