using System;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "HurtedAnimEffect", menuName = "PG/Anim/Hurted", order = 4)]
public class HurtedAnimEffectSO : AnimEffectSO
{
    [SerializeField] private float minAlpha = 0.35f;
    [SerializeField] private int blinkSteps = 4;
    [SerializeField] private float hurtTintGbScale = 0.28f;

    public override void Play(GameObject target, float duration, Action onComplete)
    {
        var sr = target.GetComponentInChildren<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogWarning($"[HurtedAnimEffect] 未找到 SpriteRenderer: {target.name}");
            onComplete?.Invoke();
            return;
        }

        sr.DOKill(false);

        Color orig = sr.color;
        Color redTint = new Color(1f, orig.g * hurtTintGbScale, orig.b * hurtTintGbScale, orig.a);

        float step = duration / (blinkSteps * 2f);
        var seq = DOTween.Sequence();
        for (int i = 0; i < blinkSteps; i++)
        {
            seq.Append(DOTween.To(() => sr.color, x => sr.color = x, redTint, step).SetTarget(sr));
            seq.Join(DOTween.ToAlpha(() => sr.color, x => sr.color = x, minAlpha, step).SetTarget(sr));
            seq.Append(DOTween.To(() => sr.color, x => sr.color = x, orig, step).SetTarget(sr));
            seq.Join(DOTween.ToAlpha(() => sr.color, x => sr.color = x, 1f, step).SetTarget(sr));
        }

        seq.SetUpdate(true)
            .SetLink(target)
            .OnComplete(() =>
            {
                var c = orig;
                c.a = 1f;
                sr.color = c;
                onComplete?.Invoke();
            });
        seq.Play();
    }
}
