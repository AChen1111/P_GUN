using System;
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

        private void Start() {
            SpawnItemSO("Chest", new Vector3(10, 5, 0));
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
        public ItemPickup SpawnItemSO(
            string itemKey, 
            Vector3 worldPosition, 
            AnimType animType = AnimType.None,
            Action onComplete = null)
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
            DOTweenAnimMgr.Play(animType, instance,onComplete: onComplete);
            return pickup;
        }
        /// <summary>
        /// 延迟生成道具
        /// </summary>
        /// <param name="itemKey">道具配置</param>
        /// <param name="worldPosition">生成位置</param>
        /// <param name="delay">延迟时间</param>
        /// <param name="animType">动画类型</param>
        public void SpawnItemSODelay(string itemKey, Vector3 worldPosition, float delay, 
                            AnimType animType = AnimType.None, Action onComplete = null)
        {
            StartCoroutine(SpawnItemSODelayCoroutine(itemKey, worldPosition, delay, animType, onComplete));
        }
        private IEnumerator SpawnItemSODelayCoroutine(string itemKey, Vector3 worldPosition, float delay, 
                            AnimType animType, Action onComplete)
        {
            yield return new WaitForSeconds(delay);
            SpawnItemSO(itemKey, worldPosition, animType, onComplete);
        }


        
    }


}
