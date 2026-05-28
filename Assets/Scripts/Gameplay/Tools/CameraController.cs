using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    public class CameraController : MonoBehaviour {
        [Header("跟随设置")]
        public Vector3 offset = new Vector3(0f, 0f, -10f);
        [Min(0.01f)] public float smoothTime = 0.2f;

        private Vector3 velocity;
        private void LateUpdate() {
            if(PlayerRegistry.Current == null) return;

            Vector3 targetPos = PlayerRegistry.Current.transform.position + offset;
            // 相机跟随使用未缩放时间, 避免冲刺慢动作期间相机刷新变慢造成画面卡顿.
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime, Mathf.Infinity, Time.unscaledDeltaTime);
        }
    }
}
