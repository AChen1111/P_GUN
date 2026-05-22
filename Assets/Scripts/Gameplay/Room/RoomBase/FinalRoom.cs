using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;
using Game.Gameplay.Save;

namespace Game.Gameplay
{
    public class FinalRoom : Room
    {
        [Header("最终房间贴图")]
        [SerializeField]private SpriteRenderer finalSR;
        override protected void OnRoomInitialized()
        {
            needGenerateDoors = true;
            finalSR.gameObject.SetActive(false);
        }
        protected override void OnPlayerEnteredRoom(Collider2D other)
        {
            finalSR.gameObject.SetActive(true);
        }
        public override void RestoreSaveData(RoomSaveData data)
        {
            base.RestoreSaveData(data);

            if (finalSR != null)
            {
                // 最终房间贴图只依赖是否到访.
                finalSR.gameObject.SetActive(data != null && data.visited);
            }
        }
    }
}
