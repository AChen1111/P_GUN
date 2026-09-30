using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Edgar.Unity;
using Game.Core;
using Game.Gameplay;
using Game.Gameplay.Save;
using Game.Items;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;
using Object = UnityEngine.Object;

public sealed class AddressableKeyTests
{
    [Test]
    public void Catalog_MissingAndDuplicateKeysExposeConfigurationErrors()
    {
        var catalog = ScriptableObject.CreateInstance<AddressableCatalog>();
        try
        {
            catalog.entries.Add(new AddressableCatalogEntry { key = "hero", reference = new AssetReference("") });
            catalog.BuildMap();
            Assert.AreEqual("hero", catalog.Get("hero").key);
            Assert.Throws<KeyNotFoundException>(() => catalog.Get("missing"));
            catalog.entries.Add(new AddressableCatalogEntry { key = "hero", reference = new AssetReference("") });
            Assert.Throws<ArgumentException>(() => catalog.BuildMap());
        }
        finally { Object.DestroyImmediate(catalog); }
    }

    [Test]
    public void MigratedItemIconsPreserveSpecificSpriteSlices()
    {
        var database = AssetDatabase.LoadAssetAtPath<ItemDatabase>("Assets/GameDataSO/DataBase/ItemDatabase.asset");
        var expected = new[] { "Potion_Round_Red", "Potion_Round_Purple", "Potion_Round_Blue", "Potion_Round_Teal", "Potion_Tall_Magenta" };
        for (var i = 0; i < expected.Length; i++)
        {
            Assert.IsTrue(database.TryGetById(i + 1, out var item));
            Assert.AreEqual(expected[i], item.iconKey);
            Assert.AreEqual(expected[i], item.icon.name);
        }
        Assert.IsTrue(database.TryGetById(6, out _), "Migration must preserve rows absent from CSV.");
    }

    [Test]
    public void ProjectCatalogsResolveEveryReferenceAndManifest()
    {
        Assert.DoesNotThrow(AddressableCatalogSetup.Validate);
        foreach (AddressableAssetKind kind in Enum.GetValues(typeof(AddressableAssetKind)))
        {
            var catalog = AssetDatabase.LoadAssetAtPath<AddressableCatalog>($"{AddressableCatalogSetup.CatalogFolder}/{kind}Catalog.asset");
            Assert.AreEqual(kind, catalog.kind);
            Assert.IsNotEmpty(catalog.entries);
        }
    }

    [Test]
    public void GameplayManifestContainsNestedGeneratedResources()
    {
        var manifest = AssetDatabase.LoadAssetAtPath<AddressablePreloadManifest>("Assets/GameDataSO/AddressablePreload/GameScenePreload.asset");
        var prefabs = manifest.resources.Where(r => r.kind == AddressableAssetKind.Prefab).Select(r => r.key).ToArray();
        foreach (var key in new[] { "Player", "PlayerBullet", "AK", "Heart", "PlayerHeart", "InventorySlot", "BuffStatusIcon", "SaveSlotItem", "NormalRoom" })
            CollectionAssert.Contains(prefabs, key);
        Assert.IsTrue(manifest.resources.Any(r => r.kind == AddressableAssetKind.AudioClip));
        Assert.IsTrue(manifest.resources.Any(r => r.kind == AddressableAssetKind.TextAsset));
    }

    [Test]
    public void KeyedGraphBuildsIndependentTopologyWithLoadedTemplates()
    {
        var definition = AssetDatabase.LoadAssetAtPath<KeyedLevelGraph>("Assets/Prefab/Room/LevelGraph/Level1Keyed.asset");
        var owned = new List<ScriptableObject>();
        try
        {
            var graph = definition.CreateRuntimeGraph(owned);
            Assert.AreEqual(definition.topology.Rooms.Count, graph.Rooms.Count);
            Assert.AreEqual(definition.topology.Connections.Count, graph.Connections.Count);
            for (var i = 0; i < graph.Rooms.Count; i++)
            {
                Assert.AreNotSame(definition.topology.Rooms[i], graph.Rooms[i]);
                Assert.IsEmpty(((Edgar.Unity.Room)definition.topology.Rooms[i]).IndividualRoomTemplates);
                Assert.IsNotEmpty(graph.Rooms[i].GetRoomTemplates());
            }
            foreach (var connection in graph.Connections)
            {
                CollectionAssert.Contains(graph.Rooms, connection.From);
                CollectionAssert.Contains(graph.Rooms, connection.To);
            }
            Assert.IsNotEmpty(graph.CorridorIndividualRoomTemplates);
        }
        finally { foreach (var obj in owned) Object.DestroyImmediate(obj); }
    }

    [Test]
    public void CsvImporterStoresShortKeyWithoutCreatingSpriteReference()
    {
        const string assetPath = "Assets/Tests/AddressableImportTest.asset";
        var csv = Path.Combine(Path.GetTempPath(), "pgun-addressable-import-test.csv");
        try
        {
            File.WriteAllText(csv, "itemId,itemName,description,icon\n101,Test,Description,Potion_Round_Red\n");
            var report = new ItemDatabaseExcelImporter().Import(csv, assetPath);
            Assert.AreEqual(0, report.ConversionErrors);
            var database = AssetDatabase.LoadAssetAtPath<ItemDatabase>(assetPath);
            Assert.IsTrue(database.TryGetById(101, out var item));
            Assert.AreEqual("Potion_Round_Red", item.iconKey);
            Assert.AreEqual("Potion_Round_Red", item.icon.name);
        }
        finally { AssetDatabase.DeleteAsset(assetPath); File.Delete(csv); }
    }

    [Test]
    public void CsvImporterReportsUnregisteredResourceKey()
    {
        const string assetPath = "Assets/Tests/AddressableBadImportTest.asset";
        var csv = Path.Combine(Path.GetTempPath(), "pgun-addressable-bad-import-test.csv");
        try
        {
            File.WriteAllText(csv, "itemId,itemName,description,icon\n101,Test,Description,__missing_csv_key__\n");
            LogAssert.Expect(LogType.Error, new Regex("custom column 'icon'.*Missing Sprite key: __missing_csv_key__"));
            var report = new ItemDatabaseExcelImporter().Import(csv, assetPath);
            Assert.AreEqual(1, report.ConversionErrors);
        }
        finally { AssetDatabase.DeleteAsset(assetPath); File.Delete(csv); }
    }

    [Test]
    public void OlderSaveVersionIsExplicitlyIncompatible()
    {
        Assert.AreEqual(2, SaveGameService.SaveVersion);
        Assert.IsFalse(SaveGameService.IsCompatibleVersion(1));
        Assert.IsTrue(SaveGameService.IsCompatibleVersion(new GameSaveData().version));
    }
}
