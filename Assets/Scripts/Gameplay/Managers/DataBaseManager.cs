using System;
using System.Threading.Tasks;
using Game.Core;
using Game.Items;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// 全局数据库管理器, 负责按需通过 AddressableLoader 加载数据库资产.
    /// </summary>
    public sealed class DataBaseManager : MonoBehaviour
    {
        [SerializeField] private string itemDatabaseKey = "ItemDatabase";
        [SerializeField] private string weaponDatabaseKey = "WeaponDatabase";
        [SerializeField] private string buffDataBaseKey = "BuffDataBase";
        [SerializeField] private string enemyDatabaseKey = "EnemyDatabase";

        private Task loadingTask;

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
        /// 兼容旧入口, 实际加载转交给 EnsureLoadedAsync.
        /// </summary>
        public Task LoadAllAsync()
        {
            return EnsureLoadedAsync();
        }

        /// <summary>
        /// 确保所有全局数据库已经加载完成.
        /// </summary>
        public async Task EnsureLoadedAsync()
        {
            if (IsLoaded)
            {
                return;
            }

            if (loadingTask != null)
            {
                await loadingTask;
                return;
            }

            loadingTask = LoadAllInternalAsync();
            try
            {
                await loadingTask;
            }
            finally
            {
                loadingTask = null;
            }
        }
        private async Task LoadAllInternalAsync()
        {
            IsLoaded = false;
            var loader = AddressableLoader.Instance;
            if (loader == null)
            {
                throw new InvalidOperationException($"{nameof(AddressableLoader)} must exist before loading databases.");
            }

            var itemTask = loader.LoadAssetAsync<ItemDatabase>(itemDatabaseKey);
            var weaponTask = loader.LoadAssetAsync<WeaponDatabase>(weaponDatabaseKey);
            var buffTask = loader.LoadAssetAsync<BuffDataBase>(buffDataBaseKey);
            var enemyTask = loader.LoadAssetAsync<EnemyDatabase>(enemyDatabaseKey);

            await Task.WhenAll(itemTask, weaponTask, buffTask, enemyTask);

            Items = itemTask.Result;
            Weapons = weaponTask.Result;
            Buffs = buffTask.Result;
            Enemies = enemyTask.Result;
            IsLoaded = Items != null && Weapons != null && Buffs != null && Enemies != null;
            if (!IsLoaded)
            {
                throw new InvalidOperationException("Required databases were not loaded.");
            }

            // Item 模块通过 RuntimeDatabase 读取显示数据, 避免反向依赖 Gameplay.
            ItemDatabase.SetRuntimeDatabase(Items);
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

            ItemDatabase.ClearRuntimeDatabase(Items);
        }
    }
}
