using Game.Core;

namespace Game.Items
{
    public static class ItemEvents
    {
        public static readonly GameEventId<ItemData> ItemTipShown = new GameEventId<ItemData>(nameof(ItemTipShown));
        public static readonly GameEventId ItemTipHidden = new GameEventId(nameof(ItemTipHidden));
        public static readonly GameEventId<Item> ItemPicked = new GameEventId<Item>(nameof(ItemPicked));
        public static readonly GameEventId InventoryChanged = new GameEventId(nameof(InventoryChanged));
    }
}
