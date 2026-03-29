using UnityEngine;
using QFramework;

namespace QFramework.PG
{
	public partial class AWP : QFramework.PG.Gun
	{
		public override PlayerBullet BulletPrefab => PlayerBullet;
		public override UnityEngine.AudioSource PlayerAudioSource => SelfAudioSource;

		[Header("属性")]
		[SerializeField] private float _duration = 2f;
		[SerializeField] private int _maxAmmo = 5;
		private ShootDuration _shootDuration;
		private GunClip _gunClip;

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
			obj.gameObject.SetActive(true);
			SelfAudioSource.PlayOneShot(shootSounds[0]);
		}
		public override void Reload() => _gunClip.Reload(ReloadSound);
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
