using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Core
{
    /// <summary>
    /// 配置属性读取统一入口, 编辑器解析 Catalog, 游戏只读取已预加载缓存.
    /// </summary>
    public static class AddressableAssetAccess
    {
#if UNITY_EDITOR
        public static Func<Type, string, Object> EditorResolver;
#endif
        public static T Get<T>(string key) where T : Object
        {
            // 空短名表示原配置的可选资源未设置, 必需资源由使用方明确校验.
            if (string.IsNullOrEmpty(key)) return null;
#if UNITY_EDITOR
            if (!Application.isPlaying) return (T)(EditorResolver ?? throw new InvalidOperationException("Catalog editor resolver is not initialized."))(typeof(T), key);
#endif
            return (AddressableLoader.Instance ?? throw new InvalidOperationException("AddressableLoader must exist in Root.")).GetLoadedAsset<T>(key);
        }

        public static T Component<T>(string key) where T : Component
        {
            var prefab = Get<GameObject>(key);
            if (prefab == null) return null;
            return prefab.GetComponent<T>() ?? throw new InvalidOperationException($"Prefab {key} has no {typeof(T).Name}.");
        }

        public static List<T> List<T>(IEnumerable<string> keys) where T : Object
        {
            var result = new List<T>();
            if (keys == null) return result;
            foreach (var key in keys) result.Add(Get<T>(key));
            return result;
        }
    }
}
