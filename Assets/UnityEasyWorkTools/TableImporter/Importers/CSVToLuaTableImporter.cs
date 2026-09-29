using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 把 Assets/csv 下的玩法配置 CSV 转成 Assets/Scripts/LuaRaw/Data 下的 Lua 数据表.
/// 列缺失, 键为空或数值无法解析时直接让导入失败, 不做静默兜底.
/// </summary>
public static class CSVToLuaTableImporter
{
    private const string CsvRoot = "Assets/csv";
    private const string OutputRoot = "Assets/Scripts/LuaRaw/Data";

    /// <summary>
    /// CSV 列的输出类型.
    /// </summary>
    private enum ColumnType
    {
        Int,
        Float,
        String,
        Bool,
        StringList,
        ModifierList
    }

    /// <summary>
    /// 单列的转换规则.
    /// </summary>
    private sealed class ColumnSpec
    {
        public string CsvColumn;  // CSV 列名.
        public string LuaField;   // Lua 字段名.
        public ColumnType Type;   // 输出类型.
        public bool AllowEmpty;   // 是否允许字符串为空, 默认不允许.

        public ColumnSpec(string csvColumn, string luaField, ColumnType type, bool allowEmpty = false)
        {
            CsvColumn = csvColumn;
            LuaField = luaField;
            Type = type;
            AllowEmpty = allowEmpty;
        }
    }

    /// <summary>
    /// 单张表的转换规则.
    /// </summary>
    private sealed class TableSpec
    {
        public string CsvFile;    // CSV 文件名, 相对 Assets/csv.
        public string ModuleName; // 输出 Lua 模块名, 也是 require 时的文件名.
        public string KeyColumn;   // 主键列, 单行表和波次表的主键由形态决定.
        public bool SingleRow;     // 单行配置表, 例如 PlayerData.
        public bool SpawnWaves;   // 波次表, 按 spawnTableId 分组.
        public ColumnSpec[] Columns;
    }

    // Buff 属性修正允许的枚举名, 与 StatType 和 ModifierType 保持一致.
    private static readonly HashSet<string> StatNames = new HashSet<string> { "MoveSpeed", "Attack", "Defense", "MaxHp" };
    private static readonly HashSet<string> ModifierNames = new HashSet<string> { "Flat", "PercentAdd", "FinalMul" };

    private static readonly TableSpec[] TableSpecs = BuildTableSpecs();

    [MenuItem("Tools/UnityEasyWorkTools/CSV To Lua Table/Import All")]
    public static void ImportAll()
    {
        Run(validateOnly: false);
    }

    [MenuItem("Tools/UnityEasyWorkTools/CSV To Lua Table/Validate Only")]
    public static void ValidateAll()
    {
        Run(validateOnly: true);
    }

    /// <summary>
    /// 依次导入或校验全部表, 任一表失败则中止并报错.
    /// </summary>
    private static void Run(bool validateOnly)
    {
        var importedCount = 0;
        try
        {
            foreach (var spec in TableSpecs)
            {
                importedCount += ImportTable(spec, validateOnly);
            }
        }
        catch (Exception exception)
        {
            Debug.LogError($"[CSVToLuaTable] 导入中止, Error: {exception.Message}");
            return;
        }

        if (validateOnly)
        {
            Debug.Log($"[CSVToLuaTable] 校验完成, 共 {importedCount} 张表通过.");
            return;
        }

        AssetDatabase.Refresh();
        Debug.Log($"[CSVToLuaTable] 导入完成, 共 {importedCount} 张表写入 {OutputRoot}.");
    }

    /// <summary>
    /// 导入单张表, 返回 1 表示处理了一张表.
    /// </summary>
    private static int ImportTable(TableSpec spec, bool validateOnly)
    {
        var csvPath = $"{CsvRoot}/{spec.CsvFile}";
        var fullPath = Path.GetFullPath(csvPath);
        if (!File.Exists(fullPath))
        {
            throw new InvalidOperationException($"找不到源表: {csvPath}.");
        }

        var rows = ReadCsv(fullPath);
        if (rows.Count == 0)
        {
            throw new InvalidOperationException($"{csvPath} 没有任何数据行.");
        }

        var headers = rows[0];
        var dataRows = rows.Skip(1).ToList();
        var columnIndex = BuildColumnIndex(spec, headers, csvPath);

        if (spec.ModuleName == "LevelData")
        {
            ValidateLevelRows(dataRows, columnIndex, csvPath);
        }

        string content;
        if (spec.SingleRow)
        {
            content = BuildSingleRowContent(spec, dataRows, columnIndex, csvPath);
        }
        else if (spec.SpawnWaves)
        {
            content = BuildSpawnWavesContent(spec, dataRows, columnIndex, csvPath);
        }
        else
        {
            content = BuildKeyedContent(spec, dataRows, columnIndex, csvPath);
        }

        if (!validateOnly)
        {
            Directory.CreateDirectory(OutputRoot);
            var outputPath = $"{OutputRoot}/{spec.ModuleName}.lua";
            File.WriteAllText(Path.GetFullPath(outputPath), content, new UTF8Encoding(false));
        }

        return 1;
    }

    /// <summary>
    /// 校验关卡行的数量约束: 起点和终点各一间, 五类数量之和等于房间总数.
    /// </summary>
    private static void ValidateLevelRows(List<string[]> dataRows, Dictionary<string, int> columnIndex, string csvPath)
    {
        for (var i = 0; i < dataRows.Count; i++)
        {
            var rowNumber = i + 2;
            var roomCount = ParseInt(ReadCell(dataRows[i], columnIndex, "roomCount"), csvPath, rowNumber, "roomCount");
            var initCount = ParseInt(ReadCell(dataRows[i], columnIndex, "initCount"), csvPath, rowNumber, "initCount");
            var finalCount = ParseInt(ReadCell(dataRows[i], columnIndex, "finalCount"), csvPath, rowNumber, "finalCount");
            var chestCount = ParseInt(ReadCell(dataRows[i], columnIndex, "chestCount"), csvPath, rowNumber, "chestCount");
            var saveCount = ParseInt(ReadCell(dataRows[i], columnIndex, "saveCount"), csvPath, rowNumber, "saveCount");
            var normalCount = ParseInt(ReadCell(dataRows[i], columnIndex, "normalCount"), csvPath, rowNumber, "normalCount");

            if (initCount != 1)
            {
                throw new InvalidOperationException($"{csvPath} 第 {rowNumber} 行 initCount 必须为 1: {initCount}.");
            }

            if (finalCount != 1)
            {
                throw new InvalidOperationException($"{csvPath} 第 {rowNumber} 行 finalCount 必须为 1: {finalCount}.");
            }

            var sum = initCount + finalCount + chestCount + saveCount + normalCount;
            if (sum != roomCount)
            {
                throw new InvalidOperationException($"{csvPath} 第 {rowNumber} 行五类房间数量之和 {sum} 不等于 roomCount {roomCount}.");
            }

            if (chestCount < 0 || saveCount < 0 || normalCount < 0)
            {
                throw new InvalidOperationException($"{csvPath} 第 {rowNumber} 行各类数量不能为负.");
            }
        }
    }

    /// <summary>
    /// 校验表头并返回列名到下标的映射.
    /// </summary>
    private static Dictionary<string, int> BuildColumnIndex(TableSpec spec, IReadOnlyList<string> headers, string csvPath)
    {
        var columnIndex = new Dictionary<string, int>();
        for (var i = 0; i < headers.Count; i++)
        {
            var header = headers[i].Trim();
            if (header.Length == 0 || columnIndex.ContainsKey(header))
            {
                throw new InvalidOperationException($"{csvPath} 表头第 {i + 1} 列为空或重名: {header}.");
            }

            columnIndex[header] = i;
        }

        var requiredColumns = new List<string> { spec.KeyColumn };
        requiredColumns.AddRange(spec.Columns.Select(column => column.CsvColumn));
        foreach (var column in requiredColumns)
        {
            if (string.IsNullOrEmpty(column))
            {
                continue;
            }

            if (!columnIndex.ContainsKey(column))
            {
                throw new InvalidOperationException($"{csvPath} 缺少列: {column}.");
            }
        }

        return columnIndex;
    }

    /// <summary>
    /// 生成单行配置表, 例如 PlayerData.
    /// </summary>
    private static string BuildSingleRowContent(TableSpec spec, List<string[]> dataRows, Dictionary<string, int> columnIndex, string csvPath)
    {
        if (dataRows.Count != 1)
        {
            throw new InvalidOperationException($"{csvPath} 应只有一行配置, 实际 {dataRows.Count} 行.");
        }

        var builder = new StringBuilder();
        AppendFileHeader(builder, spec);
        builder.AppendLine("return {");
        AppendFields(builder, spec, dataRows[0], columnIndex, csvPath, 1);
        builder.AppendLine("}");
        return builder.ToString();
    }

    /// <summary>
    /// 生成按键取行的配置表, 例如 WeaponData.
    /// </summary>
    private static string BuildKeyedContent(TableSpec spec, List<string[]> dataRows, Dictionary<string, int> columnIndex, string csvPath)
    {
        var seenKeys = new HashSet<string>();
        var builder = new StringBuilder();
        AppendFileHeader(builder, spec);
        builder.AppendLine("return {");

        for (var i = 0; i < dataRows.Count; i++)
        {
            var row = dataRows[i];
            var key = ReadCell(row, columnIndex, spec.KeyColumn);
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new InvalidOperationException($"{csvPath} 第 {i + 2} 行 {spec.KeyColumn} 为空.");
            }

            if (!seenKeys.Add(key.Trim()))
            {
                throw new InvalidOperationException($"{csvPath} 第 {i + 2} 行 {spec.KeyColumn} 重复: {key}.");
            }

            builder.AppendLine($"    [{LuaString(key.Trim())}] = {{");
            AppendFields(builder, spec, row, columnIndex, csvPath, 2, i + 2);
            builder.AppendLine("    },");
        }

        builder.AppendLine("}");
        return builder.ToString();
    }

    /// <summary>
    /// 生成波次表: enemy 行按表和波次分组, item 行按表收集掉落权重.
    /// </summary>
    private static string BuildSpawnWavesContent(TableSpec spec, List<string[]> dataRows, Dictionary<string, int> columnIndex, string csvPath)
    {
        var enemyRowsByTable = new Dictionary<string, SortedDictionary<int, List<string[]>>>();
        var itemRowsByTable = new Dictionary<string, List<string[]>>();

        for (var i = 0; i < dataRows.Count; i++)
        {
            var row = dataRows[i];
            var rowNumber = i + 2;
            var tableId = ReadCell(row, columnIndex, spec.KeyColumn);
            if (string.IsNullOrWhiteSpace(tableId))
            {
                throw new InvalidOperationException($"{csvPath} 第 {rowNumber} 行 {spec.KeyColumn} 为空.");
            }

            tableId = tableId.Trim();
            var kind = ReadCell(row, columnIndex, "kind").Trim();
            if (kind == "enemy")
            {
                var waveIndex = ParseInt(ReadCell(row, columnIndex, "waveIndex"), csvPath, rowNumber, "waveIndex");
                if (waveIndex < 0)
                {
                    throw new InvalidOperationException($"{csvPath} 第 {rowNumber} 行 waveIndex 不能小于 0: {waveIndex}.");
                }

                if (!enemyRowsByTable.TryGetValue(tableId, out var waves))
                {
                    waves = new SortedDictionary<int, List<string[]>>();
                    enemyRowsByTable[tableId] = waves;
                }

                if (!waves.TryGetValue(waveIndex, out var entries))
                {
                    entries = new List<string[]>();
                    waves[waveIndex] = entries;
                }

                entries.Add(row);
            }
            else if (kind == "item")
            {
                var itemId = ParseInt(ReadCell(row, columnIndex, "itemId"), csvPath, rowNumber, "itemId");
                var weight = ParseFloat(ReadCell(row, columnIndex, "weight"), csvPath, rowNumber, "weight");
                if (weight <= 0f)
                {
                    throw new InvalidOperationException($"{csvPath} 第 {rowNumber} 行 weight 必须大于 0: {weight}.");
                }

                if (!itemRowsByTable.TryGetValue(tableId, out var drops))
                {
                    drops = new List<string[]>();
                    itemRowsByTable[tableId] = drops;
                }

                // 同一张表里同一个道具只允许一条权重, 重复会混淆权重含义.
                foreach (var existing in drops)
                {
                    var existingItemId = ParseInt(ReadCell(existing, columnIndex, "itemId"), csvPath, 0, "itemId");
                    if (existingItemId == itemId)
                    {
                        throw new InvalidOperationException($"{csvPath} 表 {tableId} 中 itemId={itemId} 的掉落权重重复.");
                    }
                }

                drops.Add(row);
            }
            else
            {
                throw new InvalidOperationException($"{csvPath} 第 {rowNumber} 行 kind 非法: {kind}, 应为 enemy 或 item.");
            }
        }

        // 同一波里同一个 enemyId 不允许出现两次, 避免数量含义混淆.
        foreach (var pair in enemyRowsByTable)
        {
            foreach (var wavePair in pair.Value)
            {
                var seenEnemyIds = new HashSet<int>();
                foreach (var row in wavePair.Value)
                {
                    var enemyId = ParseInt(ReadCell(row, columnIndex, "enemyId"), csvPath, 0, "enemyId");
                    if (!seenEnemyIds.Add(enemyId))
                    {
                        throw new InvalidOperationException($"{csvPath} 表 {pair.Key} 的 wave {wavePair.Key} 中 enemyId={enemyId} 重复.");
                    }
                }
            }
        }

        // 两类行共用同一批表 id, 合并后逐表输出.
        var allTableIds = new HashSet<string>();
        allTableIds.UnionWith(enemyRowsByTable.Keys);
        allTableIds.UnionWith(itemRowsByTable.Keys);
        var orderedTableIds = allTableIds.OrderBy(id => id, StringComparer.Ordinal);

        var builder = new StringBuilder();
        AppendFileHeader(builder, spec);
        builder.AppendLine("return {");

        foreach (var tableId in orderedTableIds)
        {
            builder.AppendLine($"    [{LuaString(tableId)}] = {{");
            builder.AppendLine("        waves = {");
            if (enemyRowsByTable.TryGetValue(tableId, out var waves))
            {
                foreach (var wavePair in waves)
                {
                    builder.AppendLine("            {");
                    foreach (var row in wavePair.Value)
                    {
                        var enemyId = ParseInt(ReadCell(row, columnIndex, "enemyId"), csvPath, 0, "enemyId");
                        var count = ParseInt(ReadCell(row, columnIndex, "count"), csvPath, 0, "count");
                        if (count <= 0)
                        {
                            throw new InvalidOperationException($"{csvPath} 表 {tableId} 的 wave {wavePair.Key} 中 count 必须大于 0: {count}.");
                        }

                        builder.AppendLine($"                {{ enemyId = {enemyId}, count = {count} }},");
                    }

                    builder.AppendLine("            },");
                }
            }

            builder.AppendLine("        },");
            builder.AppendLine("        itemDrops = {");
            if (itemRowsByTable.TryGetValue(tableId, out var drops))
            {
                foreach (var row in drops)
                {
                    var itemId = ParseInt(ReadCell(row, columnIndex, "itemId"), csvPath, 0, "itemId");
                    var weight = ParseFloat(ReadCell(row, columnIndex, "weight"), csvPath, 0, "weight");
                    builder.AppendLine($"            {{ itemId = {itemId}, weight = {LuaNumber(weight)} }},");
                }
            }

            builder.AppendLine("        },");
            builder.AppendLine("    },");
        }

        builder.AppendLine("}");
        return builder.ToString();
    }

    /// <summary>
    /// 按列规则把一行输出成 Lua 字段.
    /// </summary>
    private static void AppendFields(StringBuilder builder, TableSpec spec, string[] row, Dictionary<string, int> columnIndex, string csvPath, int indentLevel, int rowNumber = 1)
    {
        var indent = new string(' ', indentLevel * 4);
        for (var i = 0; i < spec.Columns.Length; i++)
        {
            var column = spec.Columns[i];
            var raw = ReadCell(row, columnIndex, column.CsvColumn);
            switch (column.Type)
            {
                case ColumnType.Int:
                    builder.AppendLine($"{indent}{column.LuaField} = {ParseInt(raw, csvPath, rowNumber, column.CsvColumn)},");
                    break;
                case ColumnType.Float:
                    builder.AppendLine($"{indent}{column.LuaField} = {LuaNumber(ParseFloat(raw, csvPath, rowNumber, column.CsvColumn))},");
                    break;
                case ColumnType.Bool:
                    builder.AppendLine($"{indent}{column.LuaField} = {(ParseBool(raw, csvPath, rowNumber, column.CsvColumn) ? "true" : "false")},");
                    break;
                case ColumnType.String:
                    if (string.IsNullOrWhiteSpace(raw) && !column.AllowEmpty)
                    {
                        throw new InvalidOperationException($"{csvPath} 第 {rowNumber} 行 {column.CsvColumn} 不能为空.");
                    }

                    builder.AppendLine($"{indent}{column.LuaField} = {LuaString(raw.Trim())},");
                    break;
                case ColumnType.StringList:
                    builder.AppendLine($"{indent}{column.LuaField} = {BuildStringList(raw, csvPath, rowNumber, column.CsvColumn, indent + "    ")},");
                    break;
                case ColumnType.ModifierList:
                    builder.AppendLine($"{indent}{column.LuaField} = {BuildModifierList(raw, csvPath, rowNumber, column.CsvColumn, indent + "    ")},");
                    break;
            }
        }
    }

    /// <summary>
    /// 输出分号分隔的字符串列表.
    /// </summary>
    private static string BuildStringList(string raw, string csvPath, int rowNumber, string csvColumn, string indent)
    {
        var entries = SplitList(raw);
        if (entries.Count == 0)
        {
            return "{}";
        }

        var builder = new StringBuilder();
        builder.AppendLine("{");
        foreach (var entry in entries)
        {
            builder.AppendLine($"{indent}{LuaString(entry)},");
        }

        builder.Append($"{indent.Substring(4)}}}");
        return builder.ToString();
    }

    /// <summary>
    /// 输出 Buff 属性修正列表, 每条格式为 StatType:ModifierType:Value.
    /// </summary>
    private static string BuildModifierList(string raw, string csvPath, int rowNumber, string csvColumn, string indent)
    {
        var entries = SplitList(raw);
        if (entries.Count == 0)
        {
            return "{}";
        }

        var builder = new StringBuilder();
        builder.AppendLine("{");
        foreach (var entry in entries)
        {
            var parts = entry.Split(':');
            if (parts.Length != 3)
            {
                throw new InvalidOperationException($"{csvPath} 第 {rowNumber} 行 {csvColumn} 的 {entry} 应为 StatType:ModifierType:Value.");
            }

            var stat = parts[0].Trim();
            var modifierType = parts[1].Trim();
            var valueText = parts[2].Trim();
            if (!StatNames.Contains(stat))
            {
                throw new InvalidOperationException($"{csvPath} 第 {rowNumber} 行 {csvColumn} 的 StatType 非法: {stat}.");
            }

            if (!ModifierNames.Contains(modifierType))
            {
                throw new InvalidOperationException($"{csvPath} 第 {rowNumber} 行 {csvColumn} 的 ModifierType 非法: {modifierType}.");
            }

            var value = ParseFloat(valueText, csvPath, rowNumber, csvColumn);
            builder.AppendLine($"{indent}{{ stat = {LuaString(stat)}, type = {LuaString(modifierType)}, value = {LuaNumber(value)} }},");
        }

        builder.Append($"{indent.Substring(4)}}}");
        return builder.ToString();
    }

    /// <summary>
    /// 读取单元格, 列越界时返回空字符串.
    /// </summary>
    private static string ReadCell(string[] row, Dictionary<string, int> columnIndex, string column)
    {
        if (string.IsNullOrEmpty(column) || !columnIndex.TryGetValue(column, out var index))
        {
            return string.Empty;
        }

        return index < row.Length ? row[index] : string.Empty;
    }

    private static int ParseInt(string raw, string csvPath, int rowNumber, string column)
    {
        if (!int.TryParse(raw?.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
        {
            throw new InvalidOperationException($"{csvPath} 第 {rowNumber} 行 {column} 不是合法整数: {raw}.");
        }

        return value;
    }

    private static float ParseFloat(string raw, string csvPath, int rowNumber, string column)
    {
        if (!float.TryParse(raw?.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
        {
            throw new InvalidOperationException($"{csvPath} 第 {rowNumber} 行 {column} 不是合法数字: {raw}.");
        }

        return value;
    }

    private static bool ParseBool(string raw, string csvPath, int rowNumber, string column)
    {
        switch (raw?.Trim().ToUpperInvariant())
        {
            case "TRUE":
            case "1":
                return true;
            case "FALSE":
            case "0":
                return false;
            default:
                throw new InvalidOperationException($"{csvPath} 第 {rowNumber} 行 {column} 不是合法布尔值: {raw}.");
        }
    }

    /// <summary>
    /// 按分号拆分列表并去掉空白项.
    /// </summary>
    private static List<string> SplitList(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
        {
            return new List<string>();
        }

        return raw.Split(';')
            .Select(entry => entry.Trim())
            .Where(entry => entry.Length > 0)
            .ToList();
    }

    private static string LuaString(string value)
    {
        return "\"" + value
            .Replace("\\", "\\\\")
            .Replace("\"", "\\\"")
            .Replace("\r", " ")
            .Replace("\n", " ") + "\"";
    }

    private static string LuaNumber(float value)
    {
        return value.ToString("0.######", CultureInfo.InvariantCulture);
    }

    private static void AppendFileHeader(StringBuilder builder, TableSpec spec)
    {
        builder.AppendLine($"-- 本文件由 CSVToLuaTable 生成, 请勿手改.");
        builder.AppendLine($"-- 源表: {CsvRoot}/{spec.CsvFile}");
        builder.AppendLine();
    }

    /// <summary>
    /// 解析 CSV 文本, 支持双引号包裹和引号内逗号.
    /// </summary>
    private static List<string[]> ReadCsv(string fullPath)
    {
        var rows = new List<string[]>();
        var cells = new List<string>();
        var cell = new StringBuilder();
        var inQuotes = false;
        var rowStarted = false;

        var text = File.ReadAllText(fullPath);
        for (var i = 0; i < text.Length; i++)
        {
            var c = text[i];
            if (inQuotes)
            {
                if (c == '"')
                {
                    if (i + 1 < text.Length && text[i + 1] == '"')
                    {
                        cell.Append('"');
                        i++;
                    }
                    else
                    {
                        inQuotes = false;
                    }
                }
                else
                {
                    cell.Append(c);
                }

                continue;
            }

            switch (c)
            {
                case '"':
                    inQuotes = true;
                    rowStarted = true;
                    break;
                case ',':
                    cells.Add(cell.ToString());
                    cell.Clear();
                    rowStarted = true;
                    break;
                case '\r':
                    break;
                case '\n':
                    if (rowStarted || cell.Length > 0)
                    {
                        cells.Add(cell.ToString());
                        rows.Add(cells.ToArray());
                        cells.Clear();
                        cell.Clear();
                        rowStarted = false;
                    }

                    break;
                default:
                    cell.Append(c);
                    rowStarted = true;
                    break;
            }
        }

        if (rowStarted || cell.Length > 0)
        {
            cells.Add(cell.ToString());
            rows.Add(cells.ToArray());
        }

        return rows;
    }

    /// <summary>
    /// 定义全部数据表.
    /// </summary>
    private static TableSpec[] BuildTableSpecs()
    {
        return new[]
        {
            new TableSpec
            {
                CsvFile = "PlayerData.csv",
                ModuleName = "PlayerData",
                KeyColumn = null,
                SingleRow = true,
                Columns = new[]
                {
                    new ColumnSpec("maxHp", "maxHp", ColumnType.Int),
                    new ColumnSpec("moveSpeed", "moveSpeed", ColumnType.Float),
                    new ColumnSpec("bulletTimeEnemyScale", "bulletTimeEnemyScale", ColumnType.Float),
                    new ColumnSpec("bulletTimeDuration", "bulletTimeDuration", ColumnType.Float),
                    new ColumnSpec("bulletTimeCooldown", "bulletTimeCooldown", ColumnType.Float),
                }
            },
            new TableSpec
            {
                CsvFile = "WeaponData.csv",
                ModuleName = "WeaponData",
                KeyColumn = "weaponId",
                Columns = new[]
                {
                    new ColumnSpec("displayName", "displayName", ColumnType.String),
                    new ColumnSpec("minDamage", "minDamage", ColumnType.Int),
                    new ColumnSpec("maxDamage", "maxDamage", ColumnType.Int),
                    new ColumnSpec("maxBulletBagNum", "maxBulletBagNum", ColumnType.Int),
                    new ColumnSpec("clipSize", "clipSize", ColumnType.Int),
                    new ColumnSpec("shootInterval", "shootInterval", ColumnType.Float),
                    new ColumnSpec("bulletSpeed", "bulletSpeed", ColumnType.Int),
                    new ColumnSpec("reloadSoundAddress", "reloadSoundAddress", ColumnType.String, allowEmpty: true),
                    new ColumnSpec("shootSoundAddresses", "shootSoundAddresses", ColumnType.StringList, allowEmpty: true),
                }
            },
            new TableSpec
            {
                CsvFile = "BulletData.csv",
                ModuleName = "BulletData",
                KeyColumn = "bulletId",
                Columns = new[]
                {
                    new ColumnSpec("lifeTime", "lifeTime", ColumnType.Float),
                    new ColumnSpec("hitBuffId", "hitBuffId", ColumnType.Int),
                    new ColumnSpec("hitPlayerSoundAddress", "hitPlayerSoundAddress", ColumnType.String, allowEmpty: true),
                    new ColumnSpec("hitWallSoundAddress", "hitWallSoundAddress", ColumnType.String, allowEmpty: true),
                }
            },
            new TableSpec
            {
                CsvFile = "BuffData.csv",
                ModuleName = "BuffData",
                KeyColumn = "buffId",
                Columns = new[]
                {
                    new ColumnSpec("buffName", "buffName", ColumnType.String),
                    new ColumnSpec("description", "description", ColumnType.String, allowEmpty: true),
                    new ColumnSpec("iconAddress", "iconAddress", ColumnType.String),
                    new ColumnSpec("tag", "tag", ColumnType.String),
                    new ColumnSpec("duration", "duration", ColumnType.Float),
                    new ColumnSpec("isPermanent", "isPermanent", ColumnType.Bool),
                    new ColumnSpec("interval", "interval", ColumnType.Float),
                    new ColumnSpec("modifiers", "modifiers", ColumnType.ModifierList, allowEmpty: true),
                    new ColumnSpec("behaviorPrefabAddress", "behaviorPrefabAddress", ColumnType.String, allowEmpty: true),
                }
            },
            new TableSpec
            {
                CsvFile = "EnemyData.csv",
                ModuleName = "EnemyData",
                KeyColumn = "enemyId",
                Columns = new[]
                {
                    new ColumnSpec("displayName", "displayName", ColumnType.String),
                    new ColumnSpec("prefabAddress", "prefabAddress", ColumnType.String),
                    new ColumnSpec("maxHp", "maxHp", ColumnType.Int),
                    new ColumnSpec("moveSpeed", "moveSpeed", ColumnType.Float),
                    new ColumnSpec("damage", "damage", ColumnType.Int),
                    new ColumnSpec("itemDropChance", "itemDropChance", ColumnType.Float),
                    new ColumnSpec("visionRadius", "visionRadius", ColumnType.Float),
                    new ColumnSpec("visionAngle", "visionAngle", ColumnType.Float),
                    new ColumnSpec("searchTime", "searchTime", ColumnType.Float),
                    new ColumnSpec("separationRadius", "separationRadius", ColumnType.Float),
                    new ColumnSpec("separationWeight", "separationWeight", ColumnType.Float),
                    new ColumnSpec("attackInterval", "attackInterval", ColumnType.Float),
                    new ColumnSpec("attackRange", "attackRange", ColumnType.Float),
                }
            },
            new TableSpec
            {
                CsvFile = "ItemData.csv",
                ModuleName = "ItemData",
                KeyColumn = "itemId",
                Columns = new[]
                {
                    new ColumnSpec("itemName", "itemName", ColumnType.String),
                    new ColumnSpec("description", "description", ColumnType.String, allowEmpty: true),
                    new ColumnSpec("iconAddress", "iconAddress", ColumnType.String),
                    new ColumnSpec("effectPrefabAddress", "effectPrefabAddress", ColumnType.String),
                    new ColumnSpec("prefabAddress", "prefabAddress", ColumnType.String),
                }
            },
            new TableSpec
            {
                CsvFile = "SpawnData.csv",
                ModuleName = "SpawnData",
                KeyColumn = "spawnTableId",
                SpawnWaves = true,
                Columns = new[]
                {
                    new ColumnSpec("kind", "kind", ColumnType.String),
                    new ColumnSpec("waveIndex", "waveIndex", ColumnType.Int),
                    new ColumnSpec("enemyId", "enemyId", ColumnType.Int),
                    new ColumnSpec("count", "count", ColumnType.Int),
                    new ColumnSpec("itemId", "itemId", ColumnType.Int),
                    new ColumnSpec("weight", "weight", ColumnType.Float),
                }
            },
            new TableSpec
            {
                CsvFile = "LevelConfig.csv",
                ModuleName = "LevelData",
                KeyColumn = "levelId",
                Columns = new[]
                {
                    new ColumnSpec("roomCount", "roomCount", ColumnType.Int),
                    new ColumnSpec("initCount", "initCount", ColumnType.Int),
                    new ColumnSpec("finalCount", "finalCount", ColumnType.Int),
                    new ColumnSpec("chestCount", "chestCount", ColumnType.Int),
                    new ColumnSpec("saveCount", "saveCount", ColumnType.Int),
                    new ColumnSpec("normalCount", "normalCount", ColumnType.Int),
                    new ColumnSpec("initPrefab", "initPrefab", ColumnType.String),
                    new ColumnSpec("finalPrefab", "finalPrefab", ColumnType.String),
                    new ColumnSpec("chestPrefab", "chestPrefab", ColumnType.String),
                    new ColumnSpec("savePrefab", "savePrefab", ColumnType.String),
                    new ColumnSpec("normalPrefabs", "normalPrefabs", ColumnType.StringList),
                }
            },
        };
    }
}
