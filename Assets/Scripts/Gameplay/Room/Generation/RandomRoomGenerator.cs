using System;
using System.Collections;
using System.Collections.Generic;
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
    /// 在相邻门锚点之间拼走廊, 并填充小地图高亮数据.
    /// 不再调用 Edgar 的关卡图布局.
    /// </summary>
    public sealed class RandomRoomGenerator : MonoBehaviour
    {
        [Header("关卡配置")]
        [SerializeField] private string levelId = "level1";
        [SerializeField] private bool generateOnStart = true;

        [Header("走廊预制体")]
        [SerializeField] private GameObject lrCorridorPrefab;
        [SerializeField] private GameObject udCorridorPrefab;

        [Header("小地图")]
        [SerializeField] private int minimapLayer = 0;
        [SerializeField] private string floorTilemapName = "Floor";

        public static RandomRoomGenerator Active { get; private set; }

        private bool generated;
        private bool isGenerating;
        private int lastGeneratedSeed;
        private string forcedLevelId;
        private int? forcedSeed;

        public string LevelId => string.IsNullOrWhiteSpace(forcedLevelId) ? levelId : forcedLevelId;
        public int LastGeneratedSeed => lastGeneratedSeed;

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
                FillMinimapData(roomInstances);

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

            // 预载本关用到的全部房间预制体, 并统计最大外框算步长.
            var prefabByAddress = new Dictionary<string, GameObject>();
            var roomPrefabs = new List<GameObject>();
            foreach (var node in graph.Nodes)
            {
                if (prefabByAddress.ContainsKey(node.PrefabAddress)) continue;

                var prefab = await loader.LoadAssetAsync<GameObject>(node.PrefabAddress);
                if (prefab == null)
                {
                    throw new InvalidOperationException($"房间预制体加载失败, 地址: {node.PrefabAddress}.");
                }

                prefabByAddress[node.PrefabAddress] = prefab;
                roomPrefabs.Add(prefab);
            }

            var (horizontalStep, verticalStep) = ComputeSteps(roomPrefabs);

            var roomInstances = new Dictionary<Vector2Int, Room>();
            foreach (var node in graph.Nodes)
            {
                var prefab = prefabByAddress[node.PrefabAddress];
                var worldPosition = new Vector3(node.Cell.x * horizontalStep, node.Cell.y * verticalStep, 0f);
                var instance = Instantiate(prefab, worldPosition, Quaternion.identity, transform);
                var room = instance.GetComponent<Room>();
                if (room == null)
                {
                    throw new InvalidOperationException($"房间预制体缺少 {nameof(Room)} 组件, 地址: {node.PrefabAddress}.");
                }

                var neighborDirections = node.Neighbors
                    .Select(neighborCell => neighborCell - node.Cell)
                    .ToList();
                room.InitializeGenerationContext(node.Cell, neighborDirections);
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
        /// 把走廊放在两个门锚点的中点.
        /// </summary>
        private void PlaceCorridor(Room roomA, Room roomB, Vector2Int direction)
        {
            var prefab = direction.x != 0 ? lrCorridorPrefab : udCorridorPrefab;
            if (prefab == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(RandomRoomGenerator)}: 走廊预制体未配置, 方向: {(direction.x != 0 ? "水平" : "垂直")}.");
            }

            var anchorA = roomA.GetDoorAnchor(direction);
            if (anchorA == null)
            {
                throw new InvalidOperationException($"房间 {roomA.name} 缺少门锚点 {Room.AnchorName(direction)}, 无法拼走廊.");
            }

            var anchorB = roomB.GetDoorAnchor(-direction);
            if (anchorB == null)
            {
                throw new InvalidOperationException($"房间 {roomB.name} 缺少门锚点 {Room.AnchorName(-direction)}, 无法拼走廊.");
            }

            var center = (anchorA.position + anchorB.position) * 0.5f;
            Instantiate(prefab, center, Quaternion.identity, transform);
        }

        /// <summary>
        /// 用每个房间 Floor Tilemap 的格子填充 MinimapRoomData, 高亮层挂在生成器下.
        /// </summary>
        private void FillMinimapData(Dictionary<Vector2Int, Room> roomInstances)
        {
            var highlightObject = new GameObject("Minimap Highlight");
            highlightObject.transform.SetParent(transform);
            highlightObject.transform.localPosition = Vector3.zero;
            highlightObject.layer = minimapLayer;

            var highlightTilemap = highlightObject.AddComponent<Tilemap>();
            var renderer = highlightObject.AddComponent<TilemapRenderer>();
            // 高亮层排在普通小地图之上.
            renderer.sortingOrder = 30;

            foreach (var pair in roomInstances)
            {
                var room = pair.Value;
                var floorTilemap = room.GetComponentsInChildren<Tilemap>()
                    .FirstOrDefault(tilemap => tilemap.name == floorTilemapName);
                if (floorTilemap == null)
                {
                    throw new InvalidOperationException($"房间 {room.name} 缺少名为 {floorTilemapName} 的 Tilemap, 无法填充小地图数据.");
                }

                var data = room.GetComponent<MinimapRoomData>();
                if (data == null)
                {
                    throw new InvalidOperationException($"房间 {room.name} 缺少 {nameof(MinimapRoomData)} 组件.");
                }

                var positions = new List<Vector3Int>();
                foreach (var localPos in floorTilemap.cellBounds.allPositionsWithin)
                {
                    if (!floorTilemap.HasTile(localPos)) continue;

                    // 房间实例的世界坐标不同, 统一换算到高亮层的格子.
                    positions.Add(highlightTilemap.WorldToCell(floorTilemap.GetCellCenterWorld(localPos)));
                }

                data.Positions = positions;
                data.HighlightTilemap = highlightTilemap;
            }
        }

        /// <summary>
        /// 计算格子步长: 房间最大外框加上走廊长度.
        /// </summary>
        private Vector2 ComputeSteps(List<GameObject> roomPrefabs)
        {
            if (lrCorridorPrefab == null || udCorridorPrefab == null)
            {
                throw new InvalidOperationException($"{nameof(RandomRoomGenerator)}: 必须配置水平与垂直走廊预制体.");
            }

            var maxRoomWidth = 0f;
            var maxRoomHeight = 0f;
            foreach (var prefab in roomPrefabs)
            {
                var size = MeasureFootprint(prefab);
                maxRoomWidth = Mathf.Max(maxRoomWidth, size.x);
                maxRoomHeight = Mathf.Max(maxRoomHeight, size.y);
            }

            var corridorSize = MeasureFootprint(lrCorridorPrefab);
            var horizontalStep = maxRoomWidth + corridorSize.x;
            corridorSize = MeasureFootprint(udCorridorPrefab);
            var verticalStep = maxRoomHeight + corridorSize.y;

            return new Vector2(horizontalStep, verticalStep);
        }

        /// <summary>
        /// 用根节点 BoxCollider2D 的尺寸表示外框.
        /// </summary>
        private static Vector2 MeasureFootprint(GameObject prefab)
        {
            var collider = prefab.GetComponent<BoxCollider2D>();
            if (collider == null)
            {
                collider = prefab.GetComponentInChildren<BoxCollider2D>();
            }

            if (collider == null)
            {
                throw new InvalidOperationException($"预制体 {prefab.name} 缺少 BoxCollider2D, 无法测量外框.");
            }

            return collider.size;
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