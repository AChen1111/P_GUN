using UnityEngine;

/// <summary>
/// 敌人对象池。
/// 按 EnemyBase prefab 分池，适合普通敌人、精英敌人、Boss 等多个 prefab 共用一套池逻辑。
/// </summary>
public class EnemyPool : PoolBase<EnemyBase> {
    public new static EnemyPool Instance {
        get {
            var instance = PoolBase<EnemyBase>.Instance as EnemyPool;
            if (instance == null) {
                var go = new GameObject("[EnemyPool]");
                instance = go.AddComponent<EnemyPool>();
            }
            return instance;
        }
    }

    /// <summary>
    /// 兼容房间里用 GameObject 字段保存敌人 prefab 的写法。
    /// </summary>
    public EnemyBase Get(GameObject prefabObject, Vector3 position, Quaternion rotation, FightRoom ownerFightRoom) {
        if(prefabObject == null) {
            Debug.LogError("EnemyPool.Get failed: prefabObject is null.", this);
            return null;
        }

        var prefab = prefabObject.GetComponent<EnemyBase>();
        if(prefab == null) {
            Debug.LogError($"EnemyPool.Get failed: {prefabObject.name} has no EnemyBase component.", prefabObject);
            return null;
        }

        return Get(prefab, position, rotation, ownerFightRoom);
    }

    /// <summary>
    /// 从指定敌人 prefab 的池中取出敌人，并重置血量、死亡状态、状态机和所属房间。
    /// </summary>
    public EnemyBase Get(EnemyBase prefab, Vector3 position, Quaternion rotation, FightRoom ownerFightRoom) {
        var enemy = Get(prefab, position, rotation);
        if(enemy == null) return null;

        enemy.SetOwnerFightRoom(ownerFightRoom);
        return enemy;
    }
}
