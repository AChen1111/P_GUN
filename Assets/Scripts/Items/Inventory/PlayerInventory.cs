using System.Collections.Generic;
using Game.Core;
using UnityEngine;

namespace Game.Items
{
    /// <summary>
    /// 玩家背包, 负责拾取入包, 同类堆叠和主动使用.
    /// </summary>
    [DisallowMultipleComponent]
    public class PlayerInventory : MonoBehaviour
    {
        private readonly Dictionary<int, InventoryItemStack> stacksById = new Dictionary<int, InventoryItemStack>();
        private readonly List<InventoryItemStack> orderedStacks = new List<InventoryItemStack>();

        public IReadOnlyList<InventoryItemStack> Items => orderedStacks;
        public bool AddFromItem(Item item)
        {
            if (item == null)
            {
                Debug.LogError("背包添加失败, Item为空.", this);
                return false;
            }

            var itemId = item.ItemId;
            if (!item.TryGetItemData(out var data))
            {
                Debug.LogError($"背包添加失败, ItemDatabase缺少 itemId={itemId} 的物品数据.", item);
                return false;
            }

            var effects = item.Effects;

            if (!stacksById.TryGetValue(itemId, out var stack))
            {
                stack = new InventoryItemStack(itemId, data, effects);
                stacksById.Add(itemId, stack);
                orderedStacks.Add(stack);
            }

            stack.Add(data, effects);
            EventCenter.Trigger(ItemEvents.InventoryChanged);
            return true;
        }
        public bool TryGetStack(int itemId, out InventoryItemStack stack)
        {
            return stacksById.TryGetValue(itemId, out stack);
        }
        public bool Use(int itemId)
        {
            if (!stacksById.TryGetValue(itemId, out var stack) || stack.Count <= 0)
            {
                return false;
            }

            var ctx = BuildUseContext();
            var usableEffects = stack.GetUsableEffects(ctx);
            if (usableEffects.Count == 0)
            {
                EventCenter.Trigger(CoreEvents.PlayerHeadMessageRequested, new PlayerHeadMessageEvent("当前无法使用", 1.5f));
                return false;
            }

            for (int i = 0; i < usableEffects.Count; i++)
            {
                usableEffects[i].OnPick(ctx);
            }

            stack.TryConsumeOne();
            if (stack.Count <= 0)
            {
                stacksById.Remove(itemId);
                orderedStacks.Remove(stack);
            }

            EventCenter.Trigger(ItemEvents.InventoryChanged);
            return true;

            ItemEffectContext BuildUseContext()
            {
                // 背包使用时以玩家自身作为效果来源, 避免引用已回收的拾取物对象.
                return new ItemEffectContext
                {
                    SourceObject = gameObject,
                    WorldPosition = transform.position
                };
            }
}
        public void Clear()
        {
            stacksById.Clear();
            orderedStacks.Clear();
            EventCenter.Trigger(ItemEvents.InventoryChanged);
        }
        public bool RestoreStack(int itemId, int count, ItemDatabase database, IEnumerable<ItemEffectBase> effects)
        {
            if (count <= 0) return false;

            var sourceDatabase = database != null ? database : ItemDatabase.RuntimeDatabase;
            if (sourceDatabase == null || !sourceDatabase.TryGetById(itemId, out var data))
            {
                Debug.LogError($"背包恢复失败, ItemDatabase缺少 itemId={itemId} 的物品数据.", this);
                return false;
            }

            var stack = new InventoryItemStack(itemId, data, effects);
            for (var i = 0; i < count; i++)
            {
                stack.Add(data, effects);
            }

            stacksById[itemId] = stack;
            orderedStacks.Add(stack);
            EventCenter.Trigger(ItemEvents.InventoryChanged);
            return true;
        }
    }
}
