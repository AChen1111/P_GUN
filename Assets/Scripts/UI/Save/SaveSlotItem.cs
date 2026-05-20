using System;
using System.IO;
using Game.Gameplay.Save;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Save
{
    /// <summary>
    /// 单个存档槽位视图, 只负责展示摘要和转发按钮点击.
    /// </summary>
    public class SaveSlotItem : MonoBehaviour
    {
        [Header("文本")]
        [SerializeField] private Image snapshotImage;
        [SerializeField] private Text titleText;
        [SerializeField] private Text summaryText;
        [SerializeField] private Text detailText;

        [Header("按钮")]
        [SerializeField] private Button saveButton;
        [SerializeField] private Button loadButton;
        [SerializeField] private Button deleteButton;
        [SerializeField] private Text saveButtonText;
        [SerializeField] private Text deleteButtonText;

        private int slotIndex;
        private Action<int> saveClicked;
        private Action<int> loadClicked;
        private Action<int> deleteClicked;
        private Sprite runtimeSnapshotSprite;

        /// <summary>
        /// 执行 Configure 逻辑.
        /// </summary>
        public void Configure(
            SaveSlotSummary summary,
            SaveSlotPanelMode mode,
            bool isOverwriteConfirming,
            bool isDeleteConfirming,
            Action<int> onSaveClicked,
            Action<int> onLoadClicked,
            Action<int> onDeleteClicked)
        {
            slotIndex = summary.slotIndex;
            saveClicked = onSaveClicked;
            loadClicked = onLoadClicked;
            deleteClicked = onDeleteClicked;

            RefreshText(summary, isOverwriteConfirming, isDeleteConfirming);
            RefreshSnapshot(summary);
            RefreshButtons(summary, mode, isOverwriteConfirming, isDeleteConfirming);

            void RefreshButtons(SaveSlotSummary summary, SaveSlotPanelMode mode, bool isOverwriteConfirming, bool isDeleteConfirming)
            {
                var canSave = mode == SaveSlotPanelMode.SafeHouse;
                if (saveButton != null)
                {
                    saveButton.gameObject.SetActive(canSave);
                    saveButton.interactable = canSave;
                }

                if (saveButtonText != null)
                {
                    saveButtonText.text = summary.exists ? (isOverwriteConfirming ? "确认覆盖" : "覆盖") : "保存";
                }

                if (loadButton != null)
                {
                    loadButton.interactable = summary.exists;
                }

                if (deleteButton != null)
                {
                    deleteButton.interactable = summary.exists;
                }

                if (deleteButtonText != null)
                {
                    deleteButtonText.text = isDeleteConfirming ? "确认删除" : "删除";
                }
            }

            void RefreshText(SaveSlotSummary summary, bool isOverwriteConfirming, bool isDeleteConfirming)
            {
                if (titleText != null)
                {
                    titleText.text = $"槽位 {summary.slotIndex}";
                }

                if (!summary.exists)
                {
                    if (summaryText != null)
                        summaryText.text = "空槽位";
                    if (detailText != null)
                        detailText.text = "暂无存档";
                    return;
                }

                if (summaryText != null)
                {
                    summaryText.text = FormatSavedTime(summary.savedAtUtc);
                }

                if (detailText != null)
                {
                    var roomText = string.IsNullOrWhiteSpace(summary.currentRoomId) ? "未知房间" : summary.currentRoomId;
                    detailText.text = $"{summary.sceneName} | HP {summary.playerHp}/{summary.playerMaxHp}\n{roomText}";
                }

                if (isOverwriteConfirming && detailText != null)
                {
                    detailText.text = "再次点击保存会覆盖此槽位.";
                }

                if (isDeleteConfirming && detailText != null)
                {
                    detailText.text = "再次点击删除会移除此槽位.";
                }
            }

            void RefreshSnapshot(SaveSlotSummary summary)
            {
                ClearRuntimeSnapshot();
                if (snapshotImage == null)
                    return;
                if (!summary.exists || string.IsNullOrWhiteSpace(summary.snapshotPath) || !File.Exists(summary.snapshotPath))
                {
                    snapshotImage.color = new Color(0.08f, 0.09f, 0.11f, 1f);
                    snapshotImage.sprite = null;
                    return;
                }

                var bytes = File.ReadAllBytes(summary.snapshotPath);
                var texture = new Texture2D(2, 2, TextureFormat.RGB24, false);
                if (!texture.LoadImage(bytes))
                {
                    Destroy(texture);
                    snapshotImage.sprite = null;
                    return;
                }

                runtimeSnapshotSprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
                snapshotImage.sprite = runtimeSnapshotSprite;
                snapshotImage.color = Color.white;
            }

    static string FormatSavedTime(string savedAtUtc)
    {
        if (!DateTime.TryParse(savedAtUtc, out var dateTime))
        {
            return "保存时间未知";
        }

        return dateTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
    }
}

        /// <summary>
        /// 注册启用时需要的监听.
        /// </summary>
        private void OnEnable()
        {
            saveButton?.onClick.AddListener(HandleSaveClicked);
            loadButton?.onClick.AddListener(HandleLoadClicked);
            deleteButton?.onClick.AddListener(HandleDeleteClicked);
        }

        /// <summary>
        /// 注销禁用时需要的监听.
        /// </summary>
        private void OnDisable()
        {
            saveButton?.onClick.RemoveListener(HandleSaveClicked);
            loadButton?.onClick.RemoveListener(HandleLoadClicked);
            deleteButton?.onClick.RemoveListener(HandleDeleteClicked);
            ClearRuntimeSnapshot();
        }

        /// <summary>
        /// 执行 ClearRuntimeSnapshot 逻辑.
        /// </summary>
        private void ClearRuntimeSnapshot()
        {
            if (runtimeSnapshotSprite == null) return;

            var texture = runtimeSnapshotSprite.texture;
            Destroy(runtimeSnapshotSprite);
            if (texture != null)
            {
                Destroy(texture);
            }

            runtimeSnapshotSprite = null;
        }

        /// <summary>
        /// 执行 HandleSaveClicked 逻辑.
        /// </summary>
        private void HandleSaveClicked()
        {
            saveClicked?.Invoke(slotIndex);
        }

        /// <summary>
        /// 执行 HandleLoadClicked 逻辑.
        /// </summary>
        private void HandleLoadClicked()
        {
            loadClicked?.Invoke(slotIndex);
        }

        /// <summary>
        /// 执行 HandleDeleteClicked 逻辑.
        /// </summary>
        private void HandleDeleteClicked()
        {
            deleteClicked?.Invoke(slotIndex);
        }
    }
}
