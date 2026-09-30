using UnityEngine.Serialization;
using System;
using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;

namespace Game.Items
{
    [Serializable]
    public struct ItemData
    {
        public int itemId;
        public string itemName;

        [TextArea(2, 4)]
        public string description;

        // 配置保存短名, 资源来自阶段预加载缓存.

        [SerializeField, AddressableKey(AddressableAssetKind.Sprite)] public string iconKey;
        public Sprite icon => AddressableAssetAccess.Get<Sprite>(iconKey);

        public ItemData(int itemId, string itemName, string description, string iconKey)
        {
            this.itemId = itemId;
            this.itemName = itemName;
            this.description = description;
            this.iconKey = iconKey;
        }
    }
}
