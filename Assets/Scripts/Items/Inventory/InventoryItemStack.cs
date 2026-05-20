using System.Collections.Generic;
using UnityEngine;

namespace Game.Items
{
    /// <summary>
    /// 背包中同一种物品的运行时堆叠数据.
    /// </summary>
    public sealed class InventoryItemStack
    {
        private readonly List<ItemEffectBase> effects;

        public int ItemId { get; }
        public ItemData Data { get; private set; }
        public int Count { get; private set; }
        public IReadOnlyList<ItemEffectBase> Effects => effects;

        public InventoryItemStack(int itemId, ItemData data, IEnumerable<ItemEffectBase> sourceEffects)
        {
            ItemId = itemId;
            Data = data;
            Count = 0;
            effects = new List<ItemEffectBase>();

            if (sourceEffects != null)
            {
                foreach (var effect in sourceEffects)
                {
                    if (effect != null)
                    {
                        effects.Add(effect);
                    }
                }
            }
        }

        /// <summary>
        /// 执行 Add 逻辑.
        /// </summary>
        public void Add(ItemData data, IEnumerable<ItemEffectBase> sourceEffects)
        {
            // 每次拾取刷新展示数据, 允许数据库或 prefab 配置更新后立即反映到背包.
            Data = data;
            Count++;

            if (effects.Count > 0 || sourceEffects == null)
            {
                return;
            }

            foreach (var effect in sourceEffects)
            {
                if (effect != null)
                {
                    effects.Add(effect);
                }
            }
        }

        /// <summary>
        /// 执行 TryConsumeOne 逻辑.
        /// </summary>
        public bool TryConsumeOne()
        {
            if (Count <= 0)
            {
                return false;
            }

            Count--;
            return true;
        }

        /// <summary>
        /// 执行 GetUsableEffects 逻辑.
        /// </summary>
        public List<ItemEffectBase> GetUsableEffects(ItemEffectContext ctx)
        {
            var usableEffects = new List<ItemEffectBase>();
            for (int i = 0; i < effects.Count; i++)
            {
                var effect = effects[i];
                if (effect != null && effect.CanUse(ctx))
                {
                    usableEffects.Add(effect);
                }
            }

            return usableEffects;
        }
    }
}
