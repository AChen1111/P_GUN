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

        //记录已经生成的物品拾取物
        private HashSet<ItemPickup> itemPickups = new();

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
            bool isActive = true,
            AnimType animType = AnimType.None,
            Action onComplete = null,
            bool deferPickupUntilAnimComplete = false)
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

            var startActive = deferPickupUntilAnimComplete ? false : isActive;
            pickup.Init(_items[itemKey], startActive);
            Action wrapped = () =>
            {
                if (deferPickupUntilAnimComplete)
                    pickup.SetPickupEnabled(isActive);
                onComplete?.Invoke();
            };
            DOTweenAnimMgr.Play(animType, instance, onComplete: wrapped);
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
                            bool isActive = true, 
                            AnimType animType = AnimType.None, 
                            Action onComplete = null,
                            bool deferPickupUntilAnimComplete = false)
        {
            StartCoroutine(SpawnItemSODelayCoroutine(itemKey, worldPosition, delay, isActive, animType, onComplete, deferPickupUntilAnimComplete));
        }
        private IEnumerator SpawnItemSODelayCoroutine(string itemKey, Vector3 worldPosition, float delay, 
                            bool isActive, AnimType animType, Action onComplete, bool deferPickupUntilAnimComplete)
        {
            yield return new WaitForSeconds(delay);
            SpawnItemSO(itemKey, worldPosition, isActive, animType, onComplete, deferPickupUntilAnimComplete);
        }

        /// <summary>
        /// 移除物品拾取物
        /// </summary>
        /// <param name="itemPickup">物品拾取物</param>
        public void RemoveItemPickup(ItemPickup itemPickup)
        {
            if(itemPickup == null) return;
            if(itemPickups.Contains(itemPickup))
            {
                itemPickups.Remove(itemPickup);
            }
        }

        /// <summary>
        /// 添加物品拾取物
        /// </summary>
        /// <param name="itemPickup">物品拾取物</param>
        public void AddItemPickup(ItemPickup itemPickup)
        {
            if(itemPickup == null) return;
            itemPickups.Add(itemPickup);
        }

    }


}
