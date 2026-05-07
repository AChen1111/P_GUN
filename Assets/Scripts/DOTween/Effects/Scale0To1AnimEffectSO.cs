using System;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "Scale0To1AnimEffect", menuName = "PG/Anim/Scale 0 To 1", order = 5)]
public class Scale0To1AnimEffectSO : AnimEffectSO
{
    [SerializeField] private Ease ease = Ease.OutBack;

    public override void Play(GameObject target, float duration, Action onComplete)
    {
        var t = target.transform;
        t.localScale = Vector3.zero;
        t.DOScale(Vector3.one, duration)
            .SetEase(ease)
            .SetUpdate(true)
            .SetLink(target)
            .OnComplete(() => onComplete?.Invoke())
            .Play();
    }
}
