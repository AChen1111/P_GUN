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
        private static readonly int WallLayerMask = LayerMask.GetMask("Wall");

        private readonly EnemyBase owner;
        private readonly IEnemyAttack attack;
        private readonly List<Vector2Int> searchPath = new List<Vector2Int>();
        private readonly List<Vector2Int> patrolPath = new List<Vector2Int>();

        private EnemyBrainState state = EnemyBrainState.Idle;
        private Vector3? lastSeenPosition;
        private float searchTimer;
        private float attackLockTimer;
        private Vector2Int lastSearchTargetCell;
        private bool reportedMissingGrid;
        private float patrolWaitTimer = 1f;
        private Vector2Int? flowWaypointCell;

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
                    TickIdle(player, hasSight, enemyDeltaTime);
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
        /// 待机: 没看见玩家时在所属房间内随机散步, 发现玩家立即追击.
        /// </summary>
        private void TickIdle(Player player, bool hasSight, float enemyDeltaTime)
        {
            if (hasSight)
            {
                patrolPath.Clear();
                lastSeenPosition = player.transform.position;
                Enter(EnemyBrainState.Chase);
                return;
            }

            var grid = ResolveWalkGrid();
            if (grid == null)
            {
                Stop();
                return;
            }

            if (patrolPath.Count == 0)
            {
                MoveSeparationOnly();
                patrolWaitTimer -= enemyDeltaTime;
                if (patrolWaitTimer > 0f) return;

                var from = grid.GetCell(owner.transform.position);
                if (!grid.TryPickPatrolTarget(from, out var target) ||
                    !grid.TryFindPath(from, target, patrolPath) || patrolPath.Count == 0)
                {
                    patrolWaitTimer = 1f;
                    return;
                }
            }

            while (patrolPath.Count > 0)
            {
                var stepCenter = grid.GetCellCenterWorld(patrolPath[0]);
                if (((Vector2)(stepCenter - owner.transform.position)).sqrMagnitude > ArriveCellSqrDistance) break;
                patrolPath.RemoveAt(0);
            }
            if (patrolPath.Count == 0)
            {
                MoveSeparationOnly();
                patrolWaitTimer = UnityEngine.Random.Range(0.8f, 2.5f);
                return;
            }

            var direction = (Vector2)(grid.GetCellCenterWorld(patrolPath[0]) - owner.transform.position);
            ApplyMovement(direction.normalized, owner.transform.position, false);
            FaceTowards(direction);
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
                MoveSeparationOnly();
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
        /// 走流场的下一格格心; 路径缺失时停住, 避免直线冲向墙体.
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
                Stop();
                return;
            }
            ApplyMovement(direction, selfPosition);
        }

        /// <summary>
        /// 合成分离向量后写入速度, 距玩家小于停距时速度为零.
        /// </summary>
        private void ApplyMovement(Vector2 direction, Vector3 selfPosition, bool respectPlayerStopDistance = true)
        {
            var player = PlayerRegistry.Current;
            if (respectPlayerStopDistance && player != null)
            {
                var toPlayer = (Vector2)(player.transform.position - selfPosition);
                if (toPlayer.magnitude <= Mathf.Max(0f, owner.BrainPlayerStopDistance))
                {
                    // 距离过近时停住, 分离不能把敌人推进玩家.
                    direction = Vector2.zero;
                }
            }

            var separation = EnemySeparation.ComputeSeparation(owner, owner.BrainSeparationRadius);
            var blended = direction + owner.BrainSeparationWeight * separation;
            if (blended.sqrMagnitude <= 0.0001f)
            {
                Stop();
                return;
            }

            // 分离推力不能让敌人持续顶墙; 路径方向能走时优先回到路径.
            var moveDirection = blended.normalized;
            if (HitsWall(moveDirection))
            {
                moveDirection = direction.normalized;
                if (direction.sqrMagnitude <= 0.0001f || HitsWall(moveDirection))
                {
                    Stop();
                    return;
                }
            }
            var speed = owner.BrainMoveSpeed * GameplayTime.EnemyTimeScale * Mathf.Clamp01(blended.magnitude);
            owner.ApplyBrainVelocity(moveDirection * speed);
            owner.SetBrainAnimatorSpeed(owner.BrainMoveSpeed);
        }

        private bool HitsWall(Vector2 direction)
        {
            var radius = Mathf.Max(0.1f, owner.BrainCollisionRadius - 0.04f);
            return Physics2D.CircleCast(owner.BrainCollisionCenter, radius, direction, 0.18f, WallLayerMask).collider != null;
        }

        private void MoveSeparationOnly()
        {
            ApplyMovement(Vector2.zero, owner.transform.position, false);
        }

        /// <summary>
        /// 读取流场方向, 目标跨格时由格子重算.
        /// </summary>
        private bool TryGetFlowDirection(RoomWalkGrid grid, Vector3 targetWorldPosition, Vector3 selfPosition, out Vector2 direction)
        {
            grid.EnsureFlowField(targetWorldPosition);
            var cell = grid.GetCell(selfPosition);
            // 进入下一格边界时仍走到该格中心, 否则流场会提前拐弯并切进墙角.
            if (flowWaypointCell.HasValue)
            {
                var waypoint = (Vector2)(grid.GetCellCenterWorld(flowWaypointCell.Value) - selfPosition);
                if (waypoint.sqrMagnitude > ArriveCellSqrDistance && grid.IsWalkable(flowWaypointCell.Value))
                {
                    direction = waypoint.normalized;
                    return true;
                }
                flowWaypointCell = null;
            }
            if (grid.TryGetFlowNextCell(cell, out var next))
            {
                flowWaypointCell = next;
                direction = ((Vector2)(grid.GetCellCenterWorld(next) - selfPosition)).normalized;
                return true;
            }

            // 脚下格子不可走时, 先回到最近可走格的中心.
            if (!grid.IsWalkable(cell) && grid.TryGetNearestWalkableCell(cell, out var nearest))
            {
                direction = ((Vector2)(grid.GetCellCenterWorld(nearest) - selfPosition)).normalized;
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
                MoveSeparationOnly();
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
            owner.SetBrainFacing((Vector2)toTarget);
        }

        private void Stop()
        {
            owner.ApplyBrainVelocity(Vector2.zero);
            owner.SetBrainAnimatorSpeed(0f);
        }

        private void Enter(EnemyBrainState nextState)
        {
            state = nextState;
            if (nextState != EnemyBrainState.Chase && nextState != EnemyBrainState.Attack) flowWaypointCell = null;
            if (nextState == EnemyBrainState.Idle)
            {
                patrolPath.Clear();
                patrolWaitTimer = UnityEngine.Random.Range(0.8f, 2.5f);
            }
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
            patrolPath.Clear();
            patrolWaitTimer = 1f;
            reportedMissingGrid = false;
            flowWaypointCell = null;
            Stop();
        }
    }
}
