using System.Collections.Generic;
using UnityEngine;

namespace QFramework.PG
{
    /// <summary>
    /// 宝箱类效果：在触发位置随机生成一个道具预制体。
    /// </summary>
    [CreateAssetMenu(fileName = "ChestRandomLootEffect", menuName = "PG/Item/Effects/Chest Random Loot", order = 2)]
    public class ChestRandomLootEffect : ItemEffectBase
    {
        [Tooltip("掉落物预制体列表，随机抽取一个生成")]
        [SerializeField] private List<GameObject> lootTable = new List<GameObject>();

        public override void OnPick(ItemEffectContext ctx)
        {
            var mgr = ItemWorldManager.Instance;
            if (mgr == null)
            {
                Debug.LogError($"{nameof(ChestRandomLootEffect)}: {nameof(ItemWorldManager)} 未初始化。");
                return;
            }

            if (lootTable == null || lootTable.Count == 0)
            {
                Debug.LogWarning($"{nameof(ChestRandomLootEffect)}: 没有可用的掉落配置。");
                return;
            }

            var drop = lootTable.GetRandomItem();
            if (drop == null) return;

            var newItemPosition = ctx.WorldPosition + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
            mgr.SpawnItemDelay(drop, newItemPosition, 0.1f, isActive: true, AnimType.Jump);
        }
    }
}
