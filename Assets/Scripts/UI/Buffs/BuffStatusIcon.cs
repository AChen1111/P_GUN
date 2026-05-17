using Game.Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI
{
    public class BuffStatusIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("绑定组件")]
        [SerializeField] private Image iconImage;
        [SerializeField] private Text stackOrTimeText;

        private BuffRuntimeInfo runtimeInfo;
        private BuffTooltipPanel tooltipPanel;

        public void Configure(BuffRuntimeInfo info, BuffTooltipPanel tooltip)
        {
            runtimeInfo = info;
            tooltipPanel = tooltip;
            RefreshIcon();
            RefreshLabel();
        }

        public void RefreshLabel()
        {
            if (runtimeInfo == null || stackOrTimeText == null)
            {
                return;
            }

            stackOrTimeText.text = runtimeInfo.IsPermanent
                ? Mathf.Max(1, runtimeInfo.StackCount).ToString()
                : Mathf.CeilToInt(Mathf.Max(0f, runtimeInfo.RemainingTime)).ToString();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (tooltipPanel == null || runtimeInfo == null)
            {
                return;
            }

            tooltipPanel.Show(runtimeInfo, eventData.position);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tooltipPanel?.Hide();
        }

        private void RefreshIcon()
        {
            if (runtimeInfo == null || iconImage == null)
            {
                return;
            }

            // 图标来自 Buff 数据库, 空图标直接暴露配置缺失.
            iconImage.sprite = runtimeInfo.Buff.Icon;
            iconImage.enabled = runtimeInfo.Buff.Icon != null;
        }
    }
}
