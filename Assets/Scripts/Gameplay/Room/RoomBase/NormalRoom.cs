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

        [Header("旧版敌人预制体")]
        [FormerlySerializedAs("enemyPrefab")]
        [SerializeField] private GameObject fallbackEnemyPrefab;

        [Header("敌人可能出现的位置坐标点")]
        [SerializeField] private List<Transform> enemyPoints = new List<Transform>();

        [Header("每波敌人数量范围")]
        [SerializeField] private Vector2Int enemyCountRange = new Vector2Int(1, 3);

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

            var enemyPrefab = ResolveEnemyPrefab();
            if (enemyPrefab == null || validPoints.Count == 0)
            {
                return 0;
            }

            //随机生成敌人数量
            var spawnCount = Random.Range(enemyCountRange.x, enemyCountRange.y + 1);
            spawnCount = Mathf.Clamp(spawnCount, 0, validPoints.Count);

            // 从对象池获取敌人；EnemyPool 内部会按 prefab 分池，避免不同敌人类型混用
            var actualSpawnCount = 0;
            for (int i = 0; i < spawnCount; i++)
            {
                var enemy = EnemyPool.Instance.Get(enemyPrefab, validPoints[i].position, Quaternion.identity, this);
                if (enemy == null) continue;

                RegisterSpawnedEnemy(enemy);
                actualSpawnCount++;
            }

            return actualSpawnCount;
        }

        private EnemyBase ResolveEnemyPrefab()
        {
            var database = enemyDatabase != null ? enemyDatabase : DataBaseManager.Instance?.Enemies;
            if (database != null && database.TryGetById(enemyId, out var enemyData))
            {
                return enemyData.prefab;
            }

            if (fallbackEnemyPrefab != null)
            {
                // 兼容旧房间配置, 避免未配置 EnemyDatabase 时直接无法生成敌人.
                return fallbackEnemyPrefab.GetComponent<EnemyBase>();
            }

            Debug.LogWarning($"{nameof(NormalRoom)}: 找不到 enemyId={enemyId} 对应的敌人配置.", this);
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
