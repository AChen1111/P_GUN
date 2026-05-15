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
        public ItemSpawnTableSO itemTable;


        /// <summary>
        /// 生成物品，并播放动画
        /// </summary>
        public GameObject SpawnItem(Vector3 position,string animEffectKey = "")
        {
            //检查物品生成表是否设置
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

        public GameObject SpawnItem(GameObject prefab, Vector3 position, string animEffectKey = "")
        {
            var item = ItemPool.Instance.Spawn(prefab, position, Quaternion.identity);
            if(item == null) return null;

            var obj = item.gameObject;
            item.SetPickupEnabled(false);

            PlaySpawnAnimation(animEffectKey, obj, item);
            return obj;
        }

        private static void PlaySpawnAnimation(string animEffectKey, GameObject obj, Item item)
        {
            if(string.IsNullOrEmpty(animEffectKey) || DOTweenAnimMgr.Instance == null)
            {
                item.SetPickupEnabled(true);
                return;
            }

            DOTweenAnimMgr.Play(animEffectKey, obj, 3f, () =>
            {
                if(item != null && item.gameObject.activeSelf)
                {
                    item.SetPickupEnabled(true);
                }
            });
        }

    }
}
