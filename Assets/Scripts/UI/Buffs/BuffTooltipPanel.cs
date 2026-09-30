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
            // 场景中提示对象默认禁用; 首次 Show 激活时不能在 Awake 再关闭自身.
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0f;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }
        }
        private void Update()
        {
            if (!isVisible)
            {
                return;
            }

            UpdatePosition(Input.mousePosition);
        }
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
                titleText.text = info.Config.Name;
            }

            if (descriptionText != null)
            {
                descriptionText.text = info.Config.Description;
            }

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1f;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }

            UpdatePosition(screenPosition);
        }
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
        private void UpdatePosition(Vector2 screenPosition)
        {
            if (rectTransform == null)
            {
                return;
            }

            // GameUI 使用 Screen Space Camera, 先将屏幕坐标换算到父级 RectTransform 的局部坐标.
            var parentRect = (RectTransform)rectTransform.parent;
            var canvas = GetComponentInParent<Canvas>();
            var eventCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRect, screenPosition + screenOffset, eventCamera, out var localPoint);
            rectTransform.position = parentRect.TransformPoint(localPoint);
        }
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
