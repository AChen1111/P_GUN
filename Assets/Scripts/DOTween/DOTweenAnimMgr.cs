using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game.Animation
{
    /// <summary>
    /// DOTween 动画类型：枚举名用于代码和 Inspector 选择，实际播放时映射到已注册 SO 名称。
    /// </summary>
    public enum DOTweenAnimType
    {
        None,
        Blink,
        Jump,
        Shake,
        Hurted,
        Scale0To1
    }

    /// <summary>
    /// 动画管理器：通过 SO 注册表分发动画效果。
    /// registeredEffects 列表在 Awake 时自动按 SO 资产名建立 Dictionary 映射。
    /// 支持三种调用方式：SO 直接传入 / string key / DOTweenAnimType 枚举。
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
                RegisterEffect(effect);
            }
        }

        private void RegisterEffect(AnimEffectSO effect)
        {
            effectMap[effect.name] = effect;

            // 兼容旧代码中的短 key，例如 Jump 和 Hurted。
            const string suffix = "AnimEffect";
            if (effect.name.EndsWith(suffix, StringComparison.Ordinal))
            {
                effectMap[effect.name.Substring(0, effect.name.Length - suffix.Length)] = effect;
            }
        }


        /// <summary>
        /// 通过 SO 直接播放。
        /// </summary>
        public static void Play(AnimEffectSO effect, GameObject target, float duration = DefaultDuration, Action onComplete = null)
        {
            if (effect == null || target == null || duration <= 0f)
            {
                onComplete?.Invoke();
                return;
            }

            target.transform.DOKill(false);
            effect.Play(target, duration, onComplete);
        }

        /// <summary>
        /// 通过枚举播放，枚举会映射到注册表中的 SO 资产名。
        /// </summary>
        public static void Play(DOTweenAnimType animType, GameObject target, float duration = DefaultDuration, Action onComplete = null)
        {
            if (animType == DOTweenAnimType.None)
            {
                onComplete?.Invoke();
                return;
            }

            Play(GetEffectKey(animType), target, duration, onComplete);
        }

        /// <summary>
        /// 通过 string key 播放。
        /// </summary>
        public static void Play(string key, GameObject target, float duration = DefaultDuration, Action onComplete = null)
        {
            if (target == null || duration <= 0f || string.IsNullOrEmpty(key))
            {
                onComplete?.Invoke();
                return;
            }

            if (Instance == null)
            {
                Debug.LogWarning("[DOTweenAnimMgr] 场景中缺少 DOTweenAnimMgr 实例。");
                onComplete?.Invoke();
                return;
            }

            if (Instance.effectMap.TryGetValue(key, out var effect))
            {
                target.transform.DOKill(false);
                effect.Play(target, duration, onComplete);
                return;
            }

            Debug.LogWarning($"[DOTweenAnimMgr] 未找到 key=\"{key}\" 对应的动画 SO。");
            onComplete?.Invoke();
        }

        private static string GetEffectKey(DOTweenAnimType animType)
        {
            // 枚举名和 SO 资产名分离，避免 Inspector 显示名被资源命名细节污染。
            switch (animType)
            {
                case DOTweenAnimType.Blink:
                    return "BlinkAnimEffect";
                case DOTweenAnimType.Jump:
                    return "JumpAnimEffect";
                case DOTweenAnimType.Shake:
                    return "ShakeAnimEffect";
                case DOTweenAnimType.Hurted:
                    return "HurtedAnimEffect";
                case DOTweenAnimType.Scale0To1:
                    return "Scale0To1AnimEffect";
                default:
                    return string.Empty;
            }
        }
    }


}
