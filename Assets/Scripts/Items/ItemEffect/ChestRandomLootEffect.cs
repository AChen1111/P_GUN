using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;

namespace Game.Items
{
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

        /// <summary>
        /// 执行 CanUse 逻辑.
        /// </summary>
        public override bool CanUse(ItemEffectContext ctx)
        {
            return ResolveSpawner(ctx) != null && HasAvailableLoot();

            bool HasAvailableLoot()
            {
                if (spawnTable != null)
                {
                    return spawnTable.TryGetRandomEntry(out _);
                }

                if (lootTable == null)
                {
                    return false;
                }

                for (int i = 0; i < lootTable.Count; i++)
                {
                    if (lootTable[i] != null)
                    {
                        return true;
                    }
                }

                return false;
            }
}

        /// <summary>
        /// 执行 OnPick 逻辑.
        /// </summary>
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

            var dropAnimEffect = DOTweenAnimType.None;
            var dropAnimDuration = 0f;
            var drop = GetRandomPrefab(out dropAnimEffect, out dropAnimDuration);
            if (drop == null) return;

            if (dropAnimEffect != DOTweenAnimType.None)
            {
                spawner.SpawnItem(drop, newItemPosition, dropAnimEffect, dropAnimDuration);
                return;
            }

            spawner.SpawnItem(drop, newItemPosition, animEffectKey);

            GameObject GetRandomPrefab(out DOTweenAnimType spawnAnimEffect, out float spawnAnimDuration)
            {
                spawnAnimEffect = DOTweenAnimType.None;
                spawnAnimDuration = 0f;
                if (spawnTable != null)
                {
                    if (spawnTable.TryGetRandomEntry(out var entry))
                    {
                        spawnAnimEffect = entry.spawnAnimEffect;
                        spawnAnimDuration = entry.spawnAnimDuration;
                        return spawnTable.TryResolvePrefab(entry, out var prefab) ? prefab : null;
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
                foreach (var configuredPrefab in lootTable)
                {
                    if (configuredPrefab != null)
                    {
                        validPrefabs.Add(ResolveRuntimePrefab(configuredPrefab));
                    }
                }

                if (validPrefabs.Count == 0)
                {
                    Debug.LogWarning($"{nameof(ChestRandomLootEffect)}: 掉落配置中没有有效预制体。");
                    return null;
                }

                return validPrefabs[Random.Range(0, validPrefabs.Count)];
            }

    static GameObject ResolveRuntimePrefab(GameObject configuredPrefab)
    {
        var item = configuredPrefab != null ? configuredPrefab.GetComponent<Item>() : null;
        var content = AddressableRuntimeContent.Instance;
        if (item != null && content == null)
        {
            throw new System.InvalidOperationException($"{nameof(ChestRandomLootEffect)} requires {nameof(AddressableRuntimeContent)} for item prefab replacement.");
        }

        if (item != null && content != null && content.TryGetPrefabById("item", item.ItemId, out var runtimePrefab))
        {
            return runtimePrefab;
        }

        if (item != null)
        {
            throw new System.InvalidOperationException($"{nameof(ChestRandomLootEffect)} missing runtime item prefab, ItemId: {item.ItemId}.");
        }

        return configuredPrefab;
    }
}

        /// <summary>
        /// 执行 ResolveSpawner 逻辑.
        /// </summary>
        private ItemSpawner ResolveSpawner(ItemEffectContext ctx)
        {
            return ctx.SourceObject != null ? ctx.SourceObject.GetComponentInParent<ItemSpawner>() : null;
        }

    }
}
