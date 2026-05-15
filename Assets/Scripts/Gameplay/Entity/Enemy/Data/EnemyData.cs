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

        public string DisplayName => string.IsNullOrWhiteSpace(displayName) ? enemyId.ToString() : displayName;
    }
}
