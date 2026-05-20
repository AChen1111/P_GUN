using System.Collections;
using System.Collections.Generic;
using Game.Core;
using Game.Gameplay;
using Game.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    /// <summary>
    /// 背包面板, 左侧显示物品格, 右侧显示当前物品描述.
    /// </summary>
    public class InventoryPanel : UIPanelBase
    {
        private const string UseBlockedMessage = "当前无法使用";
        private const float UseBlockedVisibleSeconds = 1.1f;
        private const float UseBlockedFadeSeconds = 0.22f;
        private const float UseBlockedRiseDistance = 18f;

        [Header("物品列表")]
        [SerializeField] private InventorySlotView slotPrefab;
        [SerializeField] private Transform slotRoot;

        [Header("使用提示")]
        [SerializeField] private Text useBlockedHintText;

        [Header("描述区域")]
        [SerializeField] private Image detailIcon;
        [SerializeField] private Text detailNameText;
        [SerializeField] private Text detailCountText;
        [SerializeField] private Text detailDescriptionText;
        [SerializeField] private Text emptyHintText;

        private readonly List<InventorySlotView> activeSlots = new List<InventorySlotView>();
        private int selectedItemId = -1;
        private Coroutine useBlockedHintCoroutine;
        private Vector2 useBlockedHintBasePosition;
        private bool hasUseBlockedHintBasePosition;

        /// <summary>
        /// 注册启用时需要的监听.
        /// </summary>
        private void OnEnable()
        {
            EventCenter.AddListener(GameEvent.InventoryChanged, Refresh);
            Refresh();
        }

        /// <summary>
        /// 注销禁用时需要的监听.
        /// </summary>
        private void OnDisable()
        {
            EventCenter.RemoveListener(GameEvent.InventoryChanged, Refresh);
            HideUseBlockedHint();
            ClearSlots();
        }

        /// <summary>
        /// 执行 OnOpen 逻辑.
        /// </summary>
        protected override void OnOpen()
        {
            HideUseBlockedHint();
            Refresh();
        }

        /// <summary>
        /// 执行 SelectStack 逻辑.
        /// </summary>
        public void SelectStack(InventoryItemStack stack)
        {
            selectedItemId = stack != null ? stack.ItemId : -1;
            UpdateSlotSelection();
            UpdateDetails(stack);

            void UpdateDetails(InventoryItemStack stack)
            {
                bool hasStack = stack != null;
                if (emptyHintText != null)
                {
                    emptyHintText.gameObject.SetActive(!hasStack);
                }

                if (detailIcon != null)
                {
                    detailIcon.sprite = hasStack ? stack.Data.icon : null;
                    detailIcon.enabled = hasStack && stack.Data.icon != null;
                }

                if (detailNameText != null)
                {
                    detailNameText.text = hasStack ? BuildItemName(stack) : string.Empty;
                }

                if (detailCountText != null)
                {
                    detailCountText.text = hasStack ? $"数量: {stack.Count}" : string.Empty;
                }

                if (detailDescriptionText != null)
                {
                    detailDescriptionText.text = hasStack ? stack.Data.description : string.Empty;
                }
            }

            void UpdateSlotSelection()
            {
                for (int i = 0; i < activeSlots.Count; i++)
                {
                    var slot = activeSlots[i];
                    if (slot != null)
                    {
                        slot.SetSelected(false);
                    }
                }

                var inventory = ResolveInventory();
                if (inventory == null || selectedItemId < 0)
                {
                    return;
                }

                for (int i = 0; i < activeSlots.Count && i < inventory.Items.Count; i++)
                {
                    activeSlots[i].SetSelected(inventory.Items[i].ItemId == selectedItemId);
                }
            }

    static string BuildItemName(InventoryItemStack stack)
    {
        return string.IsNullOrEmpty(stack.Data.itemName) ? $"Item {stack.ItemId}" : stack.Data.itemName;
    }
}

        /// <summary>
        /// 执行 UseStack 逻辑.
        /// </summary>
        public void UseStack(InventoryItemStack stack)
        {
            if (stack == null)
            {
                return;
            }

            var inventory = ResolveInventory();
            if (inventory == null)
            {
                Debug.LogError("背包使用失败, PlayerInventory未找到.", this);
                return;
            }

            // 使用失败时保持当前选择和数量, 例如满血时治疗道具不会消耗.
            if (!inventory.Use(stack.ItemId))
            {
                ShowUseBlockedHint(UseBlockedMessage);
            }

            void ShowUseBlockedHint(string message)
            {
                if (useBlockedHintText == null)
                {
                    Debug.LogError("背包使用提示显示失败, UseBlockedHintText未绑定.", this);
                    return;
                }

                if (useBlockedHintCoroutine != null)
                {
                    StopCoroutine(useBlockedHintCoroutine);
                }

                useBlockedHintCoroutine = StartCoroutine(PlayUseBlockedHint(message));
            }

    IEnumerator PlayUseBlockedHint(string message)
    {
        var hintRect = useBlockedHintText.rectTransform;
        if (!hasUseBlockedHintBasePosition)
        {
            // 背包打开时 Time.timeScale 为 0, 提示动画必须使用未缩放时间.
            useBlockedHintBasePosition = hintRect.anchoredPosition;
            hasUseBlockedHintBasePosition = true;
        }

        useBlockedHintText.text = message;
        useBlockedHintText.gameObject.SetActive(true);
        SetUseBlockedHintAlpha(1f);
        hintRect.anchoredPosition = useBlockedHintBasePosition;
        yield return new WaitForSecondsRealtime(UseBlockedVisibleSeconds);
        float elapsed = 0f;
        while (elapsed < UseBlockedFadeSeconds)
        {
            elapsed += Time.unscaledDeltaTime;
            float progress = Mathf.Clamp01(elapsed / UseBlockedFadeSeconds);
            SetUseBlockedHintAlpha(1f - progress);
            hintRect.anchoredPosition = useBlockedHintBasePosition + Vector2.up * (UseBlockedRiseDistance * progress);
            yield return null;
        }

        useBlockedHintText.gameObject.SetActive(false);
        hintRect.anchoredPosition = useBlockedHintBasePosition;
        useBlockedHintCoroutine = null;
    }
}

        /// <summary>
        /// 执行 Refresh 逻辑.
        /// </summary>
        private void Refresh()
        {
            ClearSlots();

            var inventory = ResolveInventory();
            if (inventory == null || inventory.Items.Count == 0)
            {
                SelectStack(null);
                return;
            }

            InventoryItemStack selectedStack = null;
            for (int i = 0; i < inventory.Items.Count; i++)
            {
                var stack = inventory.Items[i];
                CreateSlot(stack);
                if (stack.ItemId == selectedItemId)
                {
                    selectedStack = stack;
                }
            }

            SelectStack(selectedStack ?? inventory.Items[0]);

            void CreateSlot(InventoryItemStack stack)
            {
                if (slotRoot == null)
                {
                    Debug.LogError("背包刷新失败, Slot根节点未绑定.", this);
                    return;
                }

                if (slotPrefab == null)
                {
                    Debug.LogError("背包刷新失败, SlotPrefab未绑定.", this);
                    return;
                }

                var slot = Instantiate(slotPrefab, slotRoot);
                slot.Configure(stack, this);
                activeSlots.Add(slot);
            }
}

        /// <summary>
        /// 执行 ClearSlots 逻辑.
        /// </summary>
        private void ClearSlots()
        {
            for (int i = activeSlots.Count - 1; i >= 0; i--)
            {
                if (activeSlots[i] != null)
                {
                    Destroy(activeSlots[i].gameObject);
                }
            }

            activeSlots.Clear();
        }

        /// <summary>
        /// 执行 ResolveInventory 逻辑.
        /// </summary>
        private static PlayerInventory ResolveInventory()
        {
            return Global.player != null ? Global.player.GetComponent<PlayerInventory>() : null;
        }

        /// <summary>
        /// 执行 HideUseBlockedHint 逻辑.
        /// </summary>
        private void HideUseBlockedHint()
        {
            if (useBlockedHintCoroutine != null)
            {
                StopCoroutine(useBlockedHintCoroutine);
                useBlockedHintCoroutine = null;
            }

            if (useBlockedHintText == null)
            {
                return;
            }

            useBlockedHintText.gameObject.SetActive(false);
            SetUseBlockedHintAlpha(0f);
            if (hasUseBlockedHintBasePosition)
            {
                useBlockedHintText.rectTransform.anchoredPosition = useBlockedHintBasePosition;
            }
        }

        /// <summary>
        /// 执行 SetUseBlockedHintAlpha 逻辑.
        /// </summary>
        private void SetUseBlockedHintAlpha(float alpha)
        {
            Color color = useBlockedHintText.color;
            color.a = alpha;
            useBlockedHintText.color = color;
        }

    }
}
