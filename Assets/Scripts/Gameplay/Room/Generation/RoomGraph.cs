using System;
using System.Collections.Generic;
using System.Linq;
using Game.Core;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// 随机生成房间类型.
    /// </summary>
    public enum GeneratedRoomType
    {
        Init,
        Normal,
        Chest,
        Save,
        Final
    }

    /// <summary>
    /// 房间图里的一个逻辑房间: 格子坐标, 类型, 预制体地址和四个方向的邻居.
    /// </summary>
    public sealed class RoomGraphNode
    {
        public Vector2Int Cell;
        public GeneratedRoomType Type;
        public bool TypeAssigned;
        public string PrefabKey;
        public readonly List<Vector2Int> Neighbors = new List<Vector2Int>();
    }

    /// <summary>
    /// 房间图: 带种子的紧凑分支生长生成一棵连通树, 再按关卡配置分配类型.
    /// 全程只用同一个 System.Random, 同一关卡配置加同一颗种子得到同一张图.
    /// </summary>
    public sealed class RoomGraph
    {
        private static readonly Vector2Int[] Directions =
        {
            Vector2Int.up,
            Vector2Int.right,
            Vector2Int.down,
            Vector2Int.left,
        };

        private readonly Dictionary<Vector2Int, RoomGraphNode> nodesByCell = new Dictionary<Vector2Int, RoomGraphNode>();

        public IReadOnlyDictionary<Vector2Int, RoomGraphNode> NodesByCell => nodesByCell;
        public IReadOnlyCollection<RoomGraphNode> Nodes => nodesByCell.Values;

        /// <summary>
        /// 按关卡配置和种子生成房间图.
        /// </summary>
        /// <param name="config">关卡配置, 来自 LevelData.lua.</param>
        /// <param name="seed">随机种子, 读档时必须与首生成一致.</param>
        /// <returns>房间图.</returns>
        public static RoomGraph Generate(LevelConfig config, int seed)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (config.RoomCount <= 0)
            {
                throw new InvalidOperationException("关卡 roomCount 必须大于 0.");
            }

            if (config.NormalCount > 0 && (config.NormalPrefabKeys == null || config.NormalPrefabKeys.Count == 0))
            {
                throw new InvalidOperationException($"关卡 {config.LevelId} 的 normalPrefabKeys 为空, 无法分配普通房.");
            }

            var rng = new System.Random(seed);
            var graph = new RoomGraph();
            GrowCompactCells(graph, config.RoomCount, rng);
            AssignTypes(graph, config, rng);
            return graph;
        }

        /// <summary>
        /// 从已生成房间的边界扩展, 优先选靠近起点且能形成分支的位置.
        /// </summary>
        private static void GrowCompactCells(RoomGraph graph, int roomCount, System.Random rng)
        {
            var origin = Vector2Int.zero;
            graph.CreateNode(origin);
            var depths = new Dictionary<Vector2Int, int> { [origin] = 0 };
            var radius = Mathf.CeilToInt((Mathf.Sqrt(roomCount) - 1f) * 0.5f);

            while (graph.nodesByCell.Count < roomCount)
            {
                var candidates = new List<(Vector2Int parent, Vector2Int cell, int score)>();
                foreach (var parent in graph.nodesByCell.Keys)
                {
                    foreach (var direction in Directions)
                    {
                        var cell = parent + direction;
                        if (graph.nodesByCell.ContainsKey(cell) ||
                            Mathf.Abs(cell.x) > radius || Mathf.Abs(cell.y) > radius) continue;

                        // 曼哈顿距离约束外扩, 深度与已有连接数抑制单条长链.
                        var score = (Mathf.Abs(cell.x) + Mathf.Abs(cell.y)) * 4
                            + depths[parent] * 3 + graph.nodesByCell[parent].Neighbors.Count * 2;
                        candidates.Add((parent, cell, score));
                    }
                }

                if (candidates.Count == 0)
                {
                    radius++;
                    continue;
                }

                var bestScore = candidates.Min(candidate => candidate.score);
                var best = candidates.Where(candidate => candidate.score == bestScore).ToList();
                var selected = best[rng.Next(best.Count)];
                graph.CreateNode(selected.cell);
                graph.Connect(selected.parent, selected.cell);
                depths[selected.cell] = depths[selected.parent] + 1;
            }
        }

        /// <summary>
        /// 按关卡配置分配房间类型和预制体地址.
        /// </summary>
        private static void AssignTypes(RoomGraph graph, LevelConfig config, System.Random rng)
        {
            var origin = graph.nodesByCell[Vector2Int.zero];
            Assign(origin, GeneratedRoomType.Init, config.InitPrefabKey);

            // 终点取曼哈顿距离最远的一间, 并列时按种子随机取.
            var finalNode = graph.nodesByCell.Values
                .Where(node => node != origin)
                .GroupBy(node => ManhattanDistance(node.Cell))
                .OrderByDescending(group => group.Key)
                .First()
                .ToList();
            Assign(finalNode[rng.Next(finalNode.Count)], GeneratedRoomType.Final, config.FinalPrefabKey);

            // 宝箱房和存档房优先从死胡同里取, 不够再从其余未分配房间补足.
            AssignDeadEndPreferred(graph, GeneratedRoomType.Chest, config.ChestCount, config.ChestPrefabKey, rng);
            AssignDeadEndPreferred(graph, GeneratedRoomType.Save, config.SaveCount, config.SavePrefabKey, rng);

            // 剩余房间必须正好等于普通房数量, 数量对不上直接暴露配置问题.
            var remaining = graph.nodesByCell.Values.Where(node => !node.TypeAssigned).ToList();
            if (remaining.Count != config.NormalCount)
            {
                throw new InvalidOperationException(
                    $"关卡 {config.LevelId} 分配后剩余 {remaining.Count} 间房, 与 normalCount {config.NormalCount} 不一致.");
            }

            foreach (var node in remaining)
            {
                var prefabIndex = rng.Next(config.NormalPrefabKeys.Count);
                Assign(node, GeneratedRoomType.Normal, config.NormalPrefabKeys[prefabIndex]);
            }

            void Assign(RoomGraphNode node, GeneratedRoomType type, string prefabKey)
            {
                node.Type = type;
                node.TypeAssigned = true;
                node.PrefabKey = prefabKey;
            }
        }

        /// <summary>
        /// 按目标数量分配宝箱房或存档房, 死胡同优先.
        /// </summary>
        private static void AssignDeadEndPreferred(
            RoomGraph graph,
            GeneratedRoomType type,
            int count,
            string prefabKey,
            System.Random rng)
        {
            var assigned = 0;
            var unassigned = graph.nodesByCell.Values.Where(node => !node.TypeAssigned).ToList();

            // 先从死胡同里抽, 死胡同只有一条边, 适合放奖励和存档点.
            var deadEnds = unassigned.Where(node => node.Neighbors.Count == 1).ToList();
            while (assigned < count && deadEnds.Count > 0)
            {
                var index = rng.Next(deadEnds.Count);
                var node = deadEnds[index];
                node.Type = type;
                node.TypeAssigned = true;
                node.PrefabKey = prefabKey;
                deadEnds.RemoveAt(index);
                unassigned.Remove(node);
                assigned++;
            }

            // 死胡同不够时从其余未分配房间补足.
            while (assigned < count && unassigned.Count > 0)
            {
                var index = rng.Next(unassigned.Count);
                var node = unassigned[index];
                node.Type = type;
                node.TypeAssigned = true;
                node.PrefabKey = prefabKey;
                unassigned.RemoveAt(index);
                assigned++;
            }

            if (assigned < count)
            {
                throw new InvalidOperationException($"关卡房间数量不足, 无法分配 {assigned}/{count} 间 {type} 房.");
            }
        }

        private static int ManhattanDistance(Vector2Int cell)
        {
            return Mathf.Abs(cell.x) + Mathf.Abs(cell.y);
        }

        private RoomGraphNode CreateNode(Vector2Int cell)
        {
            var node = new RoomGraphNode { Cell = cell };
            nodesByCell[cell] = node;
            return node;
        }

        private void Connect(Vector2Int a, Vector2Int b)
        {
            nodesByCell[a].Neighbors.Add(b);
            nodesByCell[b].Neighbors.Add(a);
        }
    }
}
