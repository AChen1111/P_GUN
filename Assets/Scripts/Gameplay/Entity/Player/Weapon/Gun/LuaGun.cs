using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// 枪械通用组件, 具体射击规则由同一预制体上的 LuaComponet 实现.
    /// </summary>
    public sealed class LuaGun : Gun
    {
        [SerializeField] private PlayerBullet bulletPrefab;

        public override PlayerBullet BulletPrefab => bulletPrefab;
    }
}
