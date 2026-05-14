using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    /// <summary>
    /// 敌人子弹对象池。
    /// 继承 PoolBase 后可以按不同 EnemyBullet prefab 自动拆分多个 ObjectPool。
    /// </summary>
    public class EnemyBulletPool : PoolBase<EnemyBullet> {
        public new static EnemyBulletPool Instance {
            get {
                var instance = PoolBase<EnemyBullet>.Instance as EnemyBulletPool;
                if (instance == null) {
                    var go = new GameObject("[EnemyBulletPool]");
                    instance = go.AddComponent<EnemyBulletPool>();
                }
                return instance;
            }
        }

        /// <summary>
        /// 兼容当前 EnemyA 使用 GameObject 字段保存子弹 prefab 的写法。
        /// </summary>
        public EnemyBullet Get(GameObject prefabObject, Vector3 position, Quaternion rotation, Vector2 dir) {
            if(prefabObject == null) {
                Debug.LogError("EnemyBulletPool.Get failed: prefabObject is null.", this);
                return null;
            }

            var prefab = prefabObject.GetComponent<EnemyBullet>();
            if(prefab == null) {
                Debug.LogError($"EnemyBulletPool.Get failed: {prefabObject.name} has no EnemyBullet component.", prefabObject);
                return null;
            }

            return Get(prefab, position, rotation, dir);
        }

        /// <summary>
        /// 从指定 prefab 的池中取出敌人子弹，并初始化本次发射方向。
        /// </summary>
        public EnemyBullet Get(EnemyBullet prefab, Vector3 position, Quaternion rotation, Vector2 dir) {
            var bullet = Get(prefab, position, rotation);
            if(bullet == null) return null;

            bullet.Init(dir);
            return bullet;
        }

    }
}
