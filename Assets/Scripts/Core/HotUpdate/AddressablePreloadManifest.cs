using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    [Serializable]
    public struct AddressableResourceKey
    {
        public AddressableAssetKind kind;
        public string key;
        public AddressableResourceKey(AddressableAssetKind kind, string key) { this.kind = kind; this.key = key; }
    }

    /// <summary>
    /// 进入场景前加载生成资源, 让 Awake, 射击和对象池操作只同步读取缓存.
    /// </summary>
    [CreateAssetMenu(menuName = "PG/Addressables/Preload Manifest")]
    public sealed class AddressablePreloadManifest : ScriptableObject
    {
        public List<AddressableResourceKey> resources = new List<AddressableResourceKey>();
    }
}
