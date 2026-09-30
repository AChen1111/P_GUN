using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using XLua;

/// <summary>
/// 把 Assets/Scripts/LuaRaw 下的所有 .lua 文件编译成 luac 字节码并打包成 Assets/LuaBundles/LuaBundle.bytes.
/// 格式: int32 文件数量 + 循环 { 文件名, int32 长度, 字节码 }.
/// 与 LuaEnvironment 的真机 Loader 保持一致: 按文件名索引, require 时无需带文件夹路径.
/// </summary>
public static class LuaBundleBuilder
{
    private const string LuaRawRoot = "Assets/Scripts/LuaRaw";
    private const string OutputDir = "Assets/LuaBundles";
    private const string OutputPath = OutputDir + "/LuaBundle.bytes";

    [MenuItem("Tools/Lua/Build LuaBundle")]
    public static void Build()
    {
        string rawRootFull = Path.GetFullPath(LuaRawRoot);
        if (!Directory.Exists(rawRootFull))
        {
            Debug.LogError($"[LuaBundleBuilder] 未找到 Lua 根目录: {LuaRawRoot}");
            return;
        }

        string[] luaFiles = Directory.GetFiles(rawRootFull, "*.lua", SearchOption.AllDirectories);
        if (luaFiles.Length == 0)
        {
            Debug.LogWarning($"[LuaBundleBuilder] {LuaRawRoot} 下没有任何 .lua 文件");
            return;
        }

        // 临时创建 LuaEnv, 用 load + string.dump 编译出 luac 字节码.
        LuaEnv luaEnv = new LuaEnv();
        try
        {
            var compile = luaEnv.DoString(@"
                return function(source, chunkname)
                    local f = assert(load(source, chunkname))
                    return string.dump(f, false)
                end")[0] as LuaFunction;

            if (!Directory.Exists(OutputDir))
            {
                Directory.CreateDirectory(OutputDir);
            }

            // 按文件名去重, 重名文件在 LuaBundle 中只保留最后一个.
            var seenNames = new HashSet<string>();
            foreach (string file in luaFiles)
            {
                string moduleName = Path.GetFileNameWithoutExtension(file);
                if (!seenNames.Add(moduleName))
                {
                    Debug.LogWarning($"[LuaBundleBuilder] 存在重名 Lua 文件: {moduleName}, 打包后的 LuaBundle 中该模块只保留最后一个文件的内容.");
                }
            }

            using (var stream = new FileStream(OutputPath, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(luaFiles.Length);
                foreach (string file in luaFiles)
                {
                    // 模块名等于文件名, 与 LuaEnvironment 的按文件名索引保持一致.
                    string relative = Path.GetRelativePath(rawRootFull, file).Replace('\\', '/');
                    string moduleName = Path.GetFileNameWithoutExtension(file);

                    byte[] source = File.ReadAllBytes(file);
                    byte[] bytecode = compile.Func<byte[], string, byte[]>(source, "@" + relative);
                    if (bytecode == null || bytecode.Length == 0)
                    {
                        throw new Exception($"编译失败: {relative}");
                    }

                    writer.Write(moduleName);
                    writer.Write(bytecode.Length);
                    writer.Write(bytecode);
                    Debug.Log($"[LuaBundleBuilder] 已打包 {moduleName} ({bytecode.Length} bytes)");
                }
            }

            compile.Dispose();
            AssetDatabase.Refresh();
            Debug.Log($"[LuaBundleBuilder] 打包完成: {OutputPath}, 共 {luaFiles.Length} 个模块.");
        }
        catch (Exception e)
        {
            Debug.LogError($"[LuaBundleBuilder] 打包失败: {e}");
        }
        finally
        {
            luaEnv.Dispose();
        }
    }
}