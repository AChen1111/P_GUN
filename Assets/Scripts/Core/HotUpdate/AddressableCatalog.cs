using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Core
{
    public enum AddressableAssetKind { Sprite, Prefab, Scene, AudioClip, TextAsset, ScriptableObject, Object }

    /// <summary>
    /// 标记配置中的短名, 编辑器据此迁移引用并收集阶段加载依赖.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class AddressableKeyAttribute : PropertyAttribute
    {
        public AddressableAssetKind Kind { get; }
        public string LegacyField { get; }
        public AddressableKeyAttribute(AddressableAssetKind kind, string legacyField = null)
        {
            Kind = kind;
            LegacyField = legacyField;
        }
    }

    [Serializable]
    public sealed class AddressableCatalogEntry
    {
        public string key;
        public AssetReference reference;
        public string preloadManifestKey;
        public string luaModulePath;
        public List<string> labels = new List<string>();
    }

    /// <summary>
    /// 一个分类对应一个 Catalog, 业务短名与 Addressables 地址互相独立.
    /// </summary>
    [CreateAssetMenu(menuName = "PG/Addressables/Catalog")]
    public sealed class AddressableCatalog : ScriptableObject
    {
        public AddressableAssetKind kind;
        public List<AddressableCatalogEntry> entries = new List<AddressableCatalogEntry>();
        private Dictionary<string, AddressableCatalogEntry> map;

        public void BuildMap()
        {
            var next = new Dictionary<string, AddressableCatalogEntry>(StringComparer.Ordinal);
            foreach (var entry in entries)
            {
                if (string.IsNullOrWhiteSpace(entry.key)) throw new InvalidOperationException($"Catalog {name} contains an empty key.");
                if (entry.reference == null) throw new InvalidOperationException($"Catalog {name}/{entry.key} has no reference.");
                next.Add(entry.key, entry);
            }
            map = next;
        }

        public AddressableCatalogEntry Get(string key)
        {
            if (map == null) BuildMap();
            if (!map.TryGetValue(key, out var entry)) throw new KeyNotFoundException($"Missing {kind} key: {key}, Catalog: {name}.");
            return entry;
        }

        public static AddressableAssetKind KindFor(Type type)
        {
            if (type == typeof(Sprite)) return AddressableAssetKind.Sprite;
            if (type == typeof(GameObject) || typeof(Component).IsAssignableFrom(type)) return AddressableAssetKind.Prefab;
            if (type == typeof(AudioClip)) return AddressableAssetKind.AudioClip;
            if (type == typeof(TextAsset)) return AddressableAssetKind.TextAsset;
            if (typeof(ScriptableObject).IsAssignableFrom(type)) return AddressableAssetKind.ScriptableObject;
            return AddressableAssetKind.Object;
        }
    }
}
