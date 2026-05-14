using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDatabase", menuName = "PG/Weapon/Weapon Database", order = 0)]
public class WeaponDatabase : ScriptableObjectDatabase<WeaponDatabase, string, WeaponData>
{
    [SerializeField] private List<WeaponData> weapons = new List<WeaponData>();

    public IReadOnlyList<WeaponData> Weapons => weapons;

    public void ReplaceWeapons(IEnumerable<WeaponData> newWeapons)
    {
        ReplaceData(weapons, newWeapons);
    }

    protected override List<WeaponData> DataValues => weapons;

    protected override IEqualityComparer<string> KeyComparer => StringComparer.OrdinalIgnoreCase;

    protected override bool TryGetKey(WeaponData data, out string key)
    {
        key = data.weaponId;
        return !string.IsNullOrEmpty(key);
    }
}
