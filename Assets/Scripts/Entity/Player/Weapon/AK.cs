using UnityEngine;
using QFramework;

namespace QFramework.PG
{
	public partial class AK : QFramework.PG.Gun
	{
		public override PlayerBullet BulletPrefab => PlayerBullet;
		public override UnityEngine.AudioSource PlayerAudioSource => SelfAudioSource;
		[Header("属性")]
		[SerializeField] private float _duration = 0.1f;
		[SerializeField] private int _maxAmmo = 30;
		private ShootDuration _shootDuration;
		private GunClip _gunClip;

		private void Awake()
		{
			_shootDuration = new ShootDuration(_duration);
			_gunClip = new GunClip(_maxAmmo);
		}

		public override void ShootDown(Vector2 dir)
		{	
			PlayerAudioSource.clip = shootSounds[0];
			PlayerAudioSource.loop = true;
			PlayerAudioSource.Play();
		}
        public override void Shooting(Vector2 dir)
        {
			if(_shootDuration.CanShoot && _gunClip.CanShoot) {
				_shootDuration.RecordShootTime();
				_gunClip.Shoot();
				var obj = Instantiate(BulletPrefab);
				obj.transform.position = transform.position;
				obj.dir = dir;
				obj.gameObject.SetActive(true);
			}
        }
		public override void ShootUp(Vector2 dir)
		{
			PlayerAudioSource.Stop();
			PlayerAudioSource.clip = AKShootEnd;
			PlayerAudioSource.Play();
			PlayerAudioSource.loop = false;
		}
		public override void Reload() => _gunClip.Reload();
	}
}
