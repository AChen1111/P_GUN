using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;

namespace Game.Items
{
    [Serializable]
    public struct ItemSpawnEntry
    {
        public int itemId;
        public string address;
        public GameObject prefab;

        [Min(0)]
        public int weight;

        public DOTweenAnimType spawnAnimEffect;

        [Min(0f)]
        public float spawnAnimDuration;
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
            if (TryGetRandomEntry(out var selectedEntry))
            {
                return TryResolvePrefab(selectedEntry, out prefab);
            }

            return false;
        }

        /// <summary>
        /// 按权重异步获取一个随机物品预制体.
        /// </summary>
        public async Task<GameObject> TryGetRandomPrefabAsync()
        {
            if (TryGetRandomEntry(out var selectedEntry))
            {
                return await TryResolvePrefabAsync(selectedEntry);
            }

            return null;
        }

        /// <summary>
        /// 执行 TryResolvePrefab 逻辑.
        /// </summary>
        public bool TryResolvePrefab(ItemSpawnEntry entry, out GameObject prefab)
        {
            if (TryResolveLoadedAddressablePrefab(entry, out prefab))
            {
                return true;
            }

            prefab = entry.prefab;
            return prefab != null;
        }

        /// <summary>
        /// 异步解析物品预制体, 优先使用 Addressables 地址.
        /// </summary>
        public async Task<GameObject> TryResolvePrefabAsync(ItemSpawnEntry entry)
        {
            var address = ResolveAddress(entry);
            if (!string.IsNullOrWhiteSpace(address))
            {
                var loader = AddressableLoader.Instance;
                if (loader == null)
                {
                    throw new InvalidOperationException($"{nameof(AddressableLoader)} must exist before loading item prefab.");
                }

                return await loader.LoadAssetAsync<GameObject>(address);
            }

            return entry.prefab;
        }

        /// <summary>
        /// 按权重获取一个随机物品配置，生成动画跟随被抽中的配置项。
        /// </summary>
        public bool TryGetRandomEntry(out ItemSpawnEntry selectedEntry)
        {
            selectedEntry = default;

            var totalWeight = 0;
            foreach (var entry in entries)
            {
                if (!HasResolvablePrefab(entry) || entry.weight <= 0) continue;
                totalWeight += entry.weight;
            }

            if (totalWeight <= 0) return false;

            var roll = UnityEngine.Random.Range(0, totalWeight);
            foreach (var entry in entries)
            {
                if (!HasResolvablePrefab(entry) || entry.weight <= 0) continue;

                if (roll < entry.weight)
                {
                    selectedEntry = entry;
                    return true;
                }

                roll -= entry.weight;
            }

            return false;
        }

        /// <summary>
        /// 执行 HasResolvablePrefab 逻辑.
        /// </summary>
        private static bool HasResolvablePrefab(ItemSpawnEntry entry)
        {
            return !string.IsNullOrWhiteSpace(ResolveAddress(entry)) || entry.prefab != null;
        }

        /// <summary>
        /// 执行 TryResolveLoadedAddressablePrefab 逻辑.
        /// </summary>
        private static bool TryResolveLoadedAddressablePrefab(ItemSpawnEntry entry, out GameObject prefab)
        {
            prefab = null;
            var loader = AddressableLoader.Instance;
            if (loader == null) return false;

            var address = ResolveAddress(entry);
            if (!string.IsNullOrWhiteSpace(address)
                && loader.TryGetLoadedAsset<GameObject>(address, out prefab))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 从配置项中解析 Addressables 地址.
        /// </summary>
        private static string ResolveAddress(ItemSpawnEntry entry)
        {
            if (!string.IsNullOrWhiteSpace(entry.address))
            {
                return entry.address.Trim();
            }

            if (entry.itemId > 0 && AddressableItemAddressCatalog.TryGetAddress(entry.itemId, out var itemIdAddress))
            {
                return itemIdAddress;
            }

            var item = entry.prefab != null ? entry.prefab.GetComponent<Item>() : null;
            return item != null && AddressableItemAddressCatalog.TryGetAddress(item.ItemId, out var prefabAddress)
                ? prefabAddress
                : null;
        }
    }
}
