using UnityEngine;
using QFramework;

namespace QFramework.PG
{
	public partial class Bow : QFramework.PG.Gun
	{
		public override PlayerBullet BulletPrefab => PlayerBullet;
		
		public SpriteRenderer ArrowSpriteRenderer => Arrow;

        public override BulletBag BulletBag => new BulletBag(-1);

        private bool mPressing = false;

		public override void Shoot(Vector2 dir)
		{
			var obj = GetBullet(dir);
			obj.transform.right = dir;
			TryPlaySound(false);
		}

		public override void ShootDown(Vector2 dir)
		{
			mPressing = true;
			mPressingTime = 0f;
			ArrowSpriteRenderer.enabled = false;
		}

		//蓄力时间计时器
		private float mPressingTime = 0f;
		public override void Shooting(Vector2 dir)
		{
			if(mPressing)
			{
				mPressingTime += Time.deltaTime;
			}
			//箭头显示
			if(mPressingTime >= 0.5f)
			{
				ArrowSpriteRenderer.enabled = true;
			}
			else
			{
				ArrowSpriteRenderer.enabled = false;
			}
		}

		public override void ShootUp(Vector2 dir)
		{
			//抬起时判断是否蓄力完成
			if(mPressing && mPressingTime > 0.5f)
			{
				Shoot(dir);
				mPressingTime = 0f;
				ArrowSpriteRenderer.enabled = false;
			}
			mPressing = false;
		}
	}
}
