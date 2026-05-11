using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 动画管理器：通过 SO 注册表分发动画效果。
/// registeredEffects 列表在 Awake 时自动按 SO 资产名建立 Dictionary 映射。
/// 支持三种调用方式：SO 直接传入 / string key / AnimType 枚举（兼容旧代码）。
/// </summary>
public class DOTweenAnimMgr : MonoBehaviour
{
    public static DOTweenAnimMgr Instance { get; private set; }

    [Header("注册的动画效果 SO（顺序无关，按资产名自动映射）")]
    [SerializeField] private List<AnimEffectSO> registeredEffects = new List<AnimEffectSO>();

    private Dictionary<string, AnimEffectSO> effectMap = new Dictionary<string, AnimEffectSO>();

    private const float DefaultDuration = 3f;

    private void Awake()
    {
        Instance = this;
        RebuildEffectMap();
    }

    private void RebuildEffectMap()
    {
        effectMap.Clear();
        foreach (var effect in registeredEffects)
        {
            if (effect == null) continue;
            effectMap[effect.name] = effect;
        }
    }



    /// <summary>
    /// 通过 string key 播放
    /// </summary>
    public static void Play(string key, GameObject target, float duration = DefaultDuration, Action onComplete = null)
    {
        //检查map中有无key
        if(!Instance.effectMap.ContainsKey(key))
        {
            onComplete?.Invoke();
            return;
        }

        if (target == null || duration <= 0f || string.IsNullOrEmpty(key))
        {
            onComplete?.Invoke();
            return;
        }

        if (Instance != null && Instance.effectMap.TryGetValue(key, out var effect))
        {
            target.transform.DOKill(false);
            effect.Play(target, duration, onComplete);
            return;
        }

        Debug.LogWarning($"[DOTweenAnimMgr] 未找到 key=\"{key}\" 对应的动画 SO。");
        onComplete?.Invoke();
    }
}


