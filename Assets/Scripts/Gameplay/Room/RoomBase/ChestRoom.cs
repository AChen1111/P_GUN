using Game.Items;
using PlasticGui;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// 休息房：提供物品生成和其他非战斗功能的房间类型。
    /// </summary>
    public class ChestRoom : Room
    {
        [Header("物品生成点")]
        [SerializeField]private Transform[] transforms;
        [Header("Debug")]
        [SerializeField]bool hasDone = false;
        protected override void OnRoomInitialized()
        {
            base.OnRoomInitialized();
        }
        protected override void OnPlayerEnteredRoom(Collider2D other)
        {
            if(hasDone)return;
            foreach(var pos in transforms)
            {
                itemSpawner.SpawnItem(pos.transform.position);
            }
        }
        protected override void OnPlayerExitedRoom(Collider2D other)
        {
            hasDone = true;
        }
    }
}