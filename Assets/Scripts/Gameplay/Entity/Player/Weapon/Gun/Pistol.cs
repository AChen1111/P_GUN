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

		/// <summary>
		/// 执行 Shoot 逻辑.
		/// </summary>
		public override void Shoot(Vector2 dir)
		{
			GetBullet(dir);
			TryPlaySound(false);
			gunFireEffect.Show(FirePointPosition, dir);
		}

		/// <summary>
		/// 执行 ShootDown 逻辑.
		/// </summary>
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
