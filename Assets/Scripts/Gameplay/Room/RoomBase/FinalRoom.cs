using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

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
    }
}
