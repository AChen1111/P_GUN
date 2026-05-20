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

        [Tooltip("物品生成表 Address, 为空时使用本地直连配置")]
        [SerializeField] private string spawnTableAddress;

        [Tooltip("旧配置兼容：未配置生成表时从这里随机抽取")]
        [SerializeField] private List<GameObject> lootTable = new List<GameObject>();

        [SerializeField] private string animEffectKey = "Jump";
        [SerializeField] private Vector2 randomOffsetRange = new Vector2(0.5f, 0.5f);

        public override bool CanUse(ItemEffectContext ctx)
        {
            return ResolveSpawner(ctx) != null && HasAvailableLoot();
        }

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
        }

        private ItemSpawner ResolveSpawner(ItemEffectContext ctx)
        {
            return ctx.SourceObject != null ? ctx.SourceObject.GetComponentInParent<ItemSpawner>() : null;
        }

        private GameObject GetRandomPrefab(out DOTweenAnimType spawnAnimEffect, out float spawnAnimDuration)
        {
            spawnAnimEffect = DOTweenAnimType.None;
            spawnAnimDuration = 0f;

            var activeSpawnTable = ResolveSpawnTable();
            if (activeSpawnTable != null)
            {
                if (activeSpawnTable.TryGetRandomEntry(out var entry))
                {
                    spawnAnimEffect = entry.spawnAnimEffect;
                    spawnAnimDuration = entry.spawnAnimDuration;
                    return activeSpawnTable.TryResolvePrefab(entry, out var prefab) ? prefab : null;
                }

                Debug.LogWarning($"{nameof(ChestRandomLootEffect)}: 生成表 {activeSpawnTable.name} 没有可用物品。");
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

        /// <summary>
        /// 旧掉落表只保存直连预制体, 运行时优先替换为已热更的同 ID 预制体.
        /// </summary>
        private static GameObject ResolveRuntimePrefab(GameObject configuredPrefab)
        {
            var item = configuredPrefab != null ? configuredPrefab.GetComponent<Item>() : null;
            var content = AddressableRuntimeContent.Instance;
            if (item != null && content != null && content.TryGetPrefabById("item", item.ItemId, out var runtimePrefab))
            {
                return runtimePrefab;
            }

            return configuredPrefab;
        }

        private ItemSpawnTableSO ResolveSpawnTable()
        {
            if (string.IsNullOrWhiteSpace(spawnTableAddress))
            {
                return spawnTable;
            }

            var content = AddressableRuntimeContent.Instance;
            if (content == null)
            {
                // 允许直接从 GameScene Play, 此时使用 Inspector 中的本地生成表.
                return spawnTable;
            }

            if (content.TryGetAsset<ItemSpawnTableSO>(spawnTableAddress, out var runtimeSpawnTable))
            {
                return runtimeSpawnTable;
            }

            Debug.LogError($"{nameof(ChestRandomLootEffect)}: 找不到已预加载的物品生成表, Address: {spawnTableAddress}.", this);
            return null;
        }

        private bool HasAvailableLoot()
        {
            var activeSpawnTable = ResolveSpawnTable();
            if (activeSpawnTable != null)
            {
                return activeSpawnTable.TryGetRandomEntry(out _);
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
}
