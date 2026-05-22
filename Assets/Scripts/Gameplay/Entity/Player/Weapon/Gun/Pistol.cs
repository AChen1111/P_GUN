using QFramework;
using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    public class Pistol : Gun
    {
        public SpriteRenderer SR;
        public PlayerBullet PlayerBullet;
        public UnityEngine.AudioSource SelfAudioSource;

		public override PlayerBullet BulletPrefab => PlayerBullet;
		public override void Shoot(Vector2 dir)
		{
			GetBullet(dir);
			TryPlaySound(false);
			PlayGunFire(dir);
		}
		public override void ShootDown(Vector2 dir)
		{
			gunClip.CheckAmmo();
			if(gunClip.CanShoot)
			{
				gunClip.Shoot();
				Shoot(dir);
			}
		}
    }
}
