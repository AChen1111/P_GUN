using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Core;
using UnityEngine;
using XLua;

namespace Game.Gameplay
{
    /// <summary>
    /// Root 场景的 Lua 启动组件: 注册配置读取实现, 并执行启动热修入口.
    /// 摆在 Root 场景, Script Execution Order 需晚于 LuaManager.
    /// </summary>
    public sealed class PgunLuaRuntimeBootstrap : MonoBehaviour, IStartupHotfixRunner
    {
        private const string StartupHotfixAddress = "hotfix/main";
        private const string StartupHotfixLabel = "hotfix";
        private const string LuaFileExtension = ".lua";

        private readonly Dictionary<string, byte[]> hotfixLuaBytesByModulePath = new Dictionary<string, byte[]>();
        private FrameworkLuaDataProvider dataProvider;
        private bool startupHotfixExecuted;

        /// <summary>
        /// 注册配置读取实现和启动热修执行器.
        /// </summary>
        private void Awake()
        {
            dataProvider = new FrameworkLuaDataProvider();
            LuaDataRuntime.RegisterProvider(dataProvider);
            StartupHotfixRuntime.RegisterRunner(this);
        }

        /// <summary>
        /// 注销时清理注册状态.
        /// </summary>
        private void OnDestroy()
        {
            StartupHotfixRuntime.UnregisterRunner(this);
            LuaDataRuntime.UnregisterProvider(dataProvider);
            dataProvider = null;
        }

        /// <summary>
        /// 从 Hotfix 分组预加载 Lua 文本, 再执行启动热修入口.
        /// </summary>
        public async Task ExecuteStartupHotfixAsync()
        {
            if (startupHotfixExecuted) return;

            var loader = AddressableLoader.Instance;
            if (loader == null)
            {
                throw new InvalidOperationException($"{nameof(PgunLuaRuntimeBootstrap)} requires {nameof(AddressableLoader)} before executing startup hotfix.");
            }

            var manager = LuaManager.Instance;
            if (manager == null || manager.LuaEnv == null)
            {
                throw new InvalidOperationException($"{nameof(PgunLuaRuntimeBootstrap)} requires {nameof(LuaManager)} in Root scene before executing startup hotfix.");
            }

            await PreloadHotfixLuaModulesAsync(loader);
            // 热修模块地址都在 hotfix/ 前缀下, 与 LuaRaw 按文件名索引的加载器互不冲突.
            manager.LuaEnv.AddLoader(LoadHotfixLuaModule);

            var hotfixEntry = await loader.LoadAssetAsync<TextAsset>(StartupHotfixAddress);
            if (hotfixEntry == null)
            {
                throw new InvalidOperationException($"Startup hotfix asset is null. Address: {StartupHotfixAddress}.");
            }

            manager.LuaEnv.DoString(hotfixEntry.text, hotfixEntry.name);
            startupHotfixExecuted = true;
            Debug.Log($"{nameof(PgunLuaRuntimeBootstrap)}: 启动热修入口执行完成, Address: {StartupHotfixAddress}.");
        }

        /// <summary>
        /// 从 Hotfix 分组预加载所有 Lua 文本, 供 require 同步读取.
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
        /// 自定义加载器, 把 require 名称映射到预加载的 Hotfix 文本.
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
    }
}