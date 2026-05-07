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
            seq.Append(sr.DOColor(redTint, step));
            seq.Join(sr.DOFade(minAlpha, step));
            seq.Append(sr.DOColor(orig, step));
            seq.Join(sr.DOFade(1f, step));
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
