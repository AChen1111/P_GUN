using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public sealed class Excel2SoImportReport
{
    public string TablePath { get; set; }

    public string TargetPath { get; set; }

    public int ImportedRows { get; set; }

    public int SkippedRows { get; set; }

    public int CreatedAssets { get; set; }

    public int UpdatedAssets { get; set; }

    public int AssignedFields { get; set; }

    public int ConversionErrors { get; set; }

    public bool Canceled { get; set; }

    public override string ToString()
    {
        if (Canceled) return "Excel2SO import canceled.";

        return $"Excel2SO imported {ImportedRows} rows, skipped {SkippedRows}, " +
               $"created {CreatedAssets} assets, updated {UpdatedAssets} assets, " +
               $"assigned {AssignedFields} fields, conversion errors {ConversionErrors}.";
    }
}

public abstract class Excel2SoImporterBase
{
    protected virtual string FilePanelTitle => $"Import {GetType().Name}";

    protected virtual string FilePanelDirectory => Application.dataPath;

    protected virtual string FilePanelExtensions => "xlsx,csv";

    public Excel2SoImportReport ImportFromFilePanel()
    {
        var tablePath = EditorUtility.OpenFilePanel(FilePanelTitle, FilePanelDirectory, FilePanelExtensions);
        if (string.IsNullOrEmpty(tablePath))
        {
            return new Excel2SoImportReport { Canceled = true };
        }

        return Import(tablePath);
    }

    public Excel2SoImportReport Import(string tablePath)
    {
        try
        {
            var table = ExcelTableReader.Read(tablePath);
            return ImportTable(table, tablePath);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Excel2SO: Failed to import '{tablePath}'. {ex}");
            return new Excel2SoImportReport
            {
                TablePath = tablePath,
                ConversionErrors = 1
            };
        }
    }

    protected abstract void Configure(Excel2SoMapping map);

    protected abstract Excel2SoImportReport ImportTable(ExcelTable table, string tablePath);

    protected Excel2SoMapping BuildMapping()
    {
        var mapping = new Excel2SoMapping();
        Configure(mapping);
        return mapping;
    }

    protected static string NormalizeAssetPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path)) return string.Empty;

        path = path.Trim().Replace('\\', '/');
        if (path.StartsWith("Assets/", StringComparison.OrdinalIgnoreCase)) return path;

        if (Path.IsPathRooted(path))
        {
            var dataPath = Application.dataPath.Replace('\\', '/');
            if (path.StartsWith(dataPath, StringComparison.OrdinalIgnoreCase))
            {
                return "Assets" + path.Substring(dataPath.Length);
            }
        }

        return path;
    }

    protected static void EnsureAssetFolder(string folderPath)
    {
        folderPath = NormalizeAssetPath(folderPath);
        if (string.IsNullOrEmpty(folderPath) || AssetDatabase.IsValidFolder(folderPath)) return;

        if (!folderPath.StartsWith("Assets/", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException($"Excel2SO asset folder must be under Assets/: {folderPath}");
        }

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

    protected static string SanitizeAssetFileName(string rawName)
    {
        rawName = string.IsNullOrWhiteSpace(rawName) ? "NewAsset" : rawName.Trim();

        foreach (var invalid in Path.GetInvalidFileNameChars())
        {
            rawName = rawName.Replace(invalid, '_');
        }

        return rawName.Replace('/', '_').Replace('\\', '_');
    }

    protected static void SaveAndSelect(Object selectedObject)
    {
        if (selectedObject != null)
        {
            EditorUtility.SetDirty(selectedObject);
            Selection.activeObject = selectedObject;
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}

public abstract class Excel2SoListAssetImporter<TAsset> : Excel2SoImporterBase
    where TAsset : ScriptableObject
{
    protected abstract string DefaultAssetPath { get; }

    protected abstract string ListPropertyPath { get; }

    protected virtual bool SkipEmptyRows => true;

    public Excel2SoImportReport Import(string tablePath, string assetPath)
    {
        try
        {
            var table = ExcelTableReader.Read(tablePath);
            return ImportTable(table, tablePath, NormalizeAssetPath(assetPath));
        }
        catch (Exception ex)
        {
            Debug.LogError($"Excel2SO: Failed to import '{tablePath}'. {ex}");
            return new Excel2SoImportReport
            {
                TablePath = tablePath,
                TargetPath = assetPath,
                ConversionErrors = 1
            };
        }
    }

    protected override Excel2SoImportReport ImportTable(ExcelTable table, string tablePath)
    {
        return ImportTable(table, tablePath, NormalizeAssetPath(DefaultAssetPath));
    }

    protected virtual void OnBeforeImportAsset(TAsset asset, ExcelTable table)
    {
    }

    protected virtual void OnAfterImportAsset(TAsset asset, ExcelTable table, Excel2SoImportReport report)
    {
    }

    private Excel2SoImportReport ImportTable(ExcelTable table, string tablePath, string assetPath)
    {
        var report = new Excel2SoImportReport
        {
            TablePath = tablePath,
            TargetPath = assetPath
        };

        var asset = LoadOrCreateAsset(assetPath, out var created);
        if (asset == null)
        {
            report.ConversionErrors++;
            return report;
        }

        if (created) report.CreatedAssets++;
        else report.UpdatedAssets++;

        var mapping = BuildMapping();
        var context = new Excel2SoImportContext();
        var serializedObject = new SerializedObject(asset);
        serializedObject.Update();

        var listProperty = serializedObject.FindProperty(ListPropertyPath);
        if (listProperty == null || !listProperty.isArray || listProperty.propertyType != SerializedPropertyType.Generic)
        {
            Debug.LogError($"Excel2SO: List property '{ListPropertyPath}' was not found on {typeof(TAsset).Name}.");
            report.ConversionErrors++;
            return report;
        }

        OnBeforeImportAsset(asset, table);
        listProperty.ClearArray();

        var arrayIndex = 0;
        foreach (var row in table.Rows)
        {
            if (SkipEmptyRows && row.IsEmpty)
            {
                report.SkippedRows++;
                continue;
            }

            listProperty.InsertArrayElementAtIndex(arrayIndex);
            var element = listProperty.GetArrayElementAtIndex(arrayIndex);
            Excel2SoSerializedPropertyUtility.ClearValue(element);
            mapping.Apply(row, serializedObject, element, asset, context);

            arrayIndex++;
            report.ImportedRows++;
        }

        serializedObject.ApplyModifiedProperties();

        report.AssignedFields = context.AssignedFields;
        report.ConversionErrors = context.ConversionErrors;

        OnAfterImportAsset(asset, table, report);
        SaveAndSelect(asset);

        Debug.Log($"{GetType().Name}: {report}");
        return report;
    }

    private static TAsset LoadOrCreateAsset(string assetPath, out bool created)
    {
        created = false;
        if (string.IsNullOrWhiteSpace(assetPath) || !assetPath.StartsWith("Assets/", StringComparison.OrdinalIgnoreCase))
        {
            Debug.LogError($"Excel2SO: Asset path must be under Assets/: {assetPath}");
            return null;
        }

        var existing = AssetDatabase.LoadAssetAtPath<TAsset>(assetPath);
        if (existing != null)
        {
            return existing;
        }

        var mainAsset = AssetDatabase.LoadMainAssetAtPath(assetPath);
        if (mainAsset != null)
        {
            Debug.LogError($"Excel2SO: Existing asset at '{assetPath}' is not a {typeof(TAsset).Name}.");
            return null;
        }

        EnsureAssetFolder(Path.GetDirectoryName(assetPath)?.Replace('\\', '/'));
        var asset = ScriptableObject.CreateInstance<TAsset>();
        AssetDatabase.CreateAsset(asset, assetPath);
        created = true;
        return asset;
    }
}

public abstract class Excel2SoRowAssetImporter<TAsset> : Excel2SoImporterBase
    where TAsset : ScriptableObject
{
    protected abstract string DefaultOutputFolder { get; }

    protected virtual string AssetPathColumn => "assetPath";

    protected virtual string AssetNameColumn => "assetName";

    protected virtual bool SkipEmptyRows => true;

    protected override Excel2SoImportReport ImportTable(ExcelTable table, string tablePath)
    {
        var report = new Excel2SoImportReport
        {
            TablePath = tablePath,
            TargetPath = NormalizeAssetPath(DefaultOutputFolder)
        };

        var mapping = BuildMapping();
        var context = new Excel2SoImportContext();

        foreach (var row in table.Rows)
        {
            if (SkipEmptyRows && row.IsEmpty)
            {
                report.SkippedRows++;
                continue;
            }

            var assetPath = ResolveAssetPath(row);
            if (string.IsNullOrEmpty(assetPath))
            {
                report.SkippedRows++;
                Debug.LogWarning($"Excel2SO: Row {row.RowNumber} has no usable assetPath or assetName.");
                continue;
            }

            var asset = LoadOrCreateAsset(assetPath, out var created);
            if (asset == null)
            {
                report.SkippedRows++;
                report.ConversionErrors++;
                continue;
            }

            if (created) report.CreatedAssets++;
            else report.UpdatedAssets++;

            var serializedObject = new SerializedObject(asset);
            serializedObject.Update();
            OnBeforeImportRow(asset, row);
            mapping.Apply(row, serializedObject, null, asset, context);
            serializedObject.ApplyModifiedProperties();
            OnAfterImportRow(asset, row);
            EditorUtility.SetDirty(asset);
            report.ImportedRows++;
        }

        report.AssignedFields = context.AssignedFields;
        report.ConversionErrors += context.ConversionErrors;

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"{GetType().Name}: {report}");
        return report;
    }

    protected virtual void OnBeforeImportRow(TAsset asset, ExcelRow row)
    {
    }

    protected virtual void OnAfterImportRow(TAsset asset, ExcelRow row)
    {
    }

    protected virtual string ResolveAssetPath(ExcelRow row)
    {
        var explicitAssetPath = NormalizeAssetPath(row.Get(AssetPathColumn));
        if (!string.IsNullOrWhiteSpace(explicitAssetPath))
        {
            return explicitAssetPath;
        }

        var rawName = row.Get(AssetNameColumn);
        if (string.IsNullOrWhiteSpace(rawName)) rawName = row.Get("name");
        if (string.IsNullOrWhiteSpace(rawName)) rawName = row.Get("id");
        if (string.IsNullOrWhiteSpace(rawName)) return string.Empty;

        var outputFolder = NormalizeAssetPath(DefaultOutputFolder).TrimEnd('/');
        return $"{outputFolder}/{SanitizeAssetFileName(rawName)}.asset";
    }

    private static TAsset LoadOrCreateAsset(string assetPath, out bool created)
    {
        created = false;
        assetPath = NormalizeAssetPath(assetPath);
        if (string.IsNullOrWhiteSpace(assetPath) || !assetPath.StartsWith("Assets/", StringComparison.OrdinalIgnoreCase))
        {
            Debug.LogError($"Excel2SO: Asset path must be under Assets/: {assetPath}");
            return null;
        }

        var existing = AssetDatabase.LoadAssetAtPath<TAsset>(assetPath);
        if (existing != null)
        {
            return existing;
        }

        var mainAsset = AssetDatabase.LoadMainAssetAtPath(assetPath);
        if (mainAsset != null)
        {
            Debug.LogError($"Excel2SO: Existing asset at '{assetPath}' is not a {typeof(TAsset).Name}.");
            return null;
        }

        EnsureAssetFolder(Path.GetDirectoryName(assetPath)?.Replace('\\', '/'));
        var asset = ScriptableObject.CreateInstance<TAsset>();
        AssetDatabase.CreateAsset(asset, assetPath);
        created = true;
        return asset;
    }
}
