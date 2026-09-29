using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    /// <summary>
    /// 普通战斗房, 波次和敌人数量来自 SpawnData.lua, 敌人预制体按 EnemyData 的地址预载.
    /// </summary>
    public class NormalRoom : FightRoom
    {
        [Header("敌人生成表 id, 对应 SpawnData.lua")]
        [SerializeField] private string spawnTableId = "normal_default";

        [Header("敌人可能出现的位置坐标点")]
        [SerializeField] private List<Transform> enemyPoints = new List<Transform>();
        [Header("单格白色墙体 Tile")]
        [SerializeField] private TileBase coverTile;


        private SpawnTableConfig spawnTable;
        private readonly Dictionary<int, EnemyBase> enemyPrefabsById = new Dictionary<int, EnemyBase>();

        /// <summary>
        /// 按地图种子在战斗房内放置墙体障碍, 每次放置后校验全部可走格仍连通.
        /// </summary>
        public void GenerateObstacles(int mapSeed, Vector2Int roomCell)
        {
            var floor = GetComponentsInChildren<Tilemap>().First(tilemap => tilemap.name == "Floor");
            var walls = GetComponentsInChildren<Tilemap>().First(tilemap => tilemap.name == "Walls");
            var bounds = floor.cellBounds;
            if (coverTile == null)
            {
                throw new InvalidOperationException($"{name} 未绑定白色墙体 Tile.");
            }

            var seed = unchecked(mapSeed * 397 ^ roomCell.x * 73856093 ^ roomCell.y * 19349663);
            var rng = new System.Random(seed);
            // 单格白墙连接成朝向随机的 L 形和宽口 U 形掩体.
            var targetCells = Mathf.RoundToInt((bounds.size.x - 6) * (bounds.size.y - 6) * 0.15f);
            var placedCells = 0;
            for (var attempt = 0; attempt < 1200 && placedCells < targetCells; attempt++)
            {
                var origin = new Vector3Int(rng.Next(bounds.xMin + 3, bounds.xMax - 3),
                    rng.Next(bounds.yMin + 3, bounds.yMax - 3), 0);
                var shape = BuildWallShape(rng, origin, bounds);
                if (shape.Count == 0) continue;
                if (shape.Any(cell => !floor.HasTile(cell) || IsSpawnClearanceCell(floor, cell) || HasNearbyWall(walls, cell))) continue;
                foreach (var cell in shape) walls.SetTile(cell, coverTile);
                if (!AllWalkableCellsConnected(floor, walls) || !AllWideAreasConnected(floor, walls))
                {
                    foreach (var cell in shape) walls.SetTile(cell, null);
                    continue;
                }
                placedCells += shape.Count;
            }
            walls.RefreshAllTiles();
        }

        private static HashSet<Vector3Int> BuildWallShape(System.Random rng, Vector3Int origin, BoundsInt bounds)
        {
            var offsets = new List<Vector2Int>();
            if (rng.Next(2) == 0)
            {
                var width = rng.Next(4, 7);
                var height = rng.Next(3, 6);
                for (var x = 0; x < width; x++) offsets.Add(new Vector2Int(x, 0));
                for (var y = 1; y < height; y++) offsets.Add(new Vector2Int(0, y));
            }
            else
            {
                // U 形横边延长到 10-13 格, 四向旋转后仍留出内侧通路.
                var width = rng.Next(10, 14);
                var height = rng.Next(5, 8);
                for (var x = 0; x < width; x++) offsets.Add(new Vector2Int(x, 0));
                for (var y = 1; y < height; y++)
                {
                    offsets.Add(new Vector2Int(0, y));
                    offsets.Add(new Vector2Int(width - 1, y));
                }
            }

            var rotation = rng.Next(4);
            var cells = new HashSet<Vector3Int>();
            foreach (var offset in offsets)
            {
                var rotated = offset;
                for (var i = 0; i < rotation; i++) rotated = new Vector2Int(-rotated.y, rotated.x);
                var cell = origin + new Vector3Int(rotated.x, rotated.y, 0);
                if (cell.x < bounds.xMin + 3 || cell.x >= bounds.xMax - 3 ||
                    cell.y < bounds.yMin + 3 || cell.y >= bounds.yMax - 3) return new HashSet<Vector3Int>();
                cells.Add(cell);
            }
            return cells;
        }

        private static bool HasNearbyWall(Tilemap walls, Vector3Int cell)
        {
            for (var x = -2; x <= 2; x++)
            for (var y = -2; y <= 2; y++)
            {
                if (walls.HasTile(cell + new Vector3Int(x, y, 0))) return true;
            }
            return false;
        }

        private static bool AllWideAreasConnected(Tilemap floor, Tilemap walls)
        {
            // 用连续 2x2 空格代表玩家可舒适转身的区域, 排除只靠单格细缝连通的布局.
            var wide = new HashSet<Vector3Int>();
            foreach (var cell in floor.cellBounds.allPositionsWithin)
            {
                if (IsFree(cell) && IsFree(cell + Vector3Int.right) &&
                    IsFree(cell + Vector3Int.up) && IsFree(cell + Vector3Int.right + Vector3Int.up)) wide.Add(cell);
            }
            if (wide.Count == 0) return false;
            var reached = new HashSet<Vector3Int>();
            var queue = new Queue<Vector3Int>();
            var first = wide.First();
            reached.Add(first);
            queue.Enqueue(first);
            var directions = new[] { Vector3Int.up, Vector3Int.right, Vector3Int.down, Vector3Int.left };
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                foreach (var direction in directions)
                {
                    var next = current + direction;
                    if (wide.Contains(next) && reached.Add(next)) queue.Enqueue(next);
                }
            }
            return reached.Count == wide.Count;

            bool IsFree(Vector3Int cell) => floor.HasTile(cell) && !walls.HasTile(cell);
        }

        private bool IsSpawnClearanceCell(Tilemap floor, Vector3Int cell)
        {
            foreach (var point in enemyPoints)
            {
                if (point == null) continue;
                var spawnCell = floor.WorldToCell(point.position);
                if (Mathf.Abs(cell.x - spawnCell.x) <= 1 && Mathf.Abs(cell.y - spawnCell.y) <= 1)
                    return true;
            }
            var center = floor.WorldToCell(GetRoomCenterPoint());
            return Mathf.Abs(cell.x - center.x) <= 1 && Mathf.Abs(cell.y - center.y) <= 1;
        }

        private static bool AllWalkableCellsConnected(Tilemap floor, Tilemap walls)
        {
            var walkable = new HashSet<Vector3Int>();
            foreach (var cell in floor.cellBounds.allPositionsWithin)
            {
                if (floor.HasTile(cell) && !walls.HasTile(cell)) walkable.Add(cell);
            }
            if (walkable.Count == 0) return false;

            var reached = new HashSet<Vector3Int>();
            var queue = new Queue<Vector3Int>();
            var first = walkable.First();
            queue.Enqueue(first);
            reached.Add(first);
            var directions = new[] { Vector3Int.up, Vector3Int.right, Vector3Int.down, Vector3Int.left };
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                foreach (var direction in directions)
                {
                    var next = current + direction;
                    if (walkable.Contains(next) && reached.Add(next)) queue.Enqueue(next);
                }
            }
            return reached.Count == walkable.Count;
        }

        protected override int GetInitialWaveCount()
        {
            return spawnTable != null && spawnTable.Waves.Count > 0
                ? spawnTable.Waves.Count
                : base.GetInitialWaveCount();
        }

        protected override void OnRoomInitialized()
        {
            // 生成表必须在基类统计波数之前就绪.
            spawnTable = LuaDataRuntime.GetSpawnTableConfig(spawnTableId);
            PreloadEnemyPrefabsAsync();
            base.OnRoomInitialized();
        }

        /// <summary>
        /// 生成波次敌人.
        /// </summary>
        protected override int SpawnWaveEnemies()
        {
            var validPoints = new List<Transform>();
            foreach (var point in enemyPoints)
            {
                if (point != null) validPoints.Add(point);
            }

            if (validPoints.Count == 0)
            {
                return 0;
            }

            if (spawnTable == null || CurrentWaveIndex >= spawnTable.Waves.Count)
            {
                return 0;
            }

            var wave = spawnTable.Waves[CurrentWaveIndex];
            var pointIndex = 0;
            var actualSpawnCount = 0;

            foreach (var entry in wave)
            {
                if (!enemyPrefabsById.TryGetValue(entry.EnemyId, out var enemyPrefab))
                {
                    Debug.LogWarning($"{nameof(NormalRoom)}: enemyId={entry.EnemyId} 的预制体尚未预载完成.", this);
                    continue;
                }

                var config = LuaDataRuntime.GetEnemyConfig(entry.EnemyId);
                for (var i = 0; i < entry.Count && pointIndex < validPoints.Count; i++, pointIndex++)
                {
                    if (SpawnEnemy(enemyPrefab, config, validPoints[pointIndex].position))
                    {
                        actualSpawnCount++;
                    }
                }
            }

            return actualSpawnCount;
        }

        protected override void OnFightAllWavesEnd()
        {
            // 清房后的额外结算交给房间物体的 LuaComponet 模块.
            var roomLua = GetComponent<LuaBehaviourHost>();
            if (roomLua != null)
            {
                roomLua.CallLuaFunction("OnFightAllWavesEnd");
                return;
            }

            Debug.LogWarning($"{nameof(NormalRoom)}: 房间未挂 LuaComponet, 清房奖励未生成.", this);
        }

        /// <summary>
        /// 生成一个敌人并写入 EnemyData 的配置.
        /// </summary>
        private bool SpawnEnemy(EnemyBase enemyPrefab, EnemyConfig config, Vector3 spawnPosition)
        {
            var pool = EnemyPool.Instance;
            if (pool == null)
            {
                throw new InvalidOperationException($"{nameof(EnemyPool)} must exist in scene before spawning enemies.");
            }

            var enemy = pool.Get(enemyPrefab, spawnPosition, Quaternion.identity, this);
            if (enemy == null)
                return false;

            // 敌人基础属性由 EnemyData.lua 控制, prefab 只负责外观和行为组件.
            enemy.ApplyConfig(config);
            RegisterSpawnedEnemy(enemy);
            return true;
        }

        /// <summary>
        /// 按生成表预载所有敌人预制体.
        /// </summary>
        private async void PreloadEnemyPrefabsAsync()
        {
            try
            {
                var loader = AddressableLoader.Instance;
                if (loader == null)
                {
                    throw new InvalidOperationException($"{nameof(NormalRoom)} requires {nameof(AddressableLoader)} before preloading enemy prefabs.");
                }

                if (spawnTable == null)
                {
                    return;
                }

                for (var waveIndex = 0; waveIndex < spawnTable.Waves.Count; waveIndex++)
                {
                    var wave = spawnTable.Waves[waveIndex];
                    for (var entryIndex = 0; entryIndex < wave.Count; entryIndex++)
                    {
                        var enemyId = wave[entryIndex].EnemyId;
                        if (enemyPrefabsById.ContainsKey(enemyId))
                        {
                            continue;
                        }

                        var config = LuaDataRuntime.GetEnemyConfig(enemyId);
                        var prefabGameObject = await loader.LoadAssetAsync<GameObject>(config.PrefabAddress);
                        var prefab = prefabGameObject != null ? prefabGameObject.GetComponent<EnemyBase>() : null;
                        if (prefab == null)
                        {
                            throw new InvalidOperationException($"敌人预制体缺少 {nameof(EnemyBase)}, 地址: {config.PrefabAddress}.");
                        }

                        enemyPrefabsById[enemyId] = prefab;
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.LogError($"{nameof(NormalRoom)}: 敌人预制体预载失败, Error: {exception.Message}", this);
            }
        }
    }
}
