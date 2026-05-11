using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

public static class AddressablesLabelTableImporter
{
    private const string MenuRoot = "PG/Addressables/";

    [MenuItem(MenuRoot + "Import Labels From Excel")]
    public static void ImportFromMenu()
    {
        var path = EditorUtility.OpenFilePanel("Import Addressables Labels", Application.dataPath, "xlsx,csv");
        if (string.IsNullOrEmpty(path)) return;

        Import(path);
    }

    [MenuItem(MenuRoot + "Create Labels CSV Template")]
    public static void CreateCsvTemplate()
    {
        var path = EditorUtility.SaveFilePanelInProject(
            "Create Addressables Labels Template",
            "AddressablesLabelsTemplate",
            "csv",
            "Choose where to save the Addressables labels CSV template."
        );

        if (string.IsNullOrEmpty(path)) return;

        var csv = string.Join(
            Environment.NewLine,
            "assetPath,address,group,labels,clearLabels",
            "Assets/Prefab/GunList/AK.prefab,weapon/ak,Local_Weapons_Base,weapons_base;weapon_ak,false"
        );

        File.WriteAllText(path, csv, new UTF8Encoding(true));
        AssetDatabase.Refresh();
        Debug.Log($"{nameof(AddressablesLabelTableImporter)}: Created template at {path}");
    }

    public static void Import(string tablePath)
    {
        if (string.IsNullOrWhiteSpace(tablePath) || !File.Exists(tablePath))
        {
            Debug.LogError($"{nameof(AddressablesLabelTableImporter)}: File not found: {tablePath}");
            return;
        }

        var settings = AddressableAssetSettingsDefaultObject.Settings;
        if (settings == null)
        {
            Debug.LogError($"{nameof(AddressablesLabelTableImporter)}: Addressables settings not found.");
            return;
        }

        var table = ReadTable(tablePath);
        if (table.Count <= 1)
        {
            Debug.LogWarning($"{nameof(AddressablesLabelTableImporter)}: Table has no data rows: {tablePath}");
            return;
        }

        var header = HeaderMap.From(table[0]);
        if (header.AssetPath < 0 || header.Labels < 0)
        {
            Debug.LogError($"{nameof(AddressablesLabelTableImporter)}: Required columns are missing. Need assetPath and labels.");
            return;
        }

        var report = new ImportReport();

        for (var rowIndex = 1; rowIndex < table.Count; rowIndex++)
        {
            var row = table[rowIndex];
            if (row.Count == 0 || row.All(string.IsNullOrWhiteSpace)) continue;

            ImportRow(settings, header, row, rowIndex + 1, report);
        }

        settings.SetDirty(AddressableAssetSettings.ModificationEvent.BatchModification, null, true, true);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log(
            $"{nameof(AddressablesLabelTableImporter)}: Imported {report.Imported} rows, skipped {report.Skipped}, " +
            $"created/moved {report.CreatedOrMovedEntries} entries, assigned {report.AssignedLabels} labels from {tablePath}"
        );
    }

    private static void ImportRow(AddressableAssetSettings settings, HeaderMap header, IReadOnlyList<string> row, int rowNumber, ImportReport report)
    {
        var assetPath = NormalizeAssetPath(GetCell(row, header.AssetPath));
        if (string.IsNullOrEmpty(assetPath))
        {
            report.Skipped++;
            return;
        }

        var guid = AssetDatabase.AssetPathToGUID(assetPath);
        if (string.IsNullOrEmpty(guid))
        {
            report.Skipped++;
            Debug.LogWarning($"{nameof(AddressablesLabelTableImporter)}: Row {rowNumber} asset not found: {assetPath}");
            return;
        }

        var group = ResolveGroup(settings, GetCell(row, header.Group), guid);
        if (group == null)
        {
            report.Skipped++;
            Debug.LogWarning($"{nameof(AddressablesLabelTableImporter)}: Row {rowNumber} has no usable Addressables group: {assetPath}");
            return;
        }

        var entry = settings.FindAssetEntry(guid);
        var shouldCreateOrMove = entry == null || entry.parentGroup != group;
        entry = settings.CreateOrMoveEntry(guid, group, false, false);
        if (shouldCreateOrMove) report.CreatedOrMovedEntries++;

        var address = GetCell(row, header.Address);
        if (!string.IsNullOrWhiteSpace(address))
        {
            entry.address = address.Trim();
        }

        if (ParseBool(GetCell(row, header.ClearLabels)))
        {
            foreach (var label in entry.labels.ToArray())
            {
                entry.SetLabel(label, false, false, false);
            }
        }

        foreach (var label in SplitLabels(GetCell(row, header.Labels)))
        {
            settings.AddLabel(label, false);
            if (entry.SetLabel(label, true, false, false))
            {
                report.AssignedLabels++;
            }
        }

        report.Imported++;
    }

    private static AddressableAssetGroup ResolveGroup(AddressableAssetSettings settings, string groupName, string guid)
    {
        groupName = groupName?.Trim();
        if (!string.IsNullOrEmpty(groupName))
        {
            return settings.FindGroup(groupName);
        }

        var existingEntry = settings.FindAssetEntry(guid);
        if (existingEntry?.parentGroup != null)
        {
            return existingEntry.parentGroup;
        }

        return settings.DefaultGroup;
    }

    private static List<List<string>> ReadTable(string tablePath)
    {
        var extension = Path.GetExtension(tablePath).ToLowerInvariant();
        if (extension == ".csv")
        {
            return ReadCsv(tablePath);
        }

        if (extension == ".xlsx")
        {
            return ReadXlsxFirstSheet(tablePath);
        }

        throw new NotSupportedException($"Unsupported table extension: {extension}");
    }

    private static List<List<string>> ReadCsv(string csvPath)
    {
        var rows = new List<List<string>>();
        using var stream = new FileStream(csvPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
        using var reader = new StreamReader(stream, DetectEncoding(stream), detectEncodingFromByteOrderMarks: true);

        while (!reader.EndOfStream)
        {
            rows.Add(ParseCsvLine(reader.ReadLine() ?? string.Empty));
        }

        return rows;
    }

    private static List<List<string>> ReadXlsxFirstSheet(string xlsxPath)
    {
        using var stream = new FileStream(xlsxPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
        using var archive = new ZipArchive(stream, ZipArchiveMode.Read);

        var sharedStrings = ReadSharedStrings(archive);
        var sheetPath = GetFirstWorksheetPath(archive);
        var sheetEntry = archive.GetEntry(sheetPath);
        if (sheetEntry == null)
        {
            throw new FileNotFoundException($"Worksheet not found in xlsx: {sheetPath}");
        }

        using var sheetStream = sheetEntry.Open();
        var document = XDocument.Load(sheetStream);
        XNamespace main = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
        var rows = new List<List<string>>();

        foreach (var row in document.Descendants(main + "row"))
        {
            var values = new List<string>();
            foreach (var cell in row.Elements(main + "c"))
            {
                var columnIndex = GetColumnIndex((string)cell.Attribute("r"));
                while (values.Count < columnIndex)
                {
                    values.Add(string.Empty);
                }

                values.Add(ReadCellValue(cell, sharedStrings, main));
            }

            rows.Add(values);
        }

        return rows;
    }

    private static string GetFirstWorksheetPath(ZipArchive archive)
    {
        var workbookEntry = archive.GetEntry("xl/workbook.xml");
        var relsEntry = archive.GetEntry("xl/_rels/workbook.xml.rels");
        if (workbookEntry == null || relsEntry == null)
        {
            throw new InvalidDataException("Invalid xlsx file: workbook metadata is missing.");
        }

        XNamespace main = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
        XNamespace relNs = "http://schemas.openxmlformats.org/package/2006/relationships";
        XNamespace officeRelNs = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";

        using var workbookStream = workbookEntry.Open();
        var workbook = XDocument.Load(workbookStream);
        var firstSheet = workbook.Descendants(main + "sheet").FirstOrDefault();
        var relationshipId = (string)firstSheet?.Attribute(officeRelNs + "id");
        if (string.IsNullOrEmpty(relationshipId))
        {
            throw new InvalidDataException("Invalid xlsx file: no worksheet found.");
        }

        using var relsStream = relsEntry.Open();
        var rels = XDocument.Load(relsStream);
        var target = rels.Descendants(relNs + "Relationship")
            .Where(node => (string)node.Attribute("Id") == relationshipId)
            .Select(node => (string)node.Attribute("Target"))
            .FirstOrDefault();

        if (string.IsNullOrEmpty(target))
        {
            throw new InvalidDataException($"Invalid xlsx file: missing relationship {relationshipId}.");
        }

        target = target.Replace('\\', '/').TrimStart('/');
        return target.StartsWith("xl/", StringComparison.OrdinalIgnoreCase) ? target : "xl/" + target;
    }

    private static List<string> ReadSharedStrings(ZipArchive archive)
    {
        var entry = archive.GetEntry("xl/sharedStrings.xml");
        var strings = new List<string>();
        if (entry == null) return strings;

        XNamespace main = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
        using var stream = entry.Open();
        var document = XDocument.Load(stream);

        foreach (var item in document.Descendants(main + "si"))
        {
            strings.Add(string.Concat(item.Descendants(main + "t").Select(node => node.Value)));
        }

        return strings;
    }

    private static string ReadCellValue(XElement cell, IReadOnlyList<string> sharedStrings, XNamespace main)
    {
        var type = (string)cell.Attribute("t");
        if (type == "inlineStr")
        {
            return string.Concat(cell.Descendants(main + "t").Select(node => node.Value)).Trim();
        }

        var rawValue = cell.Element(main + "v")?.Value ?? string.Empty;
        if (type == "s" && int.TryParse(rawValue, out var sharedStringIndex))
        {
            return sharedStringIndex >= 0 && sharedStringIndex < sharedStrings.Count
                ? sharedStrings[sharedStringIndex].Trim()
                : string.Empty;
        }

        return rawValue.Trim();
    }

    private static int GetColumnIndex(string cellReference)
    {
        if (string.IsNullOrEmpty(cellReference)) return 0;

        var index = 0;
        foreach (var c in cellReference)
        {
            if (!char.IsLetter(c)) break;
            index = index * 26 + char.ToUpperInvariant(c) - 'A' + 1;
        }

        return Mathf.Max(0, index - 1);
    }

    private static List<string> ParseCsvLine(string line)
    {
        var result = new List<string>();
        var value = new StringBuilder();
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

    private static Encoding DetectEncoding(FileStream stream)
    {
        var buffer = new byte[Mathf.Min(4096, (int)stream.Length)];
        var bytesRead = stream.Read(buffer, 0, buffer.Length);
        stream.Position = 0;

        if (bytesRead >= 3 && buffer[0] == 0xEF && buffer[1] == 0xBB && buffer[2] == 0xBF) return Encoding.UTF8;
        if (bytesRead >= 2 && buffer[0] == 0xFF && buffer[1] == 0xFE) return Encoding.Unicode;
        if (bytesRead >= 2 && buffer[0] == 0xFE && buffer[1] == 0xFF) return Encoding.BigEndianUnicode;

        var strictUtf8 = new UTF8Encoding(false, true);
        try
        {
            strictUtf8.GetString(buffer, 0, bytesRead);
            return Encoding.UTF8;
        }
        catch (DecoderFallbackException)
        {
            try
            {
                return Encoding.GetEncoding("GB18030");
            }
            catch
            {
                return Encoding.Default;
            }
        }
    }

    private static string NormalizeAssetPath(string path)
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

    private static IEnumerable<string> SplitLabels(string labels)
    {
        if (string.IsNullOrWhiteSpace(labels)) yield break;

        var separators = new[] { ';', ',', '|', '\n', '\r', '，', '；' };
        foreach (var label in labels.Split(separators, StringSplitOptions.RemoveEmptyEntries))
        {
            var normalized = label.Trim();
            if (!string.IsNullOrEmpty(normalized))
            {
                yield return normalized;
            }
        }
    }

    private static bool ParseBool(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return false;

        value = value.Trim().ToLowerInvariant();
        return value == "1" || value == "true" || value == "yes" || value == "y";
    }

    private static string GetCell(IReadOnlyList<string> row, int index)
    {
        return index >= 0 && index < row.Count ? row[index] : string.Empty;
    }

    private sealed class HeaderMap
    {
        public int AssetPath { get; private set; } = -1;
        public int Address { get; private set; } = -1;
        public int Group { get; private set; } = -1;
        public int Labels { get; private set; } = -1;
        public int ClearLabels { get; private set; } = -1;

        public static HeaderMap From(IReadOnlyList<string> headers)
        {
            var map = new HeaderMap();
            for (var i = 0; i < headers.Count; i++)
            {
                var header = NormalizeHeader(headers[i]);
                if (IsAny(header, "assetpath", "path", "asset", "resourcepath", "资源路径", "资产路径"))
                {
                    map.AssetPath = i;
                }
                else if (IsAny(header, "address", "addr", "key", "地址"))
                {
                    map.Address = i;
                }
                else if (IsAny(header, "group", "groupname", "包名", "分组"))
                {
                    map.Group = i;
                }
                else if (IsAny(header, "labels", "label", "lables", "addressableslabels", "标签"))
                {
                    map.Labels = i;
                }
                else if (IsAny(header, "clearlabels", "replacelabels", "清空标签", "替换标签"))
                {
                    map.ClearLabels = i;
                }
            }

            return map;
        }

        private static string NormalizeHeader(string header)
        {
            if (string.IsNullOrWhiteSpace(header)) return string.Empty;

            return new string(header.Trim().ToLowerInvariant()
                .Where(c => !char.IsWhiteSpace(c) && c != '_' && c != '-')
                .ToArray());
        }

        private static bool IsAny(string value, params string[] names)
        {
            return names.Contains(value);
        }
    }

    private sealed class ImportReport
    {
        public int Imported;
        public int Skipped;
        public int CreatedOrMovedEntries;
        public int AssignedLabels;
    }
}
