using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    public class NormalRoom : FightRoom
    {
        [Header("敌人数据库")]
        [SerializeField] private EnemyDatabase enemyDatabase;
        [SerializeField] private int enemyId = 1;

        [Header("敌人生成表")]
        [SerializeField] private EnemySpawnTableSO enemySpawnTable;

        [Header("旧版敌人预制体")]
        [FormerlySerializedAs("enemyPrefab")]
        [SerializeField] private GameObject fallbackEnemyPrefab;

        [Header("敌人可能出现的位置坐标点")]
        [SerializeField] private List<Transform> enemyPoints = new List<Transform>();

        [Header("每波敌人数量范围")]
        [SerializeField] private Vector2Int enemyCountRange = new Vector2Int(1, 3);

        protected override int GetInitialWaveCount()
        {
            if (enemySpawnTable != null && enemySpawnTable.WaveCount > 0)
            {
                return enemySpawnTable.WaveCount;
            }

            return base.GetInitialWaveCount();
        }

        /// <summary>
        /// 生成波次敌人
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

            if (enemySpawnTable != null && enemySpawnTable.TryGetWave(CurrentWaveIndex, out var wave))
            {
                return SpawnFromWave(wave, validPoints);
            }

            var enemyData = ResolveEnemyData(enemyId);
            var enemyPrefab = ResolveEnemyPrefab(enemyData);
            if (enemyPrefab == null) return 0;

            //随机生成敌人数量
            var spawnCount = Random.Range(enemyCountRange.x, enemyCountRange.y + 1);
            spawnCount = Mathf.Clamp(spawnCount, 0, validPoints.Count);

            // 从对象池获取敌人；EnemyPool 内部会按 prefab 分池，避免不同敌人类型混用
            var actualSpawnCount = 0;
            for (int i = 0; i < spawnCount; i++)
            {
                if (SpawnEnemy(enemyPrefab, enemyData, validPoints[i].position))
                {
                    actualSpawnCount++;
                }
            }

            return actualSpawnCount;
        }

        private int SpawnFromWave(EnemySpawnWave wave, List<Transform> validPoints)
        {
            if (wave == null || wave.enemies == null) return 0;

            var pointIndex = 0;
            var actualSpawnCount = 0;

            foreach (var entry in wave.enemies)
            {
                if (entry.count <= 0) continue;

                var enemyData = ResolveEnemyData(entry.enemyId);
                var enemyPrefab = ResolveEnemyPrefab(enemyData);
                if (enemyPrefab == null) continue;

                for (var i = 0; i < entry.count && pointIndex < validPoints.Count; i++, pointIndex++)
                {
                    if (SpawnEnemy(enemyPrefab, enemyData, validPoints[pointIndex].position))
                    {
                        actualSpawnCount++;
                    }
                }
            }

            return actualSpawnCount;
        }

        private bool SpawnEnemy(EnemyBase enemyPrefab, EnemyData? enemyData, Vector3 spawnPosition)
        {
            var enemy = EnemyPool.Instance.Get(enemyPrefab, spawnPosition, Quaternion.identity, this);
            if (enemy == null) return false;

            // 敌人基础属性由 EnemyDatabase 控制, prefab 只负责外观和行为组件.
            if (enemyData.HasValue)
            {
                enemy.ApplyConfig(enemyData.Value);
            }

            RegisterSpawnedEnemy(enemy);
            return true;
        }

        private EnemyData? ResolveEnemyData(int targetEnemyId)
        {
            var database = enemyDatabase != null ? enemyDatabase : DataBaseManager.Instance?.Enemies;
            if (database != null && database.TryGetById(targetEnemyId, out var enemyData))
            {
                return enemyData;
            }

            Debug.LogWarning($"{nameof(NormalRoom)}: 找不到 enemyId={targetEnemyId} 对应的敌人配置.", this);
            return null;
        }

        private EnemyBase ResolveEnemyPrefab(EnemyData? enemyData)
        {
            if (enemyData.HasValue && enemyData.Value.prefab != null)
            {
                return enemyData.Value.prefab;
            }

            if (fallbackEnemyPrefab != null)
            {
                // 兼容旧房间配置, 避免未配置 EnemyDatabase 时直接无法生成敌人.
                return fallbackEnemyPrefab.GetComponent<EnemyBase>();
            }

            return null;
        }

        protected override void OnFightAllWavesEnd()
        {
            if(canGenerateItems) {
                itemSpawner.SpawnItem(transform.position,"Jump");
            }
        }
    }
}
