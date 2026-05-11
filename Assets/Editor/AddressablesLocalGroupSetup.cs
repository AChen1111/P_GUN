using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEngine;

public static class AddressablesLocalGroupSetup
{
    private static readonly string[] LocalGroupNames =
    {
        "Local_GameScene_Level1",
        "Local_UI_Core",
        "Local_Player_Core",
        "Local_Combat_Shared",
        "Local_Items",
        "Local_Enemies_Base",
        "Local_Weapons_Base"
    };

    [MenuItem("PG/Addressables/Create Local Groups")]
    public static void CreateLocalGroupsFromMenu()
    {
        CreateLocalGroups();
    }

    // Can be called from batchmode:
    // Unity.exe -batchmode -quit -projectPath <project> -executeMethod AddressablesLocalGroupSetup.CreateLocalGroups
    public static void CreateLocalGroups()
    {
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        if (settings == null)
        {
            Debug.LogError("Addressables settings not found. Open Window > Asset Management > Addressables > Groups and click Create Addressables Settings first.");
            return;
        }

        var createdGroups = new List<string>();
        var updatedGroups = new List<string>();

        foreach (var groupName in LocalGroupNames)
        {
            var group = settings.FindGroup(groupName);
            if (group == null)
            {
                group = settings.CreateGroup(
                    groupName,
                    false,
                    false,
                    true,
                    null,
                    typeof(BundledAssetGroupSchema),
                    typeof(ContentUpdateGroupSchema)
                );
                createdGroups.Add(groupName);
            }
            else
            {
                updatedGroups.Add(groupName);
            }

            ConfigureAsLocalPackedGroup(settings, group);
        }

        settings.SetDirty(AddressableAssetSettings.ModificationEvent.BatchModification, null, true, true);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log(
            $"Addressables local groups ready. Created: {FormatList(createdGroups)}. Checked/updated: {FormatList(updatedGroups)}."
        );
    }

    private static void ConfigureAsLocalPackedGroup(AddressableAssetSettings settings, AddressableAssetGroup group)
    {
        var bundledSchema = group.GetSchema<BundledAssetGroupSchema>();
        if (bundledSchema == null)
        {
            bundledSchema = group.AddSchema<BundledAssetGroupSchema>();
        }

        var contentUpdateSchema = group.GetSchema<ContentUpdateGroupSchema>();
        if (contentUpdateSchema == null)
        {
            contentUpdateSchema = group.AddSchema<ContentUpdateGroupSchema>();
        }

        bundledSchema.BuildPath.SetVariableByName(settings, AddressableAssetSettings.kLocalBuildPath);
        bundledSchema.LoadPath.SetVariableByName(settings, AddressableAssetSettings.kLocalLoadPath);
        bundledSchema.IncludeInBuild = true;
        bundledSchema.BundleMode = BundledAssetGroupSchema.BundlePackingMode.PackTogether;
        bundledSchema.Compression = BundledAssetGroupSchema.BundleCompressionMode.LZ4;
        bundledSchema.BundleNaming = BundledAssetGroupSchema.BundleNamingStyle.AppendHash;
        bundledSchema.UseAssetBundleCache = true;
        bundledSchema.UseAssetBundleCrc = true;
        bundledSchema.UseUnityWebRequestForLocalBundles = false;

        // Local first-package content should not be moved by content update builds.
        contentUpdateSchema.StaticContent = true;

        EditorUtility.SetDirty(group);
        EditorUtility.SetDirty(bundledSchema);
        EditorUtility.SetDirty(contentUpdateSchema);
    }

    private static string FormatList(IReadOnlyCollection<string> values)
    {
        return values.Count == 0 ? "none" : string.Join(", ", values);
    }
}
