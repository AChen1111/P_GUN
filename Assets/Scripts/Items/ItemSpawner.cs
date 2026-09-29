using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Animation;
using Game.Core;
using Game.Presentation;
using UnityEngine;

namespace Game.Items
{
    /// <summary>
    /// 轻量物品生成器: 掉落走 SpawnData.lua 的 itemDrops 权重, 预制体按 ItemData 的地址预载.
    /// </summary>
    public class ItemSpawner : MonoBehaviour
    {
        private const float DefaultSpawnAnimDuration = 1.5f;
        private const string DefaultDropAnimKey = "Scale0To1";

        [Header("掉落表 id, 对应 SpawnData.lua")]
        [SerializeField] private string dropTableId = "normal_default";

        private SpawnTableConfig dropTable;
        private readonly Dictionary<int, GameObject> itemPrefabsById = new Dictionary<int, GameObject>();
        private bool preloadStarted;

        /// <summary>
        /// 掉落表是否可用, 供敌人掉落判定使用.
        /// </summary>
        public bool HasDropEntries => dropTable != null && dropTable.ItemDrops.Count > 0;

        /// <summary>
        /// 初始化运行时依赖.
        /// </summary>
        private void Awake()
        {
            PreloadDropTableAsync();
        }

        /// <summary>
        /// 生成物品, 只使用已预载的掉落预制体.
        /// </summary>
        public GameObject SpawnItem(Vector3 position)
        {
            var prefab = ResolveRandomLoadedPrefab();
            return prefab != null ? SpawnItem(prefab, position, DefaultDropAnimKey) : null;
        }

        /// <summary>
        /// 异步生成物品, 未预载的道具按地址加载.
        /// </summary>
        public async Task<GameObject> SpawnItemAsync(Vector3 position)
        {
            var itemId = PickRandomItemId();
            if (itemId < 0) return null;

            var prefab = await GetOrLoadItemPrefabAsync(itemId);
            return prefab != null ? SpawnItem(prefab, position, DefaultDropAnimKey) : null;
        }

        /// <summary>
        /// 生成物品, 外部传入动画时覆盖默认掉落动画.
        /// </summary>
        public GameObject SpawnItem(Vector3 position, DOTweenAnimType animEffect)
        {
            var prefab = ResolveRandomLoadedPrefab();
            return prefab != null ? SpawnItem(prefab, position, animEffect, DefaultSpawnAnimDuration) : null;
        }

        /// <summary>
        /// 异步生成物品, 外部传入动画时覆盖默认掉落动画.
        /// </summary>
        public async Task<GameObject> SpawnItemAsync(Vector3 position, DOTweenAnimType animEffect)
        {
            var itemId = PickRandomItemId();
            if (itemId < 0) return null;

            var prefab = await GetOrLoadItemPrefabAsync(itemId);
            return prefab != null ? SpawnItem(prefab, position, animEffect, DefaultSpawnAnimDuration) : null;
        }

        /// <summary>
        /// 生成物品, 使用旧 string key 播放动画.
        /// </summary>
        public GameObject SpawnItem(Vector3 position, string animEffectKey)
        {
            var prefab = ResolveRandomLoadedPrefab();
            return prefab != null ? SpawnItem(prefab, position, animEffectKey) : null;
        }

        /// <summary>
        /// 异步生成物品, 使用旧 string key 播放动画.
        /// </summary>
        public async Task<GameObject> SpawnItemAsync(Vector3 position, string animEffectKey)
        {
            var itemId = PickRandomItemId();
            if (itemId < 0) return null;

            var prefab = await GetOrLoadItemPrefabAsync(itemId);
            return prefab != null ? SpawnItem(prefab, position, animEffectKey) : null;
        }

        /// <summary>
        /// 生成指定预制体, 并通过枚举播放动画.
        /// </summary>
        public GameObject SpawnItem(GameObject prefab, Vector3 position, DOTweenAnimType animEffect)
        {
            return SpawnItem(prefab, position, animEffect, DefaultSpawnAnimDuration);
        }

        /// <summary>
        /// 异步接口兼容已解析预制体的生成入口.
        /// </summary>
        public Task<GameObject> SpawnItemAsync(GameObject prefab, Vector3 position, DOTweenAnimType animEffect)
        {
            return Task.FromResult(SpawnItem(prefab, position, animEffect));
        }

        /// <summary>
        /// 生成指定预制体, 并通过枚举播放指定秒数的动画.
        /// </summary>
        public GameObject SpawnItem(GameObject prefab, Vector3 position, DOTweenAnimType animEffect, float animDuration)
        {
            var pool = ItemPool.Instance;
            if(pool == null)
            {
                throw new System.InvalidOperationException($"{nameof(ItemPool)} must exist in scene before spawning items.");
            }

            var item = pool.Spawn(prefab, position, Quaternion.identity);
            if(item == null) return null;

            var obj = item.gameObject;
            item.SetPickupEnabled(false);

            PlaySpawnAnimation(animEffect, animDuration, obj, item);
            return obj;
        }

        /// <summary>
        /// 异步接口兼容已解析预制体和动画时长.
        /// </summary>
        public Task<GameObject> SpawnItemAsync(GameObject prefab, Vector3 position, DOTweenAnimType animEffect, float animDuration)
        {
            return Task.FromResult(SpawnItem(prefab, position, animEffect, animDuration));
        }

        /// <summary>
        /// 生成指定预制体, 并通过旧 string key 播放动画.
        /// </summary>
        public GameObject SpawnItem(GameObject prefab, Vector3 position, string animEffectKey)
        {
            var pool = ItemPool.Instance;
            if(pool == null)
            {
                throw new System.InvalidOperationException($"{nameof(ItemPool)} must exist in scene before spawning items.");
            }

            var item = pool.Spawn(prefab, position, Quaternion.identity);
            if(item == null) return null;

            var obj = item.gameObject;
            item.SetPickupEnabled(false);

            PlaySpawnAnimation(animEffectKey, DefaultSpawnAnimDuration, obj, item);
            return obj;
        }

        /// <summary>
        /// 异步接口兼容已解析预制体和旧动画 key.
        /// </summary>
        public Task<GameObject> SpawnItemAsync(GameObject prefab, Vector3 position, string animEffectKey)
        {
            return Task.FromResult(SpawnItem(prefab, position, animEffectKey));
        }

        /// <summary>
        /// 预载掉落表与全部掉落预制体, 掉落表本体在第一个 await 前就绪.
        /// </summary>
        private async void PreloadDropTableAsync()
        {
            if (preloadStarted) return;
            preloadStarted = true;

            try
            {
                var loader = AddressableLoader.Instance;
                if (loader == null)
                {
                    throw new InvalidOperationException($"{nameof(ItemSpawner)} requires {nameof(AddressableLoader)} before preloading drops.");
                }

                dropTable = LuaDataRuntime.GetSpawnTableConfig(dropTableId);
                for (var i = 0; i < dropTable.ItemDrops.Count; i++)
                {
                    var itemId = dropTable.ItemDrops[i].ItemId;
                    if (itemPrefabsById.ContainsKey(itemId)) continue;

                    var prefab = await GetOrLoadItemPrefabAsync(itemId);
                    if (prefab == null)
                    {
                        throw new InvalidOperationException($"道具预制体加载失败, itemId: {itemId}.");
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.LogError($"{nameof(ItemSpawner)}: 掉落表预载失败, Table: {dropTableId}, Error: {exception.Message}", this);
            }
        }

        /// <summary>
        /// 按权重随机取一个道具 id, 没有条目或权重非法时返回 -1 并报错.
        /// </summary>
        private int PickRandomItemId()
        {
            if (!HasDropEntries)
            {
                Debug.LogError($"{nameof(ItemSpawner)}: 掉落表 {dropTableId} 没有条目.", this);
                return -1;
            }

            var totalWeight = 0f;
            for (var i = 0; i < dropTable.ItemDrops.Count; i++)
            {
                totalWeight += Mathf.Max(0f, dropTable.ItemDrops[i].Weight);
            }

            if (totalWeight <= 0f)
            {
                Debug.LogError($"{nameof(ItemSpawner)}: 掉落表 {dropTableId} 权重总和为 0.", this);
                return -1;
            }

            var pick = UnityEngine.Random.Range(0f, totalWeight);
            var accumulated = 0f;
            for (var i = 0; i < dropTable.ItemDrops.Count; i++)
            {
                accumulated += Mathf.Max(0f, dropTable.ItemDrops[i].Weight);
                if (pick < accumulated)
                {
                    return dropTable.ItemDrops[i].ItemId;
                }
            }

            return dropTable.ItemDrops[dropTable.ItemDrops.Count - 1].ItemId;
        }

        /// <summary>
        /// 取已预载的随机道具预制体, 未预载完成时报错返回空.
        /// </summary>
        private GameObject ResolveRandomLoadedPrefab()
        {
            var itemId = PickRandomItemId();
            if (itemId < 0) return null;

            if (!itemPrefabsById.TryGetValue(itemId, out var prefab))
            {
                Debug.LogError($"{nameof(ItemSpawner)}: 道具预制体尚未预载完成, itemId: {itemId}.", this);
                return null;
            }

            return prefab;
        }

        /// <summary>
        /// 取或按地址加载道具预制体, 地址来自 ItemData.lua.
        /// </summary>
        private async Task<GameObject> GetOrLoadItemPrefabAsync(int itemId)
        {
            if (itemPrefabsById.TryGetValue(itemId, out var cached))
            {
                return cached;
            }

            var loader = AddressableLoader.Instance;
            if (loader == null)
            {
                throw new InvalidOperationException($"{nameof(ItemSpawner)} requires {nameof(AddressableLoader)} before loading item prefabs.");
            }

            var config = LuaDataRuntime.GetItemConfig(itemId);
            if (string.IsNullOrEmpty(config.PrefabAddress))
            {
                throw new InvalidOperationException($"道具 {itemId} 缺少 prefabAddress.");
            }

            var prefab = await loader.LoadAssetAsync<GameObject>(config.PrefabAddress);
            if (prefab == null)
            {
                throw new InvalidOperationException($"道具预制体加载失败, 地址: {config.PrefabAddress}.");
            }

            itemPrefabsById[itemId] = prefab;
            return prefab;
        }

        private static void PlaySpawnAnimation(DOTweenAnimType animEffect, float animDuration, GameObject obj, Item item)
        {
            if(animEffect == DOTweenAnimType.None || DOTweenAnimMgr.Instance == null)
            {
                item.SetPickupEnabled(true);
                return;
            }

            DOTweenAnimMgr.Play(animEffect, obj, animDuration, () =>
            {
                if(item != null && item.gameObject.activeSelf)
                {
                    item.SetPickupEnabled(true);
                }
            });
        }
        private static void PlaySpawnAnimation(string animEffectKey, float animDuration, GameObject obj, Item item)
        {
            if(string.IsNullOrEmpty(animEffectKey) || DOTweenAnimMgr.Instance == null)
            {
                item.SetPickupEnabled(true);
                return;
            }

            DOTweenAnimMgr.Play(animEffectKey, obj, animDuration, () =>
            {
                if(item != null && item.gameObject.activeSelf)
                {
                    item.SetPickupEnabled(true);
                }
            });
        }
    }
}