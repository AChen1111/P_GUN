using UnityEngine;
using QFramework;

namespace QFramework.PG
{
	public partial class MP5 : Gun
	{
		public override PlayerBullet BulletPrefab => PlayerBullet;
		public override AudioSource PlayerAudioSource => SelfAudioSource;
		[SerializeField] private float _duration = 0.1f;
		private ShootDuration _shootDuration;

		private void Awake() => _shootDuration = new ShootDuration(_duration);

		public override void ShootDown(Vector2 dir)
		{	
			PlayerAudioSource.clip = shootSounds[0];
			PlayerAudioSource.loop = true;
			PlayerAudioSource.Play();
		}
        public override void Shooting(Vector2 dir)
        {
			if(_shootDuration.CanShoot) {
				_shootDuration.RecordShootTime();
				var obj = Instantiate(BulletPrefab);
				obj.transform.position = transform.position;
				obj.dir = dir;
				obj.gameObject.SetActive(true);
			}
        }
		public override void ShootUp(Vector2 dir)
		{
			PlayerAudioSource.Stop();
		}
	}
}
