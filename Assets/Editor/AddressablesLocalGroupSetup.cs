using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEngine;

public static class AddressablesLocalGroupSetup
{
    private const string RemoteBuildPath = "ServerData/P_GUN/[BuildTarget]";
    private const string RemoteLoadPath = "https://achen1o1.xyz/AB/P_GUN/[BuildTarget]";

    private static readonly HotUpdateGroupDefinition[] HotUpdateGroups =
    {
        new HotUpdateGroupDefinition(
            "Room",
            new[]
            {
                Entry("Assets/Prefab/Room/LevelGraph/Level1.asset", "room/level1", "room", "level1"),
                Entry("Assets/Prefab/Room/RoomTemplate/InitRoom.prefab", "room/init", "room", "room_template"),
                Entry("Assets/Prefab/Room/RoomTemplate/NormalRoom.prefab", "room/normal", "room", "room_template"),
                Entry("Assets/Prefab/Room/RoomTemplate/FinalRoom.prefab", "room/final", "room", "room_template"),
                Entry("Assets/Prefab/Room/CorridorTemplate/LRCorridor.prefab", "room/corridor_lr", "room", "corridor"),
                Entry("Assets/Prefab/Room/CorridorTemplate/UDCorridor.prefab", "room/corridor_ud", "room", "corridor")
            }),
        new HotUpdateGroupDefinition(
            "Buff",
            new[]
            {
                Entry("Assets/GameDataSO/DataBase/BuffDataBase.asset", "BuffDataBase", "buff", "database"),
                Entry("Assets/Scripts/Gameplay/Buffs/Buff/SpeedBuff.lua.txt", "buff/lua/speed", "buff", "lua"),
                Entry("Assets/Scripts/Gameplay/Buffs/Buff/MaxHpUpBuff.lua.txt", "buff/lua/max_hp", "buff", "lua"),
                Entry("Assets/Scripts/Gameplay/Buffs/Buff/PoisonBuff.lua.txt", "buff/lua/poison", "buff", "lua"),
                Entry("Assets/Scripts/Gameplay/Buffs/Buff/DamageUpBuff.lua.txt", "buff/lua/damage_up", "buff", "lua")
            }),
        new HotUpdateGroupDefinition(
            "Item",
            new[]
            {
                Entry("Assets/GameDataSO/DataBase/ItemDatabase.asset", "ItemDatabase", "item", "database"),
                Entry("Assets/Prefab/Item/Heart.prefab", "item/heart", "item", "prefab"),
                Entry("Assets/Prefab/Item/Chest.prefab", "item/chest", "item", "prefab"),
                Entry("Assets/Prefab/Item/SpeedUp.prefab", "item/speed_up", "item", "prefab"),
                Entry("Assets/Prefab/Item/PowerUp.prefab", "item/power_up", "item", "prefab"),
                Entry("Assets/Prefab/Item/Purify.prefab", "item/purify", "item", "prefab")
            }),
        new HotUpdateGroupDefinition(
            "Enemy",
            new[]
            {
                Entry("Assets/GameDataSO/DataBase/EnemyDatabase.asset", "EnemyDatabase", "enemy", "database"),
                Entry("Assets/Prefab/Enemy/EnemyA.prefab", "enemy/enemy_a", "enemy", "prefab"),
                Entry("Assets/Prefab/Enemy/Bat.prefab", "enemy/bat", "enemy", "prefab"),
                Entry("Assets/Prefab/Enemy/Goblin.prefab", "enemy/goblin", "enemy", "prefab"),
                Entry("Assets/Prefab/Enemy/Ogre.prefab", "enemy/ogre", "enemy", "prefab"),
                Entry("Assets/Prefab/Enemy/Orc_Masked.prefab", "enemy/orc_masked", "enemy", "prefab"),
                Entry("Assets/Prefab/Enemy/Orc_Shaman.prefab", "enemy/orc_shaman", "enemy", "prefab"),
                Entry("Assets/Prefab/Enemy/Orc_Warrior.prefab", "enemy/orc_warrior", "enemy", "prefab"),
                Entry("Assets/Prefab/Enemy/Slime.prefab", "enemy/slime", "enemy", "prefab")
            }),
        new HotUpdateGroupDefinition(
            "Weapon",
            new[]
            {
                Entry("Assets/GameDataSO/DataBase/WeaponDatabase.asset", "WeaponDatabase", "weapon", "database"),
                Entry("Assets/Prefab/GunList/AK.prefab", "weapon/ak", "weapon", "prefab"),
                Entry("Assets/Prefab/GunList/AWP.prefab", "weapon/awp", "weapon", "prefab"),
                Entry("Assets/Prefab/GunList/Bow.prefab", "weapon/bow", "weapon", "prefab"),
                Entry("Assets/Prefab/GunList/Laser.prefab", "weapon/laser", "weapon", "prefab"),
                Entry("Assets/Prefab/GunList/MP5.prefab", "weapon/mp5", "weapon", "prefab"),
                Entry("Assets/Prefab/GunList/Pistol.prefab", "weapon/pistol", "weapon", "prefab"),
                Entry("Assets/Prefab/GunList/RocketGun.prefab", "weapon/rocket_gun", "weapon", "prefab"),
                Entry("Assets/Prefab/GunList/ShotGun.prefab", "weapon/shotgun", "weapon", "prefab")
            })
    };

    [MenuItem("PG/Addressables/Create Hot Update Groups")]
    public static void CreateHotUpdateGroupsFromMenu()
    {
        CreateHotUpdateGroups();
    }

    [MenuItem("PG/Addressables/Create Local Groups")]
    public static void CreateLocalGroupsFromMenu()
    {
        CreateHotUpdateGroups();
    }

    public static void CreateLocalGroups()
    {
        CreateHotUpdateGroups();
    }

    // Unity.exe -batchmode -quit -projectPath <project> -executeMethod AddressablesLocalGroupSetup.CreateHotUpdateGroups
    public static void CreateHotUpdateGroups()
    {
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        if (settings == null)
        {
            Debug.LogError("Addressables settings not found. Open Window > Asset Management > Addressables > Groups and click Create Addressables Settings first.");
            return;
        }

        // 热更配置只保留 Built In Data 和业务热更包, 首包内置后通过 remote catalog 指向更新内容.
        RemoveNonHotUpdateGroups(settings);
        ConfigureRemoteProfile(settings);

        settings.BuildRemoteCatalog = true;
        settings.RemoteCatalogBuildPath.SetVariableByName(settings, AddressableAssetSettings.kRemoteBuildPath);
        settings.RemoteCatalogLoadPath.SetVariableByName(settings, AddressableAssetSettings.kRemoteLoadPath);
        // RootHotUpdateController 手动检查远程 Catalog, 避免 Addressables 初始化阶段提前发起更新请求.
        settings.DisableCatalogUpdateOnStartup = true;
        // Catalog 请求不能无限等待, 服务器不可用时应尽快回退到包体或缓存内容.
        settings.CatalogRequestsTimeout = 10;
        settings.UniqueBundleIds = true;

        AddressableAssetGroup firstGroup = null;
        foreach (var definition in HotUpdateGroups)
        {
            var group = GetOrCreateGroup(settings, definition.GroupName);
            ConfigureAsLocalPackedGroup(settings, group);
            ClearGroupEntries(settings, group);
            AddEntries(settings, group, definition.Entries);
            firstGroup ??= group;
        }

        RemoveUnusedHotUpdateLabels(settings);

        if (firstGroup != null)
        {
            settings.DefaultGroup = firstGroup;
        }

        settings.SetDirty(AddressableAssetSettings.ModificationEvent.BatchModification, null, true, true);
        EditorUtility.SetDirty(settings);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"Addressables hot update groups ready. Groups: {string.Join(", ", HotUpdateGroups.Select(group => group.GroupName))}.");
    }

    private static void RemoveNonHotUpdateGroups(AddressableAssetSettings settings)
    {
        var keepNames = new HashSet<string>(HotUpdateGroups.Select(group => group.GroupName))
        {
            "Built In Data"
        };

        foreach (var group in settings.groups.Where(group => group != null && !keepNames.Contains(group.Name)).ToArray())
        {
            settings.RemoveGroup(group);
        }
    }

    private static void ConfigureRemoteProfile(AddressableAssetSettings settings)
    {
        // 远程构建目录对应服务器 AB 根目录, 打包后将该目录内容上传到 Nginx 的 /AB/P_GUN/[BuildTarget].
        settings.profileSettings.SetValue(settings.activeProfileId, AddressableAssetSettings.kRemoteBuildPath, RemoteBuildPath);
        settings.profileSettings.SetValue(settings.activeProfileId, AddressableAssetSettings.kRemoteLoadPath, RemoteLoadPath);
    }

    private static AddressableAssetGroup GetOrCreateGroup(AddressableAssetSettings settings, string groupName)
    {
        var group = settings.FindGroup(groupName);
        if (group != null) return group;

        return settings.CreateGroup(
            groupName,
            false,
            false,
            true,
            null,
            typeof(BundledAssetGroupSchema),
            typeof(ContentUpdateGroupSchema));
    }

    private static void ConfigureAsLocalPackedGroup(AddressableAssetSettings settings, AddressableAssetGroup group)
    {
        var bundledSchema = group.GetSchema<BundledAssetGroupSchema>() ?? group.AddSchema<BundledAssetGroupSchema>();
        var contentUpdateSchema = group.GetSchema<ContentUpdateGroupSchema>() ?? group.AddSchema<ContentUpdateGroupSchema>();

        // 首次发布时业务包进入 StreamingAssets, 后续更新由 remote catalog 重定向到服务器包.
        bundledSchema.BuildPath.SetVariableByName(settings, AddressableAssetSettings.kLocalBuildPath);
        bundledSchema.LoadPath.SetVariableByName(settings, AddressableAssetSettings.kLocalLoadPath);
        bundledSchema.IncludeInBuild = true;
        bundledSchema.BundleMode = BundledAssetGroupSchema.BundlePackingMode.PackTogether;
        bundledSchema.Compression = BundledAssetGroupSchema.BundleCompressionMode.LZ4;
        bundledSchema.BundleNaming = BundledAssetGroupSchema.BundleNamingStyle.AppendHash;
        bundledSchema.UseAssetBundleCache = true;
        bundledSchema.UseAssetBundleCrc = true;
        bundledSchema.UseUnityWebRequestForLocalBundles = true;

        // 首包内容标记为不可直接改动, 内容更新构建会把变化资源迁移到远程更新包.
        contentUpdateSchema.StaticContent = true;

        EditorUtility.SetDirty(group);
        EditorUtility.SetDirty(bundledSchema);
        EditorUtility.SetDirty(contentUpdateSchema);
    }

    private static void ClearGroupEntries(AddressableAssetSettings settings, AddressableAssetGroup group)
    {
        foreach (var entry in group.entries.ToArray())
        {
            settings.RemoveAssetEntry(entry.guid, false);
        }
    }

    private static void AddEntries(AddressableAssetSettings settings, AddressableAssetGroup group, IReadOnlyList<HotUpdateEntryDefinition> entries)
    {
        foreach (var definition in entries)
        {
            var guid = AssetDatabase.AssetPathToGUID(definition.AssetPath);
            if (string.IsNullOrEmpty(guid))
            {
                throw new InvalidOperationException($"Addressable asset not found: {definition.AssetPath}");
            }

            var entry = settings.CreateOrMoveEntry(guid, group, false, false);
            entry.address = definition.Address;

            foreach (var oldLabel in entry.labels.ToArray())
            {
                entry.SetLabel(oldLabel, false, false, false);
            }

            foreach (var label in definition.Labels)
            {
                settings.AddLabel(label, false);
                entry.SetLabel(label, true, false, false);
            }
        }
    }

    private static void RemoveUnusedHotUpdateLabels(AddressableAssetSettings settings)
    {
        var usedLabels = new HashSet<string>(HotUpdateGroups.SelectMany(group => group.Entries).SelectMany(entry => entry.Labels));
        foreach (var label in settings.GetLabels().Where(label => !usedLabels.Contains(label)).ToArray())
        {
            settings.RemoveLabel(label, false);
        }
    }

    private static HotUpdateEntryDefinition Entry(string assetPath, string address, params string[] labels)
    {
        return new HotUpdateEntryDefinition(assetPath, address, labels);
    }

    private sealed class HotUpdateGroupDefinition
    {
        public HotUpdateGroupDefinition(string groupName, IReadOnlyList<HotUpdateEntryDefinition> entries)
        {
            GroupName = groupName;
            Entries = entries;
        }

        public string GroupName { get; }
        public IReadOnlyList<HotUpdateEntryDefinition> Entries { get; }
    }

    private sealed class HotUpdateEntryDefinition
    {
        public HotUpdateEntryDefinition(string assetPath, string address, IReadOnlyList<string> labels)
        {
            AssetPath = assetPath;
            Address = address;
            Labels = labels;
        }

        public string AssetPath { get; }
        public string Address { get; }
        public IReadOnlyList<string> Labels { get; }
    }
}
