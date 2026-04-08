using UnityEngine;
using QFramework;

namespace QFramework.PG
{
	public partial class AK : QFramework.PG.Gun
	{
		public override PlayerBullet BulletPrefab => PlayerBullet;

		public override void OnGunUsed()
		{
			base.OnGunUsed();
			PlayerAudioSource.Stop();
		}

		public override void ShootDown(Vector2 dir)
		{
			if(gunClip.IsOutOfAmmo || !gunClip.CanShoot) return;
			TryPlaySound(true);
		}

        public override void Shooting(Vector2 dir)
        {
			if(shootDuration.CanShoot && gunClip.CanShoot) {
				if(!PlayerAudioSource.isPlaying) {
					TryPlaySound(true);
				}
				shootDuration.RecordShootTime();
				gunClip.Shoot();
				var obj = GetBullet(dir);
				gunFireEffect.Show(BulletPrefab.Position2D(), dir);
			} 
			else if(gunClip.IsOutOfAmmo || gunClip.IsReloading) {
				PlayerAudioSource.Stop();
			}
        }

		public override void ShootUp(Vector2 dir)
		{
			if(!PlayerAudioSource.isPlaying) return;
			TryPlaySound(AKShootEnd,false);
		}
	}
}
