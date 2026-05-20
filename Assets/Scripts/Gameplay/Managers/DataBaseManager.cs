using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    /// <summary>
    /// 全局数据库管理器, 负责在进入游戏前通过 Addressables 加载数据库资产.
    /// </summary>
    public sealed class DataBaseManager : MonoBehaviour
    {
        [SerializeField] private string itemDatabaseKey = "ItemDatabase";
        [SerializeField] private string weaponDatabaseKey = "WeaponDatabase";
        [SerializeField] private string buffDataBaseKey = "BuffDataBase";
        [SerializeField] private string enemyDatabaseKey = "EnemyDatabase";

        private AsyncOperationHandle<ItemDatabase> itemHandle;
        private AsyncOperationHandle<WeaponDatabase> weaponHandle;
        private AsyncOperationHandle<BuffDataBase> buffHandle;
        private AsyncOperationHandle<EnemyDatabase> enemyHandle;

        public static DataBaseManager Instance { get; private set; }

        public ItemDatabase Items { get; private set; }
        public WeaponDatabase Weapons { get; private set; }
        public BuffDataBase Buffs { get; private set; }
        public EnemyDatabase Enemies { get; private set; }
        public bool IsLoaded { get; private set; }

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
        /// 加载所有数据库, 登录界面或启动流程应等待该任务完成后再进入游戏.
        /// </summary>
        public async Task LoadAllAsync()
        {
            IsLoaded = false;

            itemHandle = Addressables.LoadAssetAsync<ItemDatabase>(itemDatabaseKey);
            weaponHandle = Addressables.LoadAssetAsync<WeaponDatabase>(weaponDatabaseKey);
            buffHandle = Addressables.LoadAssetAsync<BuffDataBase>(buffDataBaseKey);
            enemyHandle = Addressables.LoadAssetAsync<EnemyDatabase>(enemyDatabaseKey);

            await Task.WhenAll(itemHandle.Task, weaponHandle.Task, buffHandle.Task, enemyHandle.Task);

            Items = GetLoadedAsset(itemHandle, nameof(ItemDatabase));
            Weapons = GetLoadedAsset(weaponHandle, nameof(WeaponDatabase));
            Buffs = GetLoadedAsset(buffHandle, nameof(BuffDataBase));
            Enemies = GetLoadedAsset(enemyHandle, nameof(EnemyDatabase));
            IsLoaded = Items != null && Weapons != null && Buffs != null && Enemies != null;

            if (IsLoaded)
            {
                ItemDatabase.SetRuntimeDatabase(Items);
            }
        }

        /// <summary>
        /// 执行 GetLoadedAsset 逻辑.
        /// </summary>
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

        /// <summary>
        /// 释放销毁时持有的运行时状态.
        /// </summary>
        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }

            ReleaseHandle(itemHandle);
            ReleaseHandle(weaponHandle);
            ReleaseHandle(buffHandle);
            ReleaseHandle(enemyHandle);
            ItemDatabase.ClearRuntimeDatabase(Items);
        }

        /// <summary>
        /// 执行 ReleaseHandle 逻辑.
        /// </summary>
        private static void ReleaseHandle<TDatabase>(AsyncOperationHandle<TDatabase> handle)
        {
            if (handle.IsValid())
            {
                Addressables.Release(handle);
            }
        }
    }
}
