using UnityEngine;
using QFramework;

namespace QFramework.PG
{
	public partial class Laser : QFramework.PG.Gun
	{
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
			var origin = BulletPrefab.Position2D();
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
					
					DamageInfo damageInfo = new DamageInfo();
					damageInfo.Damage = Damage;

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
