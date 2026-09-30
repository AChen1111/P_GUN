using System.Collections.Generic;

namespace Game.Items
{
    /// <summary>
    /// 物品预制体短名表, 用于只有 itemId 的存档解析运行时预制体.
    /// </summary>
    public static class AddressableItemAddressCatalog
    {
        private static readonly Dictionary<int, string> KeysByItemId = new Dictionary<int, string>
        {
            { 1, "Heart" },
            { 2, "HarmUp" },
            { 3, "SpeedUp" },
            { 4, "PowerUp" },
            { 5, "Purify" },
            { 6, "HeartAdd" }
        };

        /// <summary>
        /// 通过 itemId 查询预制体短名.
        /// </summary>
        public static bool TryGetKey(int itemId, out string key)
        {
            return KeysByItemId.TryGetValue(itemId, out key);
        }
    }
}
