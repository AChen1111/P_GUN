using System;
using System.Threading.Tasks;
using Game.Core;
using System.Collections.Generic;
using UnityEngine;
using XLua;

/// <summary>
/// 框架 Lua 管理器, 负责初始化 Lua 环境并把生命周期转发到 Main.lua.
/// 实例来自 Root 场景的显式摆放, 不在代码里自动创建.
/// </summary>
[DefaultExecutionOrder(-200)]
public class LuaManager : PersistentMonoSingleton<LuaManager>
{
    private LuaEnvironment m_luaEnvironment;
    private LuaTable m_mainLuaTable;
    private LuaEnv m_luaEnv;

    // 负责获取对应类型的 LuaTable.
    private Func<string, GameObject, LuaTable> m_onInit;
    // 负责运行时重新加载.
    private Action<string> m_onRuntimeReload;
    // 负责运行时重新加载全部模块.
    private Action m_onRuntimeReloadAll;
    // 负责获取全部模块名.
    private Func<LuaTable> m_onGetModuleNames;

    /// <summary>
    /// 当前 Lua 环境, 供数据读取与热修入口使用.
    /// </summary>
    public LuaEnv LuaEnv => m_luaEnv;

    protected override void OnInit()
    {
        // Root 完成目录与 LuaBundle 预加载后再创建 Lua 环境.
    }

    public Task InitializeAsync()
    {
        if (IsDone) return Task.CompletedTask;
        AddressableLoader.Instance.GetLoadedAsset<TextAsset>("LuaBundle");
        // 初始化 Lua 环境.
        m_luaEnvironment = new LuaEnvironment();
        m_luaEnvironment.Init();
        m_luaEnv = m_luaEnvironment.LuaEnv;

        // 加载 Main.lua.
        m_luaEnv.DoString("require 'Main'");
        m_mainLuaTable = m_luaEnv.Global.Get<LuaTable>("Main");

        // 注册初始化函数.
        m_onInit = m_mainLuaTable.Get<Func<string, GameObject, LuaTable>>("Init");
        m_onRuntimeReload = m_mainLuaTable.Get<Action<string>>("runtimeReload");
        m_onRuntimeReloadAll = m_mainLuaTable.Get<Action>("runtimeReloadAll");
        m_onGetModuleNames = m_mainLuaTable.Get<Func<LuaTable>>("getModuleNames");

        IsDone = true;
        return Task.CompletedTask;
    }

    private void Update()
    {
        // 每帧驱动 Lua GC, 避免长时间运行后内存增长.
        m_luaEnv?.Tick();
    }

    /// <summary>
    /// 获取对应类型的 LuaTable.
    /// </summary>
    public LuaTable GetLuaTable(string typeName, GameObject gameObject)
    {
        if (m_onInit == null)
        {
            Debug.LogError("[LuaManager] Main.lua 未提供 Init 函数, 无法创建 Lua 实例.");
            return null;
        }

        return m_onInit(typeName, gameObject);
    }

    /// <summary>
    /// 运行时重新加载指定模块.
    /// </summary>
    public void RuntimeReload(string typeName)
    {
        // 重载前刷新文件索引, 保证运行期间新增的 .lua 能被 require 到.
        m_luaEnvironment.BuildFileIndex();
        try
        {
            m_onRuntimeReload?.Invoke(typeName);
        }
        catch (Exception e)
        {
            // 在工程脚本里打日志, Console 双击才会走 OnOpenAsset, 从而跳到真正的 .lua.
            Debug.LogException(e);
        }
    }

    /// <summary>
    /// 运行时重新加载全部模块.
    /// </summary>
    public void RuntimeReloadAll()
    {
        m_luaEnvironment.BuildFileIndex();
        m_onRuntimeReloadAll?.Invoke();
    }

    /// <summary>
    /// 获取全部模块名, 供 Editor 侧列出.
    /// </summary>
    public List<string> GetModuleNames()
    {
        var names = new List<string>();
        using (LuaTable table = m_onGetModuleNames())
        {
            for (int i = 1; i <= table.Length; i++)
            {
                names.Add(table.Get<int, string>(i));
            }
        }

        return names;
    }
}
