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

        /// <summary>
        /// 执行 Configure 逻辑.
        /// </summary>
        public void Configure(BuffRuntimeInfo info, BuffTooltipPanel tooltip)
        {
            runtimeInfo = info;
            tooltipPanel = tooltip;
            RefreshIcon();
            RefreshLabel();

            void RefreshIcon()
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

        /// <summary>
        /// 执行 RefreshLabel 逻辑.
        /// </summary>
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

        /// <summary>
        /// 执行 OnPointerEnter 逻辑.
        /// </summary>
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (tooltipPanel == null || runtimeInfo == null)
            {
                return;
            }

            tooltipPanel.Show(runtimeInfo, eventData.position);
        }

        /// <summary>
        /// 执行 OnPointerExit 逻辑.
        /// </summary>
        public void OnPointerExit(PointerEventData eventData)
        {
            tooltipPanel?.Hide();
        }
    }
}
