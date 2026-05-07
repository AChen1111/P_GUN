using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class ItemCSVImporter
{
    private const string DefaultDatabaseAssetPath = "Assets/Resources/ItemDatabase.asset";

    [MenuItem("Tools/PG/Import Item CSV")]
    public static void ImportFromMenu()
    {
        var csvPath = EditorUtility.OpenFilePanel("Import Item CSV", Application.dataPath, "csv");
        if (string.IsNullOrEmpty(csvPath)) return;

        Import(csvPath, DefaultDatabaseAssetPath);
    }

    public static ItemDatabase Import(string csvPath, string databaseAssetPath = DefaultDatabaseAssetPath)
    {
        if (string.IsNullOrEmpty(csvPath) || !File.Exists(csvPath))
        {
            Debug.LogError($"{nameof(ItemCSVImporter)}: CSV 文件不存在：{csvPath}");
            return null;
        }

        var items = ReadItems(csvPath);
        EnsureAssetFolder(Path.GetDirectoryName(databaseAssetPath)?.Replace('\\', '/'));

        var database = AssetDatabase.LoadAssetAtPath<ItemDatabase>(databaseAssetPath);
        if (database == null)
        {
            database = ScriptableObject.CreateInstance<ItemDatabase>();
            AssetDatabase.CreateAsset(database, databaseAssetPath);
        }

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
        var items = new List<ItemData>();
        var lineNumber = 0;

        foreach (var line in File.ReadLines(csvPath))
        {
            lineNumber++;
            if (string.IsNullOrWhiteSpace(line)) continue;

            var columns = ParseCsvLine(line);
            if (columns.Count == 0) continue;

            if (!int.TryParse(columns[0], out var itemId))
            {
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

    private static List<string> ParseCsvLine(string line)
    {
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
        return index >= 0 && index < columns.Count ? columns[index] : string.Empty;
    }

    private static Sprite LoadSprite(string iconPath)
    {
        iconPath = NormalizeAssetPath(iconPath);
        if (string.IsNullOrEmpty(iconPath)) return null;

        var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(iconPath);
        if (sprite != null) return sprite;

        sprite = AssetDatabase.LoadAllAssetsAtPath(iconPath).OfType<Sprite>().FirstOrDefault();
        if (sprite == null)
        {
            Debug.LogWarning($"{nameof(ItemCSVImporter)}: 未找到图标 Sprite：{iconPath}");
        }

        return sprite;
    }

    private static string NormalizeAssetPath(string path)
    {
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
