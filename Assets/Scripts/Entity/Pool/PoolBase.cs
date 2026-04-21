using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

/// <summary>
/// 通用对象池基类。
/// 同一个池组件可以按 prefab 自动拆分多个 ObjectPool，适合不同武器子弹共用一套池管理逻辑。
/// </summary>
public abstract class PoolBase<T> : MonoBehaviour where T : MonoBehaviour {
    private static PoolBase<T> instance;

    public static PoolBase<T> Instance => instance;

    [Header("Pool Config")]
    [SerializeField] private T defaultPrefab;
    [SerializeField] private int defaultCapacity = 16;
    [SerializeField] private int maxSize = 128;
    [SerializeField] private int prewarmCount = 0;
    [SerializeField] private bool collectionChecks = true;

    [Header("Hierarchy")]
    [SerializeField] private Transform activeRoot;
    [SerializeField] private Transform inactiveRoot;

    private readonly Dictionary<T, ObjectPool<T>> poolByPrefab = new Dictionary<T, ObjectPool<T>>();
    private readonly Dictionary<T, ObjectPool<T>> poolByInstance = new Dictionary<T, ObjectPool<T>>();
    private readonly Dictionary<T, T> prefabByInstance = new Dictionary<T, T>();

    protected T DefaultPrefab => defaultPrefab;
    public int CountInactive {
        get {
            var count = 0;
            foreach(var pool in poolByPrefab.Values) {
                count += pool.CountInactive;
            }

            return count;
        }
    }

    public int CountActive {
        get {
            var count = 0;
            foreach(var pool in poolByPrefab.Values) {
                count += pool.CountActive;
            }

            return count;
        }
    }

    public int CountAll {
        get {
            var count = 0;
            foreach(var pool in poolByPrefab.Values) {
                count += pool.CountAll;
            }

            return count;
        }
    }

    protected virtual void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        EnsureRoots();

        if(defaultPrefab != null) {
            Prewarm(defaultPrefab, prewarmCount);
        }
    }

    public T Get() {
        return Get(defaultPrefab);
    }

    public T Get(Vector3 position, Quaternion rotation) {
        return Get(defaultPrefab, position, rotation);
    }

    /// <summary>
    /// 按指定 prefab 获取对象。第一次请求某个 prefab 时会懒创建对应的 ObjectPool。
    /// </summary>
    public T Get(T prefab) {
        if(prefab == null) {
            Debug.LogError($"{GetType().Name}.Get failed: prefab is null.", this);
            return null;
        }

        return GetOrCreatePool(prefab).Get();
    }

    public T Get(T prefab, Vector3 position, Quaternion rotation) {
        var item = Get(prefab);
        if(item == null) return null;

        item.transform.SetPositionAndRotation(position, rotation);
        return item;
    }

    /// <summary>
    /// 回收对象时通过实例反查来源池，确保不同 prefab 的对象回到自己的池里。
    /// </summary>
    public void Release(T item) {
        if (item == null) return;
        if (!item.gameObject.activeSelf) return;

        if(poolByInstance.TryGetValue(item, out var pool)) {
            pool.Release(item);
            return;
        }

        Destroy(item.gameObject);
    }

    public void Prewarm(int count) {
        Prewarm(defaultPrefab, count);
    }

    /// <summary>
    /// 预热指定 prefab 的池，减少战斗中首次 Instantiate 的尖峰。
    /// </summary>
    public void Prewarm(T prefab, int count) {
        if(prefab == null || count <= 0) return;

        var pool = GetOrCreatePool(prefab);

        var items = new T[count];
        for (var i = 0; i < count; i++) {
            items[i] = pool.Get();
        }

        for (var i = 0; i < count; i++) {
            pool.Release(items[i]);
        }
    }

    protected virtual void OnCreate(T item, T prefab) {
    }

    protected virtual void OnGet(T item) {
    }

    protected virtual void OnRelease(T item) {
    }

    protected virtual void OnDestroyItem(T item) {
    }

    private ObjectPool<T> GetOrCreatePool(T prefab) {
        if(poolByPrefab.TryGetValue(prefab, out var pool)) {
            return pool;
        }

        EnsureRoots();

        var capacity = Mathf.Max(1, defaultCapacity);
        var size = Mathf.Max(capacity, maxSize);

        pool = new ObjectPool<T>(
            () => CreateItem(prefab),
            OnTakeFromPool,
            OnReturnedToPool,
            DestroyItem,
            collectionChecks,
            capacity,
            size
        );

        poolByPrefab.Add(prefab, pool);
        return pool;
    }

    private T CreateItem(T prefab) {
        var item = Instantiate(prefab, inactiveRoot);
        item.gameObject.SetActive(false);
        prefabByInstance[item] = prefab;

        OnCreate(item, prefab);
        return item;
    }

    private void OnTakeFromPool(T item) {
        if(prefabByInstance.TryGetValue(item, out var prefab) && poolByPrefab.TryGetValue(prefab, out var pool)) {
            poolByInstance[item] = pool;
        }

        item.transform.SetParent(activeRoot, false);
        item.gameObject.SetActive(true);

        OnGet(item);
    }

    private void OnReturnedToPool(T item) {
        OnRelease(item);
        item.gameObject.SetActive(false);
        item.transform.SetParent(inactiveRoot, false);
    }

    private void DestroyItem(T item) {
        OnDestroyItem(item);
        if (item != null) {
            poolByInstance.Remove(item);
            prefabByInstance.Remove(item);
            Destroy(item.gameObject);
        }
    }

    private void EnsureRoots() {
        if (activeRoot == null) {
            activeRoot = CreateRoot("Active");
        }

        if (inactiveRoot == null) {
            inactiveRoot = CreateRoot("Inactive");
        }
    }

    private Transform CreateRoot(string rootName) {
        var root = new GameObject(rootName).transform;
        root.SetParent(transform, false);
        return root;
    }

    private void OnValidate() {
        defaultCapacity = Mathf.Max(1, defaultCapacity);
        maxSize = Mathf.Max(defaultCapacity, maxSize);
        prewarmCount = Mathf.Clamp(prewarmCount, 0, maxSize);
    }
}
