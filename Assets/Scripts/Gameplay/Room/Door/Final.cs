using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    public class Final : MonoBehaviour {
        void Reset() {
            var collider = gameObject.AddComponent<BoxCollider2D>();
            collider.isTrigger = true;

        }
        void OnTriggerEnter2D(Collider2D other) {
            if(other.CompareTag("Player")) {
                EventCenter.Trigger(GameEvent.GameWin);
            }
        }
    }
}
