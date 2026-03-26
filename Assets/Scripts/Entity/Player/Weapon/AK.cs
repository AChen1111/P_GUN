using UnityEngine;
using QFramework;

namespace QFramework.PG
{
	public partial class AK : QFramework.PG.Gun
	{
		public override PlayerBullet BulletPrefab { get; }
		
		public override UnityEngine.AudioSource PlayerAudioSource { get; }
		
	}
}
