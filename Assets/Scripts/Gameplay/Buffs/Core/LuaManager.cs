using System;
using System.Collections.Generic;
using UnityEngine;
using XLua;

/// <summary>
/// Lua 全局管理器, 负责创建 Lua 环境并缓存 Buff 脚本 table.
/// </summary>
public sealed class LuaManager : MonoBehaviour
{
    private readonly Dictionary<TextAsset, LuaTable> buffTableCache = new Dictionary<TextAsset, LuaTable>();

    private LuaEnv luaEnv;

    public static LuaManager Instance { get; private set; }

    /// <summary>
    /// 获取或创建 Lua 管理器, 避免场景漏挂组件导致 Buff 无法运行.
    /// </summary>
    /// <returns>Lua 管理器实例.</returns>
    public static LuaManager GetOrCreate()
    {
        if (Instance != null) return Instance;

        var managerObject = new GameObject(nameof(LuaManager));
        return managerObject.AddComponent<LuaManager>();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        luaEnv = new LuaEnv();
    }

    private void Update()
    {
        luaEnv?.Tick();
    }

    /// <summary>
    /// 根据 Buff 配置创建 Lua Buff 实例.
    /// </summary>
    /// <param name="buff">Buff 配置.</param>
    /// <returns>Lua Buff 实例.</returns>
    public LuaBuffInstance CreateBuffInstance(Buff buff)
    {
        if (buff == null) return null;

        var table = GetBuffTable(buff);
        return table != null ? new LuaBuffInstance(buff, table) : null;
    }

    private LuaTable GetBuffTable(Buff buff)
    {
        var luaFile = buff.LuaFile;
        if (luaFile == null)
        {
            Debug.LogError($"{nameof(LuaManager)}: Buff 未绑定 Lua 文件, Buff: {buff.BuffName}.", this);
            return null;
        }

        if (buffTableCache.TryGetValue(luaFile, out var cachedTable))
        {
            return cachedTable;
        }

        try
        {
            var results = luaEnv.DoString(luaFile.text, luaFile.name);
            var table = results != null && results.Length > 0 ? results[0] as LuaTable : null;
            if (table == null)
            {
                Debug.LogError($"{nameof(LuaManager)}: Lua 文件没有返回 table, Buff: {buff.BuffName}, Lua: {luaFile.name}.", this);
                return null;
            }

            buffTableCache[luaFile] = table;
            return table;
        }
        catch (Exception exception)
        {
            Debug.LogError($"{nameof(LuaManager)}: 加载 Lua 文件失败, Buff: {buff.BuffName}, Lua: {luaFile.name}, Error: {exception.Message}.", this);
            return null;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }

        foreach (var table in buffTableCache.Values)
        {
            table?.Dispose();
        }

        buffTableCache.Clear();
        luaEnv?.Dispose();
        luaEnv = null;
    }
}
