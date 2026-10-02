using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Game.Core;
using Game.Gameplay.Save;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Gameplay
{
    /// <summary>
    /// 随机房间生成器, 挂在 GameScene.
    /// 按 LevelData 的关卡行做带种子的随机游走, 实例化房间预制体,
    /// 在相邻门锚点之间拼走廊, 并发布九宫格小地图数据.
    /// 不再调用 Edgar 的关卡图布局.
    /// </summary>
    public sealed class RandomRoomGenerator : MonoBehaviour
    {
        [Header("关卡配置")]
        [SerializeField] private string levelId = "level1";
        [SerializeField] private bool generateOnStart = true;

        [Header("走廊预制体")]
        // 配置保存短名, 资源来自阶段预加载缓存.

        [SerializeField, AddressableKey(AddressableAssetKind.Prefab)] private string lrCorridorPrefabKey = string.Empty;
        private GameObject lrCorridorPrefab => AddressableAssetAccess.Get<GameObject>(lrCorridorPrefabKey);
        // 配置保存短名, 资源来自阶段预加载缓存.

        [SerializeField, AddressableKey(AddressableAssetKind.Prefab)] private string udCorridorPrefabKey = string.Empty;
        private GameObject udCorridorPrefab => AddressableAssetAccess.Get<GameObject>(udCorridorPrefabKey);

        [SerializeField] private string floorTilemapName = "Floor";

        public static RandomRoomGenerator Active { get; private set; }

        private bool generated;
        private bool isGenerating;
        private int lastGeneratedSeed;
        private string forcedLevelId;
        private int? forcedSeed;

        public string LevelId => string.IsNullOrWhiteSpace(forcedLevelId) ? levelId : forcedLevelId;
        public int LastGeneratedSeed => lastGeneratedSeed;

        // UI 读取最终生成结果, 不自行维护另一份房间拓扑.
        public RoomGraph GeneratedGraph { get; private set; }
        public IReadOnlyDictionary<Vector2Int, Room> GeneratedRooms { get; private set; }

        public LocalMinimapSnapshot GetLocalMinimap()
        {
            if (GeneratedGraph == null) return null;
            var current = Room.CurrentPlayerRoom;
            var center = current != null ? current.GridCell : Vector2Int.zero;
            return LocalMinimapSnapshot.Build(GeneratedGraph, center, cell => GeneratedRooms[cell].Visited);
        }

        /// <summary>
        /// 初始化运行时依赖.
        /// </summary>
        private void Awake()
        {
            if (Active != null && Active != this)
            {
                throw new InvalidOperationException($"{nameof(RandomRoomGenerator)} already has an active instance.");
            }

            Active = this;
        }

        private void Start()
        {
            if (generateOnStart)
            {
                Generate();
            }
        }

        /// <summary>
        /// 释放销毁时持有的运行时状态.
        /// </summary>
        private void OnDestroy()
        {
            if (Active == this)
            {
                Active = null;
            }
        }

        /// <summary>
        /// 读档覆盖: 用存档里的关卡 id 和种子重建同一张地图.
        /// </summary>
        public void OverrideLevel(string savedLevelId, int seed)
        {
            if (string.IsNullOrWhiteSpace(savedLevelId))
            {
                throw new ArgumentException("存档关卡 id 不能为空.", nameof(savedLevelId));
            }

            forcedLevelId = savedLevelId;
            forcedSeed = seed;
        }

        public async void Generate()
        {
            try
            {
                await GenerateAsync();
            }
            catch (Exception exception)
            {
                Debug.LogError($"{nameof(RandomRoomGenerator)}: 生成房间失败, Error: {exception.Message}", this);
                throw;
            }
        }

        /// <summary>
        /// 生成一次房间布局, 已生成或正在生成时直接返回.
        /// </summary>
        public async Task GenerateAsync()
        {
            if (generated || isGenerating) return;

            isGenerating = true;
            try
            {
                // 读档时先写入存档的关卡与种子, 再开始生成.
                SaveGameService.ApplyPendingGenerationSettings(this);

                var config = LuaDataRuntime.GetLevelConfig(LevelId);
                var seed = forcedSeed ?? UnityEngine.Random.Range(int.MinValue, int.MaxValue);
                var graph = RoomGraph.Generate(config, seed);

                var roomInstances = await InstantiateRoomsAsync(graph);
                InstantiateCorridors(graph, roomInstances);
                foreach (var pair in roomInstances)
                {
                    if (pair.Value is NormalRoom normalRoom)
                    {
                        normalRoom.GenerateObstacles(seed, pair.Key);
                    }
                }
                GeneratedGraph = graph;
                GeneratedRooms = new ReadOnlyDictionary<Vector2Int, Room>(roomInstances);
                // 生成完成后显示起始房, 读档稍后会通过当前房间事件切换到恢复位置.
                roomInstances[Vector2Int.zero].MarkVisited();

                lastGeneratedSeed = seed;
                generated = true;
                StartCoroutine(RestorePendingSaveNextFrame());
            }
            finally
            {
                isGenerating = false;
            }
        }

        /// <summary>
        /// 按图实例化所有房间预制体, 返回格子到房间实例的映射.
        /// </summary>
        private async Task<Dictionary<Vector2Int, Room>> InstantiateRoomsAsync(RoomGraph graph)
        {
            var loader = AddressableLoader.Instance;
            if (loader == null)
            {
                throw new InvalidOperationException($"{nameof(RandomRoomGenerator)} requires {nameof(AddressableLoader)} before generation.");
            }

            // 预载本关用到的全部房间预制体, 再按房间尺寸计算位置.
            var prefabByAddress = new Dictionary<string, GameObject>();
            foreach (var node in graph.Nodes)
            {
                if (prefabByAddress.ContainsKey(node.PrefabKey)) continue;

                var prefab = await loader.LoadAssetAsync<GameObject>(node.PrefabKey);
                if (prefab == null)
                {
                    throw new InvalidOperationException($"房间预制体加载失败, 地址: {node.PrefabKey}.");
                }

                prefabByAddress[node.PrefabKey] = prefab;
            }

            var positions = ComputeRoomPositions(graph, prefabByAddress);

            var roomInstances = new Dictionary<Vector2Int, Room>();
            foreach (var node in graph.Nodes)
            {
                var prefab = prefabByAddress[node.PrefabKey];
                var worldPosition = positions[node.Cell];
                var instance = Instantiate(prefab, worldPosition, Quaternion.identity, transform);
                var room = instance.GetComponent<Room>();
                if (room == null)
                {
                    throw new InvalidOperationException($"房间预制体缺少 {nameof(Room)} 组件, 地址: {node.PrefabKey}.");
                }

                var neighborDirections = node.Neighbors
                    .Select(neighborCell => neighborCell - node.Cell)
                    .ToList();
                room.InitializeGenerationContext(node.Cell, neighborDirections);
                SealUnusedDoorways(room, neighborDirections);
                roomInstances[node.Cell] = room;
            }

            return roomInstances;
        }

        /// <summary>
        /// 在相邻房间的门锚点之间放走廊, 只连接上下左右邻居, 走廊永远是直线.
        /// </summary>
        private void InstantiateCorridors(RoomGraph graph, Dictionary<Vector2Int, Room> roomInstances)
        {
            foreach (var node in graph.Nodes)
            {
                // 只处理向右和向上的边, 避免同一条边拼两次走廊.
                foreach (var dir in new[] { Vector2Int.right, Vector2Int.up })
                {
                    if (!node.Neighbors.Contains(node.Cell + dir)) continue;

                    var roomA = roomInstances[node.Cell];
                    var roomB = roomInstances[node.Cell + dir];
                    PlaceCorridor(roomA, roomB, dir);
                }
            }
        }

        /// <summary>
        /// 按地板格子边界拼接走廊, 并在两端墙面各打开两格门洞.
        /// </summary>
        private void PlaceCorridor(Room roomA, Room roomB, Vector2Int direction)
        {
            var prefab = direction.x != 0 ? lrCorridorPrefab : udCorridorPrefab;
            if (prefab == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(RandomRoomGenerator)}: 走廊预制体未配置, 方向: {(direction.x != 0 ? "水平" : "垂直")}.");
            }

            var floorA = FindTilemap(roomA.gameObject, floorTilemapName);
            var floorB = FindTilemap(roomB.gameObject, floorTilemapName);
            var boundsA = floorA.cellBounds;
            var boundsB = floorB.cellBounds;
            var horizontal = direction.x != 0;
            var corridorBounds = FindTilemap(prefab, floorTilemapName).cellBounds;
            var start = horizontal
                ? roomA.transform.position.x + boundsA.xMax
                : roomA.transform.position.y + boundsA.yMax;
            var end = horizontal
                ? roomB.transform.position.x + boundsB.xMin
                : roomB.transform.position.y + boundsB.yMin;
            var length = Mathf.RoundToInt(end - start);
            if (length < 5 || Mathf.Abs(end - start - length) > 0.01f)
            {
                throw new InvalidOperationException($"房间 {roomA.name} 与 {roomB.name} 的走廊长度无效: {end - start}.");
            }

            // 不同尺寸房间靠中心对齐, 走廊重复图块延长, 避免缩放墙体和地板.
            var position = horizontal
                ? new Vector3(start - corridorBounds.xMin,
                    roomA.transform.position.y + boundsA.center.y - corridorBounds.center.y, 0f)
                : new Vector3(roomA.transform.position.x + boundsA.center.x - corridorBounds.center.x,
                    start - corridorBounds.yMin, 0f);
            var corridor = Instantiate(prefab, position, Quaternion.identity, transform);
            ResizeCorridor(corridor, horizontal, length);
            OpenDoorway(roomA, direction);
            OpenDoorway(roomB, -direction);
        }

        private void ResizeCorridor(GameObject corridor, bool horizontal, int length)
        {
            var floor = FindTilemap(corridor, floorTilemapName);
            var walls = FindTilemap(corridor, "Walls");
            var floorTile = floor.GetTile(horizontal ? new Vector3Int(0, 0, 0) : new Vector3Int(-2, 0, 0));
            var wallA = walls.GetTile(horizontal ? new Vector3Int(0, 1, 0) : new Vector3Int(-3, 0, 0));
            var wallB = walls.GetTile(horizontal ? new Vector3Int(0, -2, 0) : new Vector3Int(0, 0, 0));
            if (floorTile == null || wallA == null || wallB == null)
            {
                throw new InvalidOperationException($"走廊 {corridor.name} 缺少模板图块.");
            }

            floor.ClearAllTiles();
            walls.ClearAllTiles();
            for (var along = 0; along < length; along++)
            for (var across = 0; across < 4; across++)
            {
                var cell = horizontal
                    ? new Vector3Int(along, across - 2, 0)
                    : new Vector3Int(across - 3, along, 0);
                floor.SetTile(cell, floorTile);
                if (across == 0) walls.SetTile(cell, wallA);
                else if (across == 3) walls.SetTile(cell, wallB);
            }
            floor.CompressBounds();
            walls.CompressBounds();
        }

        private static void OpenDoorway(Room room, Vector2Int direction)
        {
            var walls = FindTilemap(room.gameObject, "Walls");
            var bounds = FindTilemap(room.gameObject, "Floor").cellBounds;
            var horizontal = direction.x != 0;
            var edge = horizontal
                ? (direction.x > 0 ? bounds.xMax - 1 : bounds.xMin)
                : (direction.y > 0 ? bounds.yMax - 1 : bounds.yMin);
            var middle = horizontal
                ? Mathf.FloorToInt((bounds.yMin + bounds.yMax - 1) * 0.5f)
                : Mathf.FloorToInt((bounds.xMin + bounds.xMax - 1) * 0.5f);
            for (var i = 0; i < 2; i++)
            {
                var cell = horizontal
                    ? new Vector3Int(edge, middle + i, 0)
                    : new Vector3Int(middle + i, edge, 0);
                walls.SetTile(cell, null);
            }
        }

        /// <summary>
        /// 未连接方向恢复两格墙体, 即使相邻格有房间也不能误穿到非生成树边.
        /// </summary>
        private static void SealUnusedDoorways(Room room, IReadOnlyCollection<Vector2Int> connectedDirections)
        {
            var walls = FindTilemap(room.gameObject, "Walls");
            var bounds = FindTilemap(room.gameObject, "Floor").cellBounds;
            foreach (var direction in new[] { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left })
            {
                if (connectedDirections.Contains(direction)) continue;
                var horizontal = direction.x != 0;
                var edge = horizontal
                    ? (direction.x > 0 ? bounds.xMax - 1 : bounds.xMin)
                    : (direction.y > 0 ? bounds.yMax - 1 : bounds.yMin);
                var middle = horizontal
                    ? Mathf.FloorToInt((bounds.yMin + bounds.yMax - 1) * 0.5f)
                    : Mathf.FloorToInt((bounds.xMin + bounds.xMax - 1) * 0.5f);
                var wallSample = horizontal
                    ? new Vector3Int(edge, middle - 1, 0)
                    : new Vector3Int(middle - 1, edge, 0);
                var tile = walls.GetTile(wallSample);
                if (tile == null)
                    throw new InvalidOperationException($"房间 {room.name} 的 {direction} 墙边缺少封门用图块.");
                for (var i = 0; i < 2; i++)
                    walls.SetTile(horizontal ? new Vector3Int(edge, middle + i, 0) : new Vector3Int(middle + i, edge, 0), tile);
            }
        }

        private static Tilemap FindTilemap(GameObject root, string tilemapName)
        {
            var tilemap = root.GetComponentsInChildren<Tilemap>(true)
                .FirstOrDefault(candidate => candidate.name == tilemapName);
            if (tilemap == null)
            {
                throw new InvalidOperationException($"{root.name} 缺少 {tilemapName} Tilemap.");
            }
            return tilemap;
        }

        /// <summary>
        /// 每列取最大房宽, 每行取最大房高, 让不同尺寸房间保持中心对齐.
        /// </summary>
        private Dictionary<Vector2Int, Vector3> ComputeRoomPositions(
            RoomGraph graph, Dictionary<string, GameObject> prefabByAddress)
        {
            if (lrCorridorPrefab == null || udCorridorPrefab == null)
            {
                throw new InvalidOperationException($"{nameof(RandomRoomGenerator)}: 必须配置水平与垂直走廊预制体.");
            }

            const int gap = 5;
            var widths = new Dictionary<int, int>();
            var heights = new Dictionary<int, int>();
            foreach (var node in graph.Nodes)
            {
                var bounds = FindTilemap(prefabByAddress[node.PrefabKey], floorTilemapName).cellBounds;
                widths[node.Cell.x] = Mathf.Max(widths.TryGetValue(node.Cell.x, out var width) ? width : 0, bounds.size.x);
                heights[node.Cell.y] = Mathf.Max(heights.TryGetValue(node.Cell.y, out var height) ? height : 0, bounds.size.y);
            }

            var xCenters = BuildAxisCenters(widths, gap);
            var yCenters = BuildAxisCenters(heights, gap);
            var positions = new Dictionary<Vector2Int, Vector3>();
            foreach (var node in graph.Nodes)
            {
                var bounds = FindTilemap(prefabByAddress[node.PrefabKey], floorTilemapName).cellBounds;
                positions[node.Cell] = new Vector3(xCenters[node.Cell.x] - bounds.center.x,
                    yCenters[node.Cell.y] - bounds.center.y, 0f);
            }
            return positions;
        }

        private static Dictionary<int, float> BuildAxisCenters(Dictionary<int, int> sizes, int gap)
        {
            var centers = new Dictionary<int, float> { [0] = 0f };
            for (var index = 1; index <= sizes.Keys.Max(); index++)
            {
                centers[index] = centers[index - 1] + (sizes[index - 1] + sizes[index]) * 0.5f + gap;
            }
            for (var index = -1; index >= sizes.Keys.Min(); index--)
            {
                centers[index] = centers[index + 1] - (sizes[index + 1] + sizes[index]) * 0.5f - gap;
            }
            return centers;
        }

        /// <summary>
        /// 房间实例的 Start 会在生成后一帧执行, 等门和房间初始化完成后再恢复存档.
        /// </summary>
        private IEnumerator RestorePendingSaveNextFrame()
        {
            yield return null;
            var restoreTask = SaveGameService.TryRestorePendingSaveAsync();
            while (!restoreTask.IsCompleted)
            {
                yield return null;
            }

            if (restoreTask.IsFaulted)
            {
                throw restoreTask.Exception;
            }
        }
    }
}
