using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Game.Core
{
    /// <summary>
    /// Addressables 诊断工具, 用于在加载失败时打印可检查的 Bundle 文件名.
    /// </summary>
    public static class AddressableDiagnostics
    {
        private static readonly Regex BundleNameRegex = new Regex(@"[A-Za-z0-9_\-]+\.bundle", RegexOptions.IgnoreCase);

        /// <summary>
        /// 构建单个 Address 加载失败时的诊断文本.
        /// </summary>
        public static async Task<string> BuildAssetLoadFailureReportAsync(string address, Type assetType, Exception exception)
        {
            var resolveResult = await ResolveBundleNamesForKeyAsync(address, assetType);
            var builder = new StringBuilder();
            builder.AppendLine("Addressables 资源加载失败.");
            builder.AppendLine($"Address: {address}");
            builder.AppendLine($"AssetType: {assetType?.Name ?? "Unknown"}");
            AppendBundleReport(builder, resolveResult);
            AppendExceptionReport(builder, exception);
            return builder.ToString().TrimEnd();
        }

        /// <summary>
        /// 构建标签依赖下载失败时的诊断文本.
        /// </summary>
        public static async Task<string> BuildDownloadFailureReportAsync(IEnumerable keys, Addressables.MergeMode mergeMode, Exception exception)
        {
            var resolveResult = await ResolveBundleNamesForKeysAsync(keys, mergeMode);
            var builder = new StringBuilder();
            builder.AppendLine("Addressables 依赖下载失败.");
            AppendBundleReport(builder, resolveResult);
            AppendExceptionReport(builder, exception);
            return builder.ToString().TrimEnd();
        }

        /// <summary>
        /// 解析单个 Key 对应的 Bundle 文件名.
        /// </summary>
        private static async Task<BundleResolveResult> ResolveBundleNamesForKeyAsync(object key, Type assetType)
        {
            var handle = Addressables.LoadResourceLocationsAsync(key, assetType);
            return await ResolveBundleNamesFromHandleAsync(handle);
        }

        /// <summary>
        /// 解析多个 Key 合并后的 Bundle 文件名.
        /// </summary>
        private static async Task<BundleResolveResult> ResolveBundleNamesForKeysAsync(IEnumerable keys, Addressables.MergeMode mergeMode)
        {
            var handle = Addressables.LoadResourceLocationsAsync(keys, mergeMode);
            return await ResolveBundleNamesFromHandleAsync(handle);
        }

        /// <summary>
        /// 从资源位置句柄里收集 Bundle 文件名.
        /// </summary>
        private static async Task<BundleResolveResult> ResolveBundleNamesFromHandleAsync(AsyncOperationHandle<IList<IResourceLocation>> handle)
        {
            try
            {
                try
                {
                    await handle.Task;
                }
                catch (Exception exception)
                {
                    return BundleResolveResult.Failed(exception.Message);
                }

                if (handle.Status != AsyncOperationStatus.Succeeded)
                {
                    return BundleResolveResult.Failed(handle.OperationException?.Message ?? "LoadResourceLocationsAsync failed.");
                }

                var bundleNames = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
                var visitedLocations = new HashSet<string>(StringComparer.Ordinal);
                if (handle.Result != null)
                {
                    foreach (var location in handle.Result)
                    {
                        CollectBundleNames(location, bundleNames, visitedLocations);
                    }
                }

                return BundleResolveResult.Succeeded(bundleNames);
            }
            finally
            {
                if (handle.IsValid())
                {
                    Addressables.Release(handle);
                }
            }
        }

        /// <summary>
        /// 递归收集资源位置及其依赖位置里的 Bundle 文件名.
        /// </summary>
        private static void CollectBundleNames(IResourceLocation location, ISet<string> bundleNames, ISet<string> visitedLocations)
        {
            if (location == null)
            {
                return;
            }

            var locationKey = $"{location.PrimaryKey}|{location.InternalId}";
            if (!visitedLocations.Add(locationKey))
            {
                return;
            }

            AddBundleName(location.InternalId, bundleNames);

            if (location.Dependencies == null)
            {
                return;
            }

            foreach (var dependency in location.Dependencies)
            {
                CollectBundleNames(dependency, bundleNames, visitedLocations);
            }
        }

        /// <summary>
        /// 从 InternalId 或 URL 中提取 Bundle 文件名.
        /// </summary>
        private static void AddBundleName(string internalId, ISet<string> bundleNames)
        {
            if (string.IsNullOrWhiteSpace(internalId))
            {
                return;
            }

            var normalizedId = internalId.Replace('\\', '/');
            var fileNameStart = normalizedId.LastIndexOf('/') + 1;
            var fileName = fileNameStart > 0 ? normalizedId.Substring(fileNameStart) : normalizedId;
            var queryIndex = fileName.IndexOf('?');
            if (queryIndex >= 0)
            {
                fileName = fileName.Substring(0, queryIndex);
            }

            if (fileName.EndsWith(".bundle", StringComparison.OrdinalIgnoreCase))
            {
                bundleNames.Add(fileName);
            }
        }

        /// <summary>
        /// 输出资源位置解析到的 Bundle 文件名.
        /// </summary>
        private static void AppendBundleReport(StringBuilder builder, BundleResolveResult resolveResult)
        {
            if (!string.IsNullOrWhiteSpace(resolveResult.Error))
            {
                builder.AppendLine($"需要的包名: 解析失败, {resolveResult.Error}");
                return;
            }

            if (resolveResult.BundleNames.Count == 0)
            {
                builder.AppendLine("需要的包名: 未解析到 bundle, 请检查 Catalog 或 Address 配置.");
                return;
            }

            builder.AppendLine("需要的包名:");
            foreach (var bundleName in resolveResult.BundleNames)
            {
                builder.AppendLine($"- {bundleName}");
            }
        }

        /// <summary>
        /// 输出异常里直接出现的 Bundle 文件名.
        /// </summary>
        private static void AppendExceptionReport(StringBuilder builder, Exception exception)
        {
            if (exception == null)
            {
                return;
            }

            var exceptionBundles = ExtractBundleNames(exception.ToString());
            if (exceptionBundles.Count > 0)
            {
                builder.AppendLine("异常中出现的包名:");
                foreach (var bundleName in exceptionBundles)
                {
                    builder.AppendLine($"- {bundleName}");
                }
            }

            builder.AppendLine($"Addressables Error: {exception.Message}");
        }

        /// <summary>
        /// 从异常文本中提取 Bundle 文件名.
        /// </summary>
        private static IReadOnlyList<string> ExtractBundleNames(string text)
        {
            var bundleNames = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            if (string.IsNullOrWhiteSpace(text))
            {
                return new List<string>();
            }

            foreach (Match match in BundleNameRegex.Matches(text))
            {
                bundleNames.Add(match.Value);
            }

            return new List<string>(bundleNames);
        }

        private readonly struct BundleResolveResult
        {
            private BundleResolveResult(IReadOnlyList<string> bundleNames, string error)
            {
                BundleNames = bundleNames;
                Error = error;
            }

            public IReadOnlyList<string> BundleNames { get; }
            public string Error { get; }

            public static BundleResolveResult Succeeded(IEnumerable<string> bundleNames)
            {
                return new BundleResolveResult(new List<string>(bundleNames), string.Empty);
            }

            public static BundleResolveResult Failed(string error)
            {
                return new BundleResolveResult(new List<string>(), error);
            }
        }
    }
}
