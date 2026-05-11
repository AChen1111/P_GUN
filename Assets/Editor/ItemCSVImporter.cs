using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class ItemCSVImporter
{
    private const string DefaultDatabaseAssetPath = "Assets/Resources/ItemDatabase.asset";

    // Unity 编辑器菜单入口：选择 CSV 文件并导入到默认数据库资产。
    [MenuItem("Tools/Excel2SO/Import Item CSV")]
    public static void ImportFromMenu()
    {
        var csvPath = EditorUtility.OpenFilePanel("Import Item CSV", Application.dataPath, "csv");
        if (string.IsNullOrEmpty(csvPath)) return;

        Import(csvPath, DefaultDatabaseAssetPath);
    }

    public static ItemDatabase Import(string csvPath, string databaseAssetPath = DefaultDatabaseAssetPath)
    {
        // 校验输入文件，避免后续读取时抛出不易定位的异常。
        if (string.IsNullOrEmpty(csvPath) || !File.Exists(csvPath))
        {
            Debug.LogError($"{nameof(ItemCSVImporter)}: CSV 文件不存在：{csvPath}");
            return null;
        }

        // 读取 CSV 并确保目标资产所在文件夹存在。
        var items = ReadItems(csvPath);
        EnsureAssetFolder(Path.GetDirectoryName(databaseAssetPath)?.Replace('\\', '/'));

        // 复用已有 ItemDatabase；不存在时创建新的 ScriptableObject 资产。
        var database = AssetDatabase.LoadAssetAtPath<ItemDatabase>(databaseAssetPath);
        if (database == null)
        {
            database = ScriptableObject.CreateInstance<ItemDatabase>();
            AssetDatabase.CreateAsset(database, databaseAssetPath);
        }

        // 用 CSV 内容覆盖数据库，并保存到 Unity 资产系统。
        database.ReplaceItems(items);
        ItemDatabase.SetDefault(database);

        EditorUtility.SetDirty(database);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Selection.activeObject = database;

        Debug.Log($"{nameof(ItemCSVImporter)}: 已导入 {items.Count} 条物品数据到 {databaseAssetPath}");
        return database;
    }

    private static List<ItemData> ReadItems(string csvPath)
    {
        // 将每一行 CSV 转成 ItemData；第一列必须是 itemId。
        var items = new List<ItemData>();
        var lineNumber = 0;

        foreach (var line in ReadCsvLines(csvPath))
        {
            lineNumber++;
            if (string.IsNullOrWhiteSpace(line)) continue;

            var columns = ParseCsvLine(line);
            if (columns.Count == 0) continue;

            if (!int.TryParse(columns[0], out var itemId))
            {
                // 第一行允许是表头；其他行 itemId 无效则跳过。
                if (lineNumber == 1) continue;

                Debug.LogWarning($"{nameof(ItemCSVImporter)}: 第 {lineNumber} 行 itemId 无效，已跳过。");
                continue;
            }

            var itemName = GetColumn(columns, 1);
            var description = GetColumn(columns, 2);
            var icon = LoadSprite(GetColumn(columns, 3));

            items.Add(new ItemData(itemId, itemName, description, icon));
        }

        return items;
    }

    private static IEnumerable<string> ReadCsvLines(string csvPath)
    {
        // 使用共享读模式，允许 CSV 正被 Excel 等工具打开时也能读取。
        using var stream = new FileStream(csvPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
        using var reader = new StreamReader(stream, DetectEncoding(stream), detectEncodingFromByteOrderMarks: true);

        while (!reader.EndOfStream)
        {
            yield return reader.ReadLine();
        }
    }

    private static Encoding DetectEncoding(FileStream stream)
    {
        // 先检查 BOM，再尝试严格 UTF-8；失败时回退到中文编码。
        var buffer = new byte[Mathf.Min(4096, (int)stream.Length)];
        var bytesRead = stream.Read(buffer, 0, buffer.Length);
        stream.Position = 0;

        if (bytesRead >= 3 && buffer[0] == 0xEF && buffer[1] == 0xBB && buffer[2] == 0xBF)
        {
            return Encoding.UTF8;
        }

        if (bytesRead >= 2 && buffer[0] == 0xFF && buffer[1] == 0xFE)
        {
            return Encoding.Unicode;
        }

        if (bytesRead >= 2 && buffer[0] == 0xFE && buffer[1] == 0xFF)
        {
            return Encoding.BigEndianUnicode;
        }

        var strictUtf8 = new UTF8Encoding(false, true);
        try
        {
            strictUtf8.GetString(buffer, 0, bytesRead);
            return Encoding.UTF8;
        }
        catch (DecoderFallbackException)
        {
            return GetChineseEncoding();
        }
    }

    private static Encoding GetChineseEncoding()
    {
        // GB18030 覆盖 GBK/GB2312 常见中文 CSV；不可用时使用系统默认编码。
        try
        {
            return Encoding.GetEncoding("GB18030");
        }
        catch
        {
            return Encoding.Default;
        }
    }

    private static List<string> ParseCsvLine(string line)
    {
        // 支持带引号字段：引号内逗号不分列，两个连续引号表示一个引号字符。
        var result = new List<string>();
        var value = new System.Text.StringBuilder();
        var inQuotes = false;

        for (var i = 0; i < line.Length; i++)
        {
            var c = line[i];
            if (c == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    value.Append('"');
                    i++;
                }
                else
                {
                    inQuotes = !inQuotes;
                }

                continue;
            }

            if (c == ',' && !inQuotes)
            {
                result.Add(value.ToString().Trim());
                value.Clear();
                continue;
            }

            value.Append(c);
        }

        result.Add(value.ToString().Trim());
        return result;
    }

    private static string GetColumn(IReadOnlyList<string> columns, int index)
    {
        // 缺失列按空字符串处理，便于兼容不完整 CSV 行。
        return index >= 0 && index < columns.Count ? columns[index] : string.Empty;
    }

    private static Sprite LoadSprite(string iconPath)
    {
        // 将 CSV 中的路径规范化为 Unity 资产路径后加载 Sprite。
        iconPath = NormalizeAssetPath(iconPath);
        if (string.IsNullOrEmpty(iconPath)) return null;

        var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(iconPath);
        if (sprite != null) return sprite;

        // 如果图片是 Multiple Sprite，Sprite 会作为子资源存在。
        sprite = AssetDatabase.LoadAllAssetsAtPath(iconPath).OfType<Sprite>().FirstOrDefault();
        if (sprite == null)
        {
            Debug.LogWarning($"{nameof(ItemCSVImporter)}: 未找到图标 Sprite：{iconPath}");
        }

        return sprite;
    }

    private static string NormalizeAssetPath(string path)
    {
        // 统一斜杠，并把绝对路径转换成 AssetDatabase 可识别的 Assets/... 路径。
        if (string.IsNullOrWhiteSpace(path)) return string.Empty;

        path = path.Trim().Replace('\\', '/');
        if (path.StartsWith("Assets/")) return path;

        if (Path.IsPathRooted(path))
        {
            var dataPath = Application.dataPath.Replace('\\', '/');
            if (path.StartsWith(dataPath))
            {
                return "Assets" + path.Substring(dataPath.Length);
            }
        }

        return path;
    }

    private static void EnsureAssetFolder(string folderPath)
    {
        // 按层级补齐目标文件夹，确保 CreateAsset 时路径有效。
        if (string.IsNullOrEmpty(folderPath) || AssetDatabase.IsValidFolder(folderPath)) return;

        var parts = folderPath.Split('/');
        var current = parts[0];

        for (var i = 1; i < parts.Length; i++)
        {
            var next = $"{current}/{parts[i]}";
            if (!AssetDatabase.IsValidFolder(next))
            {
                AssetDatabase.CreateFolder(current, parts[i]);
            }

            current = next;
        }
    }
}
