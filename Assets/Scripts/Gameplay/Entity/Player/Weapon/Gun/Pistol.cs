using UnityEngine.Serialization;
using QFramework;
using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    public class Pistol : Gun
    {
        public SpriteRenderer SR;
        // 配置保存短名, 资源来自阶段预加载缓存.

        [SerializeField, AddressableKey(AddressableAssetKind.Prefab)] public string PlayerBulletKey = string.Empty;
        public PlayerBullet PlayerBullet => AddressableAssetAccess.Component<PlayerBullet>(PlayerBulletKey);
        public UnityEngine.AudioSource SelfAudioSource;

		public override PlayerBullet BulletPrefab => PlayerBullet;
		public override void Shoot(Vector2 dir)
		{
			GetBullet(dir);
			TryPlaySound(false);
			PlayGunFire(dir);
		}
		public override void ShootDown(Vector2 dir)
		{
			gunClip.CheckAmmo();
			if(gunClip.CanShoot)
			{
				gunClip.Shoot();
				Shoot(dir);
			}
		}
    }
}
