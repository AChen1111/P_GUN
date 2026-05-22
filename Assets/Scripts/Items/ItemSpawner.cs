using System.Threading.Tasks;
using Game.Animation;
using Game.Presentation;
using UnityEngine;

namespace Game.Items
{
    /// <summary>
    /// 轻量物品生成器: 只负责在指定位置生成物品.
    /// </summary>
    public class ItemSpawner : MonoBehaviour
    {
        private const float DefaultSpawnAnimDuration = 1.5f;

        public ItemSpawnTableSO itemTable;

        /// <summary>
        /// 生成物品, 只使用已加载或直接引用的预制体.
        /// </summary>
        public GameObject SpawnItem(Vector3 position)
        {
            if (!TryValidateItemTable()) return null;

            if(itemTable.TryGetRandomEntry(out var entry))
            {
                return itemTable.TryResolvePrefab(entry, out var prefab)
                    ? SpawnItem(prefab, position, entry.spawnAnimEffect, entry.spawnAnimDuration)
                    : null;
            }

            Debug.LogWarning("No prefab found in item table");
            return null;
        }

        /// <summary>
        /// 异步生成物品, 支持按需加载 Addressables 预制体.
        /// </summary>
        public async Task<GameObject> SpawnItemAsync(Vector3 position)
        {
            if (!TryValidateItemTable()) return null;

            if(itemTable.TryGetRandomEntry(out var entry))
            {
                var prefab = await itemTable.TryResolvePrefabAsync(entry);
                return prefab != null
                    ? SpawnItem(prefab, position, entry.spawnAnimEffect, entry.spawnAnimDuration)
                    : null;
            }

            Debug.LogWarning("No prefab found in item table");
            return null;
        }

        /// <summary>
        /// 生成物品, 外部传入动画时覆盖配置项动画.
        /// </summary>
        public GameObject SpawnItem(Vector3 position, DOTweenAnimType animEffect)
        {
            if (!TryValidateItemTable()) return null;

            if(itemTable.TryGetRandomPrefab(out var prefab))
            {
                return SpawnItem(prefab, position, animEffect, DefaultSpawnAnimDuration);
            }

            Debug.LogWarning("No prefab found in item table");
            return null;
        }

        /// <summary>
        /// 异步生成物品, 外部传入动画时覆盖配置项动画.
        /// </summary>
        public async Task<GameObject> SpawnItemAsync(Vector3 position, DOTweenAnimType animEffect)
        {
            if (!TryValidateItemTable()) return null;

            var prefab = await itemTable.TryGetRandomPrefabAsync();
            if(prefab != null)
            {
                return SpawnItem(prefab, position, animEffect, DefaultSpawnAnimDuration);
            }

            Debug.LogWarning("No prefab found in item table");
            return null;
        }

        /// <summary>
        /// 生成物品, 使用旧 string key 播放动画.
        /// </summary>
        public GameObject SpawnItem(Vector3 position, string animEffectKey)
        {
            if (!TryValidateItemTable()) return null;

            if(itemTable.TryGetRandomPrefab(out var prefab))
            {
                return SpawnItem(prefab, position, animEffectKey);
            }

            Debug.LogWarning("No prefab found in item table");
            return null;
        }

        /// <summary>
        /// 异步生成物品, 使用旧 string key 播放动画.
        /// </summary>
        public async Task<GameObject> SpawnItemAsync(Vector3 position, string animEffectKey)
        {
            if (!TryValidateItemTable()) return null;

            var prefab = await itemTable.TryGetRandomPrefabAsync();
            if(prefab != null)
            {
                return SpawnItem(prefab, position, animEffectKey);
            }

            Debug.LogWarning("No prefab found in item table");
            return null;
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
        /// 检查物品生成表是否可用.
        /// </summary>
        private bool TryValidateItemTable()
        {
            if(itemTable != null && itemTable.Entries.Count > 0)
            {
                return true;
            }

            Debug.LogWarning("Item table is not set");
            return false;
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
