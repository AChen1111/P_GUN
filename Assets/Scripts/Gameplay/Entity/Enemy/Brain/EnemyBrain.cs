using System;
using System.Collections.Generic;
using Game.Core;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// 敌人行为状态.
    /// </summary>
    public enum EnemyBrainState
    {
        Idle,
        Chase,
        Search,
        Attack
    }

    /// <summary>
    /// 敌人行为大脑: 每帧按感知, 寻路, 分离, 攻击的顺序执行.
    /// 看得见走流场, 丢视线后用 A* 走向最后已知位置, 刚体速度只由本类写入.
    /// </summary>
    public sealed class EnemyBrain
    {
        // 到达格中心的判定距离平方.
        private const float ArriveCellSqrDistance = 0.04f;

        private readonly EnemyBase owner;
        private readonly IEnemyAttack attack;
        private readonly List<Vector2Int> searchPath = new List<Vector2Int>();

        private EnemyBrainState state = EnemyBrainState.Idle;
        private Vector3? lastSeenPosition;
        private float searchTimer;
        private float attackLockTimer;
        private Vector2Int lastSearchTargetCell;
        private bool reportedMissingGrid;

        public EnemyBrain(EnemyBase owner, IEnemyAttack attack)
        {
            this.owner = owner ?? throw new ArgumentNullException(nameof(owner));
            this.attack = attack ?? throw new ArgumentNullException(nameof(attack));
        }

        public EnemyBrainState State => state;

        /// <summary>
        /// 每帧驱动, deltaTime 必须是敌人局部时间.
        /// </summary>
        public void Tick(float enemyDeltaTime)
        {
            if (owner.BrainIsDead)
            {
                return;
            }

            var player = PlayerRegistry.Current;
            var selfPosition = owner.transform.position;
            var hasSight = EnemyPerception.CanSeePlayer(
                selfPosition,
                owner.BrainFacingDirection,
                player,
                owner.BrainVisionRadius,
                owner.BrainVisionAngle);

            switch (state)
            {
                case EnemyBrainState.Idle:
                    TickIdle(player, hasSight);
                    break;
                case EnemyBrainState.Chase:
                    TickChase(player, selfPosition, hasSight);
                    break;
                case EnemyBrainState.Search:
                    TickSearch(player, selfPosition, hasSight, enemyDeltaTime);
                    break;
                case EnemyBrainState.Attack:
                    TickAttack(player, selfPosition, hasSight, enemyDeltaTime);
                    break;
            }
        }

        /// <summary>
        /// 待机: 没看见也没在追踪, 速度为零.
        /// </summary>
        private void TickIdle(Player player, bool hasSight)
        {
            Stop();
            if (hasSight)
            {
                lastSeenPosition = player.transform.position;
                Enter(EnemyBrainState.Chase);
            }
        }

        /// <summary>
        /// 追击: 看得见玩家, 走流场并保持面朝.
        /// </summary>
        private void TickChase(Player player, Vector3 selfPosition, bool hasSight)
        {
            if (player == null)
            {
                Stop();
                return;
            }

            if (!hasSight)
            {
                // 丢失视线, 记下位置进入追踪.
                searchTimer = 0f;
                RebuildSearchPath(selfPosition);
                Enter(EnemyBrainState.Search);
                return;
            }

            lastSeenPosition = player.transform.position;

            var context = BuildContext(player, selfPosition, hasSight);
            if (attack.CanAttack(context))
            {
                BeginAttack(context);
                return;
            }

            MoveWithFlow(player.transform.position, selfPosition);
            FaceTowards(player.transform.position - selfPosition);
        }

        /// <summary>
        /// 追踪: 走向最后已知位置, 重新看见回追击, 超时或到达后回待机.
        /// </summary>
        private void TickSearch(Player player, Vector3 selfPosition, bool hasSight, float enemyDeltaTime)
        {
            searchTimer += enemyDeltaTime;

            if (hasSight)
            {
                lastSeenPosition = player.transform.position;
                Enter(EnemyBrainState.Chase);
                return;
            }

            if (player != null)
            {
                var context = BuildContext(player, selfPosition, hasSight);
                if (attack.CanAttack(context))
                {
                    BeginAttack(context);
                    return;
                }
            }

            if (searchTimer >= owner.BrainSearchTime)
            {
                Enter(EnemyBrainState.Idle);
                return;
            }

            FollowSearchPath(selfPosition);
        }

        /// <summary>
        /// 攻击: 由攻击模块决定停步或边走边打, 锁定结束回追击或追踪.
        /// </summary>
        private void TickAttack(Player player, Vector3 selfPosition, bool hasSight, float enemyDeltaTime)
        {
            attackLockTimer += enemyDeltaTime;

            var context = player != null ? BuildContext(player, selfPosition, hasSight) : default;
            attack.TickAttack(context, enemyDeltaTime);

            if (attackLockTimer >= attack.AttackLockDuration)
            {
                attackLockTimer = 0f;
                attack.EndAttack();
                if (hasSight)
                {
                    Enter(EnemyBrainState.Chase);
                    return;
                }

                searchTimer = 0f;
                RebuildSearchPath(selfPosition);
                Enter(EnemyBrainState.Search);
                return;
            }

            if (attack.LocksMovement)
            {
                Stop();
                return;
            }

            // 允许边走边打的攻击沿用追击的移动规则.
            if (hasSight && player != null)
            {
                MoveWithFlow(player.transform.position, selfPosition);
                FaceTowards(player.transform.position - selfPosition);
            }
            else
            {
                FollowSearchPath(selfPosition);
            }
        }

        /// <summary>
        /// 走流场方向; 缺可行走格时停住, 有格但流场没有该格时退化为直线方向.
        /// </summary>
        private void MoveWithFlow(Vector3 targetWorldPosition, Vector3 selfPosition)
        {
            var grid = ResolveWalkGrid();
            if (grid == null)
            {
                // 缺可行走格时停住, 不做直线穿墙.
                Stop();
                return;
            }

            if (!TryGetFlowDirection(grid, targetWorldPosition, selfPosition, out var direction))
            {
                var fallback = (Vector2)(targetWorldPosition - selfPosition);
                direction = fallback.sqrMagnitude > 0.0001f ? fallback.normalized : Vector2.zero;
            }

            ApplyMovement(direction, selfPosition);
        }

        /// <summary>
        /// 合成分离向量后写入速度, 距玩家小于停距时速度为零.
        /// </summary>
        private void ApplyMovement(Vector2 direction, Vector3 selfPosition)
        {
            var player = PlayerRegistry.Current;
            if (player != null)
            {
                var toPlayer = (Vector2)(player.transform.position - selfPosition);
                if (toPlayer.magnitude <= Mathf.Max(0f, owner.BrainPlayerStopDistance))
                {
                    // 距离过近时停住, 分离不能把敌人推进玩家.
                    Stop();
                    return;
                }
            }

            var separation = EnemySeparation.ComputeSeparation(owner, owner.BrainSeparationRadius);
            var blended = direction + owner.BrainSeparationWeight * separation;
            if (blended.sqrMagnitude <= 0.0001f)
            {
                Stop();
                return;
            }

            var speed = owner.BrainMoveSpeed * GameplayTime.EnemyTimeScale;
            owner.ApplyBrainVelocity(blended.normalized * speed);
            owner.SetBrainAnimatorSpeed(owner.BrainMoveSpeed);
        }

        /// <summary>
        /// 读取流场方向, 目标跨格时由格子重算.
        /// </summary>
        private bool TryGetFlowDirection(RoomWalkGrid grid, Vector3 targetWorldPosition, Vector3 selfPosition, out Vector2 direction)
        {
            grid.EnsureFlowField(targetWorldPosition);
            var cell = grid.GetCell(selfPosition);
            if (grid.TryGetFlowDirection(cell, out direction))
            {
                return true;
            }

            // 脚下不可走时读最近的可走格.
            if (grid.TryGetNearestWalkableCell(cell, out var nearest) && grid.TryGetFlowDirection(nearest, out direction))
            {
                return true;
            }

            direction = Vector2.zero;
            return false;
        }

        /// <summary>
        /// 沿 A* 路径走向最后已知位置.
        /// </summary>
        private void FollowSearchPath(Vector3 selfPosition)
        {
            var grid = ResolveWalkGrid();
            if (grid == null || !lastSeenPosition.HasValue)
            {
                Stop();
                Enter(EnemyBrainState.Idle);
                return;
            }

            var targetCell = grid.GetCell(lastSeenPosition.Value);
            var currentCell = grid.GetCell(selfPosition);
            if (currentCell == targetCell)
            {
                // 走到最后已知位置仍未看见, 结束本段追踪.
                Stop();
                Enter(EnemyBrainState.Idle);
                return;
            }

            // 目标格变化, 或路径为空, 或下一格被堵住时重新搜索.
            if (targetCell != lastSearchTargetCell || searchPath.Count == 0 || !grid.IsWalkable(searchPath[0]))
            {
                RebuildSearchPath(selfPosition);
            }

            if (searchPath.Count == 0)
            {
                // 没有路径, 本段追踪结束.
                Stop();
                Enter(EnemyBrainState.Idle);
                return;
            }

            // 到达格中心则弹出下一步.
            while (searchPath.Count > 0)
            {
                var nextCenter = grid.GetCellCenterWorld(searchPath[0]);
                if (((Vector2)(nextCenter - selfPosition)).sqrMagnitude <= ArriveCellSqrDistance)
                {
                    searchPath.RemoveAt(0);
                    continue;
                }

                break;
            }

            if (searchPath.Count == 0)
            {
                Stop();
                Enter(EnemyBrainState.Idle);
                return;
            }

            var stepCenter = grid.GetCellCenterWorld(searchPath[0]);
            var stepDirection = (Vector2)(stepCenter - selfPosition);
            if (stepDirection.sqrMagnitude <= 0.0001f)
            {
                Stop();
                return;
            }

            ApplyMovement(stepDirection.normalized, selfPosition);
            FaceTowards(stepDirection);
        }

        /// <summary>
        /// 重建到最后已知位置的 A* 路径.
        /// </summary>
        private void RebuildSearchPath(Vector3 selfPosition)
        {
            searchPath.Clear();
            var grid = ResolveWalkGrid();
            if (grid == null || !lastSeenPosition.HasValue)
            {
                return;
            }

            var from = grid.GetCell(selfPosition);
            var to = grid.GetCell(lastSeenPosition.Value);
            lastSearchTargetCell = to;
            if (!grid.TryFindPath(from, to, searchPath))
            {
                searchPath.Clear();
            }
        }

        /// <summary>
        /// 取所属战斗房间的可行走格, 缺格时只报错一次.
        /// </summary>
        private RoomWalkGrid ResolveWalkGrid()
        {
            var grid = owner.OwnerFightRoom != null ? owner.OwnerFightRoom.WalkGrid : null;
            if (grid == null && !reportedMissingGrid)
            {
                Debug.LogError($"{owner.name}: 所属战斗房间没有可行走格, 敌人无法寻路.");
                reportedMissingGrid = true;
            }

            return grid;
        }

        /// <summary>
        /// 进入攻击锁定.
        /// </summary>
        private void BeginAttack(EnemyAttackContext context)
        {
            attackLockTimer = 0f;
            attack.BeginAttack(context);
            Enter(EnemyBrainState.Attack);
            if (attack.LocksMovement)
            {
                Stop();
            }
        }

        /// <summary>
        /// 构建攻击判定上下文.
        /// </summary>
        private static EnemyAttackContext BuildContext(Player player, Vector3 selfPosition, bool hasSight)
        {
            var toPlayer = (Vector2)(player.transform.position - selfPosition);
            var distance = toPlayer.magnitude;
            return new EnemyAttackContext
            {
                HasSight = hasSight,
                DistanceToPlayer = distance,
                DirectionToPlayer = distance > 0.0001f ? toPlayer / distance : Vector2.zero,
            };
        }

        private void FaceTowards(Vector3 toTarget)
        {
            owner.SetBrainFacing(toTarget.x);
        }

        private void Stop()
        {
            owner.ApplyBrainVelocity(Vector2.zero);
            owner.SetBrainAnimatorSpeed(0f);
        }

        private void Enter(EnemyBrainState nextState)
        {
            state = nextState;
        }

        /// <summary>
        /// 死亡或回池时清空全部行为状态, 复用后的敌人没有上一次的路径和视野记忆.
        /// </summary>
        public void ResetForPoolOrDeath()
        {
            state = EnemyBrainState.Idle;
            lastSeenPosition = null;
            searchTimer = 0f;
            attackLockTimer = 0f;
            searchPath.Clear();
            reportedMissingGrid = false;
            Stop();
        }
    }
}