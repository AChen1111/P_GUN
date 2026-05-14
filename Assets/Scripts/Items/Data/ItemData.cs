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

        public Sprite icon;

        public ItemData(int itemId, string itemName, string description, Sprite icon)
        {
            this.itemId = itemId;
            this.itemName = itemName;
            this.description = description;
            this.icon = icon;
        }
    }
}
