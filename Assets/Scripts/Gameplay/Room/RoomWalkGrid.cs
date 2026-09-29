using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Gameplay
{
    /// <summary>
    /// 房间可行走格与流场, 由战斗房间持有, 不放静态全局.
    /// Floor 上的格子默认可走, Walls 上有瓦片的格子不可走.
    /// </summary>
    public sealed class RoomWalkGrid
    {
        private static readonly Vector2Int[] Directions =
        {
            Vector2Int.up,
            Vector2Int.right,
            Vector2Int.down,
            Vector2Int.left,
        };

        // 最近可走格的环形搜索半径, 超出视为敌人完全卡在墙里.
        private const int NearestWalkableSearchRadius = 6;

        private readonly Tilemap floorTilemap;
        private readonly Tilemap wallsTilemap;
        private readonly Dictionary<Vector2Int, Vector2Int> flowNextCell = new Dictionary<Vector2Int, Vector2Int>();
        private Vector2Int? lastFlowTargetCell;

        private RoomWalkGrid(Tilemap floorTilemap, Tilemap wallsTilemap)
        {
            this.floorTilemap = floorTilemap;
            this.wallsTilemap = wallsTilemap;
        }

        /// <summary>
        /// 从房间实例上名为 Floor 和 Walls 的 Tilemap 建格, 缺任一张直接报错.
        /// </summary>
        public static RoomWalkGrid BuildFrom(GameObject roomObject)
        {
            if (roomObject == null)
            {
                throw new ArgumentNullException(nameof(roomObject));
            }

            var tilemaps = roomObject.GetComponentsInChildren<Tilemap>();
            var floorTilemap = tilemaps.FirstOrDefault(tilemap => tilemap.name == "Floor");
            if (floorTilemap == null)
            {
                throw new InvalidOperationException($"房间 {roomObject.name} 缺少 Floor Tilemap, 无法建可行走格.");
            }

            var wallsTilemap = tilemaps.FirstOrDefault(tilemap => tilemap.name == "Walls");
            if (wallsTilemap == null)
            {
                throw new InvalidOperationException($"房间 {roomObject.name} 缺少 Walls Tilemap, 无法建可行走格.");
            }

            return new RoomWalkGrid(floorTilemap, wallsTilemap);
        }

        /// <summary>
        /// 世界坐标换格子坐标, 换算使用房间自身的 Floor Tilemap, 已包含房间摆放位置.
        /// </summary>
        public Vector2Int GetCell(Vector3 worldPosition)
        {
            var floorCell = floorTilemap.WorldToCell(worldPosition);
            return new Vector2Int(floorCell.x, floorCell.y);
        }

        /// <summary>
        /// 格子是否可行走.
        /// </summary>
        public bool IsWalkable(Vector2Int cell)
        {
            var floorCell = new Vector3Int(cell.x, cell.y, 0);
            if (!floorTilemap.HasTile(floorCell))
            {
                return false;
            }

            if (wallsTilemap.HasTile(floorCell))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 格子中心的世界坐标.
        /// </summary>
        public Vector3 GetCellCenterWorld(Vector2Int cell)
        {
            return floorTilemap.GetCellCenterWorld(new Vector3Int(cell.x, cell.y, 0));
        }

        /// <summary>
        /// 脚下格子不可走时, 环形扩张找最近的可走格.
        /// </summary>
        public bool TryGetNearestWalkableCell(Vector2Int cell, out Vector2Int nearest)
        {
            if (IsWalkable(cell))
            {
                nearest = cell;
                return true;
            }

            for (var radius = 1; radius <= NearestWalkableSearchRadius; radius++)
            {
                for (var dx = -radius; dx <= radius; dx++)
                {
                    for (var dy = -radius; dy <= radius; dy++)
                    {
                        // 只搜当前环, 内圈已经搜过.
                        if (Mathf.Abs(dx) != radius && Mathf.Abs(dy) != radius)
                        {
                            continue;
                        }

                        var candidate = cell + new Vector2Int(dx, dy);
                        if (IsWalkable(candidate))
                        {
                            nearest = candidate;
                            return true;
                        }
                    }
                }
            }

            nearest = default;
            return false;
        }

        /// <summary>
        /// 目标跨格时重算流场: 从目标格向外 BFS, 每个可走格记录指向目标的下一格.
        /// </summary>
        public void EnsureFlowField(Vector3 targetWorldPosition)
        {
            var targetCell = GetCell(targetWorldPosition);
            if (!TryGetNearestWalkableCell(targetCell, out var flowOrigin))
            {
                return;
            }

            if (lastFlowTargetCell == flowOrigin)
            {
                return;
            }

            lastFlowTargetCell = flowOrigin;
            flowNextCell.Clear();
            flowNextCell[flowOrigin] = flowOrigin;

            var queue = new Queue<Vector2Int>();
            queue.Enqueue(flowOrigin);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                for (var i = 0; i < Directions.Length; i++)
                {
                    var next = current + Directions[i];
                    if (!IsWalkable(next) || flowNextCell.ContainsKey(next))
                    {
                        continue;
                    }

                    // next 的下一步是 current, 沿链走即接近目标.
                    flowNextCell[next] = current;
                    queue.Enqueue(next);
                }
            }
        }

        /// <summary>
        /// 读流场方向, 没有流场或格子不在流场里返回 false.
        /// </summary>
        public bool TryGetFlowDirection(Vector2Int cell, out Vector2 direction)
        {
            direction = Vector2.zero;
            if (!flowNextCell.TryGetValue(cell, out var nextCell) || nextCell == cell)
            {
                return false;
            }

            var from = GetCellCenterWorld(cell);
            var to = GetCellCenterWorld(nextCell);
            var delta = (Vector2)(to - from);
            if (delta.sqrMagnitude <= 0.0001f)
            {
                return false;
            }

            direction = delta.normalized;
            return true;
        }

        /// <summary>
        /// 四方向 A* 寻路, 只走可走格, path[0] 是第一步, 不包含起点.
        /// </summary>
        public bool TryFindPath(Vector2Int from, Vector2Int to, List<Vector2Int> path)
        {
            path.Clear();
            if (!TryGetNearestWalkableCell(from, out var start))
            {
                return false;
            }

            if (!TryGetNearestWalkableCell(to, out var goal))
            {
                return false;
            }

            if (start == goal)
            {
                return true;
            }

            var cameFrom = new Dictionary<Vector2Int, Vector2Int>();
            var gScore = new Dictionary<Vector2Int, float> { [start] = 0f };
            var openSet = new List<Vector2Int> { start };

            while (openSet.Count > 0)
            {
                // 房间格子量不大, 线性取最小 f 即可.
                var currentIndex = 0;
                var bestScore = float.MaxValue;
                for (var i = 0; i < openSet.Count; i++)
                {
                    var score = gScore[openSet[i]] + Heuristic(openSet[i], goal);
                    if (score < bestScore)
                    {
                        bestScore = score;
                        currentIndex = i;
                    }
                }

                var current = openSet[currentIndex];
                openSet.RemoveAt(currentIndex);
                if (current == goal)
                {
                    BuildPath(cameFrom, goal, path);
                    return true;
                }

                for (var i = 0; i < Directions.Length; i++)
                {
                    var neighbor = current + Directions[i];
                    if (!IsWalkable(neighbor))
                    {
                        continue;
                    }

                    var neighborScore = gScore[current] + 1f;
                    if (gScore.TryGetValue(neighbor, out var existing) && existing <= neighborScore)
                    {
                        continue;
                    }

                    gScore[neighbor] = neighborScore;
                    cameFrom[neighbor] = current;
                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }

            return false;

            float Heuristic(Vector2Int a, Vector2Int b)
            {
                return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
            }

            void BuildPath(Dictionary<Vector2Int, Vector2Int> parents, Vector2Int end, List<Vector2Int> result)
            {
                var reversed = new List<Vector2Int>();
                var node = end;
                while (parents.TryGetValue(node, out var previous))
                {
                    reversed.Add(node);
                    node = previous;
                }

                reversed.Reverse();
                foreach (var step in reversed)
                {
                    result.Add(step);
                }
            }
        }

        /// <summary>
        /// 清掉流场缓存, 房间禁用时调用.
        /// </summary>
        public void ClearFlowField()
        {
            flowNextCell.Clear();
            lastFlowTargetCell = null;
        }
    }
}