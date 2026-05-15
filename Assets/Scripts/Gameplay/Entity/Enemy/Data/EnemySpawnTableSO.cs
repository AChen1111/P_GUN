using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public struct EnemySpawnEntry
    {
        public int enemyId;

        [Min(0)]
        public int count;
    }

    [Serializable]
    public class EnemySpawnWave
    {
        public string displayName;
        public List<EnemySpawnEntry> enemies = new List<EnemySpawnEntry>();

        public string DisplayName => string.IsNullOrWhiteSpace(displayName) ? "Wave" : displayName;
    }

    /// <summary>
    /// 敌人生成表, 只保存波次和敌人数量, 敌人属性统一从 EnemyDatabase 读取.
    /// </summary>
    [CreateAssetMenu(fileName = "EnemySpawnTable", menuName = "PG/Enemy/Enemy Spawn Table", order = 1)]
    public class EnemySpawnTableSO : ScriptableObject
    {
        [SerializeField] private EnemyDatabase enemyDatabase;
        [SerializeField] private List<EnemySpawnWave> waves = new List<EnemySpawnWave>();

        public IReadOnlyList<EnemySpawnWave> Waves => waves;

        public int WaveCount => waves == null ? 0 : waves.Count;

        public bool TryGetWave(int waveIndex, out EnemySpawnWave wave)
        {
            wave = null;
            if (waves == null || waveIndex < 0 || waveIndex >= waves.Count) return false;

            wave = waves[waveIndex];
            return wave != null;
        }

        /// <summary>
        /// 通过生成表解析敌人配置, 房间只需要依赖生成表.
        /// </summary>
        /// <param name="enemyId">敌人配置 ID.</param>
        /// <param name="enemyData">解析到的敌人配置.</param>
        /// <returns>是否找到可用配置.</returns>
        public bool TryGetEnemyData(int enemyId, out EnemyData enemyData)
        {
            var database = enemyDatabase != null ? enemyDatabase : DataBaseManager.Instance?.Enemies;
            if(database != null && database.TryGetById(enemyId, out enemyData))
            {
                return true;
            }

            enemyData = default;
            return false;
        }
    }
}
