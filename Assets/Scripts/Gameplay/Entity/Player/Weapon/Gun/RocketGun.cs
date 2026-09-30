using UnityEngine.Serialization;
using UnityEngine;
using QFramework;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    public class RocketGun : Gun
    {
        // 配置保存短名, 资源来自阶段预加载缓存.
        [SerializeField, AddressableKey(AddressableAssetKind.Prefab)] public string PlayerBulletKey = string.Empty;
        public PlayerBullet PlayerBullet => AddressableAssetAccess.Component<PlayerBullet>(PlayerBulletKey);
        public UnityEngine.AudioSource SelfAudioSource;

		public override PlayerBullet BulletPrefab => PlayerBullet;
		public override void Shoot(Vector2 dir)
		{
			gunClip.CheckAmmo();
			if(!shootDuration.CanShoot || !gunClip.CanShoot) return;
			shootDuration.RecordShootTime();
			gunClip.Shoot();
			var obj = GetBullet(dir);
			if(obj == null) return;

			obj.transform.right = dir;
			PlayGunFire(dir);
			TryPlaySound(false);
		}
		public override void ShootDown(Vector2 dir)
        {
			Shoot(dir);
        }
        public override void Shooting(Vector2 dir)
        {
            Shoot(dir);
        }
    }
}
