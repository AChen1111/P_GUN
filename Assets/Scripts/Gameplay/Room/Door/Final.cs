using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Final : MonoBehaviour {
        /// <summary>
        /// 重置编辑器默认配置.
        /// </summary>
        void Reset() {
            var collider = GetComponent<BoxCollider2D>();
            collider.isTrigger = true;

        }
        /// <summary>
        /// 处理 2D 触发进入事件.
        /// </summary>
        void OnTriggerEnter2D(Collider2D other) {
            if(other.CompareTag("Player")) {
                EventCenter.Trigger(CoreEvents.GameWin);
            }
        }
    }
}
