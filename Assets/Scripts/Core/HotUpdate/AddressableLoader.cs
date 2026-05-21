using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Game.Core
{
    /// <summary>
    /// Addressables 按需加载器, 由 Root 场景显式挂载并供业务对象请求资源.
    /// </summary>
    public sealed class AddressableLoader : MonoBehaviour
    {
        private readonly Dictionary<string, UnityEngine.Object> assetsByAddress = new Dictionary<string, UnityEngine.Object>();
        private readonly Dictionary<string, AsyncOperationHandle> handlesByAddress = new Dictionary<string, AsyncOperationHandle>();
        private readonly Dictionary<string, Task<UnityEngine.Object>> loadingTasksByAddress = new Dictionary<string, Task<UnityEngine.Object>>();

        public static AddressableLoader Instance { get; private set; }

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
        }

        /// <summary>
        /// 按 Addressables 地址加载资源, 同一地址的并发请求会复用同一个加载任务.
        /// </summary>
        /// <param name="address">Addressables 地址.</param>
        /// <typeparam name="TAsset">资源类型.</typeparam>
        /// <returns>加载到的资源.</returns>
        public async Task<TAsset> LoadAssetAsync<TAsset>(string address)
            where TAsset : UnityEngine.Object
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException("Address must not be empty.", nameof(address));
            }

            if (TryGetCachedAsset(address, out TAsset cachedAsset))
            {
                return cachedAsset;
            }

            if (loadingTasksByAddress.TryGetValue(address, out var existingTask))
            {
                return CastLoadedAsset<TAsset>(address, await existingTask);
            }

            var task = LoadAssetInternalAsync<TAsset>(address);
            loadingTasksByAddress[address] = task;
            try
            {
                return CastLoadedAsset<TAsset>(address, await task);
            }
            finally
            {
                loadingTasksByAddress.Remove(address);
            }
        }

        /// <summary>
        /// 只读取已加载缓存, 不触发新的 Addressables 请求.
        /// </summary>
        public bool TryGetLoadedAsset<TAsset>(string address, out TAsset asset)
            where TAsset : UnityEngine.Object
        {
            if (!string.IsNullOrWhiteSpace(address) && TryGetCachedAsset(address, out asset))
            {
                return true;
            }

            asset = null;
            return false;
        }

        /// <summary>
        /// 释放指定地址的缓存资源.
        /// </summary>
        public void Release(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException("Address must not be empty.", nameof(address));
            }

            if (handlesByAddress.TryGetValue(address, out var handle) && handle.IsValid())
            {
                Addressables.Release(handle);
            }

            handlesByAddress.Remove(address);
            assetsByAddress.Remove(address);
            loadingTasksByAddress.Remove(address);
        }

        /// <summary>
        /// 释放加载器持有的所有缓存资源.
        /// </summary>
        public void ReleaseAll()
        {
            foreach (var handle in handlesByAddress.Values)
            {
                if (handle.IsValid())
                {
                    Addressables.Release(handle);
                }
            }

            handlesByAddress.Clear();
            assetsByAddress.Clear();
            loadingTasksByAddress.Clear();
        }

        /// <summary>
        /// 执行 Addressables 实际加载逻辑.
        /// </summary>
        private async Task<UnityEngine.Object> LoadAssetInternalAsync<TAsset>(string address)
            where TAsset : UnityEngine.Object
        {
            var handle = Addressables.LoadAssetAsync<TAsset>(address);
            await handle.Task;

            if (handle.Status != AsyncOperationStatus.Succeeded || handle.Result == null)
            {
                var report = await AddressableDiagnostics.BuildAssetLoadFailureReportAsync(address, typeof(TAsset), handle.OperationException);
                Debug.LogError(report, this);

                if (handle.IsValid())
                {
                    Addressables.Release(handle);
                }

                throw new InvalidOperationException(report, handle.OperationException);
            }

            assetsByAddress[address] = handle.Result;
            handlesByAddress[address] = handle;
            return handle.Result;
        }

        /// <summary>
        /// 从缓存中取出指定类型的资源.
        /// </summary>
        private bool TryGetCachedAsset<TAsset>(string address, out TAsset asset)
            where TAsset : UnityEngine.Object
        {
            if (assetsByAddress.TryGetValue(address, out var cachedAsset))
            {
                if (cachedAsset is TAsset typedAsset)
                {
                    asset = typedAsset;
                    return true;
                }

                throw new InvalidOperationException($"Addressable asset type mismatch. Address: {address}, Expected: {typeof(TAsset).Name}, Actual: {cachedAsset.GetType().Name}");
            }

            asset = null;
            return false;
        }

        /// <summary>
        /// 校验异步加载结果类型.
        /// </summary>
        private static TAsset CastLoadedAsset<TAsset>(string address, UnityEngine.Object asset)
            where TAsset : UnityEngine.Object
        {
            if (asset is TAsset typedAsset)
            {
                return typedAsset;
            }

            throw new InvalidOperationException($"Addressable asset type mismatch. Address: {address}, Expected: {typeof(TAsset).Name}, Actual: {asset?.GetType().Name ?? "null"}");
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

            ReleaseAll();
        }
    }
}
