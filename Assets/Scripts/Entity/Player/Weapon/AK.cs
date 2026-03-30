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
		private GunFire _gunFire = new GunFire();

		/// <summary>
		/// 初始化
		/// </summary>
		private void Awake()
		{
			_shootDuration = new ShootDuration(_duration);
			_gunClip = new GunClip(_maxAmmo);
		}

		public override void OnGunUsed()
		{
			PlayerAudioSource.Stop();
			_gunClip.OnGunUsed();
		}

		public override void ShootDown(Vector2 dir)
		{
			// 弹药耗尽或换弹中时不启动音效
			if(_gunClip.IsOutOfAmmo || !_gunClip.CanShoot) return;
			PlayerAudioSource.clip = shootSounds[0];
			PlayerAudioSource.loop = true;
			PlayerAudioSource.Play();
		}

        public override void Shooting(Vector2 dir)
        {
			if(_shootDuration.CanShoot && _gunClip.CanShoot) {
				// 切枪后持续按住时，声音可能未启动，补启动
				if(!PlayerAudioSource.isPlaying) {
					PlayerAudioSource.clip = shootSounds[0];
					PlayerAudioSource.loop = true;
					PlayerAudioSource.Play();
				}
				_shootDuration.RecordShootTime();
				_gunClip.Shoot();
				var obj = Instantiate(BulletPrefab);
				obj.transform.position = transform.position;
				obj.dir = dir;
				obj.gameObject.SetActive(true);
				_gunFire.Show(BulletPrefab.Position2D(), dir);
			} else if(_gunClip.IsOutOfAmmo || _gunClip.IsReloading) {
				PlayerAudioSource.Stop();
			}
        }

		public override void ShootUp(Vector2 dir)
		{
			// 只在有声音播放时才触发结束音效，避免空切音
			if(!PlayerAudioSource.isPlaying) return;
			PlayerAudioSource.Stop();
			PlayerAudioSource.clip = AKShootEnd;
			PlayerAudioSource.Play();
			PlayerAudioSource.loop = false;
		}

		public override void Reload() => _gunClip.Reload(ReloadSound);
	}
}
