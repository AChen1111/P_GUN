namespace Game.Gameplay
{
    public static class PlayerRegistry
    {
        // 当前场景玩家由 Player 生命周期注册, 避免使用含义过宽的 Global 容器.
        public static Player Current { get; private set; }

        public static void Register(Player player)
        {
            Current = player;
        }

        public static void Unregister(Player player)
        {
            if (Current == player)
            {
                Current = null;
            }
        }
    }
}
