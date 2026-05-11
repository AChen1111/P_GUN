using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDatabase", menuName = "PG/Weapon/Weapon Database", order = 0)]
public class WeaponDatabase : ScriptableObject
{
    public const string DefaultResourcesPath = "WeaponDatabase";

    [SerializeField] private List<WeaponData> weapons = new List<WeaponData>();

    private static WeaponDatabase cachedDefault;
    private readonly Dictionary<string, WeaponData> weaponMap = new Dictionary<string, WeaponData>(StringComparer.OrdinalIgnoreCase);
    private int indexedWeaponCount = -1;

    public IReadOnlyList<WeaponData> Weapons => weapons;

    public static WeaponDatabase Default
    {
        get
        {
            if (cachedDefault == null)
            {
                cachedDefault = Resources.Load<WeaponDatabase>(DefaultResourcesPath);
            }

            return cachedDefault;
        }
    }

    public static void SetDefault(WeaponDatabase database)
    {
        cachedDefault = database;
    }

    private void OnEnable()
    {
        RebuildIndex();

        if (cachedDefault == null)
        {
            cachedDefault = this;
        }
    }

    private void OnValidate()
    {
        RebuildIndex();
    }

    public bool TryGetById(string id, out WeaponData data)
    {
        EnsureIndex();
        id = NormalizeId(id);
        return weaponMap.TryGetValue(id, out data);
    }

    public WeaponData GetById(string id)
    {
        if (TryGetById(id, out var data))
        {
            return data;
        }

        Debug.LogWarning($"{nameof(WeaponDatabase)}: 未找到 weaponId={id} 的武器数据。");
        return WeaponData.CreateFallback(id);
    }

    public void ReplaceWeapons(IEnumerable<WeaponData> newWeapons)
    {
        weapons.Clear();

        if (newWeapons != null)
        {
            weapons.AddRange(newWeapons);
        }

        RebuildIndex();
    }

    private void EnsureIndex()
    {
        if (indexedWeaponCount != weapons.Count)
        {
            RebuildIndex();
        }
    }

    private void RebuildIndex()
    {
        weaponMap.Clear();
        indexedWeaponCount = weapons.Count;

        foreach (var weapon in weapons)
        {
            var id = NormalizeId(weapon.weaponId);
            if (string.IsNullOrEmpty(id)) continue;

            weaponMap[id] = weapon;
        }
    }

    private static string NormalizeId(string id)
    {
        return string.IsNullOrWhiteSpace(id) ? string.Empty : id.Trim();
    }
}
