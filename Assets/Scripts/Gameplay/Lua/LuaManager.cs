using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using XLua;
using Game.Core;
using Game.Items;

namespace Game.Gameplay
{
    /// <summary>
    /// Lua 全局管理器, 负责创建 Lua 环境并缓存脚本 table.
    /// </summary>
    public sealed class LuaManager : MonoBehaviour, IStartupHotfixRunner
    {
        private const string StartupHotfixAddress = "hotfix/main";
        private const string StartupHotfixLabel = "hotfix";
        private const string LuaFileExtension = ".lua";

        private readonly Dictionary<TextAsset, LuaTable> buffTableCache = new Dictionary<TextAsset, LuaTable>();
        private readonly Dictionary<TextAsset, LuaTable> itemEffectTableCache = new Dictionary<TextAsset, LuaTable>();
        private readonly Dictionary<TextAsset, Dictionary<string, Action<ItemEffectContext>>> itemEffectMethodCache = new Dictionary<TextAsset, Dictionary<string, Action<ItemEffectContext>>>();
        private readonly Dictionary<string, byte[]> hotfixLuaBytesByModulePath = new Dictionary<string, byte[]>();

        private LuaEnv luaEnv;
        private bool startupHotfixExecuted;

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
            // 热修 require 只能同步取 bytes, 所以 loader 只读取启动阶段预加载好的 AB Lua 缓存.
            luaEnv.AddLoader(LoadHotfixLuaModule);
            // Root 场景中的 LuaManager 是 Buff 脚本工厂的唯一注册者.
            BuffScriptRuntime.RegisterFactory(CreateBuffInstance);
            // Root 场景中的 LuaManager 负责执行启动热修入口.
            StartupHotfixRuntime.RegisterRunner(this);
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
        public IBuffScriptInstance CreateBuffInstance(Buff buff)
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
        /// 从 Addressables 加载并执行启动热修入口.
        /// </summary>
        /// <returns>异步任务.</returns>
        public async Task ExecuteStartupHotfixAsync()
        {
            if (startupHotfixExecuted) return;

            var loader = AddressableLoader.Instance;
            if (loader == null)
            {
                throw new InvalidOperationException($"{nameof(AddressableLoader)} must exist before executing startup hotfix.");
            }

            await PreloadHotfixLuaModulesAsync(loader);
            var hotfixEntry = await loader.LoadAssetAsync<TextAsset>(StartupHotfixAddress);
            if (hotfixEntry == null)
            {
                throw new InvalidOperationException($"Startup hotfix asset is null. Address: {StartupHotfixAddress}.");
            }

            luaEnv.DoString(hotfixEntry.text, hotfixEntry.name);
            startupHotfixExecuted = true;
            Debug.Log($"{nameof(LuaManager)}: 启动热修入口执行完成, Address: {StartupHotfixAddress}.", this);
        }

        /// <summary>
        /// 从 Hotfix AB 包预加载所有 Lua 文本, 供 xLua require 同步读取.
        /// </summary>
        private async Task PreloadHotfixLuaModulesAsync(AddressableLoader loader)
        {
            hotfixLuaBytesByModulePath.Clear();

            var hotfixLuaAssets = await loader.LoadAssetsByLabelAsync<TextAsset>(StartupHotfixLabel);
            foreach (var pair in hotfixLuaAssets)
            {
                RegisterHotfixLuaModule(pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// 注册热修 Lua 模块路径, 支持 Addressables 地址和 TextAsset 名称两种 require 映射.
        /// </summary>
        private void RegisterHotfixLuaModule(string address, TextAsset luaAsset)
        {
            if (luaAsset == null)
            {
                throw new InvalidOperationException($"Hotfix Lua asset is null. Address: {address}.");
            }

            var bytes = luaAsset.bytes;
            hotfixLuaBytesByModulePath[NormalizeLuaModulePath(address)] = bytes;

            if (!string.IsNullOrWhiteSpace(luaAsset.name))
            {
                hotfixLuaBytesByModulePath[NormalizeLuaModulePath($"{StartupHotfixLabel}/{luaAsset.name}")] = bytes;
            }
        }

        /// <summary>
        /// xLua 自定义加载器, 把 require 名称映射到已预加载的 Hotfix AB 文本.
        /// </summary>
        private byte[] LoadHotfixLuaModule(ref string filepath)
        {
            var modulePath = NormalizeLuaModulePath(filepath);
            if (!hotfixLuaBytesByModulePath.TryGetValue(modulePath, out var bytes))
            {
                return null;
            }

            filepath = modulePath;
            return bytes;
        }

        /// <summary>
        /// 将 require 模块名或 Addressables 地址归一为 hotfix/player_bullet_reverse.lua 形式.
        /// </summary>
        private static string NormalizeLuaModulePath(string moduleNameOrAddress)
        {
            if (string.IsNullOrWhiteSpace(moduleNameOrAddress))
            {
                throw new ArgumentException("Lua module path must not be empty.", nameof(moduleNameOrAddress));
            }

            var normalized = moduleNameOrAddress.Replace('\\', '/');
            if (!normalized.Contains("/") && !normalized.EndsWith(LuaFileExtension, StringComparison.Ordinal))
            {
                normalized = normalized.Replace('.', '/');
            }

            if (!normalized.EndsWith(LuaFileExtension, StringComparison.Ordinal))
            {
                normalized += LuaFileExtension;
            }

            return normalized;
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

            BuffScriptRuntime.UnregisterFactory(CreateBuffInstance);
            StartupHotfixRuntime.UnregisterRunner(this);

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
