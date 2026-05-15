using System.Linq;
using UnityEngine;
using Game.Gameplay;

/// <summary>
/// Buff 数据库 Excel 导入器, 每行写入一个 Buff 配置.
/// </summary>
public sealed class BuffDatabaseExcelImporter : Excel2SoListAssetImporter<BuffDataBase>
{
    protected override string DefaultAssetPath => "Assets/GameDataSO/DataBase/BuffDataBase.asset";

    protected override string ListPropertyPath => "buffs";

    protected override void Configure(Excel2SoMapping map)
    {
        map.Column("id").To("id").AsInt();
        map.Column("buffName").To("buffName").AsString();
        map.Column("luaFile").To("luaFile").AsAsset<TextAsset>();
        map.Column("duration").To("duration").AsFloat();
        map.Column("isPermanent").To("isPermanent").AsBool();
        map.Column("interval").To("interval").AsFloat();
    }

    protected override void OnAfterImportAsset(BuffDataBase asset, ExcelTable table, Excel2SoImportReport report)
    {
        var importedBuffs = asset.Buffs.ToArray();
        asset.ReplaceBuffs(importedBuffs);
    }
}
