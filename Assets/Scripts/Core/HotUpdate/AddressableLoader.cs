using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Game.Core
{
    /// <summary>
    /// Root 显式挂载的加载器, 业务只接受分类 Catalog 中的短名.
    /// </summary>
    public sealed class AddressableLoader : MonoBehaviour
    {
        [SerializeField] private AssetReferenceT<AddressableCatalog>[] catalogReferences;
        private readonly Dictionary<AddressableAssetKind, AddressableCatalog> catalogs = new Dictionary<AddressableAssetKind, AddressableCatalog>();
        private readonly List<AsyncOperationHandle<AddressableCatalog>> catalogHandles = new List<AsyncOperationHandle<AddressableCatalog>>();
        private readonly Dictionary<string, Object> assets = new Dictionary<string, Object>();
        private readonly Dictionary<string, AsyncOperationHandle> handles = new Dictionary<string, AsyncOperationHandle>();
        private readonly Dictionary<string, Task<Object>> loadingTasks = new Dictionary<string, Task<Object>>();
        private readonly Dictionary<string, AsyncOperationHandle<SceneInstance>> scenes = new Dictionary<string, AsyncOperationHandle<SceneInstance>>();
        private Task initializationTask;
        private bool sceneTransition;
        private bool destroyed;
        public static AddressableLoader Instance { get; private set; }
        public bool IsInitialized { get; private set; }
        public bool IsSceneTransitioning => sceneTransition;
        public event Action<string, float> SceneLoadProgress;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public Task InitializeAsync()
        {
            if (IsInitialized) return Task.CompletedTask;
            return initializationTask ?? (initializationTask = InitializeInternalAsync());
        }

        private async Task InitializeInternalAsync()
        {
            try
            {
                if (catalogReferences == null || catalogReferences.Length != Enum.GetValues(typeof(AddressableAssetKind)).Length)
                    throw new InvalidOperationException("Root must bind all seven Catalog references.");
                foreach (var reference in catalogReferences)
                {
                    if (reference == null || !reference.RuntimeKeyIsValid()) throw new InvalidOperationException("Root Catalog reference is invalid.");
                    catalogHandles.Add(Addressables.LoadAssetAsync<AddressableCatalog>(reference));
                }
                foreach (var handle in catalogHandles)
                {
                    var catalog = await handle.Task;
                    if (destroyed) throw new ObjectDisposedException(nameof(AddressableLoader));
                    catalog.BuildMap();
                    catalogs.Add(catalog.kind, catalog);
                }
                foreach (AddressableAssetKind kind in Enum.GetValues(typeof(AddressableAssetKind)))
                    if (!catalogs.ContainsKey(kind)) throw new InvalidOperationException($"Missing Catalog: {kind}.");
                IsInitialized = true;
            }
            catch
            {
                foreach (var handle in catalogHandles) if (handle.IsValid()) Addressables.Release(handle);
                catalogHandles.Clear();
                catalogs.Clear();
                throw;
            }
        }

        private AddressableCatalog Catalog(AddressableAssetKind kind)
        {
            if (!IsInitialized) throw new InvalidOperationException("Catalogs must initialize before asset loading.");
            return catalogs[kind];
        }
        private static string CacheKey(AddressableAssetKind kind, string key) => $"{kind}:{key}";

        public async Task<T> LoadAssetAsync<T>(string key) where T : Object
        {
            if (typeof(Component).IsAssignableFrom(typeof(T))) throw new InvalidOperationException("Load GameObject first, then get its component.");
            var kind = AddressableCatalog.KindFor(typeof(T));
            var entry = Catalog(kind).Get(key);
            var cacheKey = CacheKey(kind, key);
            if (assets.TryGetValue(cacheKey, out var cached)) return Cast<T>(key, cached);
            if (!loadingTasks.TryGetValue(cacheKey, out var task))
            {
                // 先登记共享任务, 防止同步完成时遗留加载任务.
                var completion = new TaskCompletionSource<Object>();
                task = completion.Task;
                loadingTasks.Add(cacheKey, task);
                _ = CompleteLoadAsync<T>(entry, cacheKey, completion);
            }
            return Cast<T>(key, await task);
        }

        private async Task CompleteLoadAsync<T>(AddressableCatalogEntry entry, string cacheKey, TaskCompletionSource<Object> completion) where T : Object
        {
            AsyncOperationHandle<T> handle = default;
            try
            {
                handle = Addressables.LoadAssetAsync<T>(entry.reference);
                var asset = await handle.Task;
                if (destroyed) throw new ObjectDisposedException(nameof(AddressableLoader));
                if (handle.Status != AsyncOperationStatus.Succeeded || asset == null) throw new InvalidOperationException("Addressables returned no asset.", handle.OperationException);
                assets.Add(cacheKey, asset);
                handles.Add(cacheKey, handle);
                loadingTasks.Remove(cacheKey);
                completion.SetResult(asset);
            }
            catch (Exception exception)
            {
                string report;
                try { report = await AddressableDiagnostics.BuildAssetLoadFailureReportAsync(entry.reference.RuntimeKey.ToString(), typeof(T), exception); }
                catch (Exception diagnosticError) { report = exception + "\nBundle diagnostics failed: " + diagnosticError.Message; }
                if (handle.IsValid()) Addressables.Release(handle);
                Debug.LogError($"Key: {entry.key}, Reference: {entry.reference.RuntimeKey}\n{report}", this);
                loadingTasks.Remove(cacheKey);
                completion.SetException(new InvalidOperationException($"Key: {entry.key}\n{report}", exception));
            }
            finally { loadingTasks.Remove(cacheKey); }
        }

        private static T Cast<T>(string key, Object asset) where T : Object => asset as T ?? throw new InvalidOperationException($"Key {key}: expected {typeof(T).Name}, actual {asset?.GetType().Name}.");

        public bool TryGetLoadedAsset<T>(string key, out T asset) where T : Object
        {
            Catalog(AddressableCatalog.KindFor(typeof(T))).Get(key);
            if (assets.TryGetValue(CacheKey(AddressableCatalog.KindFor(typeof(T)), key), out var value)) { asset = Cast<T>(key, value); return true; }
            asset = null;
            return false;
        }
        public T GetLoadedAsset<T>(string key) where T : Object => TryGetLoadedAsset<T>(key, out var asset) ? asset : throw new InvalidOperationException($"Asset {typeof(T).Name}/{key} was not preloaded for this stage.");

        public async Task<IReadOnlyDictionary<string, T>> LoadAssetsByLabelAsync<T>(string label) where T : Object
        {
            var result = new Dictionary<string, T>();
            foreach (var entry in Catalog(AddressableCatalog.KindFor(typeof(T))).entries)
                if (entry.labels.Contains(label)) result.Add(string.IsNullOrEmpty(entry.luaModulePath) ? entry.key : entry.luaModulePath, await LoadAssetAsync<T>(entry.key));
            return result;
        }

        public async Task PreloadAsync(string manifestKey, IProgress<float> progress = null)
        {
            var manifest = await LoadAssetAsync<AddressablePreloadManifest>(manifestKey);
            for (var i = 0; i < manifest.resources.Count; i++)
            {
                var resource = manifest.resources[i];
                switch (resource.kind)
                {
                    case AddressableAssetKind.Sprite: await LoadAssetAsync<Sprite>(resource.key); break;
                    case AddressableAssetKind.Prefab: await LoadAssetAsync<GameObject>(resource.key); break;
                    case AddressableAssetKind.AudioClip: await LoadAssetAsync<AudioClip>(resource.key); break;
                    case AddressableAssetKind.TextAsset: await LoadAssetAsync<TextAsset>(resource.key); break;
                    case AddressableAssetKind.ScriptableObject: await LoadAssetAsync<ScriptableObject>(resource.key); break;
                    case AddressableAssetKind.Object: await LoadAssetAsync<Object>(resource.key); break;
                    default: throw new InvalidOperationException("Scenes must not be in resource preload manifests.");
                }
                progress?.Report((i + 1f) / manifest.resources.Count);
            }
        }

        public Task<SceneInstance> LoadSceneAsync(string key) => TransitionAsync(key, false);
        public Task<SceneInstance> ReloadSceneAsync(string key) => TransitionAsync(key, true);

        private async Task<SceneInstance> TransitionAsync(string key, bool reload)
        {
            if (sceneTransition) throw new InvalidOperationException("A scene transition is already running.");
            var entry = Catalog(AddressableAssetKind.Scene).Get(key);
            if (!reload && scenes.TryGetValue(key, out var cached) && cached.IsValid() && cached.Result.Scene.isLoaded) return cached.Result;
            sceneTransition = true;
            AsyncOperationHandle<SceneInstance> handle = default;
            try
            {
                SceneLoadProgress?.Invoke("预加载场景资源...", 0f);
                await PreloadAsync(entry.preloadManifestKey, new Progress<float>(p => SceneLoadProgress?.Invoke("预加载场景资源...", p * 0.8f)));
                // 场景激活前完成资源加载, Awake 和池预热不执行网络请求.
                handle = Addressables.LoadSceneAsync(entry.reference, LoadSceneMode.Single);
                while (!handle.IsDone) { SceneLoadProgress?.Invoke("进入场景...", 0.8f + handle.PercentComplete * 0.2f); await Task.Yield(); }
                var scene = await handle.Task;
                foreach (var old in scenes.Values) if (old.IsValid()) Addressables.Release(old);
                scenes.Clear();
                scenes.Add(key, handle);
                SceneLoadProgress?.Invoke("", 1f);
                return scene;
            }
            catch (Exception exception)
            {
                if (handle.IsValid()) Addressables.Release(handle);
                SceneLoadProgress?.Invoke($"场景加载失败: {exception.Message}", 0f);
                throw;
            }
            finally { sceneTransition = false; }
        }

        public async Task UnloadSceneAsync(string key)
        {
            if (!scenes.TryGetValue(key, out var handle)) throw new KeyNotFoundException(key);
            await Addressables.UnloadSceneAsync(handle).Task;
            scenes.Remove(key);
        }

        public void Release<T>(string key) where T : Object
        {
            var cacheKey = CacheKey(AddressableCatalog.KindFor(typeof(T)), key);
            if (loadingTasks.ContainsKey(cacheKey)) throw new InvalidOperationException($"Cannot release loading asset: {key}.");
            if (handles.TryGetValue(cacheKey, out var handle) && handle.IsValid()) Addressables.Release(handle);
            handles.Remove(cacheKey);
            assets.Remove(cacheKey);
        }

        public void ReleaseAll()
        {
            if (loadingTasks.Count > 0) throw new InvalidOperationException("Cannot release assets while requests are running.");
            foreach (var handle in handles.Values) if (handle.IsValid()) Addressables.Release(handle);
            handles.Clear();
            assets.Clear();
        }

        private void OnDestroy()
        {
            if (Instance != this) return;
            destroyed = true;
            foreach (var handle in handles.Values) if (handle.IsValid()) Addressables.Release(handle);
            foreach (var handle in catalogHandles) if (handle.IsValid()) Addressables.Release(handle);
            // 场景卸载拥有其句柄生命周期, 自动释放的句柄只在有效时处理.
            foreach (var handle in scenes.Values) if (handle.IsValid()) Addressables.Release(handle);
            handles.Clear();
            assets.Clear();
            Instance = null;
        }
    }
}
