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

        private void Awake()
        {
            if (iconRoot == null)
            {
                // 默认使用自身作为图标根节点, 便于场景内直接挂载.
                iconRoot = transform;
            }
        }

        private void OnEnable()
        {
            EventCenter.AddListener(GameEvent.PlayerBuffsChanged, Refresh);
            Refresh();
        }

        private void OnDisable()
        {
            EventCenter.RemoveListener(GameEvent.PlayerBuffsChanged, Refresh);
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
        }

        private void CreateIcon(BuffRuntimeInfo info)
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

        private static BuffManager ResolveBuffManager()
        {
            return Global.player != null ? Global.player.buffManager : null;
        }
    }
}
