using System;
using System.Collections.Generic;
using UnityEngine;
using XLua;

/// <summary>
/// 可在 Inspector 中配置的对象引用, 按字段名注入 Lua 实例表.
/// </summary>
[System.Serializable]
public class ObjectReference
{
    public string name;
    public UnityEngine.Object value;
}

/// <summary>
/// 普通数据类型枚举, 用于在 Inspector 中选择要传入的数据类型.
/// </summary>
public enum DataValueType
{
    Int,
    Float,
    String,
    Bool
}

/// <summary>
/// 可在 Inspector 中配置的基础数据引用, 和 ObjectReference 作用类似, 但传入的是普通数据而非 Unity 组件.
/// </summary>
[System.Serializable]
public class DataReference
{
    public string name;
    public DataValueType valueType;

    public int intValue;

    public float floatValue;

    public string stringValue;

    public bool boolValue;

    /// <summary>
    /// 根据 valueType 返回对应的值.
    /// </summary>
    public object GetValue()
    {
        switch (valueType)
        {
            case DataValueType.Int: return intValue;
            case DataValueType.Float: return floatValue;
            case DataValueType.String: return stringValue;
            case DataValueType.Bool: return boolValue;
            default: return null;
        }
    }
}

/// <summary>
/// 挂在场景或预制体上, 按类型名创建 Lua 模块实例, 并负责引用注入与生命周期转发.
/// 需要 Lua 逻辑的物体必须在预制体或场景上预先挂好本组件.
/// </summary>
public class LuaComponet : MonoBehaviour
{
    [SerializeField]
    [Tooltip("lua类型名, 必须已写进 module.lua 的 moduleList.")]
    private string m_typeName;

    [SerializeField]
    [Tooltip("对象引用")]
    private ObjectReference[] m_objectReferences;

    [SerializeField]
    [Tooltip("普通数据引用")]
    private DataReference[] m_dataReferences;

    private LuaTable m_luaTable;

    // 按签名缓存已绑定的 Lua 委托, 避免每次调用都走 table.Get.
    private readonly Dictionary<string, Action<LuaTable>> m_onFunctions = new Dictionary<string, Action<LuaTable>>();
    private readonly Dictionary<string, Action<LuaTable, Vector2>> m_onFunctionsVector2 = new Dictionary<string, Action<LuaTable, Vector2>>();
    private readonly Dictionary<string, Action<LuaTable, float>> m_onFunctionsFloat = new Dictionary<string, Action<LuaTable, float>>();
    private readonly Dictionary<string, Action<LuaTable, Vector2, float>> m_onFunctionsVector2Float = new Dictionary<string, Action<LuaTable, Vector2, float>>();
    private readonly Dictionary<string, Func<LuaTable, bool>> m_boolFunctions = new Dictionary<string, Func<LuaTable, bool>>();

    public string TypeName => m_typeName;

    /// <summary>
    /// 初始化运行时依赖.
    /// </summary>
    private void Awake()
    {
        if (LuaManager.Instance == null || !LuaManager.Instance.IsDone)
        {
            Debug.LogError("[LuaComponet] LuaManager 未初始化. 请把 LuaManager 摆在 Root 场景, 并在 Script Execution Order 中排在 LuaComponet 之前.");
            return;
        }

        // 初始化 Lua 表.
        m_luaTable = LuaManager.Instance.GetLuaTable(m_typeName, gameObject);
        if (m_luaTable == null)
        {
            Debug.LogError($"[LuaComponet] LuaTable not found: {m_typeName}. 请确认 moduleList 已注册该模块.");
            return;
        }

        // 注入对象.
        InitComponent();

        // 初始化生命周期函数.
        InitOnFunctions();

        // 调用 Lua Awake.
        CallLuaFunction("Awake");
    }

    /// <summary>
    /// 把 Inspector 中的引用写入实例表, 注入发生在 Lua Awake 之前.
    /// </summary>
    private void InitComponent()
    {
        if (m_objectReferences != null)
        {
            foreach (var objectReference in m_objectReferences)
            {
                m_luaTable.Set(objectReference.name, objectReference.value);
            }
        }

        if (m_dataReferences != null)
        {
            foreach (var dataReference in m_dataReferences)
            {
                m_luaTable.Set(dataReference.name, dataReference.GetValue());
            }
        }
    }

    private void Start()
    {
        CallLuaFunction("Start");
    }

    private void OnDestroy()
    {
        CallLuaFunction("OnDestroy");
    }

    private void OnEnable()
    {
        CallLuaFunction("OnEnable");
    }

    private void OnDisable()
    {
        CallLuaFunction("OnDisable");
    }

    /// <summary>
    /// 缓存基础生命周期函数.
    /// </summary>
    private void InitOnFunctions()
    {
        m_onFunctions.Add("Awake", m_luaTable.Get<Action<LuaTable>>("Awake"));
        m_onFunctions.Add("Start", m_luaTable.Get<Action<LuaTable>>("Start"));
        m_onFunctions.Add("OnDestroy", m_luaTable.Get<Action<LuaTable>>("OnDestroy"));
        m_onFunctions.Add("OnEnable", m_luaTable.Get<Action<LuaTable>>("OnEnable"));
        m_onFunctions.Add("OnDisable", m_luaTable.Get<Action<LuaTable>>("OnDisable"));
    }

    /// <summary>
    /// 按名称调用 Lua 函数, 第一个参数是实例表. 模块未定义该函数时跳过.
    /// </summary>
    public void CallLuaFunction(string functionName)
    {
        if (m_luaTable == null || m_onFunctions == null)
        {
            return;
        }

        if (m_onFunctions.TryGetValue(functionName, out var action))
        {
            action?.Invoke(m_luaTable);
            return;
        }

        var func = m_luaTable.Get<Action<LuaTable>>(functionName);
        if (func != null)
        {
            m_onFunctions.Add(functionName, func);
            func.Invoke(m_luaTable);
        }
    }

    /// <summary>
    /// 调用带一个 Vector2 参数的 Lua 函数, 例如武器开火方向.
    /// </summary>
    public void CallLuaFunction(string functionName, Vector2 arg1)
    {
        var func = GetCachedFunction(m_onFunctionsVector2, functionName);
        func?.Invoke(m_luaTable, arg1);
    }

    /// <summary>
    /// 调用带一个 float 参数的 Lua 函数, 例如时间增量.
    /// </summary>
    public void CallLuaFunction(string functionName, float arg1)
    {
        var func = GetCachedFunction(m_onFunctionsFloat, functionName);
        func?.Invoke(m_luaTable, arg1);
    }

    /// <summary>
    /// 调用带 Vector2 和 float 参数的 Lua 函数, 例如按住射击时的方向和本帧时间.
    /// </summary>
    public void CallLuaFunction(string functionName, Vector2 arg1, float arg2)
    {
        var func = GetCachedFunction(m_onFunctionsVector2Float, functionName);
        func?.Invoke(m_luaTable, arg1, arg2);
    }

    /// <summary>
    /// 调用返回 bool 的 Lua 函数, 用于 CanUse 这类判断. 模块缺少该函数时报错并返回 false.
    /// </summary>
    public bool CallLuaFunctionBool(string functionName)
    {
        if (m_luaTable == null)
        {
            Debug.LogError($"[LuaComponet] 实例表未创建, 无法调用 {functionName}. TypeName: {m_typeName}.", this);
            return false;
        }

        if (!m_boolFunctions.TryGetValue(functionName, out var func))
        {
            func = m_luaTable.Get<Func<LuaTable, bool>>(functionName);
            m_boolFunctions[functionName] = func;
        }

        if (func == null)
        {
            Debug.LogError($"[LuaComponet] Lua 模块缺少 {functionName} 函数. TypeName: {m_typeName}.", this);
            return false;
        }

        return func.Invoke(m_luaTable);
    }

    /// <summary>
    /// 判断模块是否定义了指定函数, 不触发调用.
    /// </summary>
    public bool HasLuaFunction(string functionName)
    {
        if (m_luaTable == null)
        {
            return false;
        }

        return m_luaTable.Get<Action<LuaTable>>(functionName) != null;
    }

    /// <summary>
    /// 从对应签名的缓存里取函数, 未缓存时绑定一次.
    /// </summary>
    private TDelegate GetCachedFunction<TDelegate>(Dictionary<string, TDelegate> cache, string functionName) where TDelegate : class
    {
        if (m_luaTable == null)
        {
            return null;
        }

        if (cache.TryGetValue(functionName, out var cached))
        {
            return cached;
        }

        var func = m_luaTable.Get<TDelegate>(functionName);
        cache[functionName] = func;
        return func;
    }

    /// <summary>
    /// 重载本模块代码并刷新本实例.
    /// </summary>
    public void RuntimeReload()
    {
        LuaManager.Instance.RuntimeReload(m_typeName);
        RefreshInstance();
    }

    /// <summary>
    /// 只刷新本实例: 重建函数缓存并重新注入 Inspector 引用, 不重跑生命周期.
    /// 全量重载时由 Editor 统一调用, 避免同一模块被多个实例重复重载.
    /// </summary>
    public void RefreshInstance()
    {
        if (m_luaTable == null)
        {
            return;
        }

        m_onFunctions.Clear();
        m_onFunctionsVector2.Clear();
        m_onFunctionsFloat.Clear();
        m_onFunctionsVector2Float.Clear();
        m_boolFunctions.Clear();
        InitOnFunctions();
        InitComponent();
    }
}