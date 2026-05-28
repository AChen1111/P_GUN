using UnityEngine;

namespace Game.Core
{
    /// <summary>
    /// 游戏场景鼠标状态, 统一提供 UI 和玩家战斗输入的共享判断.
    /// </summary>
    public static class GameplayCursorState
    {
        private static bool isControlKeyHeld;
        private static bool isSettingsPanelOpen;
        private static bool isInventoryPanelOpen;
        private static bool isSaveSlotPanelOpen;
        private static bool isDebugPanelOpen;

        public static bool BlocksMouseCombat => isControlKeyHeld || isSettingsPanelOpen || isInventoryPanelOpen || isSaveSlotPanelOpen || isDebugPanelOpen;
        public static void SetControlKeyHeld(bool isHeld)
        {
            if (isControlKeyHeld == isHeld)
            {
                return;
            }

            isControlKeyHeld = isHeld;
            ApplyCursorState();
        }
        public static void SetSettingsPanelOpen(bool isOpen)
        {
            if (isSettingsPanelOpen == isOpen)
            {
                return;
            }

            isSettingsPanelOpen = isOpen;
            ApplyCursorState();
        }
        public static void SetDebugPanelOpen(bool isOpen)
        {
            if (isDebugPanelOpen == isOpen)
            {
                return;
            }

            isDebugPanelOpen = isOpen;
            ApplyCursorState();
        }
        public static void SetInventoryPanelOpen(bool isOpen)
        {
            if (isInventoryPanelOpen == isOpen)
            {
                return;
            }

            isInventoryPanelOpen = isOpen;
            ApplyCursorState();
        }
        public static void SetSaveSlotPanelOpen(bool isOpen)
        {
            if (isSaveSlotPanelOpen == isOpen)
            {
                return;
            }

            isSaveSlotPanelOpen = isOpen;
            ApplyCursorState();
        }

        /// <summary>
        /// 重置编辑器默认配置.
        /// </summary>
        public static void Reset()
        {
            isControlKeyHeld = false;
            isSettingsPanelOpen = false;
            isInventoryPanelOpen = false;
            isSaveSlotPanelOpen = false;
            isDebugPanelOpen = false;
            ApplyCursorState();
        }

        /// <summary>
        /// 离开游戏场景时恢复普通系统光标.
        /// </summary>
        public static void RestoreDefaultCursor()
        {
            isControlKeyHeld = false;
            isSettingsPanelOpen = false;
            isInventoryPanelOpen = false;
            isSaveSlotPanelOpen = false;
            isDebugPanelOpen = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        private static void ApplyCursorState()
        {
            // 鼠标战斗被 UI 或 Ctrl 阻塞时显示普通光标, 默认战斗状态隐藏系统光标.
            Cursor.visible = BlocksMouseCombat;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
