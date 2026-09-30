using System;
using DG.Tweening;
using UnityEngine;

namespace Game.Animation
{
    [CreateAssetMenu(fileName = "Scale0To1AnimEffect", menuName = "PG/Anim/Scale 0 To 1", order = 5)]
    public class Scale0To1AnimEffectSO : AnimEffectSO
    {
        [SerializeField] private Ease ease = Ease.OutBack;
        public override void Play(GameObject target, float duration, Action onComplete)
        {
            var t = target.transform;
            // 缩放动画以预制体的原始尺寸为终点, 世界道具不能统一放大到 1.
            var targetScale = t.localScale;
            t.localScale = Vector3.zero;
            t.DOScale(targetScale, duration)
                .SetEase(ease)
                .SetUpdate(true)
                .SetLink(target)
                .OnComplete(() => onComplete?.Invoke())
                .Play();
        }
    }
}
