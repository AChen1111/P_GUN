using System.Collections.Generic;
using UnityEngine;
using Game.Core;

namespace Game.Gameplay
{
    [CreateAssetMenu(fileName = "EnemyDatabase", menuName = "PG/Enemy/Enemy Database", order = 0)]
    public class EnemyDatabase : ScriptableObjectDatabase<int, EnemyData>
    {
        [SerializeField] private List<EnemyData> enemies = new List<EnemyData>();

        public IReadOnlyList<EnemyData> Enemies => enemies;
        public void ReplaceEnemies(IEnumerable<EnemyData> newEnemies)
        {
            ReplaceData(enemies, newEnemies);
        }

        protected override List<EnemyData> DataValues => enemies;
        protected override bool TryGetKey(EnemyData data, out int key)
        {
            key = data.enemyId;
            return data.prefab != null;
        }
    }
}
