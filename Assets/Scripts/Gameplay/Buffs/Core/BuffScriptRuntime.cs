namespace Game.Gameplay
{
    /// <summary>
    /// Buff 脚本运行时入口, 负责隔离玩法程序集和具体 Lua 实现.
    /// </summary>
    public static class BuffScriptRuntime
    {
        public static IBuffScriptFactory Factory { get; private set; }

        /// <summary>
        /// 注册当前脚本工厂.
        /// </summary>
        /// <param name="factory">脚本工厂.</param>
        public static void RegisterFactory(IBuffScriptFactory factory)
        {
            Factory = factory;
        }

        /// <summary>
        /// 仅当注册者一致时清理脚本工厂, 避免旧实例覆盖新实例状态.
        /// </summary>
        /// <param name="factory">请求注销的脚本工厂.</param>
        public static void UnregisterFactory(IBuffScriptFactory factory)
        {
            if (Factory == factory)
            {
                Factory = null;
            }
        }
    }
}
