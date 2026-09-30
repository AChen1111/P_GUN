using Game.Core;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// 枪械通用组件, 具体射击规则由同一预制体上的 LuaComponet 实现.
    /// </summary>
    public sealed class LuaGun : Gun
    {
        // 配置保存短名, 资源来自阶段预加载缓存.

        [SerializeField, AddressableKey(AddressableAssetKind.Prefab)] private string bulletPrefabKey = string.Empty;
        private PlayerBullet bulletPrefab => AddressableAssetAccess.Component<PlayerBullet>(bulletPrefabKey);

        public override PlayerBullet BulletPrefab => bulletPrefab;
    }
}
