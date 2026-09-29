using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// 攻击判定上下文, 由 EnemyBrain 每帧构建.
    /// </summary>
    public struct EnemyAttackContext
    {
        /// <summary>本帧是否看得见玩家.</summary>
        public bool HasSight;

        /// <summary>与玩家的距离.</summary>
        public float DistanceToPlayer;

        /// <summary>指向玩家的单位方向, 看不见时为零向量.</summary>
        public Vector2 DirectionToPlayer;
    }

    /// <summary>
    /// 敌人攻击抽象: 子类只实现何时出手和打出什么, 移动一律交给 EnemyBrain.
    /// </summary>
    public interface IEnemyAttack
    {
        /// <summary>
        /// 是否满足出手条件, 例如射程, 视线, 冷却或近战检测.
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