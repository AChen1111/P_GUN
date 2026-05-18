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

        public static void SetRuntimeDatabase(ItemDatabase database)
        {
            // DataBaseManager 加载 Addressables 后写入当前数据库, Item 模块无需反向依赖 Gameplay.
            RuntimeDatabase = database;
        }

        public static void ClearRuntimeDatabase(ItemDatabase database)
        {
            if (RuntimeDatabase == database)
            {
                RuntimeDatabase = null;
            }
        }

        public void ReplaceItems(IEnumerable<ItemData> newItems)
        {
            ReplaceData(items, newItems);
        }

        protected override List<ItemData> DataValues => items;

        protected override bool TryGetKey(ItemData data, out int key)
        {
            key = data.itemId;
            return true;
        }
    }
}
