using QFramework;
using UnityEngine;

namespace QFramework.PG
{
	public partial class Pistol : Gun
	{
		public override PlayerBullet BulletPrefab => PlayerBullet;

		public override void Shoot(Vector2 dir)
		{
			GetBullet(dir);
			TryPlaySound(false);
			gunFireEffect.Show(BulletPrefab.Position2D(), dir);
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
