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
    }
}
