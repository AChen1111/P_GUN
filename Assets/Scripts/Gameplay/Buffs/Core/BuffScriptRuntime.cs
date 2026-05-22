using System;

namespace Game.Gameplay
{
    /// <summary>
    /// Buff 脚本运行时入口, 用创建委托隔离玩法程序集和具体 Lua 实现.
    /// </summary>
    public static class BuffScriptRuntime
    {
        public static Func<Buff, IBuffScriptInstance> Factory { get; private set; }

        /// <summary>
        /// 注册当前脚本实例创建委托.
        /// </summary>
        /// <param name="factory">脚本实例创建委托.</param>
        public static void RegisterFactory(Func<Buff, IBuffScriptInstance> factory)
        {
            Factory = factory;
        }

        /// <summary>
        /// 仅当注册者一致时清理脚本委托, 避免旧实例覆盖新实例状态.
        /// </summary>
        /// <param name="factory">请求注销的脚本实例创建委托.</param>
        public static void UnregisterFactory(Func<Buff, IBuffScriptInstance> factory)
        {
            if (Factory == factory)
            {
                Factory = null;
            }
        }
    }
}
