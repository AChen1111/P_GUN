using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Edgar.Unity;
using Game.Core;
using Game.Gameplay;
using Game.Items;
using Game.UI;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

/// <summary>
/// 分类目录注册, 一次性引用迁移和阶段依赖生成, 只通过 Unity API 修改资源.
/// </summary>
[InitializeOnLoad]
public static class AddressableCatalogSetup
{
    public const string CatalogFolder = "Assets/GameDataSO/AddressableCatalogs";
    private const string ManifestFolder = "Assets/GameDataSO/AddressablePreload";
    private static readonly Dictionary<AddressableAssetKind, AddressableCatalog> Catalogs = new Dictionary<AddressableAssetKind, AddressableCatalog>();
    private static readonly Dictionary<string, string> ExplicitAliases = new Dictionary<string, string>
    {
        { "Assets/Prefab/UI/PlayerHeart/Heart.prefab", "PlayerHeart" },
        { "Packages/com.unity.render-pipelines.universal/Shaders/2D/Sprite-Lit-Default.shader", "SpriteLitDefaultShader" },
        { "Packages/com.unity.render-pipelines.universal/Runtime/Materials/Sprite-Lit-Default.mat", "SpriteLitDefaultMaterial" }
    };
    private static readonly Dictionary<string, string> OldAddresses = new Dictionary<string, string>();
    private static AddressableAssetSettings Settings => AddressableAssetSettingsDefaultObject.Settings ?? throw new InvalidOperationException("Addressables Settings is missing.");

    static AddressableCatalogSetup() { AddressableAssetAccess.EditorResolver = ResolveEditor; }

    private static AddressableCatalog Catalog(AddressableAssetKind kind)
    {
        if (Catalogs.TryGetValue(kind, out var catalog) && catalog != null) return catalog;
        catalog = AssetDatabase.LoadAssetAtPath<AddressableCatalog>($"{CatalogFolder}/{kind}Catalog.asset");
        if (catalog == null) throw new InvalidOperationException($"Missing {kind} Catalog. Run the migration/setup command.");
        Catalogs[kind] = catalog;
        return catalog;
    }

    public static Object ResolveEditor(Type type, string key)
    {
        var entry = Catalog(AddressableCatalog.KindFor(type)).Get(key);
        return ResolveReference(entry, type);
    }

    private static Object ResolveReference(AddressableCatalogEntry entry, Type type)
    {
        var path = AssetDatabase.GUIDToAssetPath(entry.reference.AssetGUID);
        if (!string.IsNullOrEmpty(entry.reference.SubObjectName))
            return AssetDatabase.LoadAllAssetsAtPath(path).First(asset => type.IsInstanceOfType(asset) && asset.name == entry.reference.SubObjectName);
        return AssetDatabase.LoadAssetAtPath(path, type) ?? throw new InvalidOperationException($"Catalog {entry.key} has no {type.Name}: {path}.");
    }

    private static void EnsureFolder(string path)
    {
        if (AssetDatabase.IsValidFolder(path)) return;
        var parent = Path.GetDirectoryName(path).Replace('\\', '/');
        EnsureFolder(parent);
        AssetDatabase.CreateFolder(parent, Path.GetFileName(path));
    }

    private static AddressableAssetGroup Group(string name)
    {
        var group = Settings.FindGroup(name);
        if (group != null) return group;
        group = Settings.CreateGroup(name, false, false, false, null, typeof(BundledAssetGroupSchema), typeof(ContentUpdateGroupSchema));
        var bundled = group.GetSchema<BundledAssetGroupSchema>();
        bundled.BuildPath.SetVariableByName(Settings, AddressableAssetSettings.kLocalBuildPath);
        bundled.LoadPath.SetVariableByName(Settings, AddressableAssetSettings.kLocalLoadPath);
        bundled.IncludeInBuild = true;
        bundled.BundleMode = name == "Scene" ? BundledAssetGroupSchema.BundlePackingMode.PackSeparately : BundledAssetGroupSchema.BundlePackingMode.PackTogether;
        group.GetSchema<ContentUpdateGroupSchema>().StaticContent = true;
        EditorUtility.SetDirty(bundled);
        EditorUtility.SetDirty(group.GetSchema<ContentUpdateGroupSchema>());
        return group;
    }

    private static string GroupFor(string path)
    {
        if (path.StartsWith(CatalogFolder, StringComparison.Ordinal) || path.StartsWith(ManifestFolder, StringComparison.Ordinal)) return "Catalog";
        if (path.StartsWith("Assets/Scenes/", StringComparison.Ordinal)) return "Scene";
        if (path.Contains("/UI/")) return "UI";
        if (path.Contains("/Room/")) return "Room";
        if (path.Contains("/Buff") || path.Contains("/BuffDataBase")) return "Buff";
        if (path.Contains("/Enemy") || path.Contains("/EnemyDatabase")) return "Enemy";
        if (path.Contains("/GunList/") || path.Contains("/WeaponDatabase")) return "Weapon";
        if (path.Contains("/Hotfix/")) return "Hotfix";
        if (path.Contains("/Item") || path.Contains("/ItemDatabase")) return "Item";
        return "Shared";
    }

    private static AddressableAssetEntry Mark(string path, string groupName = null)
    {
        var guid = AssetDatabase.AssetPathToGUID(path);
        var entry = Settings.FindAssetEntry(guid);
        if (entry == null) entry = Settings.CreateOrMoveEntry(guid, Group(groupName ?? GroupFor(path)), false, false);
        Settings.AddLabel("hot_update", false);
        entry.SetLabel("hot_update", true, false, false);
        return entry;
    }

    public static string Register(Object asset, AddressableAssetKind? requestedKind = null)
    {
        if (asset == null) return string.Empty;
        if (asset is Component component) asset = component.gameObject;
        var path = AssetDatabase.GetAssetPath(asset);
        if (string.IsNullOrEmpty(path)) throw new InvalidOperationException($"Scene template {asset.name} must be extracted before registration.");
        var kind = requestedKind ?? AddressableCatalog.KindFor(asset.GetType());
        var guid = AssetDatabase.AssetPathToGUID(path);
        var subName = asset is Sprite ? asset.name : string.Empty;
        var catalog = Catalog(kind);
        var previous = catalog.entries.FirstOrDefault(e => e.reference.AssetGUID == guid && (e.reference.SubObjectName ?? string.Empty) == subName);
        if (previous != null) return previous.key;
        var key = ExplicitAliases.TryGetValue(path, out var alias) ? alias : asset is Sprite ? asset.name : Path.GetFileNameWithoutExtension(path);
        if (key.EndsWith(".lua", StringComparison.Ordinal)) key = key.Substring(0, key.Length - 4);
        var conflict = catalog.entries.FirstOrDefault(e => e.key == key);
        if (conflict != null) throw new InvalidOperationException($"Duplicate {kind} key '{key}': {path} and {AssetDatabase.GUIDToAssetPath(conflict.reference.AssetGUID)}. Configure an explicit unique alias.");
        var addressable = Mark(path);
        var reference = asset is Sprite ? (AssetReference)new AssetReferenceSprite(guid) { SubObjectName = subName } : new AssetReference(guid);
        catalog.entries.Add(new AddressableCatalogEntry
        {
            key = key,
            reference = reference,
            labels = addressable.labels.ToList(),
            luaModulePath = kind == AddressableAssetKind.TextAsset && addressable.labels.Contains("hotfix") ? addressable.address : string.Empty
        });
        OldAddresses[addressable.address] = key;
        catalog.BuildMap();
        EditorUtility.SetDirty(catalog);
        return key;
    }

    [MenuItem("Assets/Addressables/Register Selected In Catalog", false, 2000)]
    public static void RegisterSelected()
    {
        foreach (var selected in Selection.objects)
        {
            var path = AssetDatabase.GetAssetPath(selected);
            var paths = AssetDatabase.IsValidFolder(path) ? AssetDatabase.FindAssets("", new[] { path }).Select(AssetDatabase.GUIDToAssetPath) : new[] { path };
            foreach (var assetPath in paths)
            {
                if (AssetDatabase.IsValidFolder(assetPath)) continue;
                var sprites = AssetDatabase.LoadAllAssetsAtPath(assetPath).OfType<Sprite>().ToArray();
                if (sprites.Length > 0) foreach (var sprite in sprites) Register(sprite);
                else Register(AssetDatabase.LoadMainAssetAtPath(assetPath), assetPath.EndsWith(".unity") ? AddressableAssetKind.Scene : (AddressableAssetKind?)null);
            }
        }
        RebuildManifests();
        SyncAddressKeys();
        AssetDatabase.SaveAssets();
    }

    [MenuItem("Tools/UnityEasyWorkTools/Addressables/Migrate To Short Keys")]
    public static void Migrate()
    {
        ConfigureCatalogVersion();
        EnsureFolder(CatalogFolder);
        EnsureFolder(ManifestFolder);
        foreach (AddressableAssetKind kind in Enum.GetValues(typeof(AddressableAssetKind)))
        {
            var path = $"{CatalogFolder}/{kind}Catalog.asset";
            var catalog = AssetDatabase.LoadAssetAtPath<AddressableCatalog>(path);
            if (catalog == null) { catalog = ScriptableObject.CreateInstance<AddressableCatalog>(); catalog.kind = kind; AssetDatabase.CreateAsset(catalog, path); }
            Catalogs[kind] = catalog;
            Mark(path, "Catalog");
        }
        // 在任何资源重写前保留数据库内容与旧引用, 迁移可重复运行但不能覆盖已有 key.
        Directory.CreateDirectory("Library/AddressableKeyMigration");
        foreach (var guid in AssetDatabase.FindAssets("t:ScriptableObject", new[] { "Assets/GameDataSO" }))
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadMainAssetAtPath(path);
            var snapshot = $"Library/AddressableKeyMigration/{guid}.json";
            if (!File.Exists(snapshot)) File.WriteAllText(snapshot, EditorJsonUtility.ToJson(asset, true));
        }
        var knownEntries = Settings.groups.Where(g => g != null).SelectMany(g => g.entries).ToArray();
        foreach (var entry in knownEntries)
        {
            if (entry.AssetPath.StartsWith(CatalogFolder, StringComparison.Ordinal) || entry.AssetPath.StartsWith(ManifestFolder, StringComparison.Ordinal) || AssetDatabase.IsValidFolder(entry.AssetPath)) continue;
            var main = AssetDatabase.LoadMainAssetAtPath(entry.AssetPath);
            if (main == null || main is LevelGraph) continue;
            var sprites = AssetDatabase.LoadAllAssetsAtPath(entry.AssetPath).OfType<Sprite>().ToArray();
            if (sprites.Length > 0) foreach (var sprite in sprites) Register(sprite);
            else if (!(main is SceneAsset)) Register(main);
        }
        MigrateGraphs();
        foreach (var guid in AssetDatabase.FindAssets("t:ScriptableObject", new[] { "Assets/GameDataSO" }))
            foreach (var asset in AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GUIDToAssetPath(guid)).OfType<ScriptableObject>()) MigrateObject(asset);
        foreach (var guid in AssetDatabase.FindAssets("t:Prefab", new[] { "Assets/Prefab" }))
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var prefab = PrefabUtility.LoadPrefabContents(path);
            try { MigrateHierarchy(prefab); PrefabUtility.SaveAsPrefabAsset(prefab, path); }
            finally { PrefabUtility.UnloadPrefabContents(prefab); }
        }
        var originalScenes = EditorSceneManager.GetSceneManagerSetup();
        try
        {
            foreach (var sceneName in new[] { "Root", "StartScene", "GameScene" })
            {
                var scene = EditorSceneManager.OpenScene($"Assets/Scenes/{sceneName}.unity", OpenSceneMode.Single);
                foreach (var obj in scene.GetRootGameObjects()) MigrateHierarchy(obj);
                if (sceneName == "Root") BindRoot(scene);
                else AddLoadingView(scene);
                EditorSceneManager.SaveScene(scene);
                if (sceneName != "Root")
                {
                    var key = Register(AssetDatabase.LoadAssetAtPath<SceneAsset>(scene.path), AddressableAssetKind.Scene);
                    Catalog(AddressableAssetKind.Scene).Get(key).preloadManifestKey = sceneName + "Preload";
                }
            }
        }
        finally { RestoreScenes(originalScenes); }
        EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene("Assets/Scenes/Root.unity", true) };
        foreach (var kind in new[] { "Root", "StartScene", "GameScene" })
        {
            var path = $"{ManifestFolder}/{kind}Preload.asset";
            var manifest = AssetDatabase.LoadAssetAtPath<AddressablePreloadManifest>(path);
            if (manifest == null) { manifest = ScriptableObject.CreateInstance<AddressablePreloadManifest>(); AssetDatabase.CreateAsset(manifest, path); }
            Register(manifest);
        }
        RewriteCsvKeys();
        RebuildManifests();
        SyncAddressKeys();
        AssetDatabase.SaveAssets();
        Validate();
        Debug.Log("ADDRESSABLE_KEY_MIGRATION_SUCCESS");
    }

    public static void ConfigureCatalogVersion()
    {
        // 短名配置与旧引用配置不兼容, 新首包使用独立 Catalog 名称隔离旧目录缓存.
        const string suffix = "-keys-v2";
        if (!Settings.OverridePlayerVersion.EndsWith(suffix, StringComparison.Ordinal))
        {
            Settings.OverridePlayerVersion += suffix;
            EditorUtility.SetDirty(Settings);
            AssetDatabase.SaveAssets();
        }
    }

    private static FieldInfo Field(Type type, string name)
    {
        while (type != null)
        {
            var field = type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            if (field != null) return field;
            type = type.BaseType;
        }
        return null;
    }

    private static FieldInfo PropertyField(Type type, string path)
    {
        var parts = path.Split('.');
        FieldInfo result = null;
        for (var i = 0; i < parts.Length; i++)
        {
            if (parts[i] == "Array")
            {
                if (!type.IsArray && !type.IsGenericType) return null;
                type = type.IsArray ? type.GetElementType() : type.GetGenericArguments()[0];
                i++;
                continue;
            }
            result = Field(type, parts[i]);
            if (result == null) return null;
            type = result.FieldType;
        }
        return result;
    }

    private static IEnumerable<(SerializedProperty property, AddressableKeyAttribute attribute)> KeyProperties(SerializedObject serialized)
    {
        var iterator = serialized.GetIterator();
        while (iterator.Next(true))
        {
            if (iterator.propertyPath.Contains(".Array."))
            {
                var leaf = iterator.propertyPath.Substring(iterator.propertyPath.LastIndexOf('.') + 1);
                if (leaf.StartsWith("data[", StringComparison.Ordinal) || leaf == "size") continue;
            }
            var attribute = PropertyField(serialized.targetObject.GetType(), iterator.propertyPath)?.GetCustomAttribute<AddressableKeyAttribute>();
            if (attribute != null) yield return (iterator.Copy(), attribute);
        }
    }

    private static Object ExtractTemplate(Object reference, string owner)
    {
        if (!string.IsNullOrEmpty(AssetDatabase.GetAssetPath(reference))) return reference;
        var go = reference is Component component ? component.gameObject : reference as GameObject;
        if (go == null) throw new InvalidOperationException("Only GameObject templates may be scene references.");
        EnsureFolder("Assets/Prefab/UI/GeneratedTemplates");
        var path = $"Assets/Prefab/UI/GeneratedTemplates/{owner}_{go.name}.prefab";
        var clone = Object.Instantiate(go);
        try { return PrefabUtility.SaveAsPrefabAsset(clone, path); }
        finally { Object.DestroyImmediate(clone); }
    }

    private static void MigrateObject(Object target)
    {
        if (target == null || !target.GetType().Assembly.GetName().Name.StartsWith("Game.", StringComparison.Ordinal)) return;
        var serialized = new SerializedObject(target);
        foreach (var pair in KeyProperties(serialized).ToArray())
        {
            var key = pair.property;
            if (string.IsNullOrEmpty(pair.attribute.LegacyField))
            {
                if (key.propertyType == SerializedPropertyType.String && OldAddresses.TryGetValue(key.stringValue, out var shortKey)) key.stringValue = shortKey;
                continue;
            }
            var dot = key.propertyPath.LastIndexOf('.');
            var legacy = serialized.FindProperty((dot < 0 ? "" : key.propertyPath.Substring(0, dot + 1)) + pair.attribute.LegacyField);
            if (legacy == null) continue;
            if (key.isArray && key.propertyType != SerializedPropertyType.String)
            {
                if (legacy.arraySize == 0) continue;
                key.arraySize = legacy.arraySize;
                for (var i = 0; i < legacy.arraySize; i++)
                {
                    var old = legacy.GetArrayElementAtIndex(i);
                    if (old.objectReferenceValue == null) { key.GetArrayElementAtIndex(i).stringValue = string.Empty; continue; }
                    key.GetArrayElementAtIndex(i).stringValue = Register(ExtractTemplate(old.objectReferenceValue, target.GetType().Name), pair.attribute.Kind);
                    old.objectReferenceValue = null;
                }
                legacy.arraySize = 0;
            }
            else if (legacy.objectReferenceValue != null)
            {
                key.stringValue = Register(ExtractTemplate(legacy.objectReferenceValue, target.GetType().Name), pair.attribute.Kind);
                legacy.objectReferenceValue = null;
            }
        }
        if (target is Game.UI.HpSlider && string.IsNullOrEmpty(serialized.FindProperty("LineTransformKey").stringValue))
        {
            EnsureFolder("Assets/Prefab/UI/PlayerHeart");
            var path = "Assets/Prefab/UI/PlayerHeart/HeartRow.prefab";
            var row = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (row == null)
            {
                var go = new GameObject("HeartRow", typeof(RectTransform), typeof(HorizontalLayoutGroup));
                try { row = PrefabUtility.SaveAsPrefabAsset(go, path); }
                finally { Object.DestroyImmediate(go); }
            }
            serialized.FindProperty("LineTransformKey").stringValue = Register(row);
        }
        serialized.ApplyModifiedPropertiesWithoutUndo();
        EditorUtility.SetDirty(target);
        // 中断后再次迁移时, 已保存的 key 必须仍有对应目录条目.
        foreach (var pair in KeyProperties(serialized))
        {
            var keys = pair.property.isArray && pair.property.propertyType != SerializedPropertyType.String ? Enumerable.Range(0, pair.property.arraySize).Select(i => pair.property.GetArrayElementAtIndex(i).stringValue) : new[] { pair.property.stringValue };
            foreach (var key in keys) EnsureRegisteredKey(pair.attribute.Kind, key);
        }
    }

    private static void EnsureRegisteredKey(AddressableAssetKind kind, string key)
    {
        if (string.IsNullOrEmpty(key) || Catalog(kind).entries.Any(e => e.key == key)) return;
        var filter = new Dictionary<AddressableAssetKind, string>
        {
            [AddressableAssetKind.Sprite] = "t:Sprite", [AddressableAssetKind.Prefab] = "t:Prefab", [AddressableAssetKind.AudioClip] = "t:AudioClip",
            [AddressableAssetKind.TextAsset] = "t:TextAsset", [AddressableAssetKind.ScriptableObject] = "t:ScriptableObject", [AddressableAssetKind.Object] = ""
        }[kind];
        var candidates = new List<Object>();
        foreach (var guid in AssetDatabase.FindAssets(kind == AddressableAssetKind.Sprite ? filter : filter + " " + key, new[] { "Assets" }))
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            foreach (var asset in kind == AddressableAssetKind.Sprite ? AssetDatabase.LoadAllAssetsAtPath(path).OfType<Sprite>().Cast<Object>() : new[] { AssetDatabase.LoadMainAssetAtPath(path) })
            {
                if (asset == null) continue;
                var name = ExplicitAliases.TryGetValue(path, out var alias) ? alias : asset is Sprite ? asset.name : Path.GetFileNameWithoutExtension(path);
                if (name.EndsWith(".lua", StringComparison.Ordinal)) name = name.Substring(0, name.Length - 4);
                if (name == key) candidates.Add(asset);
            }
        }
        if (candidates.Count != 1) throw new InvalidOperationException($"Interrupted migration key {kind}/{key} resolves to {candidates.Count} assets. Restore the migration snapshot or configure an explicit alias.");
        Register(candidates[0], kind);
    }

    private static void RestoreScenes(SceneSetup[] setup)
    {
        if (setup.Any(s => s.isLoaded && s.isActive)) EditorSceneManager.RestoreSceneManagerSetup(setup);
        else EditorSceneManager.OpenScene("Assets/Scenes/Root.unity", OpenSceneMode.Single);
    }

    private static void MigrateHierarchy(GameObject root)
    {
        // QFramework 的额外资源绑定也要转为 key, 保留组件和层级对象的绑定.
        foreach (var binds in root.GetComponentsInChildren<QFramework.OtherBinds>(true))
        {
            foreach (var binding in binds.Binds.ToArray())
            {
                foreach (var owner in binds.GetComponents<MonoBehaviour>())
                {
                    var keyField = Field(owner.GetType(), binding.MemberName + "Key");
                    var attribute = keyField?.GetCustomAttribute<AddressableKeyAttribute>();
                    if (attribute == null || binding.Object == null) continue;
                    var serialized = new SerializedObject(owner);
                    serialized.FindProperty(keyField.Name).stringValue = Register(ExtractTemplate(binding.Object, owner.GetType().Name), attribute.Kind);
                    serialized.ApplyModifiedPropertiesWithoutUndo();
                    binds.Binds.Remove(binding);
                    EditorUtility.SetDirty(binds);
                    break;
                }
            }
        }
        foreach (var component in root.GetComponentsInChildren<MonoBehaviour>(true)) MigrateObject(component);
    }

    public static void RefreshKeyedPrefabs()
    {
        foreach (var guid in AssetDatabase.FindAssets("t:Prefab", new[] { "Assets/Prefab" }))
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var prefab = PrefabUtility.LoadPrefabContents(path);
            try { MigrateHierarchy(prefab); PrefabUtility.SaveAsPrefabAsset(prefab, path); }
            finally { PrefabUtility.UnloadPrefabContents(prefab); }
        }
        RebuildManifests();
        Validate();
        AssetDatabase.SaveAssets();
    }

    private static void BindRoot(Scene scene)
    {
        var loader = scene.GetRootGameObjects().SelectMany(g => g.GetComponentsInChildren<AddressableLoader>(true)).Single();
        var serialized = new SerializedObject(loader);
        var references = serialized.FindProperty("catalogReferences");
        references.arraySize = Catalogs.Count;
        var index = 0;
        foreach (AddressableAssetKind kind in Enum.GetValues(typeof(AddressableAssetKind)))
            references.GetArrayElementAtIndex(index++).FindPropertyRelative("m_AssetGUID").stringValue = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(Catalog(kind)));
        serialized.ApplyModifiedPropertiesWithoutUndo();
    }

    private static void AddLoadingView(Scene scene)
    {
        if (scene.GetRootGameObjects().SelectMany(g => g.GetComponentsInChildren<AddressableSceneLoadingView>(true)).Any()) return;
        var canvas = scene.GetRootGameObjects().SelectMany(g => g.GetComponentsInChildren<Canvas>(true)).First(c => c.renderMode != RenderMode.WorldSpace);
        var owner = new GameObject("ResourceLoadingView", typeof(RectTransform), typeof(AddressableSceneLoadingView));
        owner.transform.SetParent(canvas.transform, false);
        var panel = new GameObject("LoadingStatus", typeof(RectTransform), typeof(Image));
        panel.transform.SetParent(owner.transform, false);
        panel.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.9f);
        var rect = panel.GetComponent<RectTransform>(); rect.sizeDelta = new Vector2(620f, 100f); rect.anchoredPosition = new Vector2(0f, -150f);
        var text = new GameObject("Txt_Status", typeof(RectTransform), typeof(Text)); text.transform.SetParent(panel.transform, false);
        var label = text.GetComponent<Text>(); label.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf"); label.fontSize = 18; label.color = Color.white; label.alignment = TextAnchor.MiddleCenter;
        text.GetComponent<RectTransform>().sizeDelta = new Vector2(600f, 60f);
        var progress = new GameObject("Progress", typeof(RectTransform), typeof(Slider)); progress.transform.SetParent(panel.transform, false);
        var slider = progress.GetComponent<Slider>(); slider.interactable = false;
        var fill = new GameObject("Fill", typeof(RectTransform), typeof(Image)); fill.transform.SetParent(progress.transform, false);
        fill.GetComponent<Image>().color = new Color(0.2f, 0.7f, 1f); slider.fillRect = fill.GetComponent<RectTransform>();
        progress.GetComponent<RectTransform>().sizeDelta = new Vector2(550f, 8f); progress.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -35f);
        var serialized = new SerializedObject(owner.GetComponent<AddressableSceneLoadingView>());
        serialized.FindProperty("statusText").objectReferenceValue = label;
        serialized.FindProperty("progressSlider").objectReferenceValue = slider;
        serialized.FindProperty("content").objectReferenceValue = panel;
        serialized.ApplyModifiedPropertiesWithoutUndo();
        panel.SetActive(false);
    }

    private static void MigrateGraphs()
    {
        foreach (var guid in AssetDatabase.FindAssets("t:LevelGraph", new[] { "Assets/Prefab/Room" }))
        {
            var sourcePath = AssetDatabase.GUIDToAssetPath(guid);
            var source = AssetDatabase.LoadAssetAtPath<LevelGraph>(sourcePath);
            var path = Path.ChangeExtension(sourcePath, null) + "Keyed.asset";
            var definition = AssetDatabase.LoadAssetAtPath<KeyedLevelGraph>(path);
            ExplicitAliases[path] = source.name;
            OldAddresses["room/level1"] = source.name;
            if (definition != null)
            {
                // 上次迁移可能中断, 拓扑引用和注册信息必须一起完成.
                foreach (var room in source.Rooms.OfType<Edgar.Unity.Room>())
                {
                    foreach (var prefab in room.GetRoomTemplates()) Register(prefab);
                    room.IndividualRoomTemplates.Clear(); room.RoomTemplateSets.Clear(); EditorUtility.SetDirty(room);
                }
                foreach (var prefab in source.CorridorIndividualRoomTemplates.Concat(source.DefaultIndividualRoomTemplates)) Register(prefab);
                source.CorridorIndividualRoomTemplates.Clear(); source.DefaultIndividualRoomTemplates.Clear();
                source.CorridorRoomTemplateSets.Clear(); source.DefaultRoomTemplateSets.Clear();
                EditorUtility.SetDirty(source);
                Register(definition);
                Settings.RemoveAssetEntry(guid, false);
                continue;
            }
            definition = ScriptableObject.CreateInstance<KeyedLevelGraph>(); definition.topology = source;
            for (var i = 0; i < source.Rooms.Count; i++)
            {
                var room = source.Rooms[i] as Edgar.Unity.Room ?? throw new InvalidOperationException("Graph migration expects Edgar Room nodes.");
                definition.rooms.Add(new KeyedRoomTemplates { index = i, keys = room.GetRoomTemplates().Select(p => Register(p)).ToList() });
                room.IndividualRoomTemplates.Clear(); room.RoomTemplateSets.Clear(); EditorUtility.SetDirty(room);
            }
            for (var i = 0; i < source.Connections.Count; i++)
            {
                var connection = source.Connections[i] as Connection ?? throw new InvalidOperationException("Graph migration expects Edgar Connection nodes.");
                definition.connections.Add(new KeyedRoomTemplates { index = i, keys = connection.RoomTemplates.Select(p => Register(p)).ToList() });
                connection.RoomTemplates.Clear(); EditorUtility.SetDirty(connection);
            }
            definition.corridors = source.CorridorIndividualRoomTemplates.Concat(source.CorridorRoomTemplateSets.SelectMany(s => s.RoomTemplates)).Distinct().Select(p => Register(p)).ToList();
            definition.defaults = source.DefaultIndividualRoomTemplates.Concat(source.DefaultRoomTemplateSets.SelectMany(s => s.RoomTemplates)).Distinct().Select(p => Register(p)).ToList();
            source.CorridorIndividualRoomTemplates.Clear(); source.CorridorRoomTemplateSets.Clear(); source.DefaultIndividualRoomTemplates.Clear(); source.DefaultRoomTemplateSets.Clear();
            EditorUtility.SetDirty(source);
            AssetDatabase.CreateAsset(definition, path);
            // 对外保留原关卡名, 拓扑资产仅供编辑器与运行时图构建使用.
            ExplicitAliases[path] = source.name;
            Register(definition);
            OldAddresses["room/level1"] = source.name;
            Settings.RemoveAssetEntry(guid, false);
        }
        AssetDatabase.SaveAssets();
    }

    private static void RewriteCsvKeys()
    {
        var databaseRows = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>
        {
            ["ItemDatabaseExcelImporter"] = AssetDatabase.LoadAssetAtPath<ItemDatabase>("Assets/GameDataSO/DataBase/ItemDatabase.asset").Items.ToDictionary(d => d.itemId.ToString(), d => new Dictionary<string, string> { ["icon"] = d.iconKey }),
            ["BuffDatabaseExcelImporter"] = AssetDatabase.LoadAssetAtPath<BuffDataBase>("Assets/GameDataSO/DataBase/BuffDataBase.asset").Buffs.ToDictionary(d => d.Id.ToString(), d => new Dictionary<string, string> { ["icon"] = d.iconKey, ["luaFile"] = d.luaFileKey }),
            ["WeaponDatabaseExcelImporter"] = AssetDatabase.LoadAssetAtPath<WeaponDatabase>("Assets/GameDataSO/DataBase/WeaponDatabase.asset").Weapons.ToDictionary(d => d.weaponId, d => new Dictionary<string, string> { ["reloadSound"] = d.reloadSoundKey, ["shootSounds"] = string.Join(";", d.shootSoundsKeys) }),
            ["EnemyDatabaseExcelImporter"] = AssetDatabase.LoadAssetAtPath<EnemyDatabase>("Assets/GameDataSO/DataBase/EnemyDatabase.asset").Enemies.ToDictionary(d => d.enemyId.ToString(), d => new Dictionary<string, string> { ["prefab"] = d.prefabKey })
        };
        foreach (var pair in databaseRows)
        {
            var path = $"Assets/csv/{pair.Key}.csv";
            var lines = File.ReadAllLines(path);
            var header = lines[0].Split(',');
            for (var i = 1; i < lines.Length; i++)
            {
                // 当前四张源表没有带逗号的引用列, 只替换资源单元格, 不重新导出玩法数据.
                var cells = lines[i].Split(',');
                if (cells.Length != header.Length) throw new InvalidOperationException($"CSV row shape changed: {path}:{i + 1}.");
                if (!pair.Value.TryGetValue(cells[0], out var replacements)) throw new InvalidOperationException($"CSV row not found in existing database: {path}:{cells[0]}.");
                foreach (var value in replacements) cells[Array.IndexOf(header, value.Key)] = value.Value;
                lines[i] = string.Join(",", cells);
            }
            File.WriteAllLines(path, lines, new UTF8Encoding(false));
        }
    }

    private static void Collect(Object asset, Dictionary<string, AddressableResourceKey> resources, HashSet<int> visited)
    {
        if (asset == null || !visited.Add(asset.GetInstanceID())) return;
        if (asset is GameObject go)
        {
            foreach (var component in go.GetComponentsInChildren<MonoBehaviour>(true)) Collect(component, resources, visited);
            return;
        }
        if (asset is KeyedLevelGraph graph) Collect(graph.topology, resources, visited);
        if (!asset.GetType().Assembly.GetName().Name.StartsWith("Game.", StringComparison.Ordinal)) return;
        var serialized = new SerializedObject(asset);
        foreach (var pair in KeyProperties(serialized))
        {
            var property = pair.property;
            var keys = property.isArray && property.propertyType != SerializedPropertyType.String ? Enumerable.Range(0, property.arraySize).Select(i => property.GetArrayElementAtIndex(i).stringValue) : new[] { property.stringValue };
            foreach (var key in keys) AddDependency(pair.attribute.Kind, key, resources, visited);
        }
    }

    private static void AddDependency(AddressableAssetKind kind, string key, Dictionary<string, AddressableResourceKey> resources, HashSet<int> visited)
    {
        if (string.IsNullOrEmpty(key)) return;
        if (kind == AddressableAssetKind.Scene) throw new InvalidOperationException("Scene keys cannot be preload resources.");
        var entry = Catalog(kind).Get(key);
        var identity = $"{kind}:{key}";
        if (resources.ContainsKey(identity)) return;
        resources.Add(identity, new AddressableResourceKey(kind, key));
        Collect(AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(entry.reference.AssetGUID)), resources, visited);
    }

    [MenuItem("Tools/UnityEasyWorkTools/Addressables/Rebuild Preload Manifests")]
    public static void RebuildManifests()
    {
        var setup = EditorSceneManager.GetSceneManagerSetup();
        try
        {
            foreach (var stage in new[] { "Root", "StartScene", "GameScene" })
            {
                var resources = new Dictionary<string, AddressableResourceKey>();
                var visited = new HashSet<int>();
                var scene = EditorSceneManager.OpenScene($"Assets/Scenes/{stage}.unity", OpenSceneMode.Single);
                foreach (var root in scene.GetRootGameObjects()) Collect(root, resources, visited);
                if (stage == "Root")
                {
                    AddDependency(AddressableAssetKind.Object, "AudioMixer", resources, visited);
                    foreach (var entry in Catalog(AddressableAssetKind.TextAsset).entries.Where(e => e.labels.Contains("hotfix"))) AddDependency(AddressableAssetKind.TextAsset, entry.key, resources, visited);
                }
                if (stage == "GameScene")
                {
                    foreach (var key in new[] { "ItemDatabase", "WeaponDatabase", "BuffDataBase", "EnemyDatabase" }) AddDependency(AddressableAssetKind.ScriptableObject, key, resources, visited);
                    foreach (var key in new[] { "AK", "AWP", "Bow", "Laser", "MP5", "Pistol", "RocketGun", "ShotGun", "Heart", "HarmUp", "SpeedUp", "PowerUp", "Purify" }) AddDependency(AddressableAssetKind.Prefab, key, resources, visited);
                }
                var manifest = AssetDatabase.LoadAssetAtPath<AddressablePreloadManifest>($"{ManifestFolder}/{stage}Preload.asset");
                manifest.resources = resources.Values.OrderBy(r => r.kind).ThenBy(r => r.key, StringComparer.Ordinal).ToList();
                EditorUtility.SetDirty(manifest);
                Debug.Log($"Preload {stage}: {manifest.resources.Count} resources.");
            }
        }
        finally { RestoreScenes(setup); }
        AssetDatabase.SaveAssets();
    }

    public static void SyncAddressKeys()
    {
        var code = new StringBuilder("// 自动生成的资源短名, 请通过 Catalog 注册工具更新.\nnamespace Game.Core\n{\n    public static class AddressKeys\n    {\n");
        foreach (AddressableAssetKind kind in Enum.GetValues(typeof(AddressableAssetKind)))
        {
            code.AppendLine($"        public static class {kind}\n        {{");
            var identifiers = new HashSet<string>();
            foreach (var entry in Catalog(kind).entries.OrderBy(e => e.key, StringComparer.Ordinal))
            {
                var identifier = new string(entry.key.Select(c => char.IsLetterOrDigit(c) || c == '_' ? c : '_').ToArray());
                if (char.IsDigit(identifier[0])) identifier = "_" + identifier;
                if (!identifiers.Add(identifier)) throw new InvalidOperationException($"AddressKeys identifier collision: {kind}/{entry.key}.");
                code.AppendLine($"            public const string @{identifier} = \"{entry.key.Replace("\\", "\\\\").Replace("\"", "\\\"")}\";");
            }
            code.AppendLine("        }");
        }
        code.AppendLine("    }\n}");
        File.WriteAllText("Assets/Scripts/Core/HotUpdate/AddressKeys.cs", code.ToString(), new UTF8Encoding(false));
    }

    [MenuItem("Tools/UnityEasyWorkTools/Addressables/Validate Catalogs")]
    public static void Validate()
    {
        foreach (AddressableAssetKind kind in Enum.GetValues(typeof(AddressableAssetKind)))
        {
            var catalog = Catalog(kind); catalog.BuildMap();
            foreach (var entry in catalog.entries)
            {
                if (!entry.reference.RuntimeKeyIsValid()) throw new InvalidOperationException($"Invalid reference: {kind}/{entry.key}.");
                var path = AssetDatabase.GUIDToAssetPath(entry.reference.AssetGUID);
                if (Settings.FindAssetEntry(entry.reference.AssetGUID, true) == null) throw new InvalidOperationException($"Missing Addressables entry: {path}.");
                if (kind == AddressableAssetKind.Sprite) ResolveReference(entry, typeof(Sprite));
                if (kind == AddressableAssetKind.Scene) Catalog(AddressableAssetKind.ScriptableObject).Get(entry.preloadManifestKey);
            }
        }
        Debug.Log("ADDRESSABLE_CATALOG_VALIDATION_SUCCESS");
    }

    public static void ReserializeMigratedAssets()
    {
        // 旧引用字段已从脚本移除, 用 Unity 序列化器清除迁移残留.
        var paths = AssetDatabase.FindAssets("", new[] { "Assets/Prefab", "Assets/GameDataSO", "Assets/Scenes" })
            .Select(AssetDatabase.GUIDToAssetPath)
            .Where(p => p.EndsWith(".prefab") || p.EndsWith(".asset") || p.EndsWith(".unity")).ToArray();
        AssetDatabase.ForceReserializeAssets(paths);
        Validate();
        Debug.Log("ADDRESSABLE_REFERENCE_CLEANUP_SUCCESS");
    }
}
