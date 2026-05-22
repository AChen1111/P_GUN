namespace Game.Core
{
    public static class CoreEvents
    {
        public static readonly GameEventId PlayerDied = new GameEventId(nameof(PlayerDied));
        public static readonly GameEventId GameWin = new GameEventId(nameof(GameWin));
        public static readonly GameEventId GameOver = new GameEventId(nameof(GameOver));
        public static readonly GameEventId MiniMapToggleRequested = new GameEventId(nameof(MiniMapToggleRequested));
        public static readonly GameEventId MiniMapShown = new GameEventId(nameof(MiniMapShown));
        public static readonly GameEventId MiniMapHidden = new GameEventId(nameof(MiniMapHidden));
        public static readonly GameEventId AllRoomsGenerated = new GameEventId(nameof(AllRoomsGenerated));
        public static readonly GameEventId<PlayerHeadMessageEvent> PlayerHeadMessageRequested = new GameEventId<PlayerHeadMessageEvent>(nameof(PlayerHeadMessageRequested));
    }

    public struct RoomWaveDisplayEvent
    {
        // 当前显示的波次, 从 1 开始.
        public int CurrentWave;
        // 当前房间总波数, 用于显示 x/xx.
        public int TotalWave;
        // 战斗结束时隐藏波数文本.
        public bool IsVisible;

        public RoomWaveDisplayEvent(int currentWave, int totalWave, bool isVisible)
        {
            CurrentWave = currentWave;
            TotalWave = totalWave;
            IsVisible = isVisible;
        }
    }

    public struct PlayerHeadMessageEvent
    {
        public string Message;
        public float Duration;

        public PlayerHeadMessageEvent(string message, float duration)
        {
            Message = message;
            Duration = duration;
        }
    }
}
