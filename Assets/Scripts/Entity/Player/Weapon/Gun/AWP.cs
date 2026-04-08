using UnityEngine;
using QFramework;

namespace QFramework.PG
{
	public partial class AWP : QFramework.PG.Gun
	{
		public override PlayerBullet BulletPrefab => PlayerBullet;

		public override void Shoot(Vector2 dir)
		{
			gunClip.CheckAmmo();
			if(!shootDuration.CanShoot || !gunClip.CanShoot) return;
			shootDuration.RecordShootTime();
			gunClip.Shoot();
			var obj = GetBullet(dir);
			gunFireEffect.Show(BulletPrefab.Position2D(), dir);
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
