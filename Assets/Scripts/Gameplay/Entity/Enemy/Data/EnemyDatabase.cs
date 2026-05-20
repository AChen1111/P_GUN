using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    [CreateAssetMenu(fileName = "EnemyDatabase", menuName = "PG/Enemy/Enemy Database", order = 0)]
    public class EnemyDatabase : ScriptableObjectDatabase<EnemyDatabase, int, EnemyData>
    {
        [SerializeField] private List<EnemyData> enemies = new List<EnemyData>();

        public IReadOnlyList<EnemyData> Enemies => enemies;

        /// <summary>
        /// 执行 ReplaceEnemies 逻辑.
        /// </summary>
        public void ReplaceEnemies(IEnumerable<EnemyData> newEnemies)
        {
            ReplaceData(enemies, newEnemies);
        }

        protected override List<EnemyData> DataValues => enemies;

        /// <summary>
        /// 执行 TryGetKey 逻辑.
        /// </summary>
        protected override bool TryGetKey(EnemyData data, out int key)
        {
            key = data.enemyId;
            return data.prefab != null;
        }
    }
}
