using UnityEngine.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    [Serializable]
    public struct WeaponData
    {
        public string weaponId;
        public string displayName;
        // 配置保存短名, 资源来自阶段预加载缓存.
        [SerializeField, AddressableKey(AddressableAssetKind.AudioClip)] public List<string> shootSoundsKeys;
        public List<AudioClip> shootSounds => AddressableAssetAccess.List<AudioClip>(shootSoundsKeys);
        // 配置保存短名, 资源来自阶段预加载缓存.
        [SerializeField, AddressableKey(AddressableAssetKind.AudioClip)] public string reloadSoundKey;
        public AudioClip reloadSound => AddressableAssetAccess.Get<AudioClip>(reloadSoundKey);
        public int bulletSpeed;
        [Header("Damage")]
        public int minDamage;
        public int maxDamage;

        [Header("Ammo")]
        public int maxBulletBagNum;
        public int clipSize;
        public float shootInterval;

        public string DisplayName => string.IsNullOrWhiteSpace(displayName) ? weaponId : displayName;
        public int MaxDamage => Mathf.Max(minDamage, maxDamage);
        public float ShootInterval => Mathf.Max(0f, shootInterval);
        public void ApplyTo(Gun gun)
        {
            if (gun == null) return;

            gun.ApplyData(this);
        }
    }
}
