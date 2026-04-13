using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace QFramework.PG
{
    /// <summary>
    /// 宝箱类效果：在触发位置随机生成一个已有道具（走 ItemWorldManager.SpawnItemSO）。
    /// lootTable 有内容时只从表中抽；为空则用管理器上的全局 ItemSOs。
    /// </summary>
    [CreateAssetMenu(fileName = "ChestRandomLootEffect", menuName = "PG/Item/Effects/Chest Random Loot", order = 2)]
    public class ChestRandomLootEffect : ItemEffectBase
    {
        [Tooltip("非空时只从这些道具里随机；为空则用 ItemWorldManager.ItemSOs")]
        [SerializeField] private List<ItemSO> lootTable = new List<ItemSO>();

        public override void OnPick(ItemEffectContext ctx)
        {
            var mgr = ItemWorldManager.Instance;
            if (mgr == null || mgr.ItemPrefab == null)
            {
                Debug.LogError($"{nameof(ChestRandomLootEffect)}: {nameof(ItemWorldManager)} 未初始化或 ItemPrefab 未设置。");
                return;
            }

            ItemSO drop = null;
            //从自己的表中获得一个随机物品
            if (lootTable != null && lootTable.Count > 0)
            {
                drop = lootTable.GetRandomItem();
            }
            

            if (drop == null || string.IsNullOrEmpty(drop.itemKey))
            {
                Debug.LogWarning($"{nameof(ChestRandomLootEffect)}: 没有可用的掉落配置。");
                return;
            }

            //在周围随机一个位置生成物品
            var newItemPostion = ctx.WorldPosition + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
            //延迟0.5秒生成物品
            mgr.SpawnItemSODelay(drop.itemKey, newItemPostion, 0.1f, AnimType.Jump, () =>
            {
                Debug.Log("物品生成完成");
            });
        }
    }
}

