using System.Collections.Generic;
using UnityEngine;

namespace Game.Items
{
    /// <summary>
    /// 物品图标缓存, 按 itemId 保存异步加载好的 Sprite 资源.
    /// </summary>
    public static class ItemSpriteCache
    {
        private static readonly Dictionary<int, Sprite> spritesById = new Dictionary<int, Sprite>();

        /// <summary>
        /// 写入已加载的图标资源.
        /// </summary>
        public static void SetSprite(int itemId, Sprite sprite)
        {
            if (sprite == null) return;
            spritesById[itemId] = sprite;
        }

        /// <summary>
        /// 读取已缓存的图标, 尚未加载完成时返回 null.
        /// </summary>
        public static Sprite GetSprite(int itemId)
        {
            return spritesById.TryGetValue(itemId, out var sprite) ? sprite : null;
        }
    }
}