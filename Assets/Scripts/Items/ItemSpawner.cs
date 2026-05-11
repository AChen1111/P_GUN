using UnityEngine;
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
            //todo:生成方式改为对象池
            var obj = Instantiate(prefab, position, Quaternion.identity);

            //如果有道具组件
            if(obj.TryGetComponent(out Item item))
            {
                item.SetPickupEnabled(false);

                DOTweenAnimMgr.Play(animEffectKey, obj, 1f, () =>
                {
                    item.SetPickupEnabled(true);
                });

                return obj;
            }
            else
            {
                DOTweenAnimMgr.Play(animEffectKey, obj, 1f);
                return obj;
            }
        }

        //如果获取失败，则返回null
        Debug.LogWarning("No prefab found in item table");
        return null;
    }

}
