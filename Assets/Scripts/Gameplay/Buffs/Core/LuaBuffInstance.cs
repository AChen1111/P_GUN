using System;
using UnityEngine;
using XLua;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    /// <summary>
    /// 单个 Buff 的 Lua 方法缓存, 负责把 C# 生命周期转发到 Lua.
    /// </summary>
    public sealed class LuaBuffInstance : IDisposable
    {
        private readonly Buff ownerBuff;
        private Action<BuffRuntimeInfo> onAdd;
        private Action<BuffRuntimeInfo> onRemove;
        private Action<BuffRuntimeInfo, float> onUpdate;
        private Action<BuffRuntimeInfo> onInterval;
        private Action<BuffRuntimeInfo> onTrigger;

        private bool isDisposed;

        public LuaBuffInstance(Buff ownerBuff, LuaTable table)
        {
            this.ownerBuff = ownerBuff;

            if (table == null)
            {
                Debug.LogError($"{nameof(LuaBuffInstance)}: Lua table 为空, Buff: {ownerBuff?.BuffName}.");
                return;
            }

            onAdd = table.Get<Action<BuffRuntimeInfo>>("OnAdd");
            onRemove = table.Get<Action<BuffRuntimeInfo>>("OnRemove");
            onUpdate = table.Get<Action<BuffRuntimeInfo, float>>("OnUpdate");
            onInterval = table.Get<Action<BuffRuntimeInfo>>("OnInterval");
            onTrigger = table.Get<Action<BuffRuntimeInfo>>("OnTrigger");
        }

        /// <summary>
        /// 调用 Lua 的添加方法.
        /// </summary>
        /// <param name="info">Buff 运行时信息.</param>
        public void OnAdd(BuffRuntimeInfo info)
        {
            SafeCall(onAdd, nameof(OnAdd), info);
        }

        /// <summary>
        /// 调用 Lua 的移除方法.
        /// </summary>
        /// <param name="info">Buff 运行时信息.</param>
        public void OnRemove(BuffRuntimeInfo info)
        {
            SafeCall(onRemove, nameof(OnRemove), info);
        }

        /// <summary>
        /// 调用 Lua 的每帧更新方法.
        /// </summary>
        /// <param name="info">Buff 运行时信息.</param>
        /// <param name="deltaTime">时间增量.</param>
        public void OnUpdate(BuffRuntimeInfo info, float deltaTime)
        {
            if (isDisposed || onUpdate == null) return;

            try
            {
                onUpdate.Invoke(info, deltaTime);
            }
            catch (Exception exception)
            {
                Debug.LogError($"{nameof(LuaBuffInstance)}: 调用 {nameof(OnUpdate)} 失败, Buff: {ownerBuff?.BuffName}, Error: {exception.Message}.");
            }
        }

        /// <summary>
        /// 调用 Lua 的固定间隔方法.
        /// </summary>
        /// <param name="info">Buff 运行时信息.</param>
        public void OnInterval(BuffRuntimeInfo info)
        {
            SafeCall(onInterval, nameof(OnInterval), info);
        }

        /// <summary>
        /// 调用 Lua 的主动触发方法.
        /// </summary>
        /// <param name="info">Buff 运行时信息.</param>
        public void OnTrigger(BuffRuntimeInfo info)
        {
            SafeCall(onTrigger, nameof(OnTrigger), info);
        }

        public void Dispose()
        {
            isDisposed = true;
            onAdd = null;
            onRemove = null;
            onUpdate = null;
            onInterval = null;
            onTrigger = null;
        }

        private void SafeCall(Action<BuffRuntimeInfo> luaAction, string methodName, BuffRuntimeInfo info)
        {
            if (isDisposed || luaAction == null) return;

            try
            {
                luaAction.Invoke(info);
            }
            catch (Exception exception)
            {
                Debug.LogError($"{nameof(LuaBuffInstance)}: 调用 {methodName} 失败, Buff: {ownerBuff?.BuffName}, Error: {exception.Message}.");
            }
        }
    }
}
