using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;

namespace Game.Items
{
    /// <summary>
    /// 物品对象池。按 Item prefab 分池，供房间奖励、宝箱掉落等入口复用同一套生成逻辑。
    /// </summary>
    public class ItemPool : PoolBase<Item>
    {
        public new static ItemPool Instance
        {
            get
            {
                var instance = PoolBase<Item>.Instance as ItemPool;
                if (instance == null)
                {
                    var go = new GameObject("[ItemPool]");
                    instance = go.AddComponent<ItemPool>();
                }

                return instance;
            }
        }

        public Item Spawn(GameObject prefabObject, Vector3 position, Quaternion rotation)
        {
            if (prefabObject == null)
            {
                Debug.LogError("ItemPool.Spawn failed: prefabObject is null.", this);
                return null;
            }

            var prefab = prefabObject.GetComponent<Item>();
            if (prefab == null)
            {
                Debug.LogError($"ItemPool.Spawn failed: {prefabObject.name} has no Item component.", prefabObject);
                return null;
            }

            return Spawn(prefab, position, rotation);
        }

        public Item Spawn(Item prefab, Vector3 position, Quaternion rotation)
        {
            return Get(prefab, position, rotation);
        }
    }
}
