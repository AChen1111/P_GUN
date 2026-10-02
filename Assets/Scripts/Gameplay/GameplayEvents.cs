using Game.Core;

namespace Game.Gameplay
{
    public static class GameplayEvents
    {
        // 生成完成, 进房及读档更新时通知 HUD 重建局部地图.
        public static readonly GameEventId LocalMinimapChanged = new GameEventId(nameof(LocalMinimapChanged));
        public static readonly GameEventId<Player> PlayerHPChanged = new GameEventId<Player>(nameof(PlayerHPChanged));
        public static readonly GameEventId PlayerBuffsChanged = new GameEventId(nameof(PlayerBuffsChanged));
        public static readonly GameEventId<GunClip> BulletClipChanged = new GameEventId<GunClip>(nameof(BulletClipChanged));
        public static readonly GameEventId<BulletBag> BulletBagChanged = new GameEventId<BulletBag>(nameof(BulletBagChanged));
        public static readonly GameEventId<RoomWaveDisplayEvent> RoomWaveDisplayChanged = new GameEventId<RoomWaveDisplayEvent>(nameof(RoomWaveDisplayChanged));
        public static readonly GameEventId<Door> DoorOpened = new GameEventId<Door>(nameof(DoorOpened));
        public static readonly GameEventId<Door> DoorClosed = new GameEventId<Door>(nameof(DoorClosed));
        public static readonly GameEventId<EnemyDefeatedEvent> EnemyDefeated = new GameEventId<EnemyDefeatedEvent>(nameof(EnemyDefeated));
    }

    /// <summary>
    /// 敌人被击杀的事件载荷, 供吸血和屠戮类 Buff 回调使用.
    /// </summary>
    public class EnemyDefeatedEvent
    {
        public EnemyDefeatedEvent(EnemyBase enemy)
        {
            Enemy = enemy;
        }

        public EnemyBase Enemy { get; }
    }
}