using Game.Core;

namespace Game.Gameplay
{
    public static class GameplayEvents
    {
        public static readonly GameEventId<Player> PlayerHPChanged = new GameEventId<Player>(nameof(PlayerHPChanged));
        public static readonly GameEventId PlayerBuffsChanged = new GameEventId(nameof(PlayerBuffsChanged));
        public static readonly GameEventId<GunClip> BulletClipChanged = new GameEventId<GunClip>(nameof(BulletClipChanged));
        public static readonly GameEventId<BulletBag> BulletBagChanged = new GameEventId<BulletBag>(nameof(BulletBagChanged));
        public static readonly GameEventId<RoomWaveDisplayEvent> RoomWaveDisplayChanged = new GameEventId<RoomWaveDisplayEvent>(nameof(RoomWaveDisplayChanged));
        public static readonly GameEventId<Door> DoorOpened = new GameEventId<Door>(nameof(DoorOpened));
        public static readonly GameEventId<Door> DoorClosed = new GameEventId<Door>(nameof(DoorClosed));
    }
}
