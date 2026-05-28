using Game.Core;
using Game.Gameplay;
using UnityEngine;

namespace Game.UI
{
    /// <summary>
    /// 游戏场景射击准星光标, 战斗时跟随鼠标并在 UI 或自动瞄准接管时隐藏.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class GameplayCursorView : MonoBehaviour
    {
        [Header("光标相机")]
        [SerializeField] private Camera targetCamera;
        [Header("光标渲染器")]
        [SerializeField] private SpriteRenderer cursorRenderer;
        [Header("屏幕像素尺寸")]
        [SerializeField] private float screenSizePixels = 16f;
        [Header("世界坐标深度")]
        [SerializeField] private float cursorWorldZ;

        private void Awake()
        {
            ResolveReferences();
            ValidateConfiguration();
            ApplyPixelScale();
            RefreshVisibleState();
        }

        private void LateUpdate()
        {
            RefreshVisibleState();

            if (!cursorRenderer.enabled)
            {
                return;
            }

            FollowMousePosition();
            ApplyPixelScale();
        }

        private void OnDisable()
        {
            if (cursorRenderer != null)
            {
                cursorRenderer.enabled = false;
            }
        }

        private void ResolveReferences()
        {
            if (cursorRenderer == null)
            {
                cursorRenderer = GetComponent<SpriteRenderer>();
            }

            if (targetCamera == null)
            {
                targetCamera = Camera.main;
            }
        }

        private void ValidateConfiguration()
        {
            if (cursorRenderer == null)
            {
                throw new MissingComponentException($"{nameof(GameplayCursorView)} requires {nameof(SpriteRenderer)}.");
            }

            if (targetCamera == null)
            {
                throw new MissingReferenceException($"{nameof(GameplayCursorView)} requires target camera.");
            }

            if (!targetCamera.orthographic)
            {
                throw new System.InvalidOperationException($"{nameof(GameplayCursorView)} requires an orthographic camera.");
            }
        }

        private void RefreshVisibleState()
        {
            // UI, Ctrl 或自动瞄准接管鼠标目标时隐藏场景准星, 避免两个瞄准提示同时显示.
            cursorRenderer.enabled = !GameplayCursorState.BlocksMouseCombat && !IsAutoAimEnabled();
        }

        private static bool IsAutoAimEnabled()
        {
            Player player = PlayerRegistry.Current;
            return player != null && player.canAutoAim;
        }

        private void FollowMousePosition()
        {
            // 使用主相机把屏幕鼠标位置投到 2D 世界平面, 保持准星中心贴合鼠标.
            Vector3 screenPosition = Input.mousePosition;
            screenPosition.z = cursorWorldZ - targetCamera.transform.position.z;
            Vector3 worldPosition = targetCamera.ScreenToWorldPoint(screenPosition);
            worldPosition.z = cursorWorldZ;
            transform.position = worldPosition;
        }

        private void ApplyPixelScale()
        {
            if (cursorRenderer.sprite == null)
            {
                return;
            }

            float worldHeightPerPixel = targetCamera.orthographicSize * 2f / Mathf.Max(1, Screen.height);
            float desiredWorldSize = Mathf.Max(1f, screenSizePixels) * worldHeightPerPixel;
            float spriteWorldHeight = cursorRenderer.sprite.bounds.size.y;
            float scale = desiredWorldSize / spriteWorldHeight;
            transform.localScale = new Vector3(scale, scale, 1f);
        }
    }
}
