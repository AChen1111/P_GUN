using UnityEngine;
using QFramework;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    public class Laser : Gun
    {
        public PlayerBullet PlayerBullet;
        public UnityEngine.AudioSource SelfAudioSource;
        public UnityEngine.LineRenderer SelfLineRenderer;

		public override PlayerBullet BulletPrefab => PlayerBullet;
		public LineRenderer LineRenderer => SelfLineRenderer;
		/// <summary>
		/// 激光最大距离
		/// </summary>
		[SerializeField] private float maxLaserDistance = 100f;
		private float mLastDamageTime;

        public override BulletBag BulletBag => new BulletBag(-1);

        public override void ShootDown(Vector2 dir)
		{
			LineRenderer.enabled = true;
			mLastDamageTime = Time.time - shootInterval;
			//播放音效
			TryPlaySound(true);
		}
		public override void Shooting(Vector2 dir)
		{
			var origin = FirePointPosition;
			//获取墙体和敌人的layerMask
			var layerMask = LayerMask.GetMask("Wall","EnemyLayer");
			//得到碰撞点
			var hit = Physics2D.Raycast(origin, dir, maxLaserDistance, layerMask);
			//设置起始点
			LineRenderer.SetPosition(0, origin);

			if(hit.collider != null)
			{
				LineRenderer.SetPosition(1, hit.point);

				var enemy = hit.collider.GetComponent<EnemyBase>();
				if(enemy != null && Time.time - mLastDamageTime >= shootInterval)
				{
					mLastDamageTime = Time.time;

					DamageInfo damageInfo = new DamageInfo(Damage, dir);

					enemy.Hurt(damageInfo);
				}
				return;
			}

			LineRenderer.SetPosition(1, origin + dir * maxLaserDistance);
		}

		public override void ShootUp(Vector2 dir)
		{
			LineRenderer.enabled = false;
			PlayerAudioSource.Stop();
		}
    }
}
