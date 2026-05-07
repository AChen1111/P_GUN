using System;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "BlinkAnimEffect", menuName = "PG/Anim/Blink", order = 1)]
public class BlinkAnimEffectSO : AnimEffectSO
{
    [SerializeField] private float minAlpha = 0.35f;
    [SerializeField] private int blinkSteps = 4;

    public override void Play(GameObject target, float duration, Action onComplete)
    {
        var sr = target.GetComponentInChildren<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogWarning($"[BlinkAnimEffect] 未找到 SpriteRenderer: {target.name}");
            onComplete?.Invoke();
            return;
        }

        float step = duration / (blinkSteps * 2f);
        var seq = DOTween.Sequence();
        for (int i = 0; i < blinkSteps; i++)
        {
            seq.Append(sr.DOFade(minAlpha, step));
            seq.Append(sr.DOFade(1f, step));
        }

        seq.SetUpdate(true)
            .SetLink(target)
            .OnComplete(() =>
            {
                var c = sr.color;
                c.a = 1f;
                sr.color = c;
                onComplete?.Invoke();
            });
        seq.Play();
    }
}
