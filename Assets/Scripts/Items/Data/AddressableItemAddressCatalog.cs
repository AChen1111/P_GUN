using System.Collections.Generic;

namespace Game.Items
{
    /// <summary>
    /// 物品 Addressables 地址表, 用于只有 itemId 的存档和旧配置解析运行时预制体.
    /// </summary>
    public static class AddressableItemAddressCatalog
    {
        private static readonly Dictionary<int, string> AddressesByItemId = new Dictionary<int, string>
        {
            { 1, "item/heart" },
            { 2, "item/harm_up" },
            { 3, "item/speed_up" },
            { 4, "item/power_up" },
            { 5, "item/purify" }
        };

        /// <summary>
        /// 通过 itemId 查询 Addressables 地址.
        /// </summary>
        public static bool TryGetAddress(int itemId, out string address)
        {
            return AddressesByItemId.TryGetValue(itemId, out address);
        }
    }
}
