using UnityEngine;
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

        /// <summary>
        /// 生成物品，并播放抽中配置项的动画。
        /// </summary>
        public GameObject SpawnItem(Vector3 position)
        {
            // 检查物品生成表是否设置.
            if(itemTable == null || itemTable.Entries.Count == 0)
            {
                Debug.LogWarning("Item table is not set");
                return null;
            }

            //随机获取一个物品
            if(itemTable.TryGetRandomEntry(out var entry))
            {
                return itemTable.TryResolvePrefab(entry, out var prefab)
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
            // 检查物品生成表是否设置.
            if(itemTable == null || itemTable.Entries.Count == 0)
            {
                Debug.LogWarning("Item table is not set");
                return null;
            }

            //随机获取一个物品，外部传入动画时覆盖配置项动画。
            if(itemTable.TryGetRandomPrefab(out GameObject prefab))
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
            // 检查物品生成表是否设置.
            if(itemTable == null || itemTable.Entries.Count == 0)
            {
                Debug.LogWarning("Item table is not set");
                return null;
            }

            //随机获取一个物品
            if(itemTable.TryGetRandomPrefab(out GameObject prefab))
            {
                return SpawnItem(prefab, position, animEffectKey);
            }

            //如果获取失败，则返回null
            Debug.LogWarning("No prefab found in item table");
            return null;
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
        /// 生成指定预制体，并通过旧 string key 播放动画。
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
        /// 执行 PlaySpawnAnimation 逻辑.
        /// </summary>
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

        /// <summary>
        /// 执行 PlaySpawnAnimation 逻辑.
        /// </summary>
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
