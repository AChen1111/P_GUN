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
            if (lootTable != null && lootTable.Count > 0)
            {
                drop = lootTable.GetRandomItem();
            }
            else if (mgr.ItemSOs != null && mgr.ItemSOs.Count > 0)
            {
                drop = mgr.GetRandomItemSO();
                // 全表随机时尽量避免又随到「宝箱自己」
                if (ctx.SourceItem != null && mgr.ItemSOs.Count > 1)
                {
                    for (var i = 0; i < 16 && drop == ctx.SourceItem; i++)
                    {
                        drop = mgr.GetRandomItemSO();
                    }
                }
            }

            if (drop == null || string.IsNullOrEmpty(drop.itemKey))
            {
                Debug.LogWarning($"{nameof(ChestRandomLootEffect)}: 没有可用的掉落配置。");
                return;
            }

            mgr.SpawnItemSODelay(drop.itemKey, ctx.WorldPosition, 0.5f);
        }
    }
}
