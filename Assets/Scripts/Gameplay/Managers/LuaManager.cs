using System;
using System.Collections.Generic;
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
    /// Lua 全局管理器, 负责创建 Lua 环境并缓存脚本 table.
    /// </summary>
    public sealed class LuaManager : MonoBehaviour
    {
        private readonly Dictionary<TextAsset, LuaTable> buffTableCache = new Dictionary<TextAsset, LuaTable>();
        private readonly Dictionary<TextAsset, LuaTable> itemEffectTableCache = new Dictionary<TextAsset, LuaTable>();
        private readonly Dictionary<TextAsset, Dictionary<string, Action<ItemEffectContext>>> itemEffectMethodCache = new Dictionary<TextAsset, Dictionary<string, Action<ItemEffectContext>>>();

        private LuaEnv luaEnv;

        public static LuaManager Instance { get; private set; }

        /// <summary>
        /// 初始化运行时依赖.
        /// </summary>
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

        /// <summary>
        /// 执行每帧更新逻辑.
        /// </summary>
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

            LuaTable GetBuffTable(Buff buff)
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
        }

        /// <summary>
        /// 调用道具 Lua 效果上的指定方法.
        /// </summary>
        public void InvokeItemEffectMethod(TextAsset luaFile, string methodName, ItemEffectContext ctx)
        {
            var action = GetItemEffectMethod(luaFile, methodName);
            action.Invoke(ctx);
        }

        /// <summary>
        /// 获取并缓存道具 Lua 效果方法.
        /// </summary>
        private Action<ItemEffectContext> GetItemEffectMethod(TextAsset luaFile, string methodName)
        {
            if (!itemEffectMethodCache.TryGetValue(luaFile, out var methodCache))
            {
                methodCache = new Dictionary<string, Action<ItemEffectContext>>();
                itemEffectMethodCache[luaFile] = methodCache;
            }

            if (methodCache.TryGetValue(methodName, out var cachedMethod))
            {
                return cachedMethod;
            }

            var table = GetItemEffectTable(luaFile);
            var method = table.Get<Action<ItemEffectContext>>(methodName);
            methodCache[methodName] = method;
            return method;
        }

        /// <summary>
        /// 获取并缓存道具 Lua 效果 table.
        /// </summary>
        private LuaTable GetItemEffectTable(TextAsset luaFile)
        {
            if (itemEffectTableCache.TryGetValue(luaFile, out var cachedTable))
            {
                return cachedTable;
            }

            var results = luaEnv.DoString(luaFile.text, luaFile.name);
            var table = results[0] as LuaTable;
            itemEffectTableCache[luaFile] = table;
            return table;
        }

        /// <summary>
        /// 释放销毁时持有的运行时状态.
        /// </summary>
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

            foreach (var table in itemEffectTableCache.Values)
            {
                table?.Dispose();
            }

            buffTableCache.Clear();
            itemEffectTableCache.Clear();
            itemEffectMethodCache.Clear();
            luaEnv?.Dispose();
            luaEnv = null;
        }
    }
}
