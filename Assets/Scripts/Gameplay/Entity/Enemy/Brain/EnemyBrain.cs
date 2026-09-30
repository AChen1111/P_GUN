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
        // 保留攻击状态的既有数值, 避免热修代码中的枚举值变化.
        Attack = 3
    }

    /// <summary>
    /// 敌人行为大脑: 每帧追踪玩家, 执行寻路, 分离和攻击.
    /// 战斗中持续追踪玩家当前位置, 沿流场绕墙, 刚体速度只由本类写入.
    /// </summary>
    public sealed class EnemyBrain
    {
        // 到达格中心的判定距离平方.
        private const float ArriveCellSqrDistance = 0.04f;
        private static readonly int WallLayerMask = LayerMask.GetMask("Wall");

        private readonly EnemyBase owner;
        private readonly IEnemyAttack attack;
        private readonly List<Vector2Int> patrolPath = new List<Vector2Int>();

        private EnemyBrainState state = EnemyBrainState.Idle;
        private float attackLockTimer;
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
            // 生成的敌人只追踪所属战斗房间的玩家, 无角度或距离发现条件.
            var hasTarget = player != null && owner.OwnerFightRoom != null &&
                owner.OwnerFightRoom == FightRoom.currentFightRoom;
            if (!hasTarget)
            {
                if (state == EnemyBrainState.Attack) attack.EndAttack();
                if (state != EnemyBrainState.Idle) Enter(EnemyBrainState.Idle);
                TickIdle(enemyDeltaTime);
                return;
            }

            if (state == EnemyBrainState.Idle) Enter(EnemyBrainState.Chase);
            var selfPosition = owner.transform.position;
            if (state == EnemyBrainState.Attack)
                TickAttack(player, selfPosition, enemyDeltaTime);
            else
                TickChase(player, selfPosition);
        }

        /// <summary>
        /// 待机: 所属房间未战斗或玩家不存在时在房间内随机散步.
        /// </summary>
        private void TickIdle(float enemyDeltaTime)
        {
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
        /// 追击: 始终读取玩家当前位置, 用流场绕墙并保持面朝.
        /// </summary>
        private void TickChase(Player player, Vector3 selfPosition)
        {
            var context = BuildContext(player, selfPosition);
            if (attack.CanAttack(context))
            {
                BeginAttack(context);
                return;
            }

            MoveWithFlow(player.transform.position, selfPosition);
            FaceTowards(player.transform.position - selfPosition);
        }

        /// <summary>
        /// 攻击: 锁定结束后继续追击, 墙体只阻挡攻击路线而不丢失目标.
        /// </summary>
        private void TickAttack(Player player, Vector3 selfPosition, float enemyDeltaTime)
        {
            attackLockTimer += enemyDeltaTime;
            FaceTowards(player.transform.position - selfPosition);
            attack.TickAttack(BuildContext(player, selfPosition), enemyDeltaTime);
            if (attackLockTimer >= attack.AttackLockDuration)
            {
                attackLockTimer = 0f;
                attack.EndAttack();
                Enter(EnemyBrainState.Chase);
                return;
            }

            if (attack.LocksMovement)
                MoveSeparationOnly();
            else
                MoveWithFlow(player.transform.position, selfPosition);
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
        private EnemyAttackContext BuildContext(Player player, Vector3 selfPosition)
        {
            var toPlayer = (Vector2)(player.transform.position - selfPosition);
            var distance = toPlayer.magnitude;
            return new EnemyAttackContext
            {
                // 视锥只决定出手, 玩家在范围外或墙后仍会被持续追踪.
                IsPlayerInAttackCone = EnemyAttackCone.ContainsPlayer(selfPosition, owner.BrainFacingDirection,
                    player, owner.BrainAttackRange, owner.BrainAttackAngle),
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
        /// 死亡或回池时清空全部行为状态, 复用后的敌人没有上一次的路径和攻击状态.
        /// </summary>
        public void ResetForPoolOrDeath()
        {
            state = EnemyBrainState.Idle;
            attackLockTimer = 0f;
            patrolPath.Clear();
            patrolWaitTimer = 1f;
            reportedMissingGrid = false;
            flowWaypointCell = null;
            Stop();
        }
    }
}
