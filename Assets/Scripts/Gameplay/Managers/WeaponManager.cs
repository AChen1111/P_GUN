using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;

namespace Game.Gameplay
{
    /// <summary>
    /// 场景武器与子弹中转, 玩家, 敌人和 Lua 通过它取枪, 生成和回收子弹.
    /// 摆放在游戏场景中, 与对象池和 WeaponGlobal 放在一起, 不在代码里创建.
    /// </summary>
    public sealed class WeaponManager : MonoBehaviour
    {
        private readonly List<Gun> registeredGuns = new List<Gun>();
        private int currentGunIndex = -1;

        public static WeaponManager Instance { get; private set; }

        /// <summary>
        /// 初始化运行时依赖.
        /// </summary>
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        /// <summary>
        /// 释放销毁时持有的运行时状态.
        /// </summary>
        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        /// <summary>
        /// 玩家武器装载完成后登记枪械列表, 并同步当前枪索引.
        /// </summary>
        /// <param name="guns">已装载的枪械列表.</param>
        /// <param name="currentIndex">当前枪索引.</param>
        public void RegisterPlayerGuns(IReadOnlyList<Gun> guns, int currentIndex)
        {
            RequireInstance();

            registeredGuns.Clear();
            if (guns != null)
            {
                for (var i = 0; i < guns.Count; i++)
                {
                    registeredGuns.Add(guns[i]);
                }
            }

            SetCurrentGunIndex(currentIndex);
        }

        /// <summary>
        /// 同步当前枪索引, 索引超出范围时直接报错.
        /// </summary>
        public void SetCurrentGunIndex(int index)
        {
            RequireInstance();

            if (registeredGuns.Count == 0)
            {
                currentGunIndex = -1;
                return;
            }

            if (index < 0 || index >= registeredGuns.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"当前枪索引越界: {index}, 共 {registeredGuns.Count} 把.");
            }

            currentGunIndex = index;
        }

        /// <summary>
        /// 获取当前枪, 装载未完成时直接报错.
        /// </summary>
        public Gun GetCurrentGun()
        {
            RequireInstance();

            if (currentGunIndex < 0 || currentGunIndex >= registeredGuns.Count)
            {
                throw new InvalidOperationException("玩家武器尚未装载完成, 无法取当前枪.");
            }

            return registeredGuns[currentGunIndex];
        }

        /// <summary>
        /// 按 weaponId 取已登记的枪, 未登记时直接报错.
        /// </summary>
        public Gun GetGun(string weaponId)
        {
            RequireInstance();

            for (var i = 0; i < registeredGuns.Count; i++)
            {
                if (registeredGuns[i] != null && registeredGuns[i].WeaponId == weaponId)
                {
                    return registeredGuns[i];
                }
            }

            throw new InvalidOperationException($"未登记武器: {weaponId}.");
        }

        /// <summary>
        /// 生成一颗玩家子弹, 只有本管理器可以访问 PlayerBulletPool.
        /// </summary>
        public PlayerBullet SpawnPlayerBullet(PlayerBullet prefab, Vector3 position, Quaternion rotation, Vector2 dir, int damage, int bulletSpeed)
        {
            RequireInstance();

            var pool = PlayerBulletPool.Instance;
            if (pool == null)
            {
                throw new InvalidOperationException($"{nameof(PlayerBulletPool)} 必须摆放在游戏场景中.");
            }

            if (prefab == null)
            {
                throw new ArgumentNullException(nameof(prefab));
            }

            return pool.Get(prefab, position, rotation, dir, damage, bulletSpeed);
        }

        /// <summary>
        /// 生成一颗敌人子弹, 方向由调用方给出, 不在管理器里指向玩家.
        /// </summary>
        public EnemyBullet SpawnEnemyBullet(EnemyBullet prefab, Vector3 position, Vector2 dir, int damage)
        {
            RequireInstance();

            var pool = EnemyBulletPool.Instance;
            if (pool == null)
            {
                throw new InvalidOperationException($"{nameof(EnemyBulletPool)} 必须摆放在游戏场景中.");
            }

            if (prefab == null)
            {
                throw new ArgumentNullException(nameof(prefab));
            }

            return pool.Get(prefab, position, Quaternion.identity, dir, damage);
        }

        /// <summary>
        /// 生成一颗敌人子弹, 兼容敌人预制体用 GameObject 保存子弹的写法.
        /// </summary>
        public EnemyBullet SpawnEnemyBullet(GameObject prefabObject, Vector3 position, Vector2 dir, int damage)
        {
            var prefab = prefabObject != null ? prefabObject.GetComponent<EnemyBullet>() : null;
            if (prefab == null)
            {
                throw new InvalidOperationException($"敌人子弹预制体缺少 {nameof(EnemyBullet)} 组件: {prefabObject?.name}.");
            }

            return SpawnEnemyBullet(prefab, position, dir, damage);
        }

        /// <summary>
        /// 回收玩家子弹.
        /// </summary>
        public void ReleasePlayerBullet(PlayerBullet bullet)
        {
            RequireInstance();

            var pool = PlayerBulletPool.Instance;
            if (pool == null)
            {
                throw new InvalidOperationException($"{nameof(PlayerBulletPool)} 必须摆放在游戏场景中.");
            }

            pool.Release(bullet);
        }

        /// <summary>
        /// 回收敌人子弹.
        /// </summary>
        public void ReleaseEnemyBullet(EnemyBullet bullet)
        {
            RequireInstance();

            var pool = EnemyBulletPool.Instance;
            if (pool == null)
            {
                throw new InvalidOperationException($"{nameof(EnemyBulletPool)} 必须摆放在游戏场景中.");
            }

            pool.Release(bullet);
        }

        /// <summary>
        /// 播放枪口火光, 音源和贴图仍由 WeaponGlobal 持有.
        /// </summary>
        public void PlayGunFire(Vector2 position, Vector2 direction)
        {
            RequireInstance();

            var global = WeaponGlobal.Instance;
            if (global == null)
            {
                throw new InvalidOperationException($"{nameof(WeaponGlobal)} 必须摆放在游戏场景中.");
            }

            global.PlayGunFire(position, direction);
        }

        /// <summary>
        /// 校验本管理器已摆放在场景中.
        /// </summary>
        private static void RequireInstance()
        {
            if (Instance == null)
            {
                throw new InvalidOperationException($"{nameof(WeaponManager)} 未摆放在游戏场景中.");
            }
        }
    }
}