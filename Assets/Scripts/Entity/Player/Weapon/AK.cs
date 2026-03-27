using UnityEngine;
using QFramework;

namespace QFramework.PG
{
	public partial class AK : QFramework.PG.Gun
	{
		public override PlayerBullet BulletPrefab => PlayerBullet;
		public override UnityEngine.AudioSource PlayerAudioSource => SelfAudioSource;

		private float timer = 0f;
		private float timerMax = 0.1f;

		public override void ShootDown(Vector2 dir)
		{	
			PlayerAudioSource.clip = shootSounds[0];
			PlayerAudioSource.loop = true;
			PlayerAudioSource.Play();

		}
        public override void Shooting(Vector2 dir)
        {


			if(timer >= timerMax) {
				timer = 0f;
				//实例化子弹
				var obj = Instantiate(BulletPrefab);
				obj.transform.position = transform.position;

				//设置子弹方向
				obj.dir = dir;
				obj.gameObject.SetActive(true);

				//播放射击音效
				
			}
			timer += Time.deltaTime;
        }
		public override void ShootUp(Vector2 dir)
		{
			PlayerAudioSource.Stop();
			PlayerAudioSource.clip = AKShootEnd;
			PlayerAudioSource.Play();
			PlayerAudioSource.loop = false;
		}
	}
}
