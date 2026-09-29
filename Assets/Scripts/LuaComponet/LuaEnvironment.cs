using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using XLua;

/// <summary>
/// 负责初始化 Lua 环境, 并注册模块加载器.
/// Editor 环境以 Assets/Scripts/LuaRaw 为根目录直接加载 .lua 源码.
/// 真机环境从 Resources/LuaBundle.bytes 加载 luac 编译后的字节码.
/// 两种环境都按文件名索引, require 时不需要带文件夹路径.
/// </summary>
public class LuaEnvironment
{
    private LuaEnv m_luaEnv;

    public LuaEnv LuaEnv => m_luaEnv;

#if UNITY_EDITOR
    /// <summary>是否在 Editor 下自动连接 EmmyLua 调试器. IDE 未监听时只会 Warning, 不影响运行.</summary>
    public static bool EnableEmmyLuaDebug = true;
    private const int EmmyLuaPort = 9966;
#endif

    public void Init()
    {
        m_luaEnv = new LuaEnv();
        m_luaEnv.AddLoader(CustomLoader);
#if UNITY_EDITOR
        if (EnableEmmyLuaDebug)
        {
            TryConnectEmmyLua();
        }
#endif
    }

#if UNITY_EDITOR
    // Editor 环境: 以 LuaRaw 为根目录, 递归扫描所有 .lua 文件, 按文件名建立索引.
    private static readonly string LuaRawRoot = Application.dataPath + "/Scripts/LuaRaw/";

    // 文件名(不含扩展名) -> 文件完整路径.
    private Dictionary<string, string> m_fileIndex;

    private byte[] CustomLoader(ref string filepath)
    {
        if (m_fileIndex == null)
        {
            BuildFileIndex();
        }

        // require 时只需要文件名, 不需要文件夹路径, 例如 require("BaseUI").
        string fileName = filepath;
        int lastDot = fileName.LastIndexOf('.');
        if (lastDot >= 0)
        {
            fileName = fileName.Substring(lastDot + 1);
        }

        if (m_fileIndex.TryGetValue(fileName, out string fullPath))
        {
            // 设置为真实路径, 便于调试与报错定位.
            filepath = fullPath;
            return File.ReadAllBytes(fullPath);
        }

        // emmy_core 等原生库走 package.cpath, 不算业务脚本缺失.
        if (fileName != "emmy_core")
        {
            Debug.LogWarning($"[LuaEnvironment] Editor 环境未找到 Lua 文件: {filepath}");
        }
        return null;
    }

    /// <summary>
    /// 递归扫描 LuaRaw 目录下所有 .lua 文件, 按文件名建立索引.
    /// 运行时热重载前需要重新调用, 否则运行期间新增的 .lua 文件 require 不到.
    /// </summary>
    public void BuildFileIndex()
    {
        m_fileIndex = new Dictionary<string, string>();
        if (!Directory.Exists(LuaRawRoot))
        {
            Debug.LogError($"[LuaEnvironment] 未找到 Lua 根目录: {LuaRawRoot}");
            return;
        }

        string[] files = Directory.GetFiles(LuaRawRoot, "*.lua", SearchOption.AllDirectories);
        foreach (string file in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(file);
            if (m_fileIndex.ContainsKey(fileName))
            {
                Debug.LogWarning($"[LuaEnvironment] 存在重名 Lua 文件: {fileName}, 已使用 {file} 覆盖之前的路径.");
            }
            m_fileIndex[fileName] = file;
        }
    }

    /// <summary>
    /// 连接 EmmyLua 调试器: 先在 IDE 按 F5 监听, 再进 Unity Play.
    /// Windows 上 Emmy 常只绑 IPv6(::1), 故 127.0.0.1 与 ::1 都尝试.
    /// </summary>
    private void TryConnectEmmyLua()
    {
        string dllPath = FindEmmyCoreDll();
        if (string.IsNullOrEmpty(dllPath))
        {
            Debug.LogWarning("[LuaEnvironment] 未找到 emmy_core.dll. 请安装 VSCode 的 EmmyLua(tangzx) 插件.");
            return;
        }

        // package.cpath 需要 ?.dll 模板, 目录下实际文件名为 emmy_core.dll.
        string cpathDir = Path.GetDirectoryName(dllPath)?.Replace('\\', '/');
        if (string.IsNullOrEmpty(cpathDir))
        {
            return;
        }

        Debug.Log($"[LuaEnvironment] EmmyLua dll: {dllPath}");

        string lua = $@"
package.cpath = package.cpath .. ';{cpathDir}/?.dll'
local dbg = require('emmy_core')
local hosts = {{ '127.0.0.1', '::1', 'localhost' }}
local lastErr
for _, host in ipairs(hosts) do
    local ok, err = pcall(function()
        dbg.tcpConnect(host, {EmmyLuaPort})
    end)
    if ok then
        print('[EmmyLua] 已连接 ' .. host .. ':{EmmyLuaPort}')
        return
    end
    lastErr = err
end
print('[EmmyLua] 连接失败: ' .. tostring(lastErr))
print('[EmmyLua] 请先取消再 F5 启动 EmmyLua New Debug, 确认出现 Wait for connection 后再点 Unity Play')
";
        try
        {
            m_luaEnv.DoString(lua);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[LuaEnvironment] EmmyLua 连接异常: {e.Message}");
        }
    }

    /// <summary>
    /// 在 VSCode / Cursor 扩展目录中查找最新的 emmy_core.dll, 优先 x64.
    /// </summary>
    private static string FindEmmyCoreDll()
    {
        string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string[] roots =
        {
            Path.Combine(home, ".vscode", "extensions"),
            Path.Combine(home, ".cursor", "extensions"),
            Path.Combine(home, ".vscode-insiders", "extensions"),
        };

        var candidates = new List<string>();
        foreach (string root in roots)
        {
            if (!Directory.Exists(root))
            {
                continue;
            }

            // 匹配 tangzx.emmylua-* / theo.emmylua-* 等.
            foreach (string extDir in Directory.GetDirectories(root, "*emmylua*"))
            {
                string x64 = Path.Combine(extDir, "debugger", "emmy", "windows", "x64", "emmy_core.dll");
                string x86 = Path.Combine(extDir, "debugger", "emmy", "windows", "x86", "emmy_core.dll");
                if (File.Exists(x64))
                {
                    candidates.Add(x64);
                }
                else if (File.Exists(x86))
                {
                    candidates.Add(x86);
                }
            }
        }

        if (candidates.Count == 0)
        {
            return null;
        }

        // 取最近写入的一份, 通常是最新安装的插件.
        return candidates.OrderByDescending(File.GetLastWriteTimeUtc).First();
    }
#else
    // 真机环境: 从打包的 LuaBundle.bytes 中加载 luac 字节码, 按文件名索引.
    private const string LuaBundleName = "LuaBundle";
    private Dictionary<string, byte[]> m_luaBundle;

    /// <summary>
    /// 真机没有源文件索引, 留空实现让调用方无需区分平台.
    /// </summary>
    public void BuildFileIndex()
    {
    }

    private byte[] CustomLoader(ref string filepath)
    {
        if (m_luaBundle == null)
        {
            LoadLuaBundle();
        }

        // require 时只需要文件名, 例如 require("BaseUI").
        string fileName = filepath;
        int lastDot = fileName.LastIndexOf('.');
        if (lastDot >= 0)
        {
            fileName = fileName.Substring(lastDot + 1);
        }

        if (m_luaBundle.TryGetValue(fileName, out byte[] bytes))
        {
            return bytes;
        }

        Debug.LogWarning($"[LuaEnvironment] LuaBundle 中未找到模块: {fileName}");
        return null;
    }

    /// <summary>
    /// 解析 LuaBundle.bytes: int32 文件数量 + 循环 { 文件名, int32 长度, 字节码 }.
    /// </summary>
    private void LoadLuaBundle()
    {
        m_luaBundle = new Dictionary<string, byte[]>();
        TextAsset asset = Resources.Load<TextAsset>(LuaBundleName);
        if (asset == null)
        {
            Debug.LogError($"[LuaEnvironment] 未找到 Resources/{LuaBundleName}.bytes, 请先执行 Tools/Lua/Build LuaBundle 打包.");
            return;
        }

        using (var stream = new MemoryStream(asset.bytes))
        using (var reader = new BinaryReader(stream))
        {
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                string moduleName = reader.ReadString();
                int length = reader.ReadInt32();
                byte[] data = reader.ReadBytes(length);
                m_luaBundle[moduleName] = data;
            }
        }

        Debug.Log($"[LuaEnvironment] LuaBundle 加载完成, 共 {m_luaBundle.Count} 个模块.");
    }
#endif
}