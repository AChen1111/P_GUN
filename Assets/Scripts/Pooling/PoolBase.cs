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
    [SerializeField] private int defaultCapacity = 16;
    [SerializeField] private int maxSize = 128;
    [SerializeField] private List<PrefabInfo> prefabInfos;

#region 池中物体信息
    [System.Serializable]
    private struct PrefabInfo
    {
        public T prefab; //预制体
        public int prewarmCount; //预热数量
    }
#endregion

    [SerializeField] private bool collectionChecks = true;



    [Header("Hierarchy")]
    [SerializeField] private Transform activeRoot; //激活状态的物体父节点
    [SerializeField] private Transform inactiveRoot; //未激活状态的物体父节点

    /// <summary>
    /// 通过预制体找对应的池子
    /// </summary>
    private readonly Dictionary<T, ObjectPool<T>> _prefab2Pool = new Dictionary<T, ObjectPool<T>>();

    /// <summary>
    /// 通过实例找对应的池子
    /// </summary>
    private readonly Dictionary<T, ObjectPool<T>> _instance2Pool = new Dictionary<T, ObjectPool<T>>();

    /// <summary>
    /// 通过实例找对应的预制体
    /// </summary>
    private readonly Dictionary<T, T> _instance2Prefab = new Dictionary<T, T>();

    protected T defaultPrefab => prefabInfos[0].prefab;
    public T DefaultPrefab => defaultPrefab;

#region 统计池中数据
    /// <summary>
    /// 所有子池中当前未激活对象的总数.
    /// </summary>
    public int CountInactive {
        get {
            var count = 0;
            foreach(var pool in _prefab2Pool.Values) {
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
            foreach(var pool in _prefab2Pool.Values) {
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
            foreach(var pool in _prefab2Pool.Values) {
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

        foreach(var prefabInfo in prefabInfos) {
            Prewarm(prefabInfo.prefab, prefabInfo.prewarmCount);
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

        if(_instance2Pool.TryGetValue(item, out var pool)) {
            pool.Release(item);
            return;
        }

        // 如果对象没有登记在任何池里, 说明它不是由本池创建的, 直接销毁避免泄漏.
        Destroy(item.gameObject);
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


#region 子类覆写
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
#endregion


    /// <summary>
    /// 获取 prefab 对应的 ObjectPool.
    /// 如果该 prefab 还没有子池, 就创建一个新的 ObjectPool 并登记到 poolByPrefab.
    /// </summary>
    private ObjectPool<T> GetOrCreatePool(T prefab) {
        if(_prefab2Pool.TryGetValue(prefab, out var pool)) {
            return pool;
        }

        var capacity = Mathf.Max(1, defaultCapacity);
        var size = Mathf.Max(capacity, maxSize);

        // Unity ObjectPool 的四个核心回调分别对应创建, 取出, 回收, 销毁.
        pool = new ObjectPool<T>(
            () => CreateItem(prefab),
            OnTakeFromPool, //取出时调用
            OnReturnedToPool, //回收时调用
            DestroyItem, //销毁时调用
            collectionChecks, //防止重复回收对象
            capacity, //容量
            size //最大数量
        );

        _prefab2Pool.Add(prefab, pool);
        return pool;
    }

    /// <summary>
    /// 创建指定 prefab 的新实例.
    /// </summary>
    private T CreateItem(T prefab) {

        //把物体创建到inactiveRoot下，并保持未激活
        var item = Instantiate(prefab, inactiveRoot);
        item.gameObject.SetActive(false);

        _instance2Prefab[item] = prefab;

        OnCreate(item, prefab);
        return item;
    }

    /// <summary>
    /// ObjectPool 取出对象时调用.
    /// 这里负责恢复实例到池的映射, 移动到 activeRoot, 激活对象, 然后通知对象进入使用状态.
    /// </summary>
    private void OnTakeFromPool(T item) {
        if(_instance2Prefab.TryGetValue(item, out var prefab) && _prefab2Pool.TryGetValue(prefab, out var pool)) {
            _instance2Pool[item] = pool;
        }

        item.transform.SetParent(activeRoot, false);
        item.gameObject.SetActive(true);

        item.OnSpawnFromPool();//重置内部状态
        OnGet(item);
    }

    /// <summary>
    /// ObjectPool 回收对象时调用.
    /// 这里先通知对象清理自身状态, 再执行子类回收扩展点, 最后隐藏对象并移到 inactiveRoot.
    /// </summary>
    private void OnReturnedToPool(T item) {
        item.OnRecycleToPool();//重置内部状态
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
            _instance2Pool.Remove(item);
            _instance2Prefab.Remove(item);
            Destroy(item.gameObject);
        }
    }

}

