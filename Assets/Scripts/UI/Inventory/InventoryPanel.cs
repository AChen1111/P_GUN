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

        public static InventoryPanel CreateDefault(Transform parent)
        {
            var panelObject = new GameObject("InventoryPanel", typeof(RectTransform), typeof(CanvasGroup), typeof(Image));
            panelObject.SetActive(false);
            panelObject.transform.SetParent(parent, false);

            var rect = panelObject.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            var overlay = panelObject.GetComponent<Image>();
            overlay.color = new Color(0f, 0f, 0f, 0.58f);
            overlay.raycastTarget = true;

            var window = CreatePanel("Window", panelObject.transform, new Vector2(0.5f, 0.5f), new Vector2(1060f, 610f), new Color(0.07f, 0.08f, 0.1f, 0.98f));
            var title = CreateText("Txt_Title", window.transform, 34, FontStyle.Bold, TextAnchor.MiddleLeft);
            title.rectTransform.anchorMin = new Vector2(0f, 1f);
            title.rectTransform.anchorMax = new Vector2(1f, 1f);
            title.rectTransform.offsetMin = new Vector2(32f, -72f);
            title.rectTransform.offsetMax = new Vector2(-32f, -16f);
            title.text = "背包";

            var useBlockedHintText = CreateText("Txt_UseBlockedHint", window.transform, 22, FontStyle.Bold, TextAnchor.MiddleCenter);
            useBlockedHintText.rectTransform.anchorMin = new Vector2(0.5f, 1f);
            useBlockedHintText.rectTransform.anchorMax = new Vector2(0.5f, 1f);
            useBlockedHintText.rectTransform.pivot = new Vector2(0.5f, 1f);
            useBlockedHintText.rectTransform.anchoredPosition = new Vector2(0f, -18f);
            useBlockedHintText.rectTransform.sizeDelta = new Vector2(420f, 42f);
            useBlockedHintText.color = new Color(1f, 0.78f, 0.25f, 0f);
            useBlockedHintText.text = UseBlockedMessage;
            useBlockedHintText.gameObject.SetActive(false);

            var slotArea = CreatePanel("SlotArea", window.transform, new Vector2(0f, 0.5f), new Vector2(670f, 472f), new Color(0.1f, 0.11f, 0.13f, 0.94f));
            slotArea.anchorMin = new Vector2(0f, 0.5f);
            slotArea.anchorMax = new Vector2(0f, 0.5f);
            slotArea.anchoredPosition = new Vector2(32f, -42f);

            var slotRootObject = new GameObject("Trans_SlotRoot", typeof(RectTransform), typeof(GridLayoutGroup));
            slotRootObject.transform.SetParent(slotArea, false);
            var slotRoot = slotRootObject.GetComponent<RectTransform>();
            slotRoot.anchorMin = Vector2.zero;
            slotRoot.anchorMax = Vector2.one;
            slotRoot.offsetMin = new Vector2(18f, 18f);
            slotRoot.offsetMax = new Vector2(-18f, -18f);

            var grid = slotRootObject.GetComponent<GridLayoutGroup>();
            grid.cellSize = new Vector2(84f, 84f);
            grid.spacing = new Vector2(12f, 12f);
            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.constraintCount = 6;
            grid.childAlignment = TextAnchor.UpperLeft;

            var detail = CreatePanel("DetailPanel", window.transform, new Vector2(1f, 0.5f), new Vector2(300f, 472f), new Color(0.1f, 0.11f, 0.13f, 0.96f));
            detail.anchorMin = new Vector2(1f, 0.5f);
            detail.anchorMax = new Vector2(1f, 0.5f);
            detail.anchoredPosition = new Vector2(-32f, -42f);

            var detailIcon = CreateImage("Img_DetailIcon", detail.transform, new Vector2(0f, -44f), new Vector2(92f, 92f), Color.white);
            detailIcon.rectTransform.anchorMin = new Vector2(0.5f, 1f);
            detailIcon.rectTransform.anchorMax = new Vector2(0.5f, 1f);
            detailIcon.rectTransform.pivot = new Vector2(0.5f, 1f);

            var nameText = CreateText("Txt_DetailName", detail.transform, 24, FontStyle.Bold, TextAnchor.MiddleCenter);
            nameText.rectTransform.anchorMin = new Vector2(0f, 1f);
            nameText.rectTransform.anchorMax = new Vector2(1f, 1f);
            nameText.rectTransform.offsetMin = new Vector2(18f, -176f);
            nameText.rectTransform.offsetMax = new Vector2(-18f, -124f);

            var countText = CreateText("Txt_DetailCount", detail.transform, 18, FontStyle.Normal, TextAnchor.MiddleCenter);
            countText.rectTransform.anchorMin = new Vector2(0f, 1f);
            countText.rectTransform.anchorMax = new Vector2(1f, 1f);
            countText.rectTransform.offsetMin = new Vector2(18f, -220f);
            countText.rectTransform.offsetMax = new Vector2(-18f, -184f);

            var descriptionText = CreateText("Txt_DetailDescription", detail.transform, 18, FontStyle.Normal, TextAnchor.UpperLeft);
            descriptionText.rectTransform.anchorMin = new Vector2(0f, 0f);
            descriptionText.rectTransform.anchorMax = new Vector2(1f, 1f);
            descriptionText.rectTransform.offsetMin = new Vector2(22f, 28f);
            descriptionText.rectTransform.offsetMax = new Vector2(-22f, -240f);
            descriptionText.verticalOverflow = VerticalWrapMode.Overflow;

            var emptyText = CreateText("Txt_EmptyHint", detail.transform, 20, FontStyle.Normal, TextAnchor.MiddleCenter);
            emptyText.rectTransform.anchorMin = Vector2.zero;
            emptyText.rectTransform.anchorMax = Vector2.one;
            emptyText.rectTransform.offsetMin = new Vector2(24f, 24f);
            emptyText.rectTransform.offsetMax = new Vector2(-24f, -24f);
            emptyText.text = "暂无物品";

            var panel = panelObject.AddComponent<InventoryPanel>();
            panel.slotRoot = slotRoot;
            panel.useBlockedHintText = useBlockedHintText;
            panel.detailIcon = detailIcon;
            panel.detailNameText = nameText;
            panel.detailCountText = countText;
            panel.detailDescriptionText = descriptionText;
            panel.emptyHintText = emptyText;
            return panel;
        }

        private void OnEnable()
        {
            EventCenter.AddListener(GameEvent.InventoryChanged, Refresh);
            Refresh();
        }

        private void OnDisable()
        {
            EventCenter.RemoveListener(GameEvent.InventoryChanged, Refresh);
            HideUseBlockedHint();
            ClearSlots();
        }

        protected override void OnOpen()
        {
            HideUseBlockedHint();
            Refresh();
        }

        public void SelectStack(InventoryItemStack stack)
        {
            selectedItemId = stack != null ? stack.ItemId : -1;
            UpdateSlotSelection();
            UpdateDetails(stack);
        }

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
        }

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
        }

        private void CreateSlot(InventoryItemStack stack)
        {
            if (slotRoot == null)
            {
                Debug.LogError("背包刷新失败, Slot根节点未绑定.", this);
                return;
            }

            var slot = slotPrefab != null ? Instantiate(slotPrefab, slotRoot) : InventorySlotView.CreateDefault(slotRoot);
            slot.Configure(stack, this);
            activeSlots.Add(slot);
        }

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

        private void UpdateSlotSelection()
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

        private void UpdateDetails(InventoryItemStack stack)
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

        private static string BuildItemName(InventoryItemStack stack)
        {
            return string.IsNullOrEmpty(stack.Data.itemName) ? $"Item {stack.ItemId}" : stack.Data.itemName;
        }

        private static PlayerInventory ResolveInventory()
        {
            return Global.player != null ? Global.player.GetComponent<PlayerInventory>() : null;
        }

        private void ShowUseBlockedHint(string message)
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

        private IEnumerator PlayUseBlockedHint(string message)
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

        private void SetUseBlockedHintAlpha(float alpha)
        {
            Color color = useBlockedHintText.color;
            color.a = alpha;
            useBlockedHintText.color = color;
        }

        private static RectTransform CreatePanel(string name, Transform parent, Vector2 pivot, Vector2 size, Color color)
        {
            var panelObject = new GameObject(name, typeof(RectTransform), typeof(Image));
            panelObject.transform.SetParent(parent, false);

            var rect = panelObject.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = pivot;
            rect.sizeDelta = size;
            rect.anchoredPosition = Vector2.zero;

            var image = panelObject.GetComponent<Image>();
            image.color = color;
            return rect;
        }

        private static Image CreateImage(string name, Transform parent, Vector2 anchoredPosition, Vector2 size, Color color)
        {
            var imageObject = new GameObject(name, typeof(RectTransform), typeof(Image));
            imageObject.transform.SetParent(parent, false);

            var rect = imageObject.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = anchoredPosition;
            rect.sizeDelta = size;

            var image = imageObject.GetComponent<Image>();
            image.color = color;
            image.raycastTarget = false;
            return image;
        }

        private static Text CreateText(string name, Transform parent, int fontSize, FontStyle fontStyle, TextAnchor alignment)
        {
            var textObject = new GameObject(name, typeof(RectTransform), typeof(Text));
            textObject.transform.SetParent(parent, false);

            var text = textObject.GetComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            text.fontSize = fontSize;
            text.fontStyle = fontStyle;
            text.alignment = alignment;
            text.color = Color.white;
            text.raycastTarget = false;
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
            text.verticalOverflow = VerticalWrapMode.Truncate;
            return text;
        }
    }
}
