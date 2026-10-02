using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// 九宫格中的房间状态, 坐标始终相对当前房间.
    /// </summary>
    public readonly struct LocalMinimapRoom
    {
        public Vector2Int Offset { get; }
        public bool Visited { get; }
        public bool Current => Offset == Vector2Int.zero;

        public LocalMinimapRoom(Vector2Int offset, bool visited)
        {
            Offset = offset;
            Visited = visited;
        }
    }

    public readonly struct LocalMinimapConnection
    {
        public Vector2Int From { get; }
        public Vector2Int To { get; }

        public LocalMinimapConnection(Vector2Int from, Vector2Int to)
        {
            From = from;
            To = to;
        }
    }

    /// <summary>
    /// 从权威房间图提取固定 3x3 窗口, UI 不根据世界距离猜测邻居或补位.
    /// </summary>
    public sealed class LocalMinimapSnapshot
    {
        public IReadOnlyList<LocalMinimapRoom> Rooms { get; }
        public IReadOnlyList<LocalMinimapConnection> Connections { get; }

        private LocalMinimapSnapshot(List<LocalMinimapRoom> rooms, List<LocalMinimapConnection> connections)
        {
            Rooms = rooms.AsReadOnly();
            Connections = connections.AsReadOnly();
        }

        public static LocalMinimapSnapshot Build(RoomGraph graph, Vector2Int center, Func<Vector2Int, bool> isVisited)
        {
            var rooms = new List<LocalMinimapRoom>(9);
            var connections = new List<LocalMinimapConnection>(12);
            for (var y = -1; y <= 1; y++)
            for (var x = -1; x <= 1; x++)
            {
                var offset = new Vector2Int(x, y);
                var cell = center + offset;
                if (!graph.NodesByCell.TryGetValue(cell, out var node)) continue;
                rooms.Add(new LocalMinimapRoom(offset, isVisited(cell)));
                foreach (var direction in new[] { Vector2Int.right, Vector2Int.up })
                {
                    var end = offset + direction;
                    // 连接必须有两个可见端点, 不显示窗口外的半截线路.
                    if (end.x > 1 || end.y > 1 || !node.Neighbors.Contains(cell + direction)) continue;
                    connections.Add(new LocalMinimapConnection(offset, end));
                }
            }
            return new LocalMinimapSnapshot(rooms, connections);
        }
    }

}
