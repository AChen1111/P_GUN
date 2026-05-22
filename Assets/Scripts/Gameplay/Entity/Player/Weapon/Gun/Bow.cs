using UnityEngine;
using QFramework;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    public class Bow : Gun
    {
        public PlayerBullet PlayerBullet;
        public SpriteRenderer Arrow;
        public UnityEngine.AudioSource SelfAudioSource;

		public override PlayerBullet BulletPrefab => PlayerBullet;

		public SpriteRenderer ArrowSpriteRenderer => Arrow;

        public override BulletBag BulletBag => new BulletBag(-1);

        private bool mPressing = false;
		public override void Shoot(Vector2 dir)
		{
			var obj = GetBullet(dir);
			if(obj == null) return;

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
