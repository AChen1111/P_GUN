using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Debug = UnityEngine.Debug;

public static class AddressablesRemoteUploader
{
    private const string MenuPath = "PG/Addressables/一键保存上传";
    private const string LocalRelativeDirectory = "ServerData/P_GUN/StandaloneWindows64";
    private const string RemoteHost = "39.97.56.180";
    private const string RemoteUser = "root";
    private const string RemoteDirectory = "/www/wwwroot/39.97.56.180/AB/P_GUN/StandaloneWindows64";

    [MenuItem(MenuPath)]
    public static void SaveBuildAndUpload()
    {
        try
        {
            SaveEditorChanges();

            var localDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), LocalRelativeDirectory));
            ValidateLocalUploadDirectory(localDirectory);
            UpdateCatalogHashFiles(localDirectory);
            ValidateCatalogForRemoteUpload(localDirectory);

            var files = Directory.GetFiles(localDirectory, "*", SearchOption.AllDirectories);
            EnsureRemoteDirectory();
            var uploadResult = UploadFiles(localDirectory, files);

            EditorUtility.DisplayDialog(
                "一键保存上传完成",
                $"已上传 {uploadResult.UploadedCount} 个变更文件, 跳过 {uploadResult.SkippedCount} 个相同文件:\n{RemoteUser}@{RemoteHost}:{RemoteDirectory}",
                "OK");
        }
        catch (Exception exception)
        {
            Debug.LogError($"{nameof(AddressablesRemoteUploader)}: 一键保存上传失败, Error: {exception.Message}");
            EditorUtility.DisplayDialog(
                "一键保存上传失败",
                "请检查 Addressables Content State, ServerData 输出目录, 以及本机 SSH key 或 ssh-agent.\n\n" +
                $"错误: {exception.Message}",
                "OK");
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }
    }

    public static void UploadServerDataToRemote()
    {
        SaveBuildAndUpload();
    }

    private static void SaveEditorChanges()
    {
        EditorUtility.DisplayProgressBar("Addressables 一键保存上传", "保存场景和资源...", 0.05f);

        // 先保存当前编辑器改动, 避免构建到旧的序列化内容.
        EditorSceneManager.SaveOpenScenes();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private static void ValidateLocalUploadDirectory(string localDirectory)
    {
        if (!Directory.Exists(localDirectory))
        {
            throw new DirectoryNotFoundException($"找不到本地上传目录: {localDirectory}");
        }

        if (!Directory.EnumerateFiles(localDirectory, "*", SearchOption.AllDirectories).Any())
        {
            throw new InvalidOperationException($"本地上传目录没有可上传文件: {localDirectory}");
        }
    }

    private static void UpdateCatalogHashFiles(string localDirectory)
    {
        EditorUtility.DisplayProgressBar("Addressables 一键保存上传", "更新 Catalog Hash...", 0.55f);

        foreach (var catalogFile in Directory.GetFiles(localDirectory, "catalog_*.json", SearchOption.TopDirectoryOnly))
        {
            var hashFile = Path.ChangeExtension(catalogFile, ".hash");
            File.WriteAllText(hashFile, CalculateMd5(catalogFile));
        }
    }

    private static string CalculateMd5(string filePath)
    {
        using (var md5 = MD5.Create())
        using (var stream = File.OpenRead(filePath))
        {
            return string.Concat(md5.ComputeHash(stream).Select(value => value.ToString("x2")));
        }
    }

    private static void ValidateCatalogForRemoteUpload(string localDirectory)
    {
        var catalogFiles = Directory.GetFiles(localDirectory, "catalog_*.json", SearchOption.TopDirectoryOnly);
        foreach (var catalogFile in catalogFiles)
        {
            var catalogText = File.ReadAllText(catalogFile);
            if (catalogText.IndexOf("contentupdate__", StringComparison.OrdinalIgnoreCase) < 0)
            {
                continue;
            }

            if (!HasLocalContentUpdateBundlePath(catalogText))
            {
                continue;
            }

            // 这里直接阻止上传, 否则玩家会下载到指向本地 StreamingAssets 的 catalog.
            throw new InvalidOperationException(
                $"Catalog still references local Content Update bundles: {catalogFile}. " +
                "Run PG/Addressables/一键保存上传 after Unity creates the Content Update group.");
        }
    }

    private static bool HasLocalContentUpdateBundlePath(string catalogText)
    {
        var searchIndex = 0;
        while (searchIndex < catalogText.Length)
        {
            var contentUpdateIndex = catalogText.IndexOf("contentupdate__", searchIndex, StringComparison.OrdinalIgnoreCase);
            if (contentUpdateIndex < 0) return false;

            var pathStart = catalogText.LastIndexOf('"', contentUpdateIndex);
            var pathEnd = catalogText.IndexOf('"', contentUpdateIndex);
            if (pathStart >= 0 && pathEnd > pathStart)
            {
                var path = catalogText.Substring(pathStart + 1, pathEnd - pathStart - 1);
                var isLocalPath = path.IndexOf("{UnityEngine.AddressableAssets.Addressables.RuntimePath}", StringComparison.Ordinal) >= 0
                    || path.IndexOf("StreamingAssets", StringComparison.OrdinalIgnoreCase) >= 0;
                if (isLocalPath)
                {
                    return true;
                }
            }

            searchIndex = contentUpdateIndex + "contentupdate__".Length;
        }

        return false;
    }

    [MenuItem(MenuPath, true)]
    private static bool ValidateSaveBuildAndUpload()
    {
        return Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), LocalRelativeDirectory));
    }

    private static void EnsureRemoteDirectory()
    {
        EditorUtility.DisplayProgressBar("Addressables 一键保存上传", "准备远端目录...", 0.7f);

        // 远端目录不存在时先创建, 但不清理任何旧 bundle.
        RunProcess(
            ResolveExecutable("ssh"),
            $"-o BatchMode=yes -o StrictHostKeyChecking=accept-new {RemoteUser}@{RemoteHost} \"mkdir -p '{RemoteDirectory}'\"");
    }

    private static UploadResult UploadFiles(string localDirectory, IReadOnlyList<string> files)
    {
        var remoteHashes = GetRemoteFileHashes();
        var uploadedCount = 0;
        var skippedCount = 0;

        for (var index = 0; index < files.Count; index++)
        {
            var localPath = files[index];
            var relativePath = MakeRelativePath(localDirectory, localPath).Replace('\\', '/');
            var remotePath = $"{RemoteDirectory}/{relativePath}";
            var progress = files.Count > 0 ? (float)index / files.Count : 1f;
            var localHash = CalculateMd5(localPath);

            if (remoteHashes.TryGetValue(relativePath, out var remoteHash)
                && string.Equals(localHash, remoteHash, StringComparison.OrdinalIgnoreCase))
            {
                skippedCount++;
                EditorUtility.DisplayProgressBar("Addressables 一键保存上传", $"跳过未变化文件 {relativePath}", Mathf.Lerp(0.75f, 0.98f, progress));
                continue;
            }

            EditorUtility.DisplayProgressBar("Addressables 一键保存上传", $"上传 {relativePath}", Mathf.Lerp(0.75f, 0.98f, progress));

            // 每个变更文件单独上传, 这样可以保留服务器旧文件并明确失败点.
            RunProcess(
                ResolveExecutable("scp"),
                $"-o BatchMode=yes -o StrictHostKeyChecking=accept-new -p \"{localPath}\" \"{RemoteUser}@{RemoteHost}:{remotePath}\"");
            uploadedCount++;
        }

        Debug.Log($"{nameof(AddressablesRemoteUploader)}: 上传完成, Changed: {uploadedCount}, Skipped: {skippedCount}.");
        return new UploadResult(uploadedCount, skippedCount);
    }

    private static Dictionary<string, string> GetRemoteFileHashes()
    {
        EditorUtility.DisplayProgressBar("Addressables 一键保存上传", "比对远端文件...", 0.72f);

        var output = RunProcessWithOutput(
            ResolveExecutable("ssh"),
            $"-o BatchMode=yes -o StrictHostKeyChecking=accept-new {RemoteUser}@{RemoteHost} \"cd '{RemoteDirectory}' && find . -type f -exec md5sum {{}} +\"",
            false);

        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        using (var reader = new StringReader(output))
        {
            while (reader.ReadLine() is { } line)
            {
                if (string.IsNullOrWhiteSpace(line) || line.Length < 35)
                {
                    continue;
                }

                var hash = line.Substring(0, 32);
                var path = line.Substring(34).Trim();
                if (path.StartsWith("./", StringComparison.Ordinal))
                {
                    path = path.Substring(2);
                }

                path = path.Replace('\\', '/');
                if (!string.IsNullOrWhiteSpace(path))
                {
                    result[path] = hash;
                }
            }
        }

        return result;
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
        RunProcessWithOutput(executable, arguments);
    }

    private static string RunProcessWithOutput(string executable, string arguments, bool logOutput = true)
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
                if (logOutput && !string.IsNullOrWhiteSpace(stdout))
                {
                    Debug.Log(stdout.Trim());
                }

                return stdout;
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

    private readonly struct UploadResult
    {
        public UploadResult(int uploadedCount, int skippedCount)
        {
            UploadedCount = uploadedCount;
            SkippedCount = skippedCount;
        }

        public int UploadedCount { get; }
        public int SkippedCount { get; }
    }
}
