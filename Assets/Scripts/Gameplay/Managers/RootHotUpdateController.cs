using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Gameplay
{
    /// <summary>
    /// Root 场景启动控制器, 只负责检查和下载 Addressables 更新.
    /// </summary>
    public sealed class RootHotUpdateController : MonoBehaviour
    {
        private static readonly string[] DownloadLabels = { "room", "buff", "item", "enemy", "weapon", "shared" };

        [Header("启动流程")]
        [SerializeField] private string nextSceneName = "StartScene";

        [Header("更新界面")]
        [SerializeField] private Text statusText;
        [SerializeField] private Slider progressSlider;

        /// <summary>
        /// 执行启动后的初始化逻辑.
        /// </summary>
        private async void Start()
        {
            try
            {
                await RunBootFlowAsync();
            }
            catch (Exception exception)
            {
                Debug.LogError($"{nameof(RootHotUpdateController)}: 启动流程失败, Error: {exception.Message}", this);
                SetStatus("启动失败, 请检查配置.");
                throw;
            }
        }

        /// <summary>
        /// 执行 Root 场景热更新检查流程.
        /// </summary>
        private async Task RunBootFlowAsync()
        {
            SetStatus("初始化资源系统...");
            SetProgress(0f);
            await InitializeAddressablesAsync();
            await TryUpdateRemoteContentAsync();
            SetStatus("进入主菜单...");
            SetProgress(1f);
            SceneManager.LoadScene(nextSceneName);
        }

        /// <summary>
        /// 检查远程 Catalog 并下载更新依赖.
        /// </summary>
        private async Task TryUpdateRemoteContentAsync()
        {
            try
            {
                SetStatus("检查资源更新...");
                SetProgress(0.15f);
                await UpdateCatalogsIfNeededAsync();
                SetStatus("检查下载大小...");
                SetProgress(0.35f);

                var downloadSize = await GetDownloadSizeAsync();
                if (downloadSize <= 0)
                {
                    SetStatus("资源已是最新.");
                    SetProgress(0.9f);
                    return;
                }

                await DownloadDependenciesAsync(downloadSize);
            }
            catch (Exception exception)
            {
                // 更新失败时允许继续使用包体内置资源或本地缓存, 避免弱网直接阻断单机流程.
                Debug.LogWarning($"{nameof(RootHotUpdateController)}: 更新检查或下载失败, 将继续使用本地内容. Error: {exception.Message}", this);
                SetStatus("更新失败, 使用本地资源.");
                SetProgress(0.9f);
            }
        }

        /// <summary>
        /// 初始化 Addressables 系统.
        /// </summary>
        private static async Task InitializeAddressablesAsync()
        {
            var handle = Addressables.InitializeAsync(false);
            await handle.Task;
            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                throw new InvalidOperationException("Addressables initialize failed.");
            }

            Addressables.Release(handle);
        }

        /// <summary>
        /// 只在确实有远程 Catalog 时更新资源目录.
        /// </summary>
        private async Task UpdateCatalogsIfNeededAsync()
        {
            // 返回需要更新的 Catalog 列表, 空列表表示本地目录已是最新.
            var checkHandle = Addressables.CheckForCatalogUpdates(false);
            await checkHandle.Task;
            try
            {
                if (checkHandle.Status != AsyncOperationStatus.Succeeded)
                {
                    throw new InvalidOperationException("CheckForCatalogUpdates failed.");
                }

                var catalogs = checkHandle.Result;
                if (catalogs == null || catalogs.Count == 0)
                {
                    return;
                }

                SetStatus("更新资源目录...");
                var updateHandle = Addressables.UpdateCatalogs(catalogs, false);
                await updateHandle.Task;
                try
                {
                    if (updateHandle.Status != AsyncOperationStatus.Succeeded)
                    {
                        throw new InvalidOperationException("UpdateCatalogs failed.");
                    }
                }
                finally
                {
                    Addressables.Release(updateHandle);
                }
            }
            finally
            {
                Addressables.Release(checkHandle);
            }
        }

        /// <summary>
        /// 获取需要下载的资源总大小.
        /// </summary>
        private static async Task<long> GetDownloadSizeAsync()
        {
            var handle = Addressables.GetDownloadSizeAsync((IEnumerable)DownloadLabels);
            await handle.Task;

            try
            {
                if (handle.Status != AsyncOperationStatus.Succeeded)
                {
                    throw new InvalidOperationException("GetDownloadSizeAsync failed.");
                }

                return handle.Result;
            }
            finally
            {
                Addressables.Release(handle);
            }
        }

        /// <summary>
        /// 下载热更新标签下的依赖资源.
        /// </summary>
        private async Task DownloadDependenciesAsync(long downloadSize)
        {
            var handle = Addressables.DownloadDependenciesAsync((IEnumerable)DownloadLabels, Addressables.MergeMode.Union, false);

            while (!handle.IsDone)
            {
                var status = handle.GetDownloadStatus();
                var percent = status.TotalBytes > 0 ? status.Percent : handle.PercentComplete;
                SetStatus($"下载资源 {FormatBytes(status.DownloadedBytes)} / {FormatBytes(downloadSize)}");
                SetProgress(Mathf.Lerp(0.4f, 0.9f, percent));
                await Task.Yield();
            }

            await handle.Task;

            try
            {
                if (handle.Status != AsyncOperationStatus.Succeeded)
                {
                    var report = await AddressableDiagnostics.BuildDownloadFailureReportAsync((IEnumerable)DownloadLabels, Addressables.MergeMode.Union, handle.OperationException);
                    Debug.LogError(report, this);
                    throw new InvalidOperationException(report, handle.OperationException);
                }
            }
            finally
            {
                Addressables.Release(handle);
            }
        }

        /// <summary>
        /// 执行 SetStatus 逻辑.
        /// </summary>
        private void SetStatus(string message)
        {
            if (statusText != null)
            {
                statusText.text = message;
            }
        }

        /// <summary>
        /// 执行 SetProgress 逻辑.
        /// </summary>
        private void SetProgress(float value)
        {
            if (progressSlider != null)
            {
                progressSlider.value = Mathf.Clamp01(value);
            }
        }

        /// <summary>
        /// 执行 FormatBytes 逻辑.
        /// </summary>
        private static string FormatBytes(long bytes)
        {
            if (bytes < 1024L) return $"{bytes} B";
            if (bytes < 1024L * 1024L) return $"{bytes / 1024f:0.0} KB";
            return $"{bytes / (1024f * 1024f):0.0} MB";
        }
    }
}
