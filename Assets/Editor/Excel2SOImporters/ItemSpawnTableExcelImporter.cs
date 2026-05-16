using UnityEngine;
using Game.Items;
using Game.Animation;

public sealed class ItemSpawnTableExcelImporter : Excel2SoListAssetImporter<ItemSpawnTableSO>
{
    protected override string DefaultAssetPath => "Assets/GameDataSO/ItemSpawnerTable/ImportedItemSpawnTable.asset";

    protected override string ListPropertyPath => "entries";

    protected override void Configure(Excel2SoMapping map)
    {
        map.Column("prefab").To("prefab").AsAsset<GameObject>();
        map.Column("weight").To("weight").AsInt();
        map.Column("spawnAnimEffect").To("spawnAnimEffect").AsEnum<DOTweenAnimType>();
        map.Column("spawnAnimDuration").To("spawnAnimDuration").AsFloat();
    }
}
