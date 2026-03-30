using UnityEngine;
using QFramework;

namespace QFramework.PG
{
	public partial class RocketGun : QFramework.PG.Gun
	{
		public override PlayerBullet BulletPrefab => PlayerBullet;
		
		public override UnityEngine.AudioSource PlayerAudioSource => SelfAudioSource;

		[Header("属性")]
		[SerializeField] private float _duration = 1f;
		[SerializeField] private int _maxAmmo = 5;
		private ShootDuration _shootDuration;
		private GunClip _gunClip;
		private GunFire _gunFire = new GunFire();

		private void Awake()
		{
			_shootDuration = new ShootDuration(_duration);
			_gunClip = new GunClip(_maxAmmo);
		}

		public override void Shoot(Vector2 dir)
		{
			if(!_shootDuration.CanShoot || !_gunClip.CanShoot) return;
			_shootDuration.RecordShootTime();
			_gunClip.Shoot();
			var obj = Instantiate(BulletPrefab);
			obj.transform.position = transform.position;
			obj.dir = dir;
			obj.transform.right = dir;
			obj.gameObject.SetActive(true);
			_gunFire.Show(BulletPrefab.Position2D(), dir);
			SelfAudioSource.PlayOneShot(shootSounds[Random.Range(0, shootSounds.Count)]);
		}
		public override void Reload() => _gunClip.Reload();
		public override void OnGunUsed() => _gunClip.OnGunUsed();
		
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
