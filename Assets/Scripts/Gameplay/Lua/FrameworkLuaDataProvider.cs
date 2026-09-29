using System;
using System.Collections.Generic;
using System.Globalization;
using Game.Core;
using XLua;

namespace Game.Gameplay
{
    /// <summary>
    /// 基于 LuaComponet 框架环境的配置读取实现, 由 PgunLuaRuntimeBootstrap 注册.
    /// 缺表, 缺行或缺字段时直接报错, 不做静默兜底.
    /// </summary>
    public sealed class FrameworkLuaDataProvider : ILuaDataProvider
    {
        private readonly Dictionary<string, LuaTable> moduleCache = new Dictionary<string, LuaTable>();

        public PlayerConfig GetPlayerConfig()
        {
            var table = RequireModule("PlayerData");
            return new PlayerConfig
            {
                MaxHp = ReadInt(table, "maxHp", "PlayerData"),
                MoveSpeed = ReadFloat(table, "moveSpeed", "PlayerData"),
                BulletTimeEnemyScale = ReadFloat(table, "bulletTimeEnemyScale", "PlayerData"),
                BulletTimeDuration = ReadFloat(table, "bulletTimeDuration", "PlayerData"),
                BulletTimeCooldown = ReadFloat(table, "bulletTimeCooldown", "PlayerData"),
            };
        }

        public WeaponConfig GetWeaponConfig(string weaponId)
        {
            if (string.IsNullOrWhiteSpace(weaponId))
            {
                throw new ArgumentException("weaponId must not be empty.", nameof(weaponId));
            }

            var row = RequireRow("WeaponData", weaponId, "武器");
            return new WeaponConfig
            {
                WeaponId = weaponId,
                DisplayName = ReadString(row, "displayName", $"武器 {weaponId}"),
                MinDamage = ReadInt(row, "minDamage", $"武器 {weaponId}"),
                MaxDamage = ReadInt(row, "maxDamage", $"武器 {weaponId}"),
                MaxBulletBagNum = ReadInt(row, "maxBulletBagNum", $"武器 {weaponId}"),
                ClipSize = ReadInt(row, "clipSize", $"武器 {weaponId}"),
                ShootInterval = ReadFloat(row, "shootInterval", $"武器 {weaponId}"),
                BulletSpeed = ReadInt(row, "bulletSpeed", $"武器 {weaponId}"),
                ReloadSoundAddress = ReadOptionalString(row, "reloadSoundAddress"),
                ShootSoundAddresses = ReadStringList(row, "shootSoundAddresses", $"武器 {weaponId}"),
            };
        }

        public BulletConfig GetBulletConfig(string bulletId)
        {
            if (string.IsNullOrWhiteSpace(bulletId))
            {
                throw new ArgumentException("bulletId must not be empty.", nameof(bulletId));
            }

            var row = RequireRow("BulletData", bulletId, "子弹");
            return new BulletConfig
            {
                BulletId = bulletId,
                LifeTime = ReadFloat(row, "lifeTime", $"子弹 {bulletId}"),
                HitBuffId = ReadInt(row, "hitBuffId", $"子弹 {bulletId}"),
                HitPlayerSoundAddress = ReadOptionalString(row, "hitPlayerSoundAddress"),
                HitWallSoundAddress = ReadOptionalString(row, "hitWallSoundAddress"),
            };
        }

        public BuffConfig GetBuffConfig(int buffId)
        {
            var row = RequireRow("BuffData", buffId.ToString(), "Buff");
            var config = new BuffConfig
            {
                Id = buffId,
                Name = ReadString(row, "buffName", $"Buff {buffId}"),
                Description = ReadOptionalString(row, "description"),
                IconAddress = ReadString(row, "iconAddress", $"Buff {buffId}"),
                Tag = ReadString(row, "tag", $"Buff {buffId}"),
                Duration = ReadFloat(row, "duration", $"Buff {buffId}"),
                IsPermanent = ReadBool(row, "isPermanent", $"Buff {buffId}"),
                Interval = ReadFloat(row, "interval", $"Buff {buffId}"),
                BehaviorPrefabAddress = ReadOptionalString(row, "behaviorPrefabAddress"),
            };

            ReadModifiers(row, config);
            return config;
        }

        public EnemyConfig GetEnemyConfig(int enemyId)
        {
            var row = RequireRow("EnemyData", enemyId.ToString(), "敌人");
            return new EnemyConfig
            {
                Id = enemyId,
                DisplayName = ReadString(row, "displayName", $"敌人 {enemyId}"),
                PrefabAddress = ReadString(row, "prefabAddress", $"敌人 {enemyId}"),
                MaxHp = ReadInt(row, "maxHp", $"敌人 {enemyId}"),
                MoveSpeed = ReadFloat(row, "moveSpeed", $"敌人 {enemyId}"),
                Damage = ReadInt(row, "damage", $"敌人 {enemyId}"),
                ItemDropChance = ReadFloat(row, "itemDropChance", $"敌人 {enemyId}"),
                VisionRadius = ReadFloat(row, "visionRadius", $"敌人 {enemyId}"),
                VisionAngle = ReadFloat(row, "visionAngle", $"敌人 {enemyId}"),
                SearchTime = ReadFloat(row, "searchTime", $"敌人 {enemyId}"),
                SeparationRadius = ReadFloat(row, "separationRadius", $"敌人 {enemyId}"),
                SeparationWeight = ReadFloat(row, "separationWeight", $"敌人 {enemyId}"),
                AttackInterval = ReadFloat(row, "attackInterval", $"敌人 {enemyId}"),
                AttackRange = ReadFloat(row, "attackRange", $"敌人 {enemyId}"),
            };
        }

        public ItemConfig GetItemConfig(int itemId)
        {
            var row = RequireRow("ItemData", itemId.ToString(), "道具");
            return new ItemConfig
            {
                Id = itemId,
                Name = ReadString(row, "itemName", $"道具 {itemId}"),
                Description = ReadOptionalString(row, "description"),
                IconAddress = ReadString(row, "iconAddress", $"道具 {itemId}"),
                EffectPrefabAddress = ReadString(row, "effectPrefabAddress", $"道具 {itemId}"),
                PrefabAddress = ReadString(row, "prefabAddress", $"道具 {itemId}"),
            };
        }

        public SpawnTableConfig GetSpawnTableConfig(string spawnTableId)
        {
            var row = RequireRow("SpawnData", spawnTableId, "生成表");
            var config = new SpawnTableConfig { TableId = spawnTableId };

            var wavesTable = row.Get<LuaTable>("waves");
            if (wavesTable == null)
            {
                throw new InvalidOperationException($"生成表 {spawnTableId} 缺少 waves 字段.");
            }

            for (var waveIndex = 1; waveIndex <= wavesTable.Length; waveIndex++)
            {
                var wave = wavesTable.Get<int, LuaTable>(waveIndex);
                if (wave == null)
                {
                    throw new InvalidOperationException($"生成表 {spawnTableId} 第 {waveIndex} 波数据为空.");
                }

                var entries = new List<SpawnWaveEntry>();
                for (var entryIndex = 1; entryIndex <= wave.Length; entryIndex++)
                {
                    var entry = wave.Get<int, LuaTable>(entryIndex);
                    if (entry == null)
                    {
                        throw new InvalidOperationException($"生成表 {spawnTableId} 第 {waveIndex} 波第 {entryIndex} 条数据为空.");
                    }

                    entries.Add(new SpawnWaveEntry
                    {
                        EnemyId = ReadInt(entry, "enemyId", $"生成表 {spawnTableId}"),
                        Count = ReadInt(entry, "count", $"生成表 {spawnTableId}"),
                    });
                }

                config.Waves.Add(entries);
            }

            ReadItemDrops(row, config);
            return config;
        }

        public LevelConfig GetLevelConfig(string levelId)
        {
            var row = RequireRow("LevelData", levelId, "关卡");
            return new LevelConfig
            {
                LevelId = levelId,
                RoomCount = ReadInt(row, "roomCount", $"关卡 {levelId}"),
                InitCount = ReadInt(row, "initCount", $"关卡 {levelId}"),
                FinalCount = ReadInt(row, "finalCount", $"关卡 {levelId}"),
                ChestCount = ReadInt(row, "chestCount", $"关卡 {levelId}"),
                SaveCount = ReadInt(row, "saveCount", $"关卡 {levelId}"),
                NormalCount = ReadInt(row, "normalCount", $"关卡 {levelId}"),
                InitPrefab = ReadString(row, "initPrefab", $"关卡 {levelId}"),
                FinalPrefab = ReadString(row, "finalPrefab", $"关卡 {levelId}"),
                ChestPrefab = ReadString(row, "chestPrefab", $"关卡 {levelId}"),
                SavePrefab = ReadString(row, "savePrefab", $"关卡 {levelId}"),
                NormalPrefabs = ReadStringList(row, "normalPrefabs", $"关卡 {levelId}"),
            };
        }

        /// <summary>
        /// 读取掉落权重列表.
        /// </summary>
        private static void ReadItemDrops(LuaTable row, SpawnTableConfig config)
        {
            var dropsTable = row.Get<LuaTable>("itemDrops");
            if (dropsTable == null)
            {
                throw new InvalidOperationException($"生成表 {config.TableId} 缺少 itemDrops 字段.");
            }

            for (var i = 1; i <= dropsTable.Length; i++)
            {
                var entry = dropsTable.Get<int, LuaTable>(i);
                if (entry == null)
                {
                    throw new InvalidOperationException($"生成表 {config.TableId} 第 {i} 条掉落数据为空.");
                }

                config.ItemDrops.Add(new SpawnItemDropEntry
                {
                    ItemId = ReadInt(entry, "itemId", $"生成表 {config.TableId}"),
                    Weight = ReadFloat(entry, "weight", $"生成表 {config.TableId}"),
                });
            }
        }

        /// <summary>
        /// 读取并缓存数据模块的 table.
        /// </summary>
        private LuaTable RequireModule(string moduleName)
        {
            if (moduleCache.TryGetValue(moduleName, out var cached))
            {
                return cached;
            }

            var env = RequireEnv();
            object[] results;
            try
            {
                results = env.DoString($"return require('{moduleName}')", moduleName);
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException($"加载 Lua 数据模块失败: {moduleName}, Error: {exception.Message}", exception);
            }

            var table = results != null && results.Length > 0 ? results[0] as LuaTable : null;
            if (table == null)
            {
                throw new InvalidOperationException($"Lua 数据模块没有返回 table: {moduleName}.");
            }

            moduleCache[moduleName] = table;
            return table;
        }

        /// <summary>
        /// 获取框架 Lua 环境, 未初始化时直接报错.
        /// </summary>
        private static LuaEnv RequireEnv()
        {
            var manager = LuaManager.Instance;
            if (manager == null || manager.LuaEnv == null)
            {
                throw new InvalidOperationException("LuaManager 未初始化, 无法读取 Lua 数据. 请把 LuaManager 摆在 Root 场景.");
            }

            return manager.LuaEnv;
        }

        /// <summary>
        /// 从模块中取一行数据, 键为字符串形式的 id.
        /// </summary>
        private LuaTable RequireRow(string moduleName, string key, string describe)
        {
            var module = RequireModule(moduleName);
            var row = module.Get<LuaTable>(key);
            if (row == null)
            {
                throw new InvalidOperationException($"Lua 数据表 {moduleName} 缺少{describe}: {key}.");
            }

            return row;
        }

        /// <summary>
        /// 读取必填字段, 缺失时直接报错.
        /// </summary>
        private static object RequireField(LuaTable row, string field, string describe)
        {
            var value = row.Get<object>(field);
            if (value == null)
            {
                throw new InvalidOperationException($"Lua 数据 {describe} 缺少字段: {field}.");
            }

            return value;
        }

        private static int ReadInt(LuaTable row, string field, string describe)
        {
            return Convert.ToInt32(RequireField(row, field, describe), CultureInfo.InvariantCulture);
        }

        private static float ReadFloat(LuaTable row, string field, string describe)
        {
            return Convert.ToSingle(RequireField(row, field, describe), CultureInfo.InvariantCulture);
        }

        private static bool ReadBool(LuaTable row, string field, string describe)
        {
            return Convert.ToBoolean(RequireField(row, field, describe), CultureInfo.InvariantCulture);
        }

        private static string ReadString(LuaTable row, string field, string describe)
        {
            var value = row.Get<string>(field);
            if (value == null)
            {
                throw new InvalidOperationException($"Lua 数据 {describe} 缺少字段: {field}.");
            }

            return value;
        }

        /// <summary>
        /// 读取可空字符串, 缺失或为空时返回空字符串.
        /// </summary>
        private static string ReadOptionalString(LuaTable row, string field)
        {
            return row.Get<string>(field) ?? string.Empty;
        }

        /// <summary>
        /// 读取字符串数组字段.
        /// </summary>
        private static List<string> ReadStringList(LuaTable row, string field, string describe)
        {
            var result = new List<string>();
            var listTable = row.Get<LuaTable>(field);
            if (listTable == null)
            {
                throw new InvalidOperationException($"Lua 数据 {describe} 缺少字段: {field}.");
            }

            for (var i = 1; i <= listTable.Length; i++)
            {
                var entry = listTable.Get<int, string>(i);
                if (entry == null)
                {
                    throw new InvalidOperationException($"Lua 数据 {describe} 的 {field} 第 {i} 项不是字符串.");
                }

                result.Add(entry);
            }

            return result;
        }

        /// <summary>
        /// 读取 Buff 属性修正列表.
        /// </summary>
        private static void ReadModifiers(LuaTable row, BuffConfig config)
        {
            var modifiersTable = row.Get<LuaTable>("modifiers");
            if (modifiersTable == null)
            {
                throw new InvalidOperationException($"Buff {config.Id} 缺少 modifiers 字段.");
            }

            for (var i = 1; i <= modifiersTable.Length; i++)
            {
                var entry = modifiersTable.Get<int, LuaTable>(i);
                if (entry == null)
                {
                    throw new InvalidOperationException($"Buff {config.Id} 的第 {i} 条属性修正不是 table.");
                }

                config.Modifiers.Add(new StatModifierEntry
                {
                    Stat = ReadString(entry, "stat", $"Buff {config.Id} 属性修正"),
                    ModifierType = ReadString(entry, "type", $"Buff {config.Id} 属性修正"),
                    Value = ReadFloat(entry, "value", $"Buff {config.Id} 属性修正"),
                });
            }
        }
    }
}