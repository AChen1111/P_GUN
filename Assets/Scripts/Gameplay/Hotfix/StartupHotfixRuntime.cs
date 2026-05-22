using System;
using System.Threading.Tasks;

namespace Game.Gameplay
{
    /// <summary>
    /// 启动热修运行时入口, 由 LuaManager 在 Root 场景注册具体执行器.
    /// </summary>
    public static class StartupHotfixRuntime
    {
        public static IStartupHotfixRunner Runner { get; private set; }

        /// <summary>
        /// 注册启动热修执行器.
        /// </summary>
        /// <param name="runner">启动热修执行器.</param>
        public static void RegisterRunner(IStartupHotfixRunner runner)
        {
            Runner = runner;
        }

        /// <summary>
        /// 仅当注册者一致时注销启动热修执行器.
        /// </summary>
        /// <param name="runner">请求注销的执行器.</param>
        public static void UnregisterRunner(IStartupHotfixRunner runner)
        {
            if (Runner == runner)
            {
                Runner = null;
            }
        }

        /// <summary>
        /// 执行 Root 阶段启动热修脚本.
        /// </summary>
        /// <returns>异步任务.</returns>
        public static Task ExecuteStartupHotfixAsync()
        {
            if (Runner == null)
            {
                throw new InvalidOperationException("Root 场景未注册启动热修执行器.");
            }

            return Runner.ExecuteStartupHotfixAsync();
        }
    }
}
