using UnityEngine;
using QFramework;

namespace QFramework.PG
{
	public partial class AWP : QFramework.PG.Gun
	{
		public override PlayerBullet BulletPrefab => PlayerBullet;

        public override BulletBag BulletBag => 	_bulletBag;

        
		[Header("属性")]
		[SerializeField] private float _duration = 2f;
		[SerializeField] private int _maxAmmo = 5;
		private ShootDuration _shootDuration;
		private GunClip _gunClip;
		private BulletBag _bulletBag;
		private GunFire _gunFire = new GunFire();

		private void Awake()
		{
			_shootDuration = new ShootDuration(_duration);
			_gunClip = new GunClip(_maxAmmo);
			_bulletBag = new BulletBag(MaxBulletBagNum);
		}

		public override void Shoot(Vector2 dir)
		{
			if(!_shootDuration.CanShoot || !_gunClip.CanShoot) return;
			_shootDuration.RecordShootTime();
			_gunClip.Shoot();
			var obj = GetBullet(dir);
			_gunFire.Show(BulletPrefab.Position2D(), dir);

			TryPlaySound(false);
		}
		public override void Reload() => BulletBag.Reload(_gunClip,ReloadSound);
		public override void OnGunUsed() 
		{
			base.OnGunUsed();
			_gunClip.OnGunUsed();
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
