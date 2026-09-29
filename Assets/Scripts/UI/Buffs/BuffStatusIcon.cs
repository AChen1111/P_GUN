using System;
using Game.Core;
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
            RefreshLabel();
            LoadIconAsync();
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

        /// <summary>
        /// 图标按 BuffData 的地址异步加载, 加载失败直接报错.
        /// </summary>
        private async void LoadIconAsync()
        {
            if (runtimeInfo?.Config == null || iconImage == null)
            {
                return;
            }

            if (iconImage.enabled)
            {
                iconImage.enabled = false;
            }

            var loader = AddressableLoader.Instance;
            if (loader == null)
            {
                Debug.LogError($"{nameof(BuffStatusIcon)}: {nameof(AddressableLoader)} 未初始化, 无法加载 Buff 图标.", this);
                return;
            }

            try
            {
                var sprite = await loader.LoadAssetAsync<Sprite>(runtimeInfo.Config.IconAddress);
                if (runtimeInfo == null || iconImage == null)
                {
                    return;
                }

                iconImage.sprite = sprite;
                iconImage.enabled = sprite != null;
            }
            catch (Exception exception)
            {
                Debug.LogError($"{nameof(BuffStatusIcon)}: Buff 图标加载失败, 地址: {runtimeInfo.Config.IconAddress}, Error: {exception.Message}", this);
            }
        }
    }
}