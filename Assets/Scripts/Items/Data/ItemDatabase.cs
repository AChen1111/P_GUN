using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;

namespace Game.Items
{
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "PG/Item/Item Database", order = 0)]
    public class ItemDatabase : ScriptableObjectDatabase<ItemDatabase, int, ItemData>
    {
        [SerializeField] private List<ItemData> items = new List<ItemData>();

        public static ItemDatabase RuntimeDatabase { get; private set; }

        public IReadOnlyList<ItemData> Items => items;

        /// <summary>
        /// 执行 SetRuntimeDatabase 逻辑.
        /// </summary>
        public static void SetRuntimeDatabase(ItemDatabase database)
        {
            // DataBaseManager 加载 Addressables 后写入当前数据库, Item 模块无需反向依赖 Gameplay.
            RuntimeDatabase = database;
        }

        /// <summary>
        /// 执行 ClearRuntimeDatabase 逻辑.
        /// </summary>
        public static void ClearRuntimeDatabase(ItemDatabase database)
        {
            if (RuntimeDatabase == database)
            {
                RuntimeDatabase = null;
            }
        }

        /// <summary>
        /// 执行 ReplaceItems 逻辑.
        /// </summary>
        public void ReplaceItems(IEnumerable<ItemData> newItems)
        {
            ReplaceData(items, newItems);
        }

        protected override List<ItemData> DataValues => items;

        /// <summary>
        /// 执行 TryGetKey 逻辑.
        /// </summary>
        protected override bool TryGetKey(ItemData data, out int key)
        {
            key = data.itemId;
            return true;
        }
    }
}
