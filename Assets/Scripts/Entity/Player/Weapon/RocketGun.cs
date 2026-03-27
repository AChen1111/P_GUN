using UnityEngine;
using QFramework;
using System.Collections;

namespace QFramework.PG
{
	public partial class RocketGun : QFramework.PG.Gun
	{
		public override PlayerBullet BulletPrefab => PlayerBullet;
		
		public override UnityEngine.AudioSource PlayerAudioSource => SelfAudioSource;
		
		private float _duration = 1f;
		private bool _canShoot = true;


		public override void Shoot(Vector2 dir)
		{
			if(!_canShoot) return;
			StartCoroutine(ShootCoroutine());
			var obj = Instantiate(BulletPrefab);
			obj.transform.position = transform.position;
			obj.dir = dir;
			obj.transform.right = dir;
			obj.gameObject.SetActive(true);
			SelfAudioSource.PlayOneShot(shootSounds[Random.Range(0, shootSounds.Count)]);
		}

		IEnumerator ShootCoroutine()
		{
			_canShoot = false;
			yield return new WaitForSeconds(_duration);
			_canShoot = true;
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
