using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// 全局数据库管理器, 负责在进入游戏前通过 Addressables 加载数据库资产.
/// </summary>
public sealed class DataBaseManager : MonoBehaviour
{
    [SerializeField] private string itemDatabaseKey = "ItemDatabase";
    [SerializeField] private string weaponDatabaseKey = "WeaponDatabase";
    [SerializeField] private string buffDataBaseKey = "BuffDataBase";

    private AsyncOperationHandle<ItemDatabase> itemHandle;
    private AsyncOperationHandle<WeaponDatabase> weaponHandle;
    private AsyncOperationHandle<BuffDataBase> buffHandle;

    public static DataBaseManager Instance { get; private set; }

    public ItemDatabase Items { get; private set; }
    public WeaponDatabase Weapons { get; private set; }
    public BuffDataBase Buffs { get; private set; }
    public bool IsLoaded { get; private set; }

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
    /// 加载所有数据库, 登录界面或启动流程应等待该任务完成后再进入游戏.
    /// </summary>
    public async Task LoadAllAsync()
    {
        IsLoaded = false;

        itemHandle = Addressables.LoadAssetAsync<ItemDatabase>(itemDatabaseKey);
        weaponHandle = Addressables.LoadAssetAsync<WeaponDatabase>(weaponDatabaseKey);
        buffHandle = Addressables.LoadAssetAsync<BuffDataBase>(buffDataBaseKey);

        await Task.WhenAll(itemHandle.Task, weaponHandle.Task, buffHandle.Task);

        Items = GetLoadedAsset(itemHandle, nameof(ItemDatabase));
        Weapons = GetLoadedAsset(weaponHandle, nameof(WeaponDatabase));
        Buffs = GetLoadedAsset(buffHandle, nameof(BuffDataBase));
        IsLoaded = Items != null && Weapons != null && Buffs != null;
    }

    private static TDatabase GetLoadedAsset<TDatabase>(AsyncOperationHandle<TDatabase> handle, string databaseName)
        where TDatabase : ScriptableObject
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            return handle.Result;
        }

        Debug.LogError($"{nameof(DataBaseManager)}: 加载 {databaseName} 失败.");
        return null;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }

        ReleaseHandle(itemHandle);
        ReleaseHandle(weaponHandle);
        ReleaseHandle(buffHandle);
    }

    private static void ReleaseHandle<TDatabase>(AsyncOperationHandle<TDatabase> handle)
    {
        if (handle.IsValid())
        {
            Addressables.Release(handle);
        }
    }
}
