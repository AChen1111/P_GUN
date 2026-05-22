using System;
using System.Collections.Generic;
using Game.Animation;
using UnityEngine;

namespace Game.Items
{
    /// <summary>
    /// 宝箱类效果: 在触发位置随机生成一个道具预制体.
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
        }

        /// <summary>
        /// 执行 OnPick 逻辑.
        /// </summary>
        public override void OnPick(ItemEffectContext ctx)
        {
            try
            {
                SpawnLoot(ctx);
            }
            catch (Exception exception)
            {
                Debug.LogError($"{nameof(ChestRandomLootEffect)}: 生成宝箱掉落失败, Error: {exception.Message}", this);
                throw;
            }
        }

        /// <summary>
        /// 生成掉落物.
        /// </summary>
        private void SpawnLoot(ItemEffectContext ctx)
        {
            var spawner = ResolveSpawner(ctx);
            if (spawner == null)
            {
                Debug.LogError($"{nameof(ChestRandomLootEffect)}: 找不到可用的 {nameof(ItemSpawner)}。");
                return;
            }

            var newItemPosition = ctx.WorldPosition + new Vector3(
                UnityEngine.Random.Range(-randomOffsetRange.x, randomOffsetRange.x),
                UnityEngine.Random.Range(-randomOffsetRange.y, randomOffsetRange.y),
                0f);

            var selection = GetRandomPrefab();
            if (selection.Prefab == null) return;

            if (selection.SpawnAnimEffect != DOTweenAnimType.None)
            {
                spawner.SpawnItem(selection.Prefab, newItemPosition, selection.SpawnAnimEffect, selection.SpawnAnimDuration);
                return;
            }

            spawner.SpawnItem(selection.Prefab, newItemPosition, animEffectKey);
        }

        /// <summary>
        /// 获取一个可生成的掉落物预制体.
        /// </summary>
        private LootSelection GetRandomPrefab()
        {
            if (spawnTable != null)
            {
                if (spawnTable.TryGetRandomEntry(out var entry))
                {
                    spawnTable.TryResolvePrefab(entry, out var prefab);
                    return new LootSelection
                    {
                        Prefab = prefab,
                        SpawnAnimEffect = entry.spawnAnimEffect,
                        SpawnAnimDuration = entry.spawnAnimDuration
                    };
                }

                Debug.LogWarning($"{nameof(ChestRandomLootEffect)}: 生成表 {spawnTable.name} 没有可用物品。");
                return default;
            }

            if (lootTable == null || lootTable.Count == 0)
            {
                Debug.LogWarning($"{nameof(ChestRandomLootEffect)}: 没有可用的掉落配置。");
                return default;
            }

            var validPrefabs = new List<GameObject>();
            foreach (var configuredPrefab in lootTable)
            {
                if (configuredPrefab != null)
                {
                    // 宝箱旧配置直接使用 Inspector 中引用的 prefab, 不再通过 itemId 替换为 Addressables 资源.
                    validPrefabs.Add(configuredPrefab);
                }
            }

            if (validPrefabs.Count == 0)
            {
                Debug.LogWarning($"{nameof(ChestRandomLootEffect)}: 掉落配置中没有有效预制体。");
                return default;
            }

            return new LootSelection
            {
                Prefab = validPrefabs[UnityEngine.Random.Range(0, validPrefabs.Count)]
            };
        }

        /// <summary>
        /// 宝箱掉落选择结果.
        /// </summary>
        private struct LootSelection
        {
            public GameObject Prefab;
            public DOTweenAnimType SpawnAnimEffect;
            public float SpawnAnimDuration;
        }

        /// <summary>
        /// 检查是否存在可用掉落配置.
        /// </summary>
        private bool HasAvailableLoot()
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

        /// <summary>
        /// 执行 ResolveSpawner 逻辑.
        /// </summary>
        private ItemSpawner ResolveSpawner(ItemEffectContext ctx)
        {
            return ctx.SourceObject != null ? ctx.SourceObject.GetComponentInParent<ItemSpawner>() : null;
        }
    }
}
