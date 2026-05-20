using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;

namespace Game.Items
{
    /// <summary>
    /// 轻量物品生成器：只负责在指定位置生成物品。
    /// </summary>
    public class ItemSpawner : MonoBehaviour
    {
        private const float DefaultSpawnAnimDuration = 1.5f;

        public ItemSpawnTableSO itemTable;
        [SerializeField] private string itemTableAddress = "item/spawn_table/normal_room";

        /// <summary>
        /// 生成物品，并播放抽中配置项的动画。
        /// </summary>
        public GameObject SpawnItem(Vector3 position)
        {
            if(!TryGetItemTable(out var table))
            {
                return null;
            }

            //随机获取一个物品
            if(table.TryGetRandomEntry(out var entry))
            {
                return table.TryResolvePrefab(entry, out var prefab)
                    ? SpawnItem(prefab, position, entry.spawnAnimEffect, entry.spawnAnimDuration)
                    : null;
            }

            //如果获取失败，则返回null
            Debug.LogWarning("No prefab found in item table");
            return null;
        }

        /// <summary>
        /// 生成物品，并通过枚举播放动画。
        /// </summary>
        public GameObject SpawnItem(Vector3 position, DOTweenAnimType animEffect)
        {
            if(!TryGetItemTable(out var table))
            {
                return null;
            }

            //随机获取一个物品，外部传入动画时覆盖配置项动画。
            if(table.TryGetRandomPrefab(out GameObject prefab))
            {
                return SpawnItem(prefab, position, animEffect, DefaultSpawnAnimDuration);
            }

            //如果获取失败，则返回null
            Debug.LogWarning("No prefab found in item table");
            return null;
        }

        /// <summary>
        /// 生成物品，并通过旧 string key 播放动画。
        /// </summary>
        public GameObject SpawnItem(Vector3 position, string animEffectKey)
        {
            if(!TryGetItemTable(out var table))
            {
                return null;
            }

            //随机获取一个物品
            if(table.TryGetRandomPrefab(out GameObject prefab))
            {
                return SpawnItem(prefab, position, animEffectKey);
            }

            //如果获取失败，则返回null
            Debug.LogWarning("No prefab found in item table");
            return null;
        }

        public bool HasAvailableTable()
        {
            var table = ResolveItemTable();
            return table != null && table.Entries.Count > 0;
        }

        public ItemSpawnTableSO ResolveItemTable()
        {
            var content = AddressableRuntimeContent.Instance;
            if(content == null)
            {
                // 允许直接从 GameScene Play, 此时使用 Inspector 中的本地生成表.
                return itemTable;
            }

            if(string.IsNullOrWhiteSpace(itemTableAddress))
            {
                Debug.LogError($"{nameof(ItemSpawner)}: 物品生成表 Address 未配置.", this);
                return null;
            }

            if(content.TryGetAsset<ItemSpawnTableSO>(itemTableAddress, out var runtimeItemTable))
            {
                return runtimeItemTable;
            }

            Debug.LogError($"{nameof(ItemSpawner)}: 找不到已预加载的物品生成表, Address: {itemTableAddress}.", this);
            return null;
        }

        private bool TryGetItemTable(out ItemSpawnTableSO table)
        {
            table = ResolveItemTable();
            if(table != null && table.Entries.Count > 0)
            {
                return true;
            }

            Debug.LogWarning("Item table is not set");
            return false;
        }

        /// <summary>
        /// 生成指定预制体，并通过枚举播放动画。
        /// </summary>
        public GameObject SpawnItem(GameObject prefab, Vector3 position, DOTweenAnimType animEffect)
        {
            return SpawnItem(prefab, position, animEffect, DefaultSpawnAnimDuration);
        }

        /// <summary>
        /// 生成指定预制体，并通过枚举播放指定秒数的动画。
        /// </summary>
        public GameObject SpawnItem(GameObject prefab, Vector3 position, DOTweenAnimType animEffect, float animDuration)
        {
            var item = ItemPool.Instance.Spawn(prefab, position, Quaternion.identity);
            if(item == null) return null;

            var obj = item.gameObject;
            item.SetPickupEnabled(false);

            PlaySpawnAnimation(animEffect, animDuration, obj, item);
            return obj;
        }

        /// <summary>
        /// 生成指定预制体，并通过旧 string key 播放动画。
        /// </summary>
        public GameObject SpawnItem(GameObject prefab, Vector3 position, string animEffectKey)
        {
            var item = ItemPool.Instance.Spawn(prefab, position, Quaternion.identity);
            if(item == null) return null;

            var obj = item.gameObject;
            item.SetPickupEnabled(false);

            PlaySpawnAnimation(animEffectKey, DefaultSpawnAnimDuration, obj, item);
            return obj;
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
