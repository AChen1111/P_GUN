using System.Collections.Generic;
using Game.Core;
using Game.Gameplay;
using UnityEngine;

namespace Game.UI
{
    public class BuffStatusPanel : MonoBehaviour
    {
        [Header("Buff 状态配置")]
        [SerializeField] private BuffStatusIcon iconPrefab;
        [SerializeField] private Transform iconRoot;
        [SerializeField] private BuffTooltipPanel tooltipPanel;

        private readonly List<BuffStatusIcon> activeIcons = new List<BuffStatusIcon>();

        /// <summary>
        /// 初始化运行时依赖.
        /// </summary>
        private void Awake()
        {
            if (iconRoot == null)
            {
                // 默认使用自身作为图标根节点, 便于场景内直接挂载.
                iconRoot = transform;
            }
        }

        /// <summary>
        /// 注册启用时需要的监听.
        /// </summary>
        private void OnEnable()
        {
            EventCenter.AddListener(GameplayEvents.PlayerBuffsChanged, Refresh);
            Refresh();
        }

        /// <summary>
        /// 注销禁用时需要的监听.
        /// </summary>
        private void OnDisable()
        {
            EventCenter.RemoveListener(GameplayEvents.PlayerBuffsChanged, Refresh);
            ClearIcons();
        }
        private void Update()
        {
            for (var i = 0; i < activeIcons.Count; i++)
            {
                activeIcons[i].RefreshLabel();
            }
        }
        private void Refresh()
        {
            ClearIcons();

            var buffManager = ResolveBuffManager();
            if (buffManager == null)
            {
                return;
            }

            for (var i = 0; i < buffManager.ActiveBuffs.Count; i++)
            {
                CreateIcon(buffManager.ActiveBuffs[i]);
            }

            static BuffManager ResolveBuffManager()
            {
                return PlayerRegistry.Current != null ? PlayerRegistry.Current.buffManager : null;
            }

            void CreateIcon(BuffRuntimeInfo info)
            {
                if (iconPrefab == null)
                {
                    Debug.LogError("BuffStatusPanel刷新失败, BuffStatusIcon预制体未绑定.", this);
                    return;
                }

                var icon = Instantiate(iconPrefab, iconRoot);
                icon.Configure(info, tooltipPanel);
                activeIcons.Add(icon);
            }
}
        private void ClearIcons()
        {
            tooltipPanel?.Hide();

            for (var i = activeIcons.Count - 1; i >= 0; i--)
            {
                if (activeIcons[i] != null)
                {
                    Destroy(activeIcons[i].gameObject);
                }
            }

            activeIcons.Clear();
        }
    }
}
