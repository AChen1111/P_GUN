using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemSpawnEntry
{
    public GameObject prefab;

    [Min(0)]
    public int weight;
}

/// <summary>
/// 物品生成表：只负责“能生成什么”和权重，物品展示数据继续放在 ItemDatabase。
/// </summary>
[CreateAssetMenu(fileName = "ItemSpawnTable", menuName = "PG/Item/Item Spawn Table", order = 1)]
public class ItemSpawnTableSO : ScriptableObject
{
    [SerializeField] private List<ItemSpawnEntry> entries = new List<ItemSpawnEntry>();

    public IReadOnlyList<ItemSpawnEntry> Entries => entries;

    /// <summary>
    /// 按权重获取一个随机物品预制体。
    /// </summary>
    public bool TryGetRandomPrefab(out GameObject prefab)
    {
        prefab = null;

        var totalWeight = 0;
        foreach (var entry in entries)
        {
            if (entry.prefab == null || entry.weight <= 0) continue;
            totalWeight += entry.weight;
        }

        if (totalWeight <= 0) return false;

        var roll = UnityEngine.Random.Range(0, totalWeight);
        foreach (var entry in entries)
        {
            if (entry.prefab == null || entry.weight <= 0) continue;

            if (roll < entry.weight)
            {
                prefab = entry.prefab;
                return true;
            }

            roll -= entry.weight;
        }

        return false;
    }
}
