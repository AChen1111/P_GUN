using Game.Pooling;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Core;
using UnityEngine;

namespace Game.Items
{
    /// <summary>
    /// 玩家背包, 只保存物品 id 和数量; 使用时从对象池取出效果预制体并调用它的 Lua 模块.
    /// </summary>
    [DisallowMultipleComponent]
    public class PlayerInventory : MonoBehaviour
    {
        // 效果预制体和图标缓存的是资源而非场景实例, 跨场景保留是安全的.
        private static readonly Dictionary<int, LuaBehaviourHost> effectPrefabsById = new Dictionary<int, LuaBehaviourHost>();

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
            var config = LuaDataRuntime.GetItemConfig(itemId);
            PreloadItemResourcesAsync(itemId, config);

            if (!stacksById.TryGetValue(itemId, out var stack))
            {
                stack = new InventoryItemStack(itemId, BuildDisplayData(itemId, config));
                stacksById.Add(itemId, stack);
                orderedStacks.Add(stack);
            }

            stack.Add();
            EventCenter.Trigger(ItemEvents.InventoryChanged);
            return true;
        }

        public bool TryGetStack(int itemId, out InventoryItemStack stack)
        {
            return stacksById.TryGetValue(itemId, out stack);
        }

        /// <summary>
        /// 使用一个物品: 取出效果预制体, 先问 CanUse, 再执行 OnPick, 最后回池并消耗数量.
        /// </summary>
        public bool Use(int itemId)
        {
            if (!stacksById.TryGetValue(itemId, out var stack) || stack.Count <= 0)
            {
                return false;
            }

            if (!effectPrefabsById.TryGetValue(itemId, out var prefab))
            {
                EventCenter.Trigger(CoreEvents.PlayerHeadMessageRequested, new PlayerHeadMessageEvent("道具效果未就绪", 1.5f));
                return false;
            }

            var pool = ItemEffectPool.Instance;
            if (pool == null)
            {
                Debug.LogError($"{nameof(PlayerInventory)}: {nameof(ItemEffectPool)} 必须摆放在游戏场景中.", this);
                return false;
            }

            var effect = pool.Get(prefab);
            if (effect == null)
            {
                Debug.LogError($"{nameof(PlayerInventory)}: 取出道具效果预制体失败, itemId={itemId}.", this);
                return false;
            }

            effect.SetLuaField("itemId", itemId);
            effect.SetLuaField("sourceObject", gameObject);
            effect.SetLuaField("sourcePosition", transform.position);

            var canUse = effect.CallLuaFunctionBool("CanUse");
            if (!canUse)
            {
                pool.Release(effect);
                EventCenter.Trigger(CoreEvents.PlayerHeadMessageRequested, new PlayerHeadMessageEvent("当前无法使用", 1.5f));
                return false;
            }

            if (!effect.HasLuaFunction("OnPick"))
            {
                pool.Release(effect);
                Debug.LogError($"{nameof(PlayerInventory)}: 道具效果模块缺少 OnPick, itemId={itemId}.", this);
                return false;
            }

            effect.CallLuaFunction("OnPick");
            pool.Release(effect);

            stack.TryConsumeOne();
            if (stack.Count <= 0)
            {
                stacksById.Remove(itemId);
                orderedStacks.Remove(stack);
            }

            EventCenter.Trigger(ItemEvents.InventoryChanged);
            return true;
        }

        public void Clear()
        {
            stacksById.Clear();
            orderedStacks.Clear();
            EventCenter.Trigger(ItemEvents.InventoryChanged);
        }

        /// <summary>
        /// 读档恢复物品堆叠, 效果预制体按 ItemData 的地址预载.
        /// </summary>
        public bool RestoreStack(int itemId, int count)
        {
            if (count <= 0) return false;

            var config = LuaDataRuntime.GetItemConfig(itemId);
            PreloadItemResourcesAsync(itemId, config);

            var stack = new InventoryItemStack(itemId, BuildDisplayData(itemId, config));
            for (var i = 0; i < count; i++)
            {
                stack.Add();
            }

            stacksById[itemId] = stack;
            orderedStacks.Add(stack);
            EventCenter.Trigger(ItemEvents.InventoryChanged);
            return true;
        }

        /// <summary>
        /// 构建显示数据, 图标来自按 id 缓存的 Sprite.
        /// </summary>
        private static ItemData BuildDisplayData(int itemId, ItemConfig config)
        {
            return new ItemData(itemId, config.Name, config.Description, ItemSpriteCache.GetSprite(itemId));
        }

        /// <summary>
        /// 预载道具图标和效果预制体, 完成后触发一次背包刷新让图标就位.
        /// </summary>
        private static async void PreloadItemResourcesAsync(int itemId, ItemConfig config)
        {
            try
            {
                var loader = AddressableLoader.Instance;
                if (loader == null)
                {
                    throw new InvalidOperationException($"{nameof(AddressableLoader)} 必须先初始化.");
                }

                if (!string.IsNullOrEmpty(config.IconAddress))
                {
                    var sprite = await loader.LoadAssetAsync<Sprite>(config.IconAddress);
                    ItemSpriteCache.SetSprite(itemId, sprite);
                }

                if (!effectPrefabsById.ContainsKey(itemId))
                {
                    var prefabGameObject = await loader.LoadAssetAsync<GameObject>(config.EffectPrefabAddress);
                    var prefab = prefabGameObject != null ? prefabGameObject.GetComponent<LuaBehaviourHost>() : null;
                    if (prefab == null)
                    {
                        throw new InvalidOperationException($"道具效果预制体缺少 LuaComponet, 地址: {config.EffectPrefabAddress}.");
                    }

                    effectPrefabsById[itemId] = prefab;
                }

                EventCenter.Trigger(ItemEvents.InventoryChanged);
            }
            catch (Exception exception)
            {
                Debug.LogError($"{nameof(PlayerInventory)}: 道具资源预载失败, itemId={itemId}, Error: {exception.Message}");
            }
        }
    }
}