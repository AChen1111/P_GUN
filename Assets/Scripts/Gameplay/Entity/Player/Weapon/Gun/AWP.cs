using UnityEngine;
using QFramework;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    public class AWP : Gun
    {
        public PlayerBullet PlayerBullet;
        public UnityEngine.AudioSource SelfAudioSource;

		public override PlayerBullet BulletPrefab => PlayerBullet;

		/// <summary>
		/// 执行 Shoot 逻辑.
		/// </summary>
		public override void Shoot(Vector2 dir)
		{
			gunClip.CheckAmmo();
			if(!shootDuration.CanShoot || !gunClip.CanShoot) return;
			shootDuration.RecordShootTime();
			gunClip.Shoot();
			var obj = GetBullet(dir);
			gunFireEffect.Show(FirePointPosition, dir);
			TryPlaySound(false);
		}

		/// <summary>
		/// 执行 ShootDown 逻辑.
		/// </summary>
		public override void ShootDown(Vector2 dir)
        {
			Shoot(dir);
        }

        /// <summary>
        /// 执行 Shooting 逻辑.
        /// </summary>
        public override void Shooting(Vector2 dir)
        {
            Shoot(dir);
        }
    }
}
