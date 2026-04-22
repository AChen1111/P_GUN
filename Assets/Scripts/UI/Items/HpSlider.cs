using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 心形血条：每个 heart 表示 2 点血量（full=2, half=1, empty=0）。
/// </summary>
public class HpSlider : MonoBehaviour
{
    [Header("预制体与贴图")]
    public GameObject HpPrefab;
    public Sprite heartFull;
    public Sprite heartHalf;
    public Sprite heartEmpty;

    [Header("布局")]
    [Tooltip("每行最大 heart 数，超出后自动换行。")]
    public int maxHeartsPerLine = 10;
    [Tooltip("所有 heart 的统一缩放。")]
    public float heartScale = 1f;
    [Tooltip("行节点模板（可选）。为空时自动创建带 HorizontalLayoutGroup 的行节点。")]
    public Transform LineTransform;

    [Header("初始化动画")]
    [Tooltip("初始化动画固定总时长（秒）。小于等于 0 时直接显示最终血量。")]
    public float initAnimationDuration = 1f;
    [Tooltip("每个 heart 单步变化的时间权重（empty->half / half->full）。")]
    public float initStepDuration = 0.08f;
    [Tooltip("初始化时每个 heart 开始动画的间隔权重。")]
    public float initHeartInterval = 0.05f;

    [Header("可选：自动同步 Global")]
    public bool autoSyncGlobalHp = true;

    readonly List<Image> _hearts = new List<Image>();
    readonly List<int> _heartValues = new List<int>(); // 0 empty, 1 half, 2 full
    readonly List<Transform> _rows = new List<Transform>();

    int _maxHp;
    int _currentHp;

    public int CurrentHp => _currentHp;
    public int MaxHp => _maxHp;

    void Awake()
    {
        if (autoSyncGlobalHp)
        {
            Init(Global.MaxHP, Global.HP, true);
        }
    }

    void OnEnable()
    {
        if (autoSyncGlobalHp)
            Global.OnHPChange += SyncFromGlobal;
    }

    void OnDisable()
    {
        if (autoSyncGlobalHp)
            Global.OnHPChange -= SyncFromGlobal;
    }

    /// <summary>
    /// 初始化：按最大血量构造 heart，并从左到右、从上到下执行 empty->half->full。
    /// </summary>
    public void Init(int maxHp, int currentHp = -1, bool playInitAnimation = true)
    {
        if (HpPrefab == null)
        {
            Debug.LogWarning("[HpSlider] HpPrefab 未设置。");
            return;
        }

        ClearAllHearts();

        _maxHp = Mathf.Max(0, maxHp);
        _currentHp = currentHp < 0 ? _maxHp : Mathf.Clamp(currentHp, 0, _maxHp);

        int heartCount = Mathf.CeilToInt(_maxHp / 2f);
        for (int i = 0; i < heartCount; i++)
        {
            var row = GetOrCreateRow(i / Mathf.Max(1, maxHeartsPerLine));
            var go = Instantiate(HpPrefab, row);
            var image = go.GetComponent<Image>();
            if (image == null) image = go.GetComponentInChildren<Image>();
            if (image == null)
            {
                Debug.LogWarning($"[HpSlider] Heart 预制体缺少 Image：{go.name}");
                continue;
            }

            image.sprite = heartEmpty;
            ApplyHeartScale(image);
            _hearts.Add(image);
            _heartValues.Add(0);
        }

        if (playInitAnimation)
            StartCoroutine(PlayInitBuildAnimation());
        else
            RefreshByHpImmediate(_currentHp);
    }

    /// <summary>扣血：从最后一个非空 heart 开始，full->half->empty。</summary>
    public void Damage(int amount = 1)
    {
        int step = Mathf.Max(0, amount);
        while (step-- > 0 && _currentHp > 0)
        {
            _currentHp--;
            for (int i = _heartValues.Count - 1; i >= 0; i--)
            {
                if (_heartValues[i] <= 0) continue;
                SetHeartValue(i, _heartValues[i] - 1);
                break;
            }
        }
    }

    /// <summary>回血：与扣血反向，按从左到右补充，empty->half->full。</summary>
    public void Heal(int amount = 1)
    {
        int step = Mathf.Max(0, amount);
        while (step-- > 0 && _currentHp < _maxHp)
        {
            _currentHp++;
            for (int i = 0; i < _heartValues.Count; i++)
            {
                if (_heartValues[i] >= 2) continue;
                SetHeartValue(i, _heartValues[i] + 1);
                break;
            }
        }
    }

    /// <summary>
    /// 增加最大血量：每次新增一个 empty heart（即 +2 最大血量），并遵循换行规则。
    /// </summary>
    public void AddMaxHpHeart()
    {
        _maxHp += 2;

        int newHeartIndex = _hearts.Count;
        var row = GetOrCreateRow(newHeartIndex / Mathf.Max(1, maxHeartsPerLine));
        var go = Instantiate(HpPrefab, row);
        var image = go.GetComponent<Image>();
        if (image == null) image = go.GetComponentInChildren<Image>();
        if (image == null)
        {
            Debug.LogWarning($"[HpSlider] Heart 预制体缺少 Image：{go.name}");
            return;
        }

        image.sprite = heartEmpty;
        ApplyHeartScale(image);
        _hearts.Add(image);
        _heartValues.Add(0);
    }

    /// <summary>按目标血量直接刷新（不播放初始化构造动画）。</summary>
    public void SetHp(int hp)
    {
        _currentHp = Mathf.Clamp(hp, 0, _maxHp);
        RefreshByHpImmediate(_currentHp);
    }

    void SyncFromGlobal()
    {
        if (_maxHp != Global.MaxHP || _hearts.Count != Mathf.CeilToInt(Global.MaxHP / 2f))
        {
            Init(Global.MaxHP, Global.HP, false);
            return;
        }
        SetHp(Global.HP);
    }

    IEnumerator PlayInitBuildAnimation()
    {
        int hpToAnimate = Mathf.Min(Mathf.Clamp(_currentHp, 0, _maxHp), _hearts.Count * 2);
        if (initAnimationDuration <= 0f || hpToAnimate <= 0 || _hearts.Count == 0)
        {
            RefreshByHpImmediate(_currentHp);
            yield break;
        }

        int animatedHeartCount = Mathf.CeilToInt(hpToAnimate / 2f);
        int intervalCount = Mathf.Max(0, animatedHeartCount - 1);
        float stepWeight = Mathf.Max(0f, initStepDuration);
        float intervalWeight = Mathf.Max(0f, initHeartInterval);
        float totalWeight = hpToAnimate * stepWeight + intervalCount * intervalWeight;
        float stepDuration;
        float heartInterval;

        if (totalWeight <= 0f)
        {
            stepDuration = initAnimationDuration / hpToAnimate;
            heartInterval = 0f;
        }
        else
        {
            float timingScale = initAnimationDuration / totalWeight;
            stepDuration = stepWeight * timingScale;
            heartInterval = intervalWeight * timingScale;
        }

        int hpLeft = hpToAnimate;
        int animatedHeartIndex = 0;
        for (int i = 0; i < _hearts.Count; i++)
        {
            int target = Mathf.Clamp(hpLeft, 0, 2);
            hpLeft -= target;

            if (target <= 0) continue;

            if (target >= 1)
            {
                SetHeartValue(i, 1);
                PulseHeart(_hearts[i], stepDuration);
                if (stepDuration > 0f)
                    yield return new WaitForSecondsRealtime(stepDuration);
            }

            if (target >= 2)
            {
                SetHeartValue(i, 2);
                PulseHeart(_hearts[i], stepDuration);
                if (stepDuration > 0f)
                    yield return new WaitForSecondsRealtime(stepDuration);
            }

            animatedHeartIndex++;
            if (animatedHeartIndex < animatedHeartCount && heartInterval > 0f)
                yield return new WaitForSecondsRealtime(heartInterval);
        }
    }

    void RefreshByHpImmediate(int hp)
    {
        int hpLeft = hp;
        for (int i = 0; i < _heartValues.Count; i++)
        {
            int target = Mathf.Clamp(hpLeft, 0, 2);
            SetHeartValue(i, target);
            hpLeft -= target;
        }
    }

    void SetHeartValue(int index, int value)
    {
        if (index < 0 || index >= _hearts.Count) return;
        int clamp = Mathf.Clamp(value, 0, 2);
        _heartValues[index] = clamp;
        _hearts[index].sprite = clamp == 2 ? heartFull : clamp == 1 ? heartHalf : heartEmpty;
        ApplyHeartScale(_hearts[index]);
    }

    void PulseHeart(Image heart, float duration)
    {
        heart.transform.DOKill(false);
        Vector3 baseScale = GetHeartScale();
        heart.transform.localScale = baseScale;
        if (duration <= 0f) return;

        heart.transform.DOScale(baseScale * 1.15f, duration * 0.5f)
            .SetLoops(2, LoopType.Yoyo)
            .SetUpdate(true);
    }

    void ApplyHeartScale(Image heart)
    {
        if (heart == null) return;
        heart.transform.localScale = GetHeartScale();
    }

    Vector3 GetHeartScale()
    {
        return Vector3.one * Mathf.Max(0f, heartScale);
    }

    Transform GetOrCreateRow(int rowIndex)
    {
        while (_rows.Count <= rowIndex)
        {
            Transform row;
            if (LineTransform != null)
            {
                row = Instantiate(LineTransform, transform);
                row.name = $"HPRow_{_rows.Count}";
            }
            else
            {
                var rowGo = new GameObject($"HPRow_{_rows.Count}", typeof(RectTransform), typeof(HorizontalLayoutGroup));
                row = rowGo.transform;
                row.SetParent(transform, false);
            }

            EnsureRowKeepsHeartSize(row);
            row.gameObject.SetActive(true);
            _rows.Add(row);
        }

        return _rows[rowIndex];
    }

    /// <summary>
    /// 只做排列，不控制子节点尺寸，保持 heart 预制体原始大小。
    /// </summary>
    void EnsureRowKeepsHeartSize(Transform row)
    {
        var layout = row.GetComponent<HorizontalLayoutGroup>();
        if (layout == null) return;

        layout.childControlWidth = false;
        layout.childControlHeight = false;
        layout.childForceExpandWidth = false;
        layout.childForceExpandHeight = false;
    }

    void ClearAllHearts()
    {
        for (int i = 0; i < _rows.Count; i++)
        {
            if (_rows[i] != null) Destroy(_rows[i].gameObject);
        }
        _rows.Clear();
        _hearts.Clear();
        _heartValues.Clear();
    }
}
