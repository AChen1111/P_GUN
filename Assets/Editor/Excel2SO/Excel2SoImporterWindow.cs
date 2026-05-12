using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public sealed class Excel2SoImporterWindow : EditorWindow
{
    private const string MenuPath = "Tools/Excel2SO/Importer Window";
    private const string LayoutPath = "Assets/Editor/Excel2SO/Excel2SoImporterWindow.uxml";
    private const string StylePath = "Assets/Editor/Excel2SO/Excel2SoImporterWindow.uss";

    private readonly List<ImporterOption> importerOptions = new List<ImporterOption>();

    private PopupField<ImporterOption> importerField;
    private VisualElement formRoot;
    private TextField tablePathField;
    private TextField targetPathField;
    private Button importButton;
    private Label statusLabel;
    private ImporterOption selectedImporter;

    [MenuItem(MenuPath)]
    public static void Open()
    {
        var window = GetWindow<Excel2SoImporterWindow>();
        window.titleContent = new GUIContent("Excel2SO Importer");
        window.minSize = new Vector2(520f, 280f);
        window.Show();
    }

    public void CreateGUI()
    {
        importerOptions.Clear();
        importerOptions.AddRange(DiscoverImporters());

        var root = rootVisualElement;
        root.Clear();
        LoadWindowLayout(root);

        if (!TryBindLayout(root, out var importerContainer, out var tableBrowseButton, out var targetBrowseButton, out var targetDefaultButton))
        {
            return;
        }

        if (importerOptions.Count == 0)
        {
            formRoot.SetEnabled(false);
            importButton.SetEnabled(false);
            ShowStatus("No list asset importers were found.");
            return;
        }

        selectedImporter = importerOptions[0];

        importerField = new PopupField<ImporterOption>(string.Empty, importerOptions, selectedImporter);
        importerField.AddToClassList("importer-popup");
        importerField.formatListItemCallback = FormatImporterOption;
        importerField.formatSelectedValueCallback = FormatImporterOption;
        importerField.RegisterValueChangedCallback(evt =>
        {
            selectedImporter = evt.newValue;
            ResetTargetPath();
            UpdateImportState();
        });
        importerContainer.Add(importerField);

        tablePathField.RegisterValueChangedCallback(_ => UpdateImportState());
        targetPathField.value = selectedImporter.CreateImporter().DefaultTargetAssetPath;
        targetPathField.RegisterValueChangedCallback(_ => UpdateImportState());

        tableBrowseButton.clicked += SelectTablePath;
        targetBrowseButton.clicked += SelectTargetPath;
        targetDefaultButton.clicked += ResetTargetPath;
        importButton.clicked += ImportSelected;

        UpdateImportState();
    }

    private static void LoadWindowLayout(VisualElement root)
    {
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(LayoutPath);
        if (visualTree == null)
        {
            root.Add(new Label($"Missing Excel2SO importer layout: {LayoutPath}"));
            return;
        }

        visualTree.CloneTree(root);

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(StylePath);
        if (styleSheet != null)
        {
            root.styleSheets.Add(styleSheet);
        }
    }

    private bool TryBindLayout(
        VisualElement root,
        out VisualElement importerContainer,
        out Button tableBrowseButton,
        out Button targetBrowseButton,
        out Button targetDefaultButton)
    {
        formRoot = root.Q<VisualElement>("form-root");
        importerContainer = root.Q<VisualElement>("importer-container");
        tablePathField = root.Q<TextField>("table-path-field");
        targetPathField = root.Q<TextField>("target-path-field");
        tableBrowseButton = root.Q<Button>("table-browse-button");
        targetBrowseButton = root.Q<Button>("target-browse-button");
        targetDefaultButton = root.Q<Button>("target-default-button");
        importButton = root.Q<Button>("import-button");
        statusLabel = root.Q<Label>("status-label");

        if (formRoot != null
            && importerContainer != null
            && tablePathField != null
            && targetPathField != null
            && tableBrowseButton != null
            && targetBrowseButton != null
            && targetDefaultButton != null
            && importButton != null
            && statusLabel != null)
        {
            return true;
        }

        root.Clear();
        statusLabel = new Label("Excel2SO importer layout is missing required controls.");
        root.Add(statusLabel);
        return false;
    }

    private static IEnumerable<ImporterOption> DiscoverImporters()
    {
        return TypeCache.GetTypesDerivedFrom<Excel2SoImporterBase>()
            .Where(IsSupportedImporterType)
            .OrderBy(type => type.Name, StringComparer.OrdinalIgnoreCase)
            .Select(type => new ImporterOption(type));
    }

    private static bool IsSupportedImporterType(Type type)
    {
        return type != null
               && !type.IsAbstract
               && !type.ContainsGenericParameters
               && typeof(IExcel2SoListAssetImporter).IsAssignableFrom(type)
               && type.GetConstructor(Type.EmptyTypes) != null;
    }

    private static string FormatImporterOption(ImporterOption option)
    {
        return option == null ? string.Empty : option.DisplayName;
    }

    private void SelectTablePath()
    {
        var directory = GetExistingDirectory(tablePathField.value, Application.dataPath);
        var path = EditorUtility.OpenFilePanel("Select Excel2SO Table", directory, "xlsx,csv");
        if (!string.IsNullOrEmpty(path))
        {
            tablePathField.value = path;
        }
    }

    private void SelectTargetPath()
    {
        var targetPath = NormalizeAssetPath(targetPathField.value);
        var defaultName = Path.GetFileNameWithoutExtension(targetPath);
        if (string.IsNullOrWhiteSpace(defaultName))
        {
            defaultName = selectedImporter == null ? "ImportedData" : selectedImporter.Type.Name;
        }

        var path = EditorUtility.SaveFilePanelInProject(
            "Select Target Asset",
            defaultName,
            "asset",
            "Choose where to save the imported ScriptableObject asset."
        );

        if (!string.IsNullOrEmpty(path))
        {
            targetPathField.value = path;
        }
    }

    private void ResetTargetPath()
    {
        if (selectedImporter == null || targetPathField == null)
        {
            return;
        }

        targetPathField.value = selectedImporter.CreateImporter().DefaultTargetAssetPath;
    }

    private void ImportSelected()
    {
        if (!TryGetValidatedInput(out var tablePath, out var targetPath, out var validationMessage))
        {
            ShowStatus(validationMessage);
            return;
        }

        try
        {
            var importer = selectedImporter.CreateImporter();
            var report = importer.Import(tablePath, targetPath);
            ShowReport(report);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            ShowStatus($"Import failed: {ex.Message}");
        }
    }

    private void UpdateImportState()
    {
        if (importButton == null)
        {
            return;
        }

        var isValid = TryGetValidatedInput(out _, out _, out var message);
        importButton.SetEnabled(isValid);
        ShowStatus(isValid ? "Ready to import." : message);
    }

    private bool TryGetValidatedInput(out string tablePath, out string targetPath, out string message)
    {
        tablePath = string.Empty;
        targetPath = string.Empty;
        message = string.Empty;

        if (selectedImporter == null)
        {
            message = "Select an importer.";
            return false;
        }

        var rawTablePath = NormalizePath(tablePathField == null ? string.Empty : tablePathField.value);
        if (string.IsNullOrWhiteSpace(rawTablePath))
        {
            message = "Choose a .xlsx or .csv table file.";
            return false;
        }

        var resolvedTablePath = ResolveProjectPath(rawTablePath);
        if (!File.Exists(resolvedTablePath))
        {
            message = $"Table file does not exist: {rawTablePath}";
            return false;
        }

        var extension = Path.GetExtension(resolvedTablePath);
        if (!string.Equals(extension, ".xlsx", StringComparison.OrdinalIgnoreCase)
            && !string.Equals(extension, ".csv", StringComparison.OrdinalIgnoreCase))
        {
            message = "Table file must be .xlsx or .csv.";
            return false;
        }

        var rawTargetPath = NormalizeAssetPath(targetPathField == null ? string.Empty : targetPathField.value);
        if (string.IsNullOrWhiteSpace(rawTargetPath))
        {
            message = "Choose a target .asset path.";
            return false;
        }

        if (!rawTargetPath.StartsWith("Assets/", StringComparison.OrdinalIgnoreCase))
        {
            message = $"Target asset path must be under Assets/: {rawTargetPath}";
            return false;
        }

        if (!rawTargetPath.EndsWith(".asset", StringComparison.OrdinalIgnoreCase))
        {
            message = "Target asset path must end with .asset.";
            return false;
        }

        tablePath = resolvedTablePath;
        targetPath = rawTargetPath;
        return true;
    }

    private void ShowReport(Excel2SoImportReport report)
    {
        if (report == null)
        {
            ShowStatus("Import finished without a report.");
            return;
        }

        ShowStatus(
            $"{report}\n" +
            $"Table: {report.TablePath}\n" +
            $"Target: {report.TargetPath}");
    }

    private void ShowStatus(string message)
    {
        if (statusLabel != null)
        {
            statusLabel.text = message;
        }
    }

    private static string GetExistingDirectory(string path, string fallback)
    {
        var normalizedPath = ResolveProjectPath(NormalizePath(path));
        if (File.Exists(normalizedPath))
        {
            return Path.GetDirectoryName(normalizedPath);
        }

        if (Directory.Exists(normalizedPath))
        {
            return normalizedPath;
        }

        return fallback;
    }

    private static string ResolveProjectPath(string path)
    {
        path = NormalizePath(path);
        if (string.IsNullOrEmpty(path))
        {
            return string.Empty;
        }

        if (path.StartsWith("Assets/", StringComparison.OrdinalIgnoreCase))
        {
            var projectRoot = Directory.GetParent(Application.dataPath)?.FullName;
            return string.IsNullOrEmpty(projectRoot)
                ? path
                : NormalizePath(Path.Combine(projectRoot, path));
        }

        return path;
    }

    private static string NormalizeAssetPath(string path)
    {
        path = NormalizePath(path);
        if (string.IsNullOrEmpty(path))
        {
            return string.Empty;
        }

        if (path.StartsWith("Assets/", StringComparison.OrdinalIgnoreCase))
        {
            return path;
        }

        if (Path.IsPathRooted(path))
        {
            var dataPath = NormalizePath(Application.dataPath);
            if (path.Equals(dataPath, StringComparison.OrdinalIgnoreCase))
            {
                return "Assets";
            }

            if (path.StartsWith(dataPath + "/", StringComparison.OrdinalIgnoreCase))
            {
                return "Assets" + path.Substring(dataPath.Length);
            }
        }

        return path;
    }

    private static string NormalizePath(string path)
    {
        return string.IsNullOrWhiteSpace(path) ? string.Empty : path.Trim().Replace('\\', '/');
    }

    private sealed class ImporterOption
    {
        public ImporterOption(Type type)
        {
            Type = type;
            DisplayName = string.IsNullOrEmpty(type.Namespace) ? type.Name : type.FullName;
        }

        public Type Type { get; }

        public string DisplayName { get; }

        public IExcel2SoListAssetImporter CreateImporter()
        {
            return (IExcel2SoListAssetImporter)Activator.CreateInstance(Type);
        }
    }
}
