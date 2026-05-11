using UnityEngine;

/// <summary>
/// 玩家子弹对象池。
/// 这里按 PlayerBullet prefab 分池，避免手枪、火箭、弓箭等不同子弹外观或参数互相混用。
/// </summary>
public class PlayerBulletPool : PoolBase<PlayerBullet> {
    public new static PlayerBulletPool Instance {
        get {
            var instance = PoolBase<PlayerBullet>.Instance as PlayerBulletPool;
            if(instance == null) {
                var go = new GameObject("[PlayerBulletPool]");
                instance = go.AddComponent<PlayerBulletPool>();
            }

            return instance;
        }
    }

    /// <summary>
    /// 从对应 prefab 的池中取出一颗子弹，并完成本次发射所需的运行时初始化。
    /// </summary>
    public PlayerBullet Get(PlayerBullet prefab, Vector3 position, Quaternion rotation, Vector2 dir, int damage) {
        var bullet = Get(prefab, position, rotation);
        if(bullet == null) return null;

        bullet.Init(dir, damage);
        return bullet;
    }

}
