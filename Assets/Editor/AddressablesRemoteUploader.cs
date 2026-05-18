using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public static class AddressablesRemoteUploader
{
    private const string MenuPath = "PG/Addressables/Upload ServerData To Remote";
    private const string LocalRelativeDirectory = "ServerData/P_GUN/StandaloneWindows64";
    private const string RemoteHost = "39.97.56.180";
    private const string RemoteUser = "root";
    private const string RemoteDirectory = "/www/wwwroot/39.97.56.180/AB/P_GUN/StandaloneWindows64";

    [MenuItem(MenuPath)]
    public static void UploadServerDataToRemote()
    {
        var localDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), LocalRelativeDirectory));
        if (!Directory.Exists(localDirectory))
        {
            EditorUtility.DisplayDialog("上传失败", $"找不到本地目录: {localDirectory}", "OK");
            return;
        }

        var files = Directory.GetFiles(localDirectory, "*", SearchOption.AllDirectories);
        if (files.Length == 0)
        {
            EditorUtility.DisplayDialog("上传失败", $"本地目录没有可上传文件: {localDirectory}", "OK");
            return;
        }

        try
        {
            EnsureRemoteDirectory();
            UploadFiles(localDirectory, files);
            EditorUtility.DisplayDialog("上传完成", $"已上传 {files.Length} 个文件到:\n{RemoteUser}@{RemoteHost}:{RemoteDirectory}", "OK");
        }
        catch (Exception exception)
        {
            Debug.LogError($"{nameof(AddressablesRemoteUploader)}: 上传失败, Error: {exception.Message}");
            EditorUtility.DisplayDialog(
                "上传失败",
                "Unity 菜单使用系统 ssh/scp 上传, 需要本机已配置 SSH key 或 ssh-agent.\n\n" +
                $"错误: {exception.Message}",
                "OK");
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }
    }

    [MenuItem(MenuPath, true)]
    private static bool ValidateUploadServerDataToRemote()
    {
        return Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), LocalRelativeDirectory));
    }

    private static void EnsureRemoteDirectory()
    {
        // 远端目录不存在时先创建, 但不清理任何旧 bundle.
        RunProcess(
            ResolveExecutable("ssh"),
            $"-o BatchMode=yes -o StrictHostKeyChecking=accept-new {RemoteUser}@{RemoteHost} \"mkdir -p '{RemoteDirectory}'\"");
    }

    private static void UploadFiles(string localDirectory, IReadOnlyList<string> files)
    {
        for (var index = 0; index < files.Count; index++)
        {
            var localPath = files[index];
            var relativePath = MakeRelativePath(localDirectory, localPath).Replace('\\', '/');
            var remotePath = $"{RemoteDirectory}/{relativePath}";
            var progress = files.Count > 0 ? (float)index / files.Count : 1f;

            EditorUtility.DisplayProgressBar("上传 Addressables 热更文件", relativePath, progress);

            // 每个文件单独上传, 这样可以保留服务器旧文件并明确失败点.
            RunProcess(
                ResolveExecutable("scp"),
                $"-o BatchMode=yes -o StrictHostKeyChecking=accept-new -p \"{localPath}\" \"{RemoteUser}@{RemoteHost}:{remotePath}\"");
        }
    }

    private static string ResolveExecutable(string executableName)
    {
#if UNITY_EDITOR_WIN
        return $"{executableName}.exe";
#else
        return executableName;
#endif
    }

    private static void RunProcess(string executable, string arguments)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = executable,
            Arguments = arguments,
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        using (var process = Process.Start(startInfo))
        {
            if (process == null)
            {
                throw new InvalidOperationException($"无法启动进程: {executable}");
            }

            var stdout = process.StandardOutput.ReadToEnd();
            var stderr = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (process.ExitCode == 0)
            {
                if (!string.IsNullOrWhiteSpace(stdout))
                {
                    Debug.Log(stdout.Trim());
                }

                return;
            }

            var message = string.IsNullOrWhiteSpace(stderr) ? stdout : stderr;
            throw new InvalidOperationException($"{executable} exit code {process.ExitCode}: {message.Trim()}");
        }
    }

    private static string MakeRelativePath(string rootDirectory, string fullPath)
    {
        // Unity 的 .NET 配置不固定, 这里避免依赖 Path.GetRelativePath.
        var rootUri = new Uri(AppendDirectorySeparator(rootDirectory));
        var fileUri = new Uri(fullPath);
        return Uri.UnescapeDataString(rootUri.MakeRelativeUri(fileUri).ToString());
    }

    private static string AppendDirectorySeparator(string path)
    {
        return path.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal)
            ? path
            : path + Path.DirectorySeparatorChar;
    }
}
