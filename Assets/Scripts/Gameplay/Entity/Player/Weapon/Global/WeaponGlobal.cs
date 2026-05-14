using UnityEngine;
using QFramework;
using System.Collections.Generic;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    public class WeaponGlobal : ViewController
    {
        public SpriteRenderer GunFire;

		public static WeaponGlobal Instance { get; private set; }
		private void Awake() {
			Instance = this;
		}
		public AudioSource WeaponAudioSource;

    }
}
