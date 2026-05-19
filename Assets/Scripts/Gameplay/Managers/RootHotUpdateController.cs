using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Edgar.Unity;
using Game.Core;
using Game.Items;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Gameplay
{
    /// <summary>
    /// Root 场景启动控制器, 负责检查 Addressables 更新并预加载首屏玩法资源.
    /// </summary>
    public sealed class RootHotUpdateController : MonoBehaviour
    {
        private static readonly string[] DownloadLabels = { "room", "buff", "item", "enemy", "weapon" };
        private static readonly string[] ItemAddresses =
        {
            "item/heart",
            "item/chest",
            "item/speed_up",
            "item/power_up",
            "item/purify"
        };
        private static readonly string[] WeaponAddresses =
        {
            "weapon/pistol",
            "weapon/ak",
            "weapon/awp",
            "weapon/bow",
            "weapon/laser",
            "weapon/mp5",
            "weapon/rocket_gun",
            "weapon/shotgun"
        };

        [Header("启动流程")]
        [SerializeField] private string nextSceneName = "StartScene";
        [SerializeField] private DataBaseManager dataBaseManager;
        [SerializeField] private AddressableRuntimeContent runtimeContent;

        [Header("更新界面")]
        [SerializeField] private Text statusText;
        [SerializeField] private Slider progressSlider;

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

        private async Task RunBootFlowAsync()
        {
            ResolveSceneReferences();

            SetStatus("初始化资源系统...");
            SetProgress(0f);
            await InitializeAddressablesAsync();

            await TryUpdateRemoteContentAsync();

            SetStatus("加载数据库...");
            SetProgress(0.8f);
            await dataBaseManager.LoadAllAsync();
            if (!dataBaseManager.IsLoaded)
            {
                throw new InvalidOperationException("Required databases were not loaded.");
            }

            SetStatus("加载热更资源...");
            SetProgress(0.9f);
            await LoadRuntimeContentAsync();
            runtimeContent.MarkReady();

            SetStatus("进入主菜单...");
            SetProgress(1f);
            SceneManager.LoadScene(nextSceneName);
        }

        private void ResolveSceneReferences()
        {
            if (dataBaseManager == null)
            {
                dataBaseManager = FindObjectOfType<DataBaseManager>();
            }

            if (runtimeContent == null)
            {
                runtimeContent = FindObjectOfType<AddressableRuntimeContent>();
            }

            if (dataBaseManager == null)
            {
                throw new InvalidOperationException($"{nameof(DataBaseManager)} must be placed in Root scene.");
            }

            if (runtimeContent == null)
            {
                throw new InvalidOperationException($"{nameof(AddressableRuntimeContent)} must be placed in Root scene.");
            }
        }

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
                    SetProgress(0.7f);
                    return;
                }

                await DownloadDependenciesAsync(downloadSize);
            }
            catch (Exception exception)
            {
                // 更新失败时允许继续使用包体内置资源或本地缓存, 避免弱网直接阻断单机流程.
                Debug.LogWarning($"{nameof(RootHotUpdateController)}: 更新检查或下载失败, 将继续使用本地内容. Error: {exception.Message}", this);
                SetStatus("更新失败, 使用本地资源.");
                SetProgress(0.7f);
            }
        }

        private async Task UpdateCatalogsIfNeededAsync()
        {

            //返回:需要更新的 Catalog 列表, 如果没有更新则返回空列表. 
            var checkHandle = Addressables.CheckForCatalogUpdates(false);
            await checkHandle.Task;

            try
            {
                if (checkHandle.Status != AsyncOperationStatus.Succeeded)
                {
                    throw new InvalidOperationException("CheckForCatalogUpdates failed.");
                }

                /// 只有当确实有新的 Catalog 可用时才调用 UpdateCatalogs, 避免不必要的网络请求和资源重载.
                var catalogs = checkHandle.Result;
                if (catalogs == null || catalogs.Count == 0)
                {
                    return;
                }

                SetStatus("更新资源目录...");
                //这里只更新资源索引,没有发生资源的替换
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
        /// 获取需要下载的资源总大小, 用于在 UI 上显示下载进度. 这个方法会检查 DownloadLabels 标签下的所有资源, 包括它们的依赖项, 并返回需要下载的总字节数.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception> <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
        /// 下载依赖资源
        /// </summary>
        /// <param name="downloadSize"></param>
        /// <returns></returns>
        private async Task DownloadDependenciesAsync(long downloadSize)
        {
            var handle = Addressables.DownloadDependenciesAsync((IEnumerable)DownloadLabels, Addressables.MergeMode.Union, false);

            while (!handle.IsDone)
            {
                var status = handle.GetDownloadStatus();
                var percent = status.TotalBytes > 0 ? status.Percent : handle.PercentComplete;
                SetStatus($"下载资源 {FormatBytes(status.DownloadedBytes)} / {FormatBytes(downloadSize)}");
                SetProgress(Mathf.Lerp(0.4f, 0.75f, percent));
                await Task.Yield();
            }

            await handle.Task;

            try
            {
                if (handle.Status != AsyncOperationStatus.Succeeded)
                {
                    throw new InvalidOperationException("DownloadDependenciesAsync failed.");
                }
            }
            finally
            {
                Addressables.Release(handle);
            }
        }

        //加载资源
        private async Task LoadRuntimeContentAsync()
        {
            await runtimeContent.LoadAssetAsync<LevelGraph>("room/level1");

            //预加载物品资源
            foreach (var address in ItemAddresses)
            {
                var prefab = await runtimeContent.LoadAssetAsync<GameObject>(address);
                var item = prefab.GetComponent<Item>();
                if (item == null)
                {
                    throw new InvalidOperationException($"Item prefab missing Item component: {address}");
                }

                runtimeContent.RegisterPrefabById("item", item.ItemId, prefab);
            }

            foreach (var address in WeaponAddresses)
            {
                await runtimeContent.LoadAssetAsync<GameObject>(address);
            }
        }

        private void SetStatus(string message)
        {
            if (statusText != null)
            {
                statusText.text = message;
            }
        }

        private void SetProgress(float value)
        {
            if (progressSlider != null)
            {
                progressSlider.value = Mathf.Clamp01(value);
            }
        }

        private static string FormatBytes(long bytes)
        {
            if (bytes < 1024L) return $"{bytes} B";
            if (bytes < 1024L * 1024L) return $"{bytes / 1024f:0.0} KB";
            return $"{bytes / (1024f * 1024f):0.0} MB";
        }
    }
}
