using System.Threading.Tasks;

namespace Game.Gameplay
{
    /// <summary>
    /// 启动热修执行器抽象, 让 Root 启动流程不直接依赖 xLua.
    /// </summary>
    public interface IStartupHotfixRunner
    {
        /// <summary>
        /// 执行启动热修入口脚本.
        /// </summary>
        /// <returns>异步任务.</returns>
        Task ExecuteStartupHotfixAsync();
    }
}
