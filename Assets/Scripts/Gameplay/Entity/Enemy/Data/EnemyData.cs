using System;
using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    [Serializable]
    public struct EnemyData
    {
        public int enemyId;
        public string displayName;
        public EnemyBase prefab;

        [Min(1)]
        public int maxHp;

        [Min(0f)]
        public float moveSpeed;

        [Min(0)]
        public int damage;

        /// <summary>
        /// 死亡后掉落物品的概率, 由 EnemyBase 在生成时读取.
        /// </summary>
        [Range(0f, 1f)]
        public float itemDropChance;

        public string DisplayName => string.IsNullOrWhiteSpace(displayName) ? enemyId.ToString() : displayName;
    }
}
