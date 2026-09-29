using System;

namespace Game.Core
{
    /// <summary>
    /// 玩法配置读取接口, 由 Lua 环境侧实现并注册.
    /// 玩法程序集只依赖本接口, 不接触 LuaTable.
    /// </summary>
    public interface ILuaDataProvider
    {
        PlayerConfig GetPlayerConfig();
        WeaponConfig GetWeaponConfig(string weaponId);
        BulletConfig GetBulletConfig(string bulletId);
        BuffConfig GetBuffConfig(int buffId);
        EnemyConfig GetEnemyConfig(int enemyId);
        ItemConfig GetItemConfig(int itemId);
        SpawnTableConfig GetSpawnTableConfig(string spawnTableId);
        LevelConfig GetLevelConfig(string levelId);
    }

    /// <summary>
    /// Lua 数据读取门面, 隔离玩法程序集和具体 Lua 实现.
    /// </summary>
    public static class LuaDataRuntime
    {
        /// <summary>
        /// 已注册的数据提供者, 未注册时为 null.
        /// </summary>
        public static ILuaDataProvider Provider { get; private set; }

        /// <summary>
        /// 注册数据提供者.
        /// </summary>
        /// <param name="provider">数据提供者.</param>
        public static void RegisterProvider(ILuaDataProvider provider)
        {
            Provider = provider;
        }

        /// <summary>
        /// 仅当注册者一致时注销数据提供者.
        /// </summary>
        /// <param name="provider">请求注销的数据提供者.</param>
        public static void UnregisterProvider(ILuaDataProvider provider)
        {
            if (Provider == provider)
            {
                Provider = null;
            }
        }

        public static PlayerConfig GetPlayerConfig()
        {
            return RequireProvider().GetPlayerConfig();
        }

        public static WeaponConfig GetWeaponConfig(string weaponId)
        {
            return RequireProvider().GetWeaponConfig(weaponId);
        }

        public static BulletConfig GetBulletConfig(string bulletId)
        {
            return RequireProvider().GetBulletConfig(bulletId);
        }

        public static BuffConfig GetBuffConfig(int buffId)
        {
            return RequireProvider().GetBuffConfig(buffId);
        }

        public static EnemyConfig GetEnemyConfig(int enemyId)
        {
            return RequireProvider().GetEnemyConfig(enemyId);
        }

        public static ItemConfig GetItemConfig(int itemId)
        {
            return RequireProvider().GetItemConfig(itemId);
        }

        public static SpawnTableConfig GetSpawnTableConfig(string spawnTableId)
        {
            return RequireProvider().GetSpawnTableConfig(spawnTableId);
        }

        public static LevelConfig GetLevelConfig(string levelId)
        {
            return RequireProvider().GetLevelConfig(levelId);
        }

        /// <summary>
        /// 获取已注册的数据提供者, 未注册时直接报错.
        /// </summary>
        private static ILuaDataProvider RequireProvider()
        {
            if (Provider == null)
            {
                throw new InvalidOperationException("Lua 数据提供者未注册. 请在 Root 场景摆放 PgunLuaRuntimeBootstrap.");
            }

            return Provider;
        }
    }
}