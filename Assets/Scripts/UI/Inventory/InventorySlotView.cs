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
        public void Configure(InventoryItemStack itemStack, InventoryPanel panel)
        {
            stack = itemStack;
            owner = panel;
            Refresh();

            void Refresh()
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
    }
}
