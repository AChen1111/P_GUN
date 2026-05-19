using System.Collections.Generic;
using Game.Gameplay.Save;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Save
{
    /// <summary>
    /// 存档槽位面板, 主菜单用于读档, 安全屋测试入口用于存读删.
    /// </summary>
    public class SaveSlotPanel : UIPanelBase
    {
        [Header("槽位列表")]
        [SerializeField] private SaveSlotItem slotPrefab;
        [SerializeField] private Transform slotRoot;

        [Header("文本")]
        [SerializeField] private Text titleText;
        [SerializeField] private Text statusText;

        [Header("按钮")]
        [SerializeField] private Button backButton;

        private readonly List<SaveSlotItem> activeItems = new List<SaveSlotItem>();
        private SaveSlotPanelMode mode = SaveSlotPanelMode.MainMenu;
        private int pendingDeleteSlot = -1;
        private int pendingOverwriteSlot = -1;

        public void OpenForMainMenu()
        {
            mode = SaveSlotPanelMode.MainMenu;
            OpenByStack();
        }

        public void OpenForSafeHouse()
        {
            mode = SaveSlotPanelMode.SafeHouse;
            OpenByStack();
        }

        protected override void OnOpen()
        {
            pendingDeleteSlot = -1;
            pendingOverwriteSlot = -1;
            if (statusText != null)
            {
                statusText.text = string.Empty;
            }

            Refresh();
        }

        protected override void OnClose()
        {
            pendingDeleteSlot = -1;
            pendingOverwriteSlot = -1;
            ClearItems();
        }

        private void OnEnable()
        {
            backButton?.onClick.AddListener(CloseByStack);
        }

        private void OnDisable()
        {
            backButton?.onClick.RemoveListener(CloseByStack);
        }

        private void OpenByStack()
        {
            var stackManager = UIStackManager.Instance;
            if (stackManager == null)
            {
                return;
            }

            stackManager.Push(this);
        }

        private void CloseByStack()
        {
            var stackManager = UIStackManager.Instance;
            if (stackManager != null)
            {
                stackManager.Pop();
            }
        }

        private void Refresh()
        {
            ClearItems();

            if (titleText != null)
            {
                titleText.text = mode == SaveSlotPanelMode.SafeHouse ? "安全屋存档" : "读取存档";
            }

            if (statusText != null && string.IsNullOrWhiteSpace(statusText.text))
            {
                statusText.text = mode == SaveSlotPanelMode.SafeHouse ? "选择槽位保存或读取." : "选择已有槽位读取.";
            }

            if (slotPrefab == null || slotRoot == null)
            {
                SetStatus("存档面板配置错误, 槽位预制体或根节点未绑定.");
                return;
            }

            var summaries = SaveGameService.GetSlotSummaries();
            for (var i = 0; i < summaries.Count; i++)
            {
                var summary = summaries[i];
                var item = Instantiate(slotPrefab, slotRoot);
                item.Configure(
                    summary,
                    mode,
                    pendingOverwriteSlot == summary.slotIndex,
                    pendingDeleteSlot == summary.slotIndex,
                    HandleSaveClicked,
                    HandleLoadClicked,
                    HandleDeleteClicked);
                activeItems.Add(item);
            }
        }

        private void ClearItems()
        {
            for (var i = activeItems.Count - 1; i >= 0; i--)
            {
                if (activeItems[i] != null)
                {
                    Destroy(activeItems[i].gameObject);
                }
            }

            activeItems.Clear();
        }

        private void HandleSaveClicked(int slotIndex)
        {
            var summary = SaveSlotStorage.ReadSummary(slotIndex);
            if (summary.exists && pendingOverwriteSlot != slotIndex)
            {
                pendingOverwriteSlot = slotIndex;
                pendingDeleteSlot = -1;
                SetStatus($"再次点击槽位 {slotIndex} 的保存按钮会覆盖存档.");
                Refresh();
                return;
            }

            pendingOverwriteSlot = -1;
            pendingDeleteSlot = -1;
            var result = SaveGameService.SaveToSlot(slotIndex);
            SetStatus(result.Message);
            Refresh();
        }

        private void HandleLoadClicked(int slotIndex)
        {
            pendingOverwriteSlot = -1;
            pendingDeleteSlot = -1;
            var result = SaveGameService.LoadFromSlot(slotIndex);
            SetStatus(result.Message);
            Refresh();
        }

        private void HandleDeleteClicked(int slotIndex)
        {
            var summary = SaveSlotStorage.ReadSummary(slotIndex);
            if (!summary.exists)
            {
                SetStatus("删除失败, 槽位为空.");
                Refresh();
                return;
            }

            if (pendingDeleteSlot != slotIndex)
            {
                pendingDeleteSlot = slotIndex;
                pendingOverwriteSlot = -1;
                SetStatus($"再次点击槽位 {slotIndex} 的删除按钮会移除存档.");
                Refresh();
                return;
            }

            pendingDeleteSlot = -1;
            pendingOverwriteSlot = -1;
            var result = SaveGameService.DeleteSlot(slotIndex);
            SetStatus(result.Message);
            Refresh();
        }

        private void SetStatus(string message)
        {
            if (statusText != null)
            {
                statusText.text = message;
            }
        }
    }
}
