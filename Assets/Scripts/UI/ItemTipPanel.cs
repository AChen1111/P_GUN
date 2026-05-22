using UnityEngine;
using UnityEngine.UI;
using Game.Core;
using Game.Items;
using Game.Gameplay;

namespace Game.UI
{
    [DisallowMultipleComponent]
    public class ItemTipPanel : MonoBehaviour
    {
        private const string DefaultPromptText = "[F] 拾取";

        [SerializeField] private Image iconImage;
        [SerializeField] private Text itemNameText;
        [SerializeField] private Text descriptionText;
        [SerializeField] private Text promptText;
        [SerializeField] private CanvasGroup canvasGroup;

        /// <summary>
        /// 初始化运行时依赖.
        /// </summary>
        private void Awake()
        {
            ResolveReferences();
            Hide();
        }

        /// <summary>
        /// 重置编辑器默认配置.
        /// </summary>
        private void Reset()
        {
            ResolveReferences();
        }
        public void Show(ItemData data)
        {
            ResolveReferences();
            gameObject.SetActive(true);

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
            }

            if (iconImage != null)
            {
                iconImage.sprite = data.icon;
                iconImage.enabled = data.icon != null;
            }

            if (itemNameText != null)
            {
                itemNameText.text = string.IsNullOrEmpty(data.itemName) ? $"Item {data.itemId}" : data.itemName;
            }

            if (descriptionText != null)
            {
                descriptionText.text = string.IsNullOrEmpty(data.description) ? " " : data.description;
            }

            if (promptText != null)
            {
                promptText.text = DefaultPromptText;
            }
        }
        public void Hide()
        {
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0f;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
            }

            gameObject.SetActive(false);
        }
        private void ResolveReferences()
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }
        }

    }
}
