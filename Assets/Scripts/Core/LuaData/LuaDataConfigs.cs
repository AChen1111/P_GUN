using System.Collections.Generic;

namespace Game.Core
{
    /// <summary>
    /// 玩家基础配置, 来自 PlayerData.lua.
    /// </summary>
    public sealed class PlayerConfig
    {
        public int MaxHp;
        public float MoveSpeed;
        public float BulletTimeEnemyScale;
        public float BulletTimeDuration;
        public float BulletTimeCooldown;
    }

    /// <summary>
    /// 武器配置, 来自 WeaponData.lua, 按 weaponId 取行.
    /// </summary>
    public sealed class WeaponConfig
    {
        public string WeaponId;
        public string DisplayName;
        public int MinDamage;
        public int MaxDamage;
        public int MaxBulletBagNum;
        public int ClipSize;
        public float ShootInterval;
        public int BulletSpeed;
        public string ReloadSoundAddress;
        public List<string> ShootSoundAddresses = new List<string>();
    }

    /// <summary>
    /// 子弹配置, 来自 BulletData.lua, 按 bulletId 取行.
    /// </summary>
    public sealed class BulletConfig
    {
        public string BulletId;
        public float LifeTime;
        public int HitBuffId;
        public string HitPlayerSoundAddress;
        public string HitWallSoundAddress;
    }

    /// <summary>
    /// 单条 Buff 属性修正, 数值来自 BuffData.lua 的 modifiers 列.
    /// Stat 与 ModifierType 保持字符串, 由玩法程序集负责转成本地枚举.
    /// </summary>
    public sealed class StatModifierEntry
    {
        public string Stat;
        public string ModifierType;
        public float Value;
    }

    /// <summary>
    /// Buff 配置, 来自 BuffData.lua, 按 buffId 取行.
    /// </summary>
    public sealed class BuffConfig
    {
        public int Id;
        public string Name;
        public string Description;
        public string IconAddress;
        public string Tag;
        public float Duration;
        public bool IsPermanent;
        public float Interval;
        public List<StatModifierEntry> Modifiers = new List<StatModifierEntry>();
        public string BehaviorPrefabAddress;
    }

    /// <summary>
    /// 敌人配置, 来自 EnemyData.lua, 按 enemyId 取行.
    /// 视野与分离参数供敌人行为层使用.
    /// </summary>
    public sealed class EnemyConfig
    {
        public int Id;
        public string DisplayName;
        public string PrefabAddress;
        public int MaxHp;
        public float MoveSpeed;
        public int Damage;
        public float ItemDropChance;
        public float VisionRadius;
        public float VisionAngle;
        public float SearchTime;
        public float SeparationRadius;
        public float SeparationWeight;
        public float AttackInterval;
        public float AttackRange;
    }

    /// <summary>
    /// 道具配置, 来自 ItemData.lua, 按 itemId 取行.
    /// </summary>
    public sealed class ItemConfig
    {
        public int Id;
        public string Name;
        public string Description;
        public string IconAddress;
        public string EffectPrefabAddress;
        public string PrefabAddress;
    }

    /// <summary>
    /// 单个波次里的一种敌人生成数量.
    /// </summary>
    public sealed class SpawnWaveEntry
    {
        public int EnemyId;
        public int Count;
    }

    /// <summary>
    /// 掉落表里的一种道具权重.
    /// </summary>
    public sealed class SpawnItemDropEntry
    {
        public int ItemId;
        public float Weight;
    }

    /// <summary>
    /// 一张敌人生成表, 来自 SpawnData.lua, 按 spawnTableId 取行.
    /// </summary>
    public sealed class SpawnTableConfig
    {
        public string TableId;
        public List<List<SpawnWaveEntry>> Waves = new List<List<SpawnWaveEntry>>();
        public List<SpawnItemDropEntry> ItemDrops = new List<SpawnItemDropEntry>();
    }

    /// <summary>
    /// 关卡布局配置, 来自 LevelData.lua, 按 levelId 取行.
    /// </summary>
    public sealed class LevelConfig
    {
        public string LevelId;
        public int RoomCount;
        public int InitCount;
        public int FinalCount;
        public int ChestCount;
        public int SaveCount;
        public int NormalCount;
        public string InitPrefab;
        public string FinalPrefab;
        public string ChestPrefab;
        public string SavePrefab;
        public List<string> NormalPrefabs = new List<string>();
    }
}