using UnityEngine;
using QFramework;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    public class AK : Gun
    {
        public SpriteRenderer SR;
        public PlayerBullet PlayerBullet;
        public UnityEngine.AudioSource SelfAudioSource;
        public UnityEngine.AudioClip AKShootEnd;

		public override PlayerBullet BulletPrefab => PlayerBullet;

		/// <summary>
		/// 执行 OnGunUsed 逻辑.
		/// </summary>
		public override void OnGunUsed()
		{
			base.OnGunUsed();
			PlayerAudioSource.Stop();
		}

		/// <summary>
		/// 执行 ShootDown 逻辑.
		/// </summary>
		public override void ShootDown(Vector2 dir)
		{
			if(gunClip.IsOutOfAmmo || !gunClip.CanShoot) return;
			TryPlaySound(true);
		}

        /// <summary>
        /// 执行 Shooting 逻辑.
        /// </summary>
        public override void Shooting(Vector2 dir)
        {
			gunClip.CheckAmmo();
			if(shootDuration.CanShoot && gunClip.CanShoot) {
				if(!PlayerAudioSource.isPlaying) {
					TryPlaySound(true);
				}
				shootDuration.RecordShootTime();
				gunClip.Shoot();
				var obj = GetBullet(dir);
				gunFireEffect.Show(FirePointPosition, dir);
			}
			else if(gunClip.IsOutOfAmmo || gunClip.IsReloading) {
				PlayerAudioSource.Stop();
			}
        }

		/// <summary>
		/// 执行 ShootUp 逻辑.
		/// </summary>
		public override void ShootUp(Vector2 dir)
		{
			if(!PlayerAudioSource.isPlaying) return;
			TryPlaySound(AKShootEnd,false);
		}
    }
}
