using UnityEngine;
using QFramework;

namespace Game.Gameplay
{
    public class WeaponGlobal : ViewController
    {
        public SpriteRenderer GunFire;

		public static WeaponGlobal Instance { get; private set; }
		/// <summary>
		/// 初始化运行时依赖.
		/// </summary>
		private void Awake() {
			Instance = this;
		}
		public AudioSource WeaponAudioSource;

        public void PlayGunFire(Vector2 position, Vector2 direction)
        {
            GunFire.Position2D(position);
            GunFire.transform.right = direction;
            GunFire.Show();

            // 枪口火光只显示短暂数帧, 具体 SpriteRenderer 由场景 WeaponGlobal 绑定.
            ActionKit.DelayFrame(3, () => GunFire.Hide()).StartCurrentScene();
        }
    }
}
