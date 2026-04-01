using UnityEngine;
using QFramework;

namespace QFramework.PG
{
	public partial class WeaponGlobal : ViewController
	{
		public static WeaponGlobal Instance { get; private set; }
		private void Awake() {
			Instance = this;
		}
		public AudioSource WeaponAudioSource;

	}
}
