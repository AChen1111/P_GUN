using Game.Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI
{
    /// <summary>
    /// 背包物品格, 负责展示堆叠数量并把鼠标操作转交给面板.
    /// </summary>
    public class InventorySlotView : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
    {
        [Header("绑定组件")]
        [SerializeField] private Image iconImage;
        [SerializeField] private Text countText;
        [SerializeField] private Image selectedFrame;

        private InventoryPanel owner;
        private InventoryItemStack stack;

        public static InventorySlotView CreateDefault(Transform parent)
        {
            var slotObject = new GameObject("InventorySlot", typeof(RectTransform), typeof(Image), typeof(InventorySlotView));
            slotObject.transform.SetParent(parent, false);

            var rect = slotObject.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(84f, 84f);

            var background = slotObject.GetComponent<Image>();
            background.color = new Color(0.12f, 0.13f, 0.15f, 0.95f);
            background.raycastTarget = true;

            var slot = slotObject.GetComponent<InventorySlotView>();
            slot.selectedFrame = CreateImage("Img_SelectedFrame", slotObject.transform, Vector2.zero, new Vector2(84f, 84f), new Color(1f, 0.8f, 0.18f, 0.32f));
            slot.selectedFrame.enabled = false;

            slot.iconImage = CreateImage("Img_Icon", slotObject.transform, new Vector2(0f, 4f), new Vector2(58f, 58f), Color.white);

            slot.countText = CreateText("Txt_Count", slotObject.transform, 18, FontStyle.Bold, TextAnchor.LowerRight);
            slot.countText.rectTransform.anchorMin = Vector2.zero;
            slot.countText.rectTransform.anchorMax = Vector2.one;
            slot.countText.rectTransform.offsetMin = new Vector2(6f, 4f);
            slot.countText.rectTransform.offsetMax = new Vector2(-8f, -4f);

            return slot;
        }

        public void Configure(InventoryItemStack itemStack, InventoryPanel panel)
        {
            stack = itemStack;
            owner = panel;
            Refresh();
        }

        public void SetSelected(bool selected)
        {
            if (selectedFrame != null)
            {
                selectedFrame.enabled = selected;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (stack != null)
            {
                owner?.SelectStack(stack);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (stack == null)
            {
                return;
            }

            if (eventData.button == PointerEventData.InputButton.Right)
            {
                owner?.UseStack(stack);
                return;
            }

            if (eventData.button == PointerEventData.InputButton.Left)
            {
                owner?.SelectStack(stack);
            }
        }

        private void Refresh()
        {
            if (stack == null)
            {
                return;
            }

            if (iconImage != null)
            {
                iconImage.sprite = stack.Data.icon;
                iconImage.enabled = stack.Data.icon != null;
            }

            if (countText != null)
            {
                countText.text = Mathf.Max(0, stack.Count).ToString();
            }
        }

        private static Image CreateImage(string name, Transform parent, Vector2 anchoredPosition, Vector2 size, Color color)
        {
            var imageObject = new GameObject(name, typeof(RectTransform), typeof(Image));
            imageObject.transform.SetParent(parent, false);

            var rect = imageObject.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = anchoredPosition;
            rect.sizeDelta = size;

            var image = imageObject.GetComponent<Image>();
            image.color = color;
            image.raycastTarget = false;
            return image;
        }

        private static Text CreateText(string name, Transform parent, int fontSize, FontStyle fontStyle, TextAnchor alignment)
        {
            var textObject = new GameObject(name, typeof(RectTransform), typeof(Text));
            textObject.transform.SetParent(parent, false);

            var text = textObject.GetComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            text.fontSize = fontSize;
            text.fontStyle = fontStyle;
            text.alignment = alignment;
            text.color = Color.white;
            text.raycastTarget = false;
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
            text.verticalOverflow = VerticalWrapMode.Truncate;
            return text;
        }
    }
}
