using System.Linq;
using UnityEditor;
using UnityEngine;

public sealed class WeaponDatabaseExcelImporter : Excel2SoListAssetImporter<WeaponDatabase>
{
    protected override string DefaultAssetPath => "Assets/Resources/WeaponDatabase.asset";

    protected override string ListPropertyPath => "weapons";

    [MenuItem("Tools/Excel2SO/Import Weapon Database Excel")]
    public static void ImportFromMenu()
    {
        new WeaponDatabaseExcelImporter().ImportFromFilePanel();
    }

    protected override void Configure(Excel2SoMapping map)
    {
        map.Column("weaponId").To("weaponId").AsString();
        map.Column("displayName").To("displayName").AsString();
        map.Column("minDamage").To("minDamage").AsInt();
        map.Column("maxDamage").To("maxDamage").AsInt();
        map.Column("maxBulletBagNum").To("maxBulletBagNum").AsInt();
        map.Column("clipSize").To("clipSize").AsInt();
        map.Column("shootInterval").To("shootInterval").AsFloat();
        map.Column("reloadSound").To("reloadSound").AsAsset<AudioClip>();
        map.Column("shootSounds").To("shootSounds").AsAssetList<AudioClip>(";");
    }

    protected override void OnAfterImportAsset(WeaponDatabase asset, ExcelTable table, Excel2SoImportReport report)
    {
        asset.ReplaceWeapons(asset.Weapons.ToArray());
        WeaponDatabase.SetDefault(asset);

        foreach (var row in table.Rows)
        {
            if (row.IsEmpty) continue;

            AssignDatabaseToPrefab(asset, row);
        }
    }

    private static void AssignDatabaseToPrefab(WeaponDatabase database, ExcelRow row)
    {
        var prefabPath = NormalizeAssetPath(row.Get("weaponPrefab"));
        if (string.IsNullOrEmpty(prefabPath)) return;

        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        if (prefab == null)
        {
            Debug.LogWarning($"{nameof(WeaponDatabaseExcelImporter)}: Weapon prefab not found: {prefabPath}");
            return;
        }

        var gun = prefab.GetComponent<Gun>();
        if (gun == null)
        {
            Debug.LogWarning($"{nameof(WeaponDatabaseExcelImporter)}: Prefab has no {nameof(Gun)} component: {prefabPath}");
            return;
        }

        var serializedGun = new SerializedObject(gun);
        serializedGun.Update();

        var weaponId = row.Get("weaponId");
        var weaponIdProperty = serializedGun.FindProperty("weaponId");
        if (weaponIdProperty != null && !string.IsNullOrWhiteSpace(weaponId))
        {
            weaponIdProperty.stringValue = weaponId.Trim();
        }

        var databaseProperty = serializedGun.FindProperty("weaponDatabase");
        if (databaseProperty != null)
        {
            databaseProperty.objectReferenceValue = database;
        }

        serializedGun.ApplyModifiedProperties();
        EditorUtility.SetDirty(gun);
        EditorUtility.SetDirty(prefab);
        PrefabUtility.SavePrefabAsset(prefab);
    }
}
