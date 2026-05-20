using System;
using DG.Tweening;
using UnityEngine;

namespace Game.Animation
{
    [CreateAssetMenu(fileName = "BlinkAnimEffect", menuName = "PG/Anim/Blink", order = 1)]
    public class BlinkAnimEffectSO : AnimEffectSO
    {
        [SerializeField] private float minAlpha = 0.35f;
        [SerializeField] private int blinkSteps = 4;

        /// <summary>
        /// 执行 Play 逻辑.
        /// </summary>
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
                seq.Append(DOTween.ToAlpha(() => sr.color, x => sr.color = x, minAlpha, step).SetTarget(sr));
                seq.Append(DOTween.ToAlpha(() => sr.color, x => sr.color = x, 1f, step).SetTarget(sr));
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
}
