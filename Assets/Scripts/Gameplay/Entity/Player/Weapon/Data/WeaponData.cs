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
        public List<AudioClip> shootSounds;
        public AudioClip reloadSound;
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
