using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
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

        private SpawnTableConfig spawnTable;
        private readonly Dictionary<int, EnemyBase> enemyPrefabsById = new Dictionary<int, EnemyBase>();

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
            var roomLua = GetComponent<LuaComponet>();
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