using UnityEngine;

namespace Game.Items
{
    /// <summary>
    /// 背包中同一种物品的运行时堆叠数据, 只保留 id, 数量和显示信息.
    /// </summary>
    public sealed class InventoryItemStack
    {
        public int ItemId { get; }
        public ItemData Data { get; private set; }
        public int Count { get; private set; }

        public InventoryItemStack(int itemId, ItemData data)
        {
            ItemId = itemId;
            Data = data;
            Count = 0;
        }

        /// <summary>
        /// 追加一个数量.
        /// </summary>
        public void Add()
        {
            Count++;
        }

        public bool TryConsumeOne()
        {
            if (Count <= 0)
            {
                return false;
            }

            Count--;
            return true;
        }
    }
}