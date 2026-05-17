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

        private void Awake()
        {
            ResolveReferences();
            Hide();
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

            // Tooltip 使用屏幕空间 Canvas, 直接跟随鼠标屏幕坐标.
            rectTransform.position = screenPosition + screenOffset;
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
