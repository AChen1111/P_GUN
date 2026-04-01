using UnityEngine;
using QFramework;

namespace QFramework.PG
{
	public partial class MP5 : Gun
	{
		public override PlayerBullet BulletPrefab => PlayerBullet;
		[Header("属性")]
		[SerializeField] private float _duration = 0.1f;
		[SerializeField] private int _maxAmmo = 30;
		private ShootDuration _shootDuration;
		private GunClip _gunClip;
		private GunFire _gunFire = new GunFire();

		private void Awake()
		{
			_shootDuration = new ShootDuration(_duration);
			_gunClip = new GunClip(_maxAmmo);
		}

		public override void ShootDown(Vector2 dir)
		{
			// 弹药耗尽或换弹中时不启动音效
			if(_gunClip.IsOutOfAmmo || !_gunClip.CanShoot) return;
			TryPlaySound(true);
		}

        public override void Shooting(Vector2 dir)
        {
			if(_shootDuration.CanShoot && _gunClip.CanShoot) {
				// 切枪后持续按住时，声音可能未启动，补启动
				if(!PlayerAudioSource.isPlaying) {
					TryPlaySound(true);
				}
				_shootDuration.RecordShootTime();
				_gunClip.Shoot();
				var obj = GetBullet(dir);
				_gunFire.Show(BulletPrefab.Position2D(), dir);
			} 
			else if(_gunClip.IsOutOfAmmo || _gunClip.IsReloading) {
				PlayerAudioSource.Stop();
			}
        }

		public override void ShootUp(Vector2 dir)
		{
			PlayerAudioSource.Stop();
		}

		public override void Reload() => _gunClip.Reload(ReloadSound);
		public override void OnGunUsed()
		{
			PlayerAudioSource.Stop();
			_gunClip.OnGunUsed();
		}
	}
}
