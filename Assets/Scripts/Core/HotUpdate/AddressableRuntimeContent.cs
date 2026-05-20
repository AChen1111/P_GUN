using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Game.Core
{
    /// <summary>
    /// Addressables 运行时内容缓存, Root 启动流程预加载后供玩法同步读取.
    /// </summary>
    public sealed class AddressableRuntimeContent : MonoBehaviour
    {
        private readonly Dictionary<string, UnityEngine.Object> assetsByAddress = new Dictionary<string, UnityEngine.Object>();
        private readonly Dictionary<string, Dictionary<int, GameObject>> prefabsByCategoryAndId = new Dictionary<string, Dictionary<int, GameObject>>();
        private readonly List<AsyncOperationHandle> ownedHandles = new List<AsyncOperationHandle>();

        public static AddressableRuntimeContent Instance { get; private set; }

        public bool IsReady { get; private set; }

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
        /// 预加载指定 Address, 后续玩法代码只从缓存同步读取.
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

            if (assetsByAddress.TryGetValue(address, out var cachedAsset))
            {
                return cachedAsset as TAsset;
            }

            var handle = Addressables.LoadAssetAsync<TAsset>(address);
            ownedHandles.Add(handle);
            await handle.Task;

            if (handle.Status != AsyncOperationStatus.Succeeded || handle.Result == null)
            {
                throw new InvalidOperationException($"Addressable asset load failed: {address}");
            }

            assetsByAddress[address] = handle.Result;
            return handle.Result;
        }

        /// <summary>
        /// 标记启动预加载完成, GameScene 只在该标记后使用热更内容.
        /// </summary>
        public void MarkReady()
        {
            IsReady = true;
        }

        /// <summary>
        /// 执行 TryGetAsset 逻辑.
        /// </summary>
        public bool TryGetAsset<TAsset>(string address, out TAsset asset)
            where TAsset : UnityEngine.Object
        {
            if (assetsByAddress.TryGetValue(address, out var cachedAsset) && cachedAsset is TAsset typedAsset)
            {
                asset = typedAsset;
                return true;
            }

            asset = null;
            return false;
        }

        /// <summary>
        /// 执行 RegisterPrefabById 逻辑.
        /// </summary>
        public void RegisterPrefabById(string category, int id, GameObject prefab)
        {
            if (string.IsNullOrWhiteSpace(category) || id <= 0 || prefab == null) return;

            if (!prefabsByCategoryAndId.TryGetValue(category, out var prefabsById))
            {
                prefabsById = new Dictionary<int, GameObject>();
                prefabsByCategoryAndId[category] = prefabsById;
            }

            prefabsById[id] = prefab;
        }

        /// <summary>
        /// 执行 TryGetPrefabById 逻辑.
        /// </summary>
        public bool TryGetPrefabById(string category, int id, out GameObject prefab)
        {
            if (prefabsByCategoryAndId.TryGetValue(category, out var prefabsById)
                && prefabsById.TryGetValue(id, out prefab)
                && prefab != null)
            {
                return true;
            }

            prefab = null;
            return false;
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

            foreach (var handle in ownedHandles)
            {
                if (handle.IsValid())
                {
                    Addressables.Release(handle);
                }
            }

            ownedHandles.Clear();
            assetsByAddress.Clear();
            prefabsByCategoryAndId.Clear();
        }
    }
}
