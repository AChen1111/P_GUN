using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Game.Core;
using UnityEngine;

namespace Game.Gameplay
{
    public enum GeneratedRoomType { Init, Normal, Chest, Save, Final }

    /// <summary>
    /// 房间图只读节点, 连接仅由生成器维护.
    /// </summary>
    public sealed class RoomGraphNode
    {
        internal readonly List<Vector2Int> Connections = new List<Vector2Int>();
        public Vector2Int Cell { get; internal set; }
        public GeneratedRoomType Type { get; internal set; }
        internal bool TypeAssigned;
        public string PrefabKey { get; internal set; }
        public IReadOnlyList<Vector2Int> Neighbors { get; }

        internal RoomGraphNode(Vector2Int cell)
        {
            Cell = cell;
            Neighbors = Connections.AsReadOnly();
        }
    }

    /// <summary>
    /// 分段随机生长房间位置, 用随机权重最小生成树确定通路, 再裁掉起点不可达的房间.
    /// 坐标与边均稳定排序, 同配置和同种子得到相同房间及连接.
    /// </summary>
    public sealed class RoomGraph
    {
        private static readonly Vector2Int[] Directions =
        {
            Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left
        };
        private readonly Dictionary<Vector2Int, RoomGraphNode> nodesByCell = new Dictionary<Vector2Int, RoomGraphNode>();
        public IReadOnlyDictionary<Vector2Int, RoomGraphNode> NodesByCell { get; }
        public IReadOnlyCollection<RoomGraphNode> Nodes => nodesByCell.Values;

        public RoomGraph()
        {
            NodesByCell = new ReadOnlyDictionary<Vector2Int, RoomGraphNode>(nodesByCell);
        }

        public static RoomGraph Generate(LevelConfig config, int seed)
        {
            ValidateConfig(config);
            var rng = new System.Random(seed);
            var graph = new RoomGraph();
            graph.GrowCells(config.RoomCount, rng);
            graph.FinalizeGraph(config, rng);
            return graph;
        }

        /// <summary>
        /// 从外部候选格生成通路, 供编辑器验证断开区域的裁剪与类型分配.
        /// 候选格必须唯一并包含起点, 不补齐删除掉的房间.
        /// </summary>
        public static RoomGraph GenerateFromCells(LevelConfig config, int seed, IEnumerable<Vector2Int> cells)
        {
            ValidateConfig(config);
            var graph = new RoomGraph();
            foreach (var cell in cells.OrderBy(cell => cell.x).ThenBy(cell => cell.y))
                graph.nodesByCell.Add(cell, new RoomGraphNode(cell));
            if (!graph.nodesByCell.ContainsKey(Vector2Int.zero))
                throw new InvalidOperationException("候选房间必须包含起点 (0, 0).");
            if (graph.nodesByCell.Count > config.RoomCount)
                throw new InvalidOperationException("候选房间数不能超过关卡 roomCount.");
            graph.FinalizeGraph(config, new System.Random(seed));
            return graph;
        }

        private static void ValidateConfig(LevelConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            if (config.InitCount != 1 || config.FinalCount != 1 || config.ChestCount < 0 ||
                config.SaveCount < 0 || config.NormalCount < 0 ||
                config.RoomCount != 2 + config.ChestCount + config.SaveCount + config.NormalCount)
                throw new InvalidOperationException($"关卡 {config.LevelId} 房间数量配置不合法.");
            if (config.NormalCount > 0 && config.NormalPrefabKeys.Count == 0)
                throw new InvalidOperationException($"关卡 {config.LevelId} 的 normalPrefabKeys 为空.");
        }

        private List<Vector2Int> OrderedCells() => nodesByCell.Keys.OrderBy(cell => cell.x).ThenBy(cell => cell.y).ToList();

        private void GrowCells(int roomCount, System.Random rng)
        {
            nodesByCell.Add(Vector2Int.zero, new RoomGraphNode(Vector2Int.zero));
            while (nodesByCell.Count < roomCount)
            {
                // 每段重新选起点, 不用距离分数把地图强行填成方块.
                var frontier = OrderedCells().Where(cell => Directions.Any(dir => !nodesByCell.ContainsKey(cell + dir))).ToList();
                var current = frontier[rng.Next(frontier.Count)];
                var length = rng.Next(2, 7);
                for (var step = 0; step < length && nodesByCell.Count < roomCount; step++)
                {
                    var free = Directions.Where(dir => !nodesByCell.ContainsKey(current + dir)).ToList();
                    if (free.Count == 0) break;
                    current += free[rng.Next(free.Count)];
                    nodesByCell.Add(current, new RoomGraphNode(current));
                }
            }
        }

        private void FinalizeGraph(LevelConfig config, System.Random rng)
        {
            BuildMinimumSpanningTree(rng);
            var distances = PruneUnreachable();
            var specialCount = 2 + config.ChestCount + config.SaveCount;
            if (nodesByCell.Count < specialCount)
                throw new InvalidOperationException($"关卡 {config.LevelId} 裁剪后仅有 {nodesByCell.Count} 间, 无法容纳 {specialCount} 间特殊房.");
            AssignTypes(config, rng, distances);
        }

        private void BuildMinimumSpanningTree(System.Random rng)
        {
            var cells = OrderedCells();
            var indices = cells.Select((cell, index) => (cell, index)).ToDictionary(pair => pair.cell, pair => pair.index);
            var parents = Enumerable.Range(0, cells.Count).ToArray();
            var edges = new List<(int a, int b, int weight)>();
            for (var a = 0; a < cells.Count; a++)
            {
                // 只枚举右边与上边, 每条无向候选边赋一次随机权重.
                foreach (var dir in new[] { Vector2Int.right, Vector2Int.up })
                    if (indices.TryGetValue(cells[a] + dir, out var b))
                        edges.Add((a, b, rng.Next()));
            }
            edges.Sort((left, right) =>
            {
                var comparison = left.weight.CompareTo(right.weight);
                if (comparison != 0) return comparison;
                comparison = left.a.CompareTo(right.a);
                return comparison != 0 ? comparison : left.b.CompareTo(right.b);
            });
            foreach (var edge in edges)
            {
                var aRoot = Find(edge.a);
                var bRoot = Find(edge.b);
                if (aRoot == bRoot) continue;
                parents[bRoot] = aRoot;
                nodesByCell[cells[edge.a]].Connections.Add(cells[edge.b]);
                nodesByCell[cells[edge.b]].Connections.Add(cells[edge.a]);
            }
            foreach (var node in nodesByCell.Values)
                node.Connections.Sort((a, b) => a.x != b.x ? a.x.CompareTo(b.x) : a.y.CompareTo(b.y));

            int Find(int index)
            {
                while (parents[index] != index)
                {
                    parents[index] = parents[parents[index]];
                    index = parents[index];
                }
                return index;
            }
        }

        private Dictionary<Vector2Int, int> PruneUnreachable()
        {
            var distances = new Dictionary<Vector2Int, int> { [Vector2Int.zero] = 0 };
            var pending = new Queue<Vector2Int>();
            pending.Enqueue(Vector2Int.zero);
            while (pending.Count > 0)
            {
                var cell = pending.Dequeue();
                foreach (var neighbor in nodesByCell[cell].Neighbors)
                {
                    if (distances.ContainsKey(neighbor)) continue;
                    distances.Add(neighbor, distances[cell] + 1);
                    pending.Enqueue(neighbor);
                }
            }
            foreach (var cell in OrderedCells().Where(cell => !distances.ContainsKey(cell)))
                nodesByCell.Remove(cell);
            foreach (var node in nodesByCell.Values)
                node.Connections.RemoveAll(cell => !distances.ContainsKey(cell));
            return distances;
        }

        private void AssignTypes(LevelConfig config, System.Random rng, Dictionary<Vector2Int, int> distances)
        {
            Assign(nodesByCell[Vector2Int.zero], GeneratedRoomType.Init, config.InitPrefabKey);
            // 路径距离最远的终点, 不再用几何距离代替实际路线长度.
            var farthestDistance = distances.Values.Max();
            var finalCandidates = OrderedCells().Where(cell => distances[cell] == farthestDistance).ToList();
            Assign(nodesByCell[finalCandidates[rng.Next(finalCandidates.Count)]], GeneratedRoomType.Final, config.FinalPrefabKey);
            AssignSpecial(GeneratedRoomType.Chest, config.ChestCount, config.ChestPrefabKey);
            AssignSpecial(GeneratedRoomType.Save, config.SaveCount, config.SavePrefabKey);
            foreach (var cell in OrderedCells())
            {
                var node = nodesByCell[cell];
                if (!node.TypeAssigned)
                    Assign(node, GeneratedRoomType.Normal, config.NormalPrefabKeys[rng.Next(config.NormalPrefabKeys.Count)]);
            }

            void AssignSpecial(GeneratedRoomType type, int count, string key)
            {
                for (var i = 0; i < count; i++)
                {
                    var unassigned = OrderedCells().Select(cell => nodesByCell[cell]).Where(node => !node.TypeAssigned).ToList();
                    var deadEnds = unassigned.Where(node => node.Neighbors.Count == 1).ToList();
                    var candidates = deadEnds.Count > 0 ? deadEnds : unassigned;
                    Assign(candidates[rng.Next(candidates.Count)], type, key);
                }
            }
        }

        private static void Assign(RoomGraphNode node, GeneratedRoomType type, string key)
        {
            node.Type = type;
            node.TypeAssigned = true;
            node.PrefabKey = key;
        }
    }
}
