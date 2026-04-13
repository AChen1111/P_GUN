using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.Serialization;

namespace QFramework.PG
{
    /// <summary>
    /// 管理世界中道具的生成：在指定坐标实例化拾取预制体并绑定配置。
    /// </summary>
    public class ItemWorldManager : MonoBehaviour
    {

        public static ItemWorldManager Instance { get; private set; }

        private void Awake() {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (ItemSOs == null) return;
            foreach (var item in ItemSOs)
            {
                if (item == null || string.IsNullOrEmpty(item.itemKey)) continue;
                _items[item.itemKey] = item;
            }
        }

        [Header("道具配置")]
        [FormerlySerializedAs("ItemDefinitions")]
        public List<ItemSO> ItemSOs = new();

        private Dictionary<string, ItemSO> _items = new();

        [Header("道具预制体")]
        public GameObject ItemPrefab;

        public ItemSO GetRandomItemSO()
        {
            if (ItemSOs == null || ItemSOs.Count == 0) return null;
            return ItemSOs.GetRandomItem();
        }

        /// <summary>
        /// 生成道具
        /// </summary>
        /// <param name="itemKey">道具配置</param>
        /// <param name="worldPosition">生成位置</param>
        /// <returns>道具拾取物</returns>
        public ItemPickup SpawnItemSO(string itemKey, Vector3 worldPosition)
        {
            if (!_items.ContainsKey(itemKey)) {
                Debug.LogError($"道具配置不存在: {itemKey}");
                return null;
            }


            //实例化道具预制体
            var instance = Instantiate(ItemPrefab, worldPosition, Quaternion.identity, transform);
            instance.name = itemKey;
            instance.SetActive(true);


            //初始化道具拾取物
            var pickup = instance.GetComponent<ItemPickup>();
            if (pickup == null)
            {
                Debug.LogError($"{nameof(ItemWorldManager)}: ItemPrefab 上缺少 {nameof(ItemPickup)}。");
                Destroy(instance);
                return null;
            }
            pickup.Init(_items[itemKey]);
            return pickup;
        }

        public void SpawnItemSODelay(string itemKey, Vector3 worldPosition, float delay)
        {
            StartCoroutine(SpawnItemSODelayCoroutine(itemKey, worldPosition, delay));
        }

        private IEnumerator SpawnItemSODelayCoroutine(string itemKey, Vector3 worldPosition, float delay)
        {
            yield return new WaitForSeconds(delay);
            SpawnItemSO(itemKey, worldPosition);
        }

        //todo: 生成物品时播放动画
        
    }

    public enum AnimType
    {
        None,//无动画
        Jump,
        Shake,
    }
}
