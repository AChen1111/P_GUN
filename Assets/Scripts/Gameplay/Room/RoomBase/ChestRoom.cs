using Game.Pooling;
using System.Collections.Generic;
using Game.Items;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// 宝箱房: 玩家进入时把生成点位交给房间物体的 LuaComponet 模块, 由模块决定生成内容.
    /// </summary>
    public class ChestRoom : Room
    {
        [Header("物品生成点")]
        [SerializeField]private Transform[] transforms;
        [Header("Debug")]
        [SerializeField]bool hasDone = false;//是否完成了生成

        protected override void OnRoomInitialized()
        {
            base.OnRoomInitialized();
        }

        protected override void OnPlayerEnteredRoom(Collider2D other)
        {
            if(hasDone)return;

            var roomLua = GetComponent<LuaBehaviourHost>();
            if (roomLua == null)
            {
                Debug.LogError($"{nameof(ChestRoom)}: 房间未挂 LuaComponet, 无法执行进入生成.", this);
                return;
            }

            // 生成点位以世界坐标传给 Lua 模块, 完成标记和读档字段仍留在房间组件.
            var spawnPoints = new List<Vector3>();
            foreach (var point in transforms)
            {
                if (point != null)
                {
                    spawnPoints.Add(point.position);
                }
            }

            roomLua.SetLuaField("spawnPoints", spawnPoints);
            roomLua.CallLuaFunction("OnPlayerEnteredRoom");
        }

        protected override void OnPlayerExitedRoom(Collider2D other)
        {
            hasDone = true;
        }
    }
}