using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    [CreateAssetMenu(fileName = "WeaponDatabase", menuName = "PG/Weapon/Weapon Database", order = 0)]
    public class WeaponDatabase : ScriptableObjectDatabase<WeaponDatabase, string, WeaponData>
    {
        [SerializeField] private List<WeaponData> weapons = new List<WeaponData>();

        public IReadOnlyList<WeaponData> Weapons => weapons;

        /// <summary>
        /// 执行 ReplaceWeapons 逻辑.
        /// </summary>
        public void ReplaceWeapons(IEnumerable<WeaponData> newWeapons)
        {
            ReplaceData(weapons, newWeapons);
        }

        protected override List<WeaponData> DataValues => weapons;

        protected override IEqualityComparer<string> KeyComparer => StringComparer.OrdinalIgnoreCase;

        /// <summary>
        /// 执行 TryGetKey 逻辑.
        /// </summary>
        protected override bool TryGetKey(WeaponData data, out string key)
        {
            key = data.weaponId;
            return !string.IsNullOrEmpty(key);
        }
    }
}
