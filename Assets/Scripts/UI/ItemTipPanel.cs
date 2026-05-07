using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class ItemTipPanel : MonoBehaviour
{
    private const string DefaultPromptText = "[F] 拾取";

    [SerializeField] private Image iconImage;
    [SerializeField] private Text itemNameText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text promptText;
    [SerializeField] private CanvasGroup canvasGroup;

    private void Awake()
    {
        ResolveReferences();
        Hide();
    }

    private void Reset()
    {
        ResolveReferences();
    }

    public static ItemTipPanel CreateDefault(Transform parent)
    {
        var panelObject = new GameObject("ItemTipPanel", typeof(RectTransform), typeof(CanvasGroup), typeof(Image), typeof(ItemTipPanel));
        panelObject.transform.SetParent(parent, false);

        var rect = panelObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0f);
        rect.anchorMax = new Vector2(0.5f, 0f);
        rect.pivot = new Vector2(0.5f, 0f);
        rect.anchoredPosition = new Vector2(0f, 96f);
        rect.sizeDelta = new Vector2(360f, 112f);

        var background = panelObject.GetComponent<Image>();
        background.color = new Color(0f, 0f, 0f, 0.72f);

        var panel = panelObject.GetComponent<ItemTipPanel>();
        panel.canvasGroup = panelObject.GetComponent<CanvasGroup>();
        panel.canvasGroup.blocksRaycasts = false;
        panel.canvasGroup.interactable = false;

        panel.iconImage = CreateImage("Icon", panelObject.transform, new Vector2(24f, -24f), new Vector2(64f, 64f));
        panel.itemNameText = CreateText("Name", panelObject.transform, 18, FontStyle.Bold, new Vector2(104f, -16f), new Vector2(232f, 28f));
        panel.descriptionText = CreateText("Description", panelObject.transform, 14, FontStyle.Normal, new Vector2(104f, -46f), new Vector2(232f, 40f));
        panel.promptText = CreateText("Prompt", panelObject.transform, 14, FontStyle.Bold, new Vector2(104f, -88f), new Vector2(232f, 20f));
        panel.promptText.text = DefaultPromptText;
        panel.Hide();

        return panel;
    }

    public void Show(ItemData data)
    {
        ResolveReferences();
        gameObject.SetActive(true);

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }

        if (iconImage != null)
        {
            iconImage.sprite = data.icon;
            iconImage.enabled = data.icon != null;
        }

        if (itemNameText != null)
        {
            itemNameText.text = string.IsNullOrEmpty(data.itemName) ? $"Item {data.itemId}" : data.itemName;
        }

        if (descriptionText != null)
        {
            descriptionText.text = string.IsNullOrEmpty(data.description) ? " " : data.description;
        }

        if (promptText != null)
        {
            promptText.text = DefaultPromptText;
        }
    }

    public void Hide()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }

        gameObject.SetActive(false);
    }

    private void ResolveReferences()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }

    private static Image CreateImage(string name, Transform parent, Vector2 anchoredPosition, Vector2 size)
    {
        var imageObject = new GameObject(name, typeof(RectTransform), typeof(Image));
        imageObject.transform.SetParent(parent, false);

        var rect = imageObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0f, 1f);
        rect.anchorMax = new Vector2(0f, 1f);
        rect.pivot = new Vector2(0f, 1f);
        rect.anchoredPosition = anchoredPosition;
        rect.sizeDelta = size;

        return imageObject.GetComponent<Image>();
    }

    private static Text CreateText(string name, Transform parent, int fontSize, FontStyle fontStyle, Vector2 anchoredPosition, Vector2 size)
    {
        var textObject = new GameObject(name, typeof(RectTransform), typeof(Text));
        textObject.transform.SetParent(parent, false);

        var rect = textObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0f, 1f);
        rect.anchorMax = new Vector2(0f, 1f);
        rect.pivot = new Vector2(0f, 1f);
        rect.anchoredPosition = anchoredPosition;
        rect.sizeDelta = size;

        var text = textObject.GetComponent<Text>();
        text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        text.fontSize = fontSize;
        text.fontStyle = fontStyle;
        text.color = Color.white;
        text.horizontalOverflow = HorizontalWrapMode.Wrap;
        text.verticalOverflow = VerticalWrapMode.Truncate;
        text.raycastTarget = false;

        return text;
    }
}
