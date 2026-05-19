using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Save
{
    /// <summary>
    /// JSON 存档根数据, 只保存稳定进度和玩家状态.
    /// </summary>
    [Serializable]
    public class GameSaveData
    {
        public int version = 1;
        public string savedAtUtc;
        public string sceneName;
        public string levelGraphAddress;
        public int mapSeed;
        public string currentRoomId;
        public PlayerSaveData player = new PlayerSaveData();
        public List<RoomSaveData> rooms = new List<RoomSaveData>();
    }

    /// <summary>
    /// 玩家快照, 不保存地面掉落物和临时子弹.
    /// </summary>
    [Serializable]
    public class PlayerSaveData
    {
        public Vector3Data position;
        public int hp;
        public int maxHp;
        public int currentGunIndex;
        public List<InventoryStackSaveData> inventory = new List<InventoryStackSaveData>();
        public List<BuffSaveData> buffs = new List<BuffSaveData>();
        public List<WeaponSaveData> weapons = new List<WeaponSaveData>();
    }

    /// <summary>
    /// 房间进度快照, 第一版只保存访问和清房状态.
    /// </summary>
    [Serializable]
    public class RoomSaveData
    {
        public string roomId;
        public string roomType;
        public Vector3Data position;
        public bool visited;
        public bool cleared;
    }

    /// <summary>
    /// 背包堆叠快照, 只记录可稳定查询的 itemId 和数量.
    /// </summary>
    [Serializable]
    public class InventoryStackSaveData
    {
        public int itemId;
        public int count;
    }

    /// <summary>
    /// Buff 快照, 后续恢复时通过 BuffDataBase 按 id 重建运行时实例.
    /// </summary>
    [Serializable]
    public class BuffSaveData
    {
        public int buffId;
        public float remainingTime;
        public int stackCount;
        public bool isPermanent;
    }

    /// <summary>
    /// 武器弹药快照, 只记录当前武器和弹夹/备弹数量.
    /// </summary>
    [Serializable]
    public class WeaponSaveData
    {
        public string weaponId;
        public bool isCurrent;
        public int clipAmmo;
        public int clipMaxAmmo;
        public int bagAmmo;
        public int bagMaxAmmo;
    }

    /// <summary>
    /// JsonUtility 可序列化的 Vector3 结构.
    /// </summary>
    [Serializable]
    public struct Vector3Data
    {
        public float x;
        public float y;
        public float z;

        public Vector3Data(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector3Data FromVector3(Vector3 value)
        {
            return new Vector3Data(value.x, value.y, value.z);
        }

        public Vector3 ToVector3()
        {
            return new Vector3(x, y, z);
        }
    }
}
