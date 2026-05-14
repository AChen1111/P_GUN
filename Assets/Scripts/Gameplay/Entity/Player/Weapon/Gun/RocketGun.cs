using UnityEngine;
using QFramework;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    public class RocketGun : Gun
    {
        public PlayerBullet PlayerBullet;
        public UnityEngine.AudioSource SelfAudioSource;

		public override PlayerBullet BulletPrefab => PlayerBullet;

		public override void Shoot(Vector2 dir)
		{
			gunClip.CheckAmmo();
			if(!shootDuration.CanShoot || !gunClip.CanShoot) return;
			shootDuration.RecordShootTime();
			gunClip.Shoot();
			var obj = GetBullet(dir);
			if(obj == null) return;

			obj.transform.right = dir;
			gunFireEffect.Show(FirePointPosition, dir);
			TryPlaySound(false);
		}

		public override void ShootDown(Vector2 dir)
        {
			Shoot(dir);
        }

        public override void Shooting(Vector2 dir)
        {
            Shoot(dir);
        }
    }
}
