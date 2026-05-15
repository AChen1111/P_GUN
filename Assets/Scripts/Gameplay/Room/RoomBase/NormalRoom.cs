using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    public class NormalRoom : FightRoom
    {
        [Header("敌人生成表")]
        [SerializeField] private EnemySpawnTableSO enemySpawnTable;

        [Header("敌人可能出现的位置坐标点")]
        [SerializeField] private List<Transform> enemyPoints = new List<Transform>();

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

            if (enemySpawnTable == null || !enemySpawnTable.TryGetWave(CurrentWaveIndex, out var wave)) return 0;

            return SpawnFromWave(wave, validPoints);
        }

        private int SpawnFromWave(EnemySpawnWave wave, List<Transform> validPoints)
        {
            if (wave == null || wave.enemies == null) return 0;

            var pointIndex = 0;
            var actualSpawnCount = 0;

            foreach (var entry in wave.enemies)
            {
                if (entry.count <= 0) continue;

                if (!enemySpawnTable.TryGetEnemyData(entry.enemyId, out var enemyData))
                {
                    Debug.LogWarning($"{nameof(NormalRoom)}: 生成表找不到 enemyId={entry.enemyId} 对应的敌人配置.", this);
                    continue;
                }

                var enemyPrefab = enemyData.prefab;
                if (enemyPrefab == null)
                {
                    Debug.LogWarning($"{nameof(NormalRoom)}: enemyId={entry.enemyId} 的敌人 prefab 未配置.", this);
                    continue;
                }

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

        private bool SpawnEnemy(EnemyBase enemyPrefab, EnemyData enemyData, Vector3 spawnPosition)
        {
            var enemy = EnemyPool.Instance.Get(enemyPrefab, spawnPosition, Quaternion.identity, this);
            if (enemy == null) return false;

            // 敌人基础属性由 EnemyDatabase 控制, prefab 只负责外观和行为组件.
            enemy.ApplyConfig(enemyData);

            RegisterSpawnedEnemy(enemy);
            return true;
        }

        protected override void OnFightAllWavesEnd()
        {
            if(canGenerateItems) {
                itemSpawner.SpawnItem(transform.position,"Jump");
            }
        }
    }
}
