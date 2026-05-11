using System.Linq;
using UnityEditor;
using UnityEngine;

public sealed class ItemDatabaseExcelImporter : Excel2SoListAssetImporter<ItemDatabase>
{
    protected override string DefaultAssetPath => "Assets/Resources/ItemDatabase.asset";

    protected override string ListPropertyPath => "items";

    [MenuItem("Tools/Excel2SO/Import ItemDatabase Excel")]
    public static void ImportFromMenu()
    {
        new ItemDatabaseExcelImporter().ImportFromFilePanel();
    }

    protected override void Configure(Excel2SoMapping map)
    {
        map.Column("itemId").To("itemId").AsInt();
        map.Column("itemName").To("itemName").AsString();
        map.Column("description").To("description").AsString();
        map.Column("icon").To("icon").AsAsset<Sprite>();
    }

    protected override void OnAfterImportAsset(ItemDatabase asset, ExcelTable table, Excel2SoImportReport report)
    {
        var importedItems = asset.Items.ToArray();
        asset.ReplaceItems(importedItems);
        ItemDatabase.SetDefault(asset);
    }
}

public sealed class ItemSpawnTableExcelImporter : Excel2SoListAssetImporter<ItemSpawnTableSO>
{
    protected override string DefaultAssetPath => "Assets/GameDataSO/ItemSpawnerTable/ImportedItemSpawnTable.asset";

    protected override string ListPropertyPath => "entries";

    [MenuItem("Tools/Excel2SO/Import Item Spawn Table Excel")]
    public static void ImportFromMenu()
    {
        new ItemSpawnTableExcelImporter().ImportFromFilePanel();
    }

    protected override void Configure(Excel2SoMapping map)
    {
        map.Column("prefab").To("prefab").AsAsset<GameObject>();
        map.Column("weight").To("weight").AsInt();
    }
}

public sealed class HealItemEffectExcelImporter : Excel2SoRowAssetImporter<HealItemEffect>
{
    protected override string DefaultOutputFolder => "Assets/GameDataSO/ItemEffects/EffectSO/ImportedHealEffects";

    [MenuItem("Tools/Excel2SO/Import Heal Item Effects Excel")]
    public static void ImportFromMenu()
    {
        new HealItemEffectExcelImporter().ImportFromFilePanel();
    }

    protected override void Configure(Excel2SoMapping map)
    {
        map.Column("healAmount").To("healAmount").AsInt();
    }
}
