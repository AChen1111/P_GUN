using Game.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class BuffTooltipPanel : MonoBehaviour
    {
        [Header("绑定组件")]
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Text titleText;
        [SerializeField] private Text descriptionText;
        [SerializeField] private Vector2 screenOffset = new Vector2(18f, 18f);

        private RectTransform rectTransform;
        private bool isVisible;

        /// <summary>
        /// 初始化运行时依赖.
        /// </summary>
        private void Awake()
        {
            ResolveReferences();
            Hide();
        }

        /// <summary>
        /// 执行每帧更新逻辑.
        /// </summary>
        private void Update()
        {
            if (!isVisible)
            {
                return;
            }

            UpdatePosition(Input.mousePosition);
        }

        /// <summary>
        /// 执行 Show 逻辑.
        /// </summary>
        public void Show(BuffRuntimeInfo info, Vector2 screenPosition)
        {
            if (info == null)
            {
                return;
            }

            ResolveReferences();
            gameObject.SetActive(true);
            isVisible = true;

            if (titleText != null)
            {
                titleText.text = info.Buff.BuffName;
            }

            if (descriptionText != null)
            {
                descriptionText.text = info.Buff.Description;
            }

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1f;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }

            UpdatePosition(screenPosition);
        }

        /// <summary>
        /// 执行 Hide 逻辑.
        /// </summary>
        public void Hide()
        {
            ResolveReferences();
            isVisible = false;

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0f;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }

            gameObject.SetActive(false);
        }

        /// <summary>
        /// 执行 UpdatePosition 逻辑.
        /// </summary>
        private void UpdatePosition(Vector2 screenPosition)
        {
            if (rectTransform == null)
            {
                return;
            }

            // Tooltip 使用屏幕空间 Canvas, 直接跟随鼠标屏幕坐标.
            rectTransform.position = screenPosition + screenOffset;
        }

        /// <summary>
        /// 执行 ResolveReferences 逻辑.
        /// </summary>
        private void ResolveReferences()
        {
            if (rectTransform == null)
            {
                rectTransform = GetComponent<RectTransform>();
            }

            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }
        }
    }
}
