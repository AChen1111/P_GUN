using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Game.UI;
using UnityEditor;
using UnityEngine.Tilemaps;
using Game.Core;
using Game.Gameplay;
using Game.Gameplay.Save;
using NUnit.Framework;
using UnityEngine;

public class RoomGenerationTests
{
    private static LevelConfig Config(int count = 100) => new LevelConfig
    {
        LevelId = "test", RoomCount = count, InitCount = 1, FinalCount = 1,
        ChestCount = 1, SaveCount = 1, NormalCount = count - 4,
        InitPrefabKey = "InitRoom", FinalPrefabKey = "FinalRoom", ChestPrefabKey = "ChestRoom",
        SavePrefabKey = "SaveRoom", NormalPrefabKeys = new List<string> { "NormalRoom" }
    };

    [Test]
    public void HundredSeedsProduceDeterministicConnectedTrees()
    {
        var signatures = new HashSet<string>();
        var degrees = new HashSet<int>();
        for (var seed = 0; seed < 100; seed++)
        {
            var graph = RoomGraph.Generate(Config(), seed);
            Assert.AreEqual(100, graph.Nodes.Count);
            Assert.AreEqual(99, graph.Nodes.Sum(node => node.Neighbors.Count) / 2);
            Assert.AreEqual(96, graph.Nodes.Count(node => node.Type == GeneratedRoomType.Normal));
            foreach (var type in new[] { GeneratedRoomType.Init, GeneratedRoomType.Final, GeneratedRoomType.Chest, GeneratedRoomType.Save })
                Assert.AreEqual(1, graph.Nodes.Count(node => node.Type == type));
            var distances = Distances(graph);
            Assert.AreEqual(100, distances.Count);
            var final = graph.Nodes.Single(node => node.Type == GeneratedRoomType.Final);
            Assert.AreEqual(distances.Values.Max(), distances[final.Cell]);
            foreach (var node in graph.Nodes)
            {
                Assert.That(node.Neighbors.Count, Is.InRange(1, 4));
                degrees.Add(node.Neighbors.Count);
                Assert.AreEqual(node.Neighbors.Count, node.Neighbors.Distinct().Count());
                foreach (var neighbor in node.Neighbors)
                {
                    Assert.IsTrue(graph.NodesByCell[neighbor].Neighbors.Contains(node.Cell));
                    var delta = neighbor - node.Cell;
                    Assert.AreEqual(1, Math.Abs(delta.x) + Math.Abs(delta.y));
                }
            }
            var signature = Signature(graph);
            Assert.AreEqual(signature, Signature(RoomGraph.Generate(Config(), seed)));
            signatures.Add(signature);
        }
        Assert.AreEqual(100, signatures.Count);
        CollectionAssert.AreEquivalent(new[] { 1, 2, 3, 4 }, degrees);
    }

    [Test]
    public void IsolatedComponentsArePrunedBeforeTypeAssignment()
    {
        var cells = Enumerable.Range(0, 6).Select(x => new Vector2Int(x, 0))
            .Concat(new[] { new Vector2Int(20, 20), new Vector2Int(20, 21) });
        var graph = RoomGraph.GenerateFromCells(Config(8), 42, cells);
        Assert.AreEqual(6, graph.Nodes.Count);
        Assert.AreEqual(2, graph.Nodes.Count(node => node.Type == GeneratedRoomType.Normal));
        Assert.AreEqual(6, Distances(graph).Count);
        Assert.IsFalse(graph.NodesByCell.ContainsKey(new Vector2Int(20, 20)));
        foreach (var node in graph.Nodes)
            Assert.IsTrue(node.Neighbors.All(graph.NodesByCell.ContainsKey));
    }

    [Test]
    public void InsufficientReachableSpecialRoomsFailClearly()
    {
        Assert.Throws<InvalidOperationException>(() => RoomGraph.GenerateFromCells(Config(4), 0,
            new[] { Vector2Int.zero, Vector2Int.right, new Vector2Int(10, 10), new Vector2Int(11, 10) }));
    }

    [Test]
    public void NineCellWindowKeepsHolesAndNeverLeaksConnections()
    {
        var cells = new[] { Vector2Int.zero, new Vector2Int(-1, 0), new Vector2Int(-2, 0),
            new Vector2Int(-2, -1), new Vector2Int(-2, -2), new Vector2Int(-1, -2), new Vector2Int(0, -2) };
        var graph = RoomGraph.GenerateFromCells(Config(7), 23, cells);
        var snapshot = LocalMinimapSnapshot.Build(graph, new Vector2Int(-1, -1), cell => cell == Vector2Int.zero);
        Assert.AreEqual(7, snapshot.Rooms.Count);
        Assert.IsFalse(snapshot.Rooms.Any(room => room.Current));
        Assert.IsFalse(snapshot.Rooms.Any(room => room.Offset == Vector2Int.zero));
        Assert.AreEqual(1, snapshot.Rooms.Count(room => room.Visited));
        foreach (var center in cells)
        {
            snapshot = LocalMinimapSnapshot.Build(graph, center, cell => cell == Vector2Int.zero);
            Assert.That(snapshot.Rooms.Count, Is.InRange(1, 9));
            Assert.AreEqual(1, snapshot.Rooms.Count(room => room.Current));
            foreach (var link in snapshot.Connections)
            {
                Assert.IsTrue(snapshot.Rooms.Any(room => room.Offset == link.From));
                Assert.IsTrue(snapshot.Rooms.Any(room => room.Offset == link.To));
                Assert.IsTrue(graph.NodesByCell[center + link.From].Neighbors.Contains(center + link.To));
                Assert.That(Math.Abs(link.From.x), Is.AtMost(1));
                Assert.That(Math.Abs(link.From.y), Is.AtMost(1));
                Assert.That(Math.Abs(link.To.x), Is.AtMost(1));
                Assert.That(Math.Abs(link.To.y), Is.AtMost(1));
            }
        }
    }

    [Test]
    public void SaveVersionThreeRejectsOlderLayouts()
    {
        Assert.AreEqual(3, SaveGameService.SaveVersion);
        Assert.IsFalse(SaveGameService.IsCompatibleVersion(2));
        Assert.IsTrue(SaveGameService.IsCompatibleVersion(new GameSaveData().version));
    }

    [TestCase(0)]
    [TestCase(17)]
    [TestCase(91)]
    public void PrefabDoorsAndCorridorsMatchFinalTree(int seed)
    {
        var root = new GameObject("Room Geometry Verification");
        try
        {
            var generator = root.AddComponent<RandomRoomGenerator>();
            var serialized = new SerializedObject(generator);
            serialized.FindProperty("lrCorridorPrefabKey").stringValue = "LRCorridor";
            serialized.FindProperty("udCorridorPrefabKey").stringValue = "UDCorridor";
            serialized.ApplyModifiedPropertiesWithoutUndo();
            var graph = RoomGraph.Generate(Config(), seed);
            var prefabs = graph.Nodes.Select(node => node.PrefabKey).Distinct().ToDictionary(key => key,
                key => AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/Prefab/Room/RoomTemplate/{key}.prefab"));
            var positions = (Dictionary<Vector2Int, Vector3>)Invoke(generator, "ComputeRoomPositions", graph, prefabs);
            var rooms = new Dictionary<Vector2Int, Room>();
            foreach (var node in graph.Nodes)
            {
                var instance = UnityEngine.Object.Instantiate(prefabs[node.PrefabKey], positions[node.Cell], Quaternion.identity, root.transform);
                var room = instance.GetComponent<Room>();
                var directions = node.Neighbors.Select(cell => cell - node.Cell).ToList();
                room.InitializeGenerationContext(node.Cell, directions);
                typeof(RandomRoomGenerator).GetMethod("SealUnusedDoorways", BindingFlags.Static | BindingFlags.NonPublic)
                    .Invoke(null, new object[] { room, directions });
                rooms.Add(node.Cell, room);
            }
            Invoke(generator, "InstantiateCorridors", graph, rooms);
            Assert.AreEqual(199, root.transform.childCount);
            foreach (var node in graph.Nodes)
            {
                var room = rooms[node.Cell];
                var floor = room.GetComponentsInChildren<Tilemap>().Single(map => map.name == "Floor");
                var walls = room.GetComponentsInChildren<Tilemap>().Single(map => map.name == "Walls");
                var bounds = floor.cellBounds;
                foreach (var direction in new[] { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left })
                {
                    var horizontal = direction.x != 0;
                    var edge = horizontal ? (direction.x > 0 ? bounds.xMax - 1 : bounds.xMin)
                        : (direction.y > 0 ? bounds.yMax - 1 : bounds.yMin);
                    var middle = horizontal ? (bounds.yMin + bounds.yMax - 1) / 2f : (bounds.xMin + bounds.xMax - 1) / 2f;
                    var connected = node.Neighbors.Contains(node.Cell + direction);
                    for (var i = 0; i < 2; i++)
                    {
                        var cell = horizontal ? new Vector3Int(edge, Mathf.FloorToInt(middle) + i, 0)
                            : new Vector3Int(Mathf.FloorToInt(middle) + i, edge, 0);
                        Assert.AreEqual(!connected, walls.HasTile(cell));
                        if (!connected) continue;
                        // 从门洞向外一格必须踩到走廊地板, 不能有 Tilemap 接缝空隙.
                        var outside = floor.GetCellCenterWorld(cell + new Vector3Int(direction.x, direction.y, 0));
                        var corridor = root.GetComponentsInChildren<Tilemap>().Where(map => map.name == "Floor" && map.GetComponentInParent<Room>() == null)
                            .Single(map => map.HasTile(map.WorldToCell(outside)));
                        var corridorWalls = corridor.transform.parent.GetComponentsInChildren<Tilemap>().Single(map => map.name == "Walls");
                        Assert.IsFalse(corridorWalls.HasTile(corridorWalls.WorldToCell(outside)));
                    }
                }
            }
        }
        finally { UnityEngine.Object.DestroyImmediate(root); }
    }

    [Test]
    public void GraphicRefreshesOnEntryRestoreAndReopening()
    {
        var root = new GameObject("Minimap State Verification");
        var display = new GameObject("Minimap Graphic", typeof(RectTransform), typeof(CanvasRenderer));
        var previousCurrent = Room.CurrentPlayerRoom;
        try
        {
            var generator = root.AddComponent<RandomRoomGenerator>();
            Invoke(generator, "Awake");
            var cells = new[] { Vector2Int.zero, Vector2Int.right, Vector2Int.up, Vector2Int.left };
            var graph = RoomGraph.GenerateFromCells(Config(4), 5, cells);
            var rooms = new Dictionary<Vector2Int, Room>();
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Room/RoomTemplate/InitRoom.prefab");
            foreach (var cell in cells)
            {
                var room = UnityEngine.Object.Instantiate(prefab, root.transform).GetComponent<Room>();
                room.InitializeGenerationContext(cell, graph.NodesByCell[cell].Neighbors.Select(neighbor => neighbor - cell));
                rooms.Add(cell, room);
            }
            typeof(RandomRoomGenerator).GetProperty("GeneratedGraph").SetValue(generator, graph);
            typeof(RandomRoomGenerator).GetProperty("GeneratedRooms").SetValue(generator, rooms);
            var graphic = display.AddComponent<LocalMinimapGraphic>();
            rooms[Vector2Int.zero].MarkVisited();
            Assert.AreEqual(1, Snapshot(graphic).Rooms.Count(room => room.Current));
            // 离开到走廊没有当前房间赋值, 保留上一房窗口.
            Assert.AreEqual(Vector2Int.zero, Room.CurrentPlayerRoom.GridCell);
            display.SetActive(false);
            rooms[Vector2Int.right].MarkVisited();
            display.SetActive(true);
            Assert.AreEqual(3, Snapshot(graphic).Rooms.Count);
            Assert.IsTrue(Snapshot(graphic).Rooms.Single(room => room.Offset == Vector2Int.left).Visited);
            Room.SetCurrentPlayerRoom(rooms[Vector2Int.left]);
            Assert.AreEqual(3, Snapshot(graphic).Rooms.Count);
            Assert.AreEqual(1, Snapshot(graphic).Rooms.Count(room => room.Current));
        }
        finally
        {
            UnityEngine.Object.DestroyImmediate(display);
            UnityEngine.Object.DestroyImmediate(root);
            Room.SetCurrentPlayerRoom(previousCurrent);
        }

        LocalMinimapSnapshot Snapshot(LocalMinimapGraphic graphic) => (LocalMinimapSnapshot)typeof(LocalMinimapGraphic)
            .GetField("snapshot", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(graphic);
    }

    private static object Invoke(object target, string method, params object[] parameters) => target.GetType()
        .GetMethod(method, BindingFlags.NonPublic | BindingFlags.Instance).Invoke(target, parameters);

    private static Dictionary<Vector2Int, int> Distances(RoomGraph graph)
    {
        var visited = new Dictionary<Vector2Int, int> { [Vector2Int.zero] = 0 };
        var pending = new Queue<Vector2Int>();
        pending.Enqueue(Vector2Int.zero);
        while (pending.Count > 0)
        {
            var cell = pending.Dequeue();
            foreach (var neighbor in graph.NodesByCell[cell].Neighbors)
                if (!visited.ContainsKey(neighbor))
                {
                    visited.Add(neighbor, visited[cell] + 1);
                    pending.Enqueue(neighbor);
                }
        }
        return visited;
    }

    private static string Signature(RoomGraph graph) => string.Join(";", graph.Nodes.OrderBy(node => node.Cell.x).ThenBy(node => node.Cell.y)
        .Select(node => $"{node.Cell}:{node.Type}:{node.PrefabKey}:{string.Join(",", node.Neighbors)}"));
}
