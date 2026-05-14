using System;
using DG.Tweening;
using UnityEngine;

namespace Game.Animation
{
    [CreateAssetMenu(fileName = "JumpAnimEffect", menuName = "PG/Anim/Jump", order = 2)]
    public class JumpAnimEffectSO : AnimEffectSO
    {
        [SerializeField] private float jumpHeight = 0.35f;
        [SerializeField] private int bounceCount = 3;
        [SerializeField] private float leftDistance = 0.4f;

        public override void Play(GameObject target, float duration, Action onComplete)
        {
            var t = target.transform;
            float bounceDuration = duration * 0.6f;
            float moveDuration = duration - bounceDuration;
            float singleBounce = bounceDuration / (bounceCount * 2f);

            Vector3 start = t.localPosition;
            Vector3 up = start + Vector3.up * jumpHeight;
            Vector3 leftEnd = start + Vector3.left * leftDistance;

            var seq = DOTween.Sequence();
            for (int i = 0; i < bounceCount; i++)
            {
                seq.Append(t.DOLocalMoveY(up.y, singleBounce).SetEase(Ease.OutQuad));
                seq.Append(t.DOLocalMoveY(start.y, singleBounce).SetEase(Ease.InQuad));
            }

            seq.Append(t.DOLocalMove(leftEnd, Mathf.Max(0.05f, moveDuration)).SetEase(Ease.OutCubic));
            seq.SetUpdate(true)
                .SetLink(target)
                .OnComplete(() => onComplete?.Invoke());
            seq.Play();
        }
    }
}
