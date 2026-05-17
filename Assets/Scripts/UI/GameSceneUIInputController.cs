using Game.Core;
using UnityEngine;

namespace Game.UI
{
    public class GameSceneUIInputController : MonoBehaviour
    {
        [Header("游戏场景设置面板")]
        [SerializeField] private SettingsPanel settingsPanel;

        private bool settingsOpen;
        private float previousTimeScale = 1f;

        private void OnEnable()
        {
            GameplayCursorState.Reset();
        }

        private void OnDisable()
        {
            if (settingsOpen)
            {
                RestoreSettingsCloseState();
            }

            GameplayCursorState.Reset();
        }

        private void Update()
        {
            // Ctrl 只接管鼠标战斗, 不影响移动, 换枪, 装弹和地图快捷键.
            GameplayCursorState.SetControlKeyHeld(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl));

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleSettingsPanel();
            }

            if (settingsOpen && settingsPanel != null && !settingsPanel.gameObject.activeSelf)
            {
                // 设置面板也可能通过 Back 按钮出栈, 这里统一恢复暂停和鼠标状态.
                RestoreSettingsCloseState();
            }
        }

        private void ToggleSettingsPanel()
        {
            if (settingsOpen)
            {
                CloseSettingsPanel();
                return;
            }

            OpenSettingsPanel();
        }

        private void OpenSettingsPanel()
        {
            if (settingsPanel == null)
            {
                Debug.LogError("打开游戏设置面板失败, SettingsPanel未绑定.", this);
                return;
            }

            UIStackManager stackManager = UIStackManager.Instance;
            if (stackManager == null)
            {
                return;
            }

            previousTimeScale = Time.timeScale;
            settingsOpen = true;
            Time.timeScale = 0f;
            GameplayCursorState.SetSettingsPanelOpen(true);
            stackManager.Push(settingsPanel);
        }

        private void CloseSettingsPanel()
        {
            UIStackManager stackManager = UIStackManager.Instance;
            if (stackManager != null && stackManager.Peek() == settingsPanel)
            {
                stackManager.Pop();
            }

            RestoreSettingsCloseState();
        }

        private void RestoreSettingsCloseState()
        {
            settingsOpen = false;
            Time.timeScale = previousTimeScale;
            GameplayCursorState.SetSettingsPanelOpen(false);
        }
    }
}
