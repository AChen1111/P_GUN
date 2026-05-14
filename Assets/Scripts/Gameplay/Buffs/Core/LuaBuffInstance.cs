using System;
using UnityEngine;
using XLua;

/// <summary>
/// 单个 Buff 的 Lua 方法缓存, 负责把 C# 生命周期转发到 Lua.
/// </summary>
public sealed class LuaBuffInstance : IDisposable
{
    private readonly Buff ownerBuff;
    private LuaFunction onAdd;
    private LuaFunction onRemove;
    private LuaFunction onUpdate;
    private LuaFunction onInterval;
    private LuaFunction onTrigger;

    private bool isDisposed;

    public LuaBuffInstance(Buff ownerBuff, LuaTable table)
    {
        this.ownerBuff = ownerBuff;

        if (table == null)
        {
            Debug.LogError($"{nameof(LuaBuffInstance)}: Lua table 为空, Buff: {ownerBuff?.BuffName}.");
            return;
        }

        onAdd = table.Get<LuaFunction>("OnAdd");
        onRemove = table.Get<LuaFunction>("OnRemove");
        onUpdate = table.Get<LuaFunction>("OnUpdate");
        onInterval = table.Get<LuaFunction>("OnInterval");
        onTrigger = table.Get<LuaFunction>("OnTrigger");
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
        SafeCall(onUpdate, nameof(OnUpdate), info, deltaTime);
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
        onAdd?.Dispose();
        onRemove?.Dispose();
        onUpdate?.Dispose();
        onInterval?.Dispose();
        onTrigger?.Dispose();
        onAdd = null;
        onRemove = null;
        onUpdate = null;
        onInterval = null;
        onTrigger = null;
    }

    private void SafeCall(LuaFunction luaFunction, string methodName, params object[] args)
    {
        if (isDisposed || luaFunction == null) return;

        try
        {
            luaFunction.Call(args);
        }
        catch (Exception exception)
        {
            Debug.LogError($"{nameof(LuaBuffInstance)}: 调用 {methodName} 失败, Buff: {ownerBuff?.BuffName}, Error: {exception.Message}.");
        }
    }
}
