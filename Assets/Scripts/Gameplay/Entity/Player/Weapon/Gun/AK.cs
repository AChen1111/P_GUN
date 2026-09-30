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
    public class AK : Gun
    {
        public SpriteRenderer SR;
        // 配置保存短名, 资源来自阶段预加载缓存.

        [SerializeField, AddressableKey(AddressableAssetKind.Prefab)] public string PlayerBulletKey = string.Empty;
        public PlayerBullet PlayerBullet => AddressableAssetAccess.Component<PlayerBullet>(PlayerBulletKey);
        public UnityEngine.AudioSource SelfAudioSource;
        // 松开扳机的音频与其它武器音频一样通过短名读取.

        [SerializeField, AddressableKey(AddressableAssetKind.AudioClip)] public string AKShootEndKey = string.Empty;
        public AudioClip AKShootEnd => AddressableAssetAccess.Get<AudioClip>(AKShootEndKey);

		public override PlayerBullet BulletPrefab => PlayerBullet;
		public override void OnGunUsed()
		{
			base.OnGunUsed();
			PlayerAudioSource.Stop();
		}
		public override void ShootDown(Vector2 dir)
		{
			if(gunClip.IsOutOfAmmo || !gunClip.CanShoot) return;
			TryPlaySound(true);
		}
        public override void Shooting(Vector2 dir)
        {
			gunClip.CheckAmmo();
			if(shootDuration.CanShoot && gunClip.CanShoot) {
				if(!PlayerAudioSource.isPlaying) {
					TryPlaySound(true);
				}
				shootDuration.RecordShootTime();
				gunClip.Shoot();
				var obj = GetBullet(dir);
				PlayGunFire(dir);
			}
			else if(gunClip.IsOutOfAmmo || gunClip.IsReloading) {
				PlayerAudioSource.Stop();
			}
        }
		public override void ShootUp(Vector2 dir)
		{
			if(!PlayerAudioSource.isPlaying) return;
			TryPlaySound(AKShootEnd,false);
		}
    }
}
