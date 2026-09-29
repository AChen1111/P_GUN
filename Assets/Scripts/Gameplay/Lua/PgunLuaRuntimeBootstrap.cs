using System.Threading.Tasks;
using Game.Core;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Root 场景的 Lua 数据入口, 注册内置 Lua 配置读取器.
    /// 当前版本不加载远端 Lua 补丁.
    /// </summary>
    [DefaultExecutionOrder(-100)]
    public sealed class PgunLuaRuntimeBootstrap : MonoBehaviour, IStartupHotfixRunner
    {
        private FrameworkLuaDataProvider dataProvider;

        private void Awake()
        {
            dataProvider = new FrameworkLuaDataProvider();
            LuaDataRuntime.RegisterProvider(dataProvider);
            StartupHotfixRuntime.RegisterRunner(this);
        }

        private void OnDestroy()
        {
            StartupHotfixRuntime.UnregisterRunner(this);
            LuaDataRuntime.UnregisterProvider(dataProvider);
            dataProvider = null;
        }

        public Task ExecuteStartupHotfixAsync()
        {
            // 验收和正式启动都直接使用包体 Lua, 暂不启用远端 Lua 热更.
            return Task.CompletedTask;
        }
    }
}
