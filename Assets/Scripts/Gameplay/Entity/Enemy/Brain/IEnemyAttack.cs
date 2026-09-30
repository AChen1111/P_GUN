using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// 攻击判定上下文, 由 EnemyBrain 每帧构建.
    /// </summary>
    public struct EnemyAttackContext
    {
        /// <summary>玩家是否在面朝攻击视锥内, 且攻击路线没有墙体遮挡.</summary>
        public bool IsPlayerInAttackCone;

        /// <summary>与玩家的距离.</summary>
        public float DistanceToPlayer;

        /// <summary>指向玩家当前位置的单位方向.</summary>
        public Vector2 DirectionToPlayer;
    }

    /// <summary>
    /// 敌人攻击抽象: 子类只实现何时出手和打出什么, 移动一律交给 EnemyBrain.
    /// </summary>
    public interface IEnemyAttack
    {
        /// <summary>
        /// 是否满足出手条件, 例如射程, 攻击路线, 冷却或近战检测.
        /// </summary>
        bool CanAttack(EnemyAttackContext context);

        /// <summary>
        /// 进入攻击锁定, 记录本次攻击的冷却与表现.
        /// </summary>
        void BeginAttack(EnemyAttackContext context);

        /// <summary>
        /// 攻击期间每帧逻辑, 例如延迟发射与分段计时.
        /// </summary>
        void TickAttack(EnemyAttackContext context, float enemyDeltaTime);

        /// <summary>
        /// 攻击锁定结束时清理攻击状态.
        /// </summary>
        void EndAttack();

        /// <summary>
        /// 攻击是否要求停步, 大型敌人的点射段可以返回 false.
        /// </summary>
        bool LocksMovement { get; }

        /// <summary>
        /// 攻击锁定时长, 到时由行为大脑退出攻击状态.
        /// </summary>
        float AttackLockDuration { get; }
    }
}
