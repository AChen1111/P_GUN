using UnityEngine;
using QFramework;

namespace QFramework.PG
{
	public partial class RocketGun : QFramework.PG.Gun
	{
		public override PlayerBullet BulletPrefab => PlayerBullet;
		
		public override UnityEngine.AudioSource PlayerAudioSource => SelfAudioSource;

		[SerializeField] private float _duration = 1f;
		private ShootDuration _shootDuration;

		private void Awake() => _shootDuration = new ShootDuration(_duration);

		public override void Shoot(Vector2 dir)
		{
			if(!_shootDuration.CanShoot) return;
			_shootDuration.RecordShootTime();
			var obj = Instantiate(BulletPrefab);
			obj.transform.position = transform.position;
			obj.dir = dir;
			obj.transform.right = dir;
			obj.gameObject.SetActive(true);
			SelfAudioSource.PlayOneShot(shootSounds[Random.Range(0, shootSounds.Count)]);
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
