using UnityEngine;
using QFramework;

namespace QFramework.PG
{
	public partial class Laser : QFramework.PG.Gun
	{
		public override PlayerBullet BulletPrefab => PlayerBullet;
		public LineRenderer LineRenderer => SelfLineRenderer;


		public override void ShootDown(Vector2 dir)
		{
			LineRenderer.enabled = true;
			//播放音效
			TryPlaySound(true);
		}
		public override void Shooting(Vector2 dir)
		{
			var obj = GetBullet(dir);

			//获取墙体和敌人的layerMask
			var layerMask = LayerMask.GetMask("Default","Enemy");
			//得到碰撞点
			var hit = Physics2D.Raycast(transform.position, dir, float.MaxValue, layerMask);
			//设置起始点
			LineRenderer.SetPosition(0, PlayerBullet.Position2D());
			//设置终点
			LineRenderer.SetPosition(1, hit.point);
		}

		public override void ShootUp(Vector2 dir)
		{
			LineRenderer.enabled = false;
			PlayerAudioSource.Stop();
		}
	}
}
