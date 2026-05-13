using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

/// <summary>
/// 通用对象池基类.
/// 一个 PoolBase 组件可以管理多个 prefab 对应的 ObjectPool.
/// 适合玩家子弹, 敌人, 特效等同类型但不同 prefab 的对象复用.
/// </summary>
/// <typeparam name="T">
/// 被池管理的组件类型. T 必须是 MonoBehaviour, 并且实现 IPoolable,
/// 这样池在取出和回收对象时可以统一通知对象重置自身状态.
/// </summary>
public abstract class PoolBase<T> : MonoBehaviour where T : MonoBehaviour, IPoolable {
    // 每个 T 类型只保留一个池入口, 子类可以通过 new static Instance 暴露更具体的类型.
    private static PoolBase<T> _instance;

    public static PoolBase<T> Instance => _instance;

    [Header("Pool Config")]
    [SerializeField] private T defaultPrefab;
    [SerializeField] private int defaultCapacity = 16;
    [SerializeField] private int maxSize = 128;
    [SerializeField] private int prewarmCount = 0;
    [SerializeField] private bool collectionChecks = true;

    [Header("Hierarchy")]
    [SerializeField] private Transform activeRoot;
    [SerializeField] private Transform inactiveRoot;

    /// <summary>
    /// 按prefab分池,key是prefab,value是该prefab对应的ObjectPool
    /// </summary>
    private readonly Dictionary<T, ObjectPool<T>> poolByPrefab = new Dictionary<T, ObjectPool<T>>();
    /// <summary>
    /// 按实例分池,key是实例,value是该实例对应的ObjectPool
    /// </summary>
    private readonly Dictionary<T, ObjectPool<T>> poolByInstance = new Dictionary<T, ObjectPool<T>>();

    /// <summary>
    /// 按实例分池,key是实例,value是该实例对应的prefab
    /// </summary>
    private readonly Dictionary<T, T> prefabByInstance = new Dictionary<T, T>();

    // 子类需要读取默认 prefab 时使用, 外部仍然通过 Get 接口取对象.
    protected T DefaultPrefab => defaultPrefab;

#region 统计池中数据
    /// <summary>
    /// 所有子池中当前未激活对象的总数.
    /// </summary>
    public int CountInactive {
        get {
            var count = 0;
            foreach(var pool in poolByPrefab.Values) {
                count += pool.CountInactive;
            }

            return count;
        }
    }

    /// <summary>
    /// 所有子池中当前已取出对象的总数.
    /// </summary>
    public int CountActive {
        get {
            var count = 0;
            foreach(var pool in poolByPrefab.Values) {
                count += pool.CountActive;
            }

            return count;
        }
    }

    /// <summary>
    /// 所有子池中已创建对象的总数, 包括激活和未激活对象.
    /// </summary>
    public int CountAll {
        get {
            var count = 0;
            foreach(var pool in poolByPrefab.Values) {
                count += pool.CountAll;
            }

            return count;
        }
    }
#endregion
    
    protected virtual void Awake() {
        // 同一个 T 类型只允许存在一个 PoolBase 实例.
        // 如果场景里重复放了池组件, 后创建的会被销毁.
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        EnsureRoots();

        // 预热默认 prefab, 避免战斗中第一次 Instantiate 造成卡顿.
        if(defaultPrefab != null) {
            Prewarm(defaultPrefab, prewarmCount);
        }
    }

    /// <summary>
    /// 池子中只存一种prefab时调用
    /// </summary>
    public T Get() {
        return Get(defaultPrefab);
    }

    /// <summary>
    /// 池子中只存一种prefab时调用，并设置世界坐标和旋转
    /// </summary>
    public T Get(Vector3 position, Quaternion rotation) {
        return Get(defaultPrefab, position, rotation);
    }

    /// <summary>
    /// 按指定 prefab 获取对象.
    /// 第一次请求某个 prefab 时会懒创建它对应的 ObjectPool.
    /// </summary>
    public T Get(T prefab) {
        if(prefab == null) {
            Debug.LogError($"{GetType().Name}.Get failed: prefab is null.", this);
            return null;
        }

        return GetOrCreatePool(prefab).Get();
    }

    /// <summary>
    /// 按指定 prefab 获取对象, 并设置世界坐标和旋转.
    /// </summary>
    public T Get(T prefab, Vector3 position, Quaternion rotation) {
        var item = Get(prefab);
        if(item == null) return null;

        item.transform.SetPositionAndRotation(position, rotation);
        return item;
    }

    /// <summary>
    /// 回收对象.
    /// 这里不要求调用方知道对象来自哪个 prefab.
    /// Release 会通过实例映射找到来源池, 确保不同 prefab 的实例回到自己的池里.
    /// </summary>
    public void Release(T item) {
        if (item == null || !item.gameObject.activeSelf) return;

        if(poolByInstance.TryGetValue(item, out var pool)) {
            pool.Release(item);
            return;
        }

        // 如果对象没有登记在任何池里, 说明它不是由本池创建的, 直接销毁避免泄漏.
        Destroy(item.gameObject);
    }

    /// <summary>
    /// 预热默认 prefab 对应的池.
    /// </summary>
    public void Prewarm(int count) {
        Prewarm(defaultPrefab, count);
    }

    /// <summary>
    /// 预热指定 prefab 的池.
    /// 预热会先取出 count 个对象, 再立即放回池中.
    /// 这样运行时真正需要对象时可以直接复用, 减少第一次 Instantiate 的性能尖峰.
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

    /// <summary>
    /// 对象第一次由 prefab Instantiate 后调用.
    /// 子类可以在这里做一次性初始化, 例如缓存组件或设置父级外的额外数据.
    /// </summary>
    protected virtual void OnCreate(T item, T prefab) {
    }

    /// <summary>
    /// 对象从池中取出并激活后调用.
    /// 子类可以在这里做和池管理相关的取出逻辑.
    /// 具体对象自身的重置逻辑应优先放在 IPoolable.OnSpawnFromPool 中.
    /// </summary>
    protected virtual void OnGet(T item) {
    }

    /// <summary>
    /// 对象回收到池中时调用.
    /// 调用时机在对象自身 OnRecycleToPool 之后, SetActive(false) 之前.
    /// 子类可以在这里解除外部订阅或清理池级别状态.
    /// </summary>
    protected virtual void OnRelease(T item) {
    }

    /// <summary>
    /// Unity ObjectPool 因超过 maxSize 或销毁池对象而销毁实例前调用.
    /// 子类可以在这里释放额外资源.
    /// </summary>
    protected virtual void OnDestroyItem(T item) {
    }

    /// <summary>
    /// 获取 prefab 对应的 ObjectPool.
    /// 如果该 prefab 还没有子池, 就创建一个新的 ObjectPool 并登记到 poolByPrefab.
    /// </summary>
    private ObjectPool<T> GetOrCreatePool(T prefab) {
        if(poolByPrefab.TryGetValue(prefab, out var pool)) {
            return pool;
        }

        EnsureRoots();

        var capacity = Mathf.Max(1, defaultCapacity);
        var size = Mathf.Max(capacity, maxSize);

        // Unity ObjectPool 的四个核心回调分别对应创建, 取出, 回收, 销毁.
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

    /// <summary>
    /// 创建指定 prefab 的新实例.
    /// 新实例默认放在 inactiveRoot 下并保持未激活, 等 ObjectPool 取出时再激活.
    /// </summary>
    private T CreateItem(T prefab) {

        //把物体创建到inactiveRoot下，并保持未激活
        var item = Instantiate(prefab, inactiveRoot);
        item.gameObject.SetActive(false);

        prefabByInstance[item] = prefab;

        OnCreate(item, prefab);
        return item;
    }

    /// <summary>
    /// ObjectPool 取出对象时调用.
    /// 这里负责恢复实例到池的映射, 移动到 activeRoot, 激活对象, 然后通知对象进入使用状态.
    /// </summary>
    private void OnTakeFromPool(T item) {
        if(prefabByInstance.TryGetValue(item, out var prefab) && poolByPrefab.TryGetValue(prefab, out var pool)) {
            poolByInstance[item] = pool;
        }

        item.transform.SetParent(activeRoot, false);
        item.gameObject.SetActive(true);

        //NotifySpawnFromPool(item);
        item.OnSpawnFromPool();
        OnGet(item);
    }

    /// <summary>
    /// ObjectPool 回收对象时调用.
    /// 这里先通知对象清理自身状态, 再执行子类回收扩展点, 最后隐藏对象并移到 inactiveRoot.
    /// </summary>
    private void OnReturnedToPool(T item) {
        item.OnRecycleToPool();
        OnRelease(item);
        item.gameObject.SetActive(false);
        item.transform.SetParent(inactiveRoot, false);
    }

    /// <summary>
    /// ObjectPool 真正销毁对象时调用.
    /// 需要同步清除实例映射, 避免字典保留已经销毁的对象引用.
    /// </summary>
    private void DestroyItem(T item) {
        OnDestroyItem(item);
        if (item != null) {
            poolByInstance.Remove(item);
            prefabByInstance.Remove(item);
            Destroy(item.gameObject);
        }
    }

    /// <summary>
    /// 确保 activeRoot 和 inactiveRoot 存在.
    /// 如果没有在 Inspector 中手动指定, 就自动创建子节点.
    /// </summary>
    private void EnsureRoots() {
        if (activeRoot == null) {
            activeRoot = CreateRoot("Active");
        }

        if (inactiveRoot == null) {
            inactiveRoot = CreateRoot("Inactive");
        }
    }

    /// <summary>
    /// 创建池内部使用的分组节点.
    /// </summary>
    private Transform CreateRoot(string rootName) {
        var root = new GameObject(rootName).transform;
        root.SetParent(transform, false);
        return root;
    }

    /// <summary>
    /// Inspector 参数校验.
    /// 保证容量配置始终有效, 避免运行时创建 ObjectPool 时传入非法数值.
    /// </summary>
    private void OnValidate() {
        defaultCapacity = Mathf.Max(1, defaultCapacity);
        maxSize = Mathf.Max(defaultCapacity, maxSize);
        prewarmCount = Mathf.Clamp(prewarmCount, 0, maxSize);
    }
}
