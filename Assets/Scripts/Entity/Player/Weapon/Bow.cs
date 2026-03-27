using UnityEngine;
using QFramework;

namespace QFramework.PG
{
	public partial class Bow : QFramework.PG.Gun
	{
		public override PlayerBullet BulletPrefab => PlayerBullet;
		
		public override UnityEngine.AudioSource PlayerAudioSource => SelfAudioSource;
		public SpriteRenderer ArrowSpriteRenderer => Arrow;

		private bool mPressing = false;

		public override void Shoot(Vector2 dir)
		{
			var obj = Instantiate(BulletPrefab);
			obj.transform.position = transform.position;
			obj.dir = dir;
			obj.transform.right = dir;
			obj.gameObject.SetActive(true);
			SelfAudioSource.PlayOneShot(shootSounds[Random.Range(0, shootSounds.Count)]);
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
