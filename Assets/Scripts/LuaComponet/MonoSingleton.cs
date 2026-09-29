using UnityEngine;

/// <summary>
/// 非泛型基类, 便于统一收进列表并按顺序初始化.
/// </summary>
public abstract class MonoSingleton : MonoBehaviour
{
    public bool IsDone { get; protected set; }

    /// <summary>
    /// 由外部按顺序调用, 内部启动初始化; 完成后置 IsDone=true.
    /// </summary>
    public abstract void BeginInit();
}

/// <summary>
/// MonoBehaviour 单例: 实例只来自场景或预制体, 不在代码里自动创建.
/// </summary>
public abstract class MonoSingleton<T> : MonoSingleton where T : MonoSingleton<T>
{
    private static T s_instance;

    /// <summary>
    /// 当前场景中的实例, 未摆放时为 null, 由调用方显式报错.
    /// </summary>
    public static T Instance => s_instance;

    protected virtual bool Persistent => false;

    private void Awake()
    {
        if (s_instance != null && s_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        s_instance = (T)this;
        if (Persistent)
        {
            DontDestroyOnLoad(gameObject);
        }

        BeginInit();
    }

    public override void BeginInit()
    {
        OnInit();
    }

    private void OnDestroy()
    {
        if (s_instance == this)
        {
            s_instance = null;
        }

        OnRelease();
    }

    /// <summary>
    /// 子类在此写初始化逻辑; 完成时置 IsDone=true.
    /// </summary>
    protected virtual void OnInit() { IsDone = true; }

    protected virtual void OnRelease() { }
}

/// <summary>
/// 跨场景保留的 MonoBehaviour 单例, 同样只认场景中已摆放的实例.
/// </summary>
public abstract class PersistentMonoSingleton<T> : MonoSingleton<T> where T : PersistentMonoSingleton<T>
{
    protected override bool Persistent => true;
}