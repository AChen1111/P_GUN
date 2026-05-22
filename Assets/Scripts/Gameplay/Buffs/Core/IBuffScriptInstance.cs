using System;

namespace Game.Gameplay
{
    /// <summary>
    /// Buff 脚本实例抽象, 让玩法程序集不直接依赖 xLua.
    /// </summary>
    public interface IBuffScriptInstance : IDisposable
    {
        /// <summary>
        /// 调用脚本添加回调.
        /// </summary>
        /// <param name="info">Buff 运行时信息.</param>
        void OnAdd(BuffRuntimeInfo info);

        /// <summary>
        /// 调用脚本移除回调.
        /// </summary>
        /// <param name="info">Buff 运行时信息.</param>
        void OnRemove(BuffRuntimeInfo info);

        /// <summary>
        /// 调用脚本每帧更新回调.
        /// </summary>
        /// <param name="info">Buff 运行时信息.</param>
        /// <param name="deltaTime">时间增量.</param>
        void OnUpdate(BuffRuntimeInfo info, float deltaTime);

        /// <summary>
        /// 调用脚本固定间隔回调.
        /// </summary>
        /// <param name="info">Buff 运行时信息.</param>
        void OnInterval(BuffRuntimeInfo info);

        /// <summary>
        /// 调用脚本主动触发回调.
        /// </summary>
        /// <param name="info">Buff 运行时信息.</param>
        void OnTrigger(BuffRuntimeInfo info);
    }
}
