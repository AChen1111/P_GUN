using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 宝箱类效果：在触发位置随机生成一个道具预制体。
/// </summary>
[CreateAssetMenu(fileName = "ChestRandomLootEffect", menuName = "PG/Item/Effects/Chest Random Loot", order = 2)]
public class ChestRandomLootEffect : ItemEffectBase
{
    [Tooltip("优先使用的物品生成表")]
    [SerializeField] private ItemSpawnTableSO spawnTable;

    [Tooltip("旧配置兼容：未配置生成表时从这里随机抽取")]
    [SerializeField] private List<GameObject> lootTable = new List<GameObject>();

    [SerializeField] private string animEffectKey = "Jump";
    [SerializeField] private Vector2 randomOffsetRange = new Vector2(0.5f, 0.5f);

    public override void OnPick(ItemEffectContext ctx)
    {
        var spawner = ResolveSpawner(ctx);
        if (spawner == null)
        {
            Debug.LogError($"{nameof(ChestRandomLootEffect)}: 找不到可用的 {nameof(ItemSpawner)}。");
            return;
        }

        var newItemPosition = ctx.WorldPosition + new Vector3(
            Random.Range(-randomOffsetRange.x, randomOffsetRange.x),
            Random.Range(-randomOffsetRange.y, randomOffsetRange.y),
            0f);

        var drop = GetRandomPrefab();
        if (drop == null) return;

        spawner.SpawnItem(newItemPosition, animEffectKey);
    }

    private ItemSpawner ResolveSpawner(ItemEffectContext ctx)
    {
        return ctx.SourceObject != null ? ctx.SourceObject.GetComponentInParent<ItemSpawner>() : null;
    }

    private GameObject GetRandomPrefab()
    {
        if (spawnTable != null)
        {
            if (spawnTable.TryGetRandomPrefab(out var prefab))
            {
                return prefab;
            }

            Debug.LogWarning($"{nameof(ChestRandomLootEffect)}: 生成表 {spawnTable.name} 没有可用物品。");
            return null;
        }

        if (lootTable == null || lootTable.Count == 0)
        {
            Debug.LogWarning($"{nameof(ChestRandomLootEffect)}: 没有可用的掉落配置。");
            return null;
        }

        var validPrefabs = new List<GameObject>();
        foreach (var prefab in lootTable)
        {
            if (prefab != null) validPrefabs.Add(prefab);
        }

        if (validPrefabs.Count == 0)
        {
            Debug.LogWarning($"{nameof(ChestRandomLootEffect)}: 掉落配置中没有有效预制体。");
            return null;
        }

        return validPrefabs[Random.Range(0, validPrefabs.Count)];
    }
}
