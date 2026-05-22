using System;
using DG.Tweening;
using UnityEngine;

namespace Game.Animation
{
    [CreateAssetMenu(fileName = "ShakeAnimEffect", menuName = "PG/Anim/Shake", order = 3)]
    public class ShakeAnimEffectSO : AnimEffectSO
    {
        [SerializeField] private Vector3 strength = new Vector3(0.35f, 0.35f, 0f);
        [SerializeField] private int vibrato = 24;
        [SerializeField] private float randomness = 100f;
        public override void Play(GameObject target, float duration, Action onComplete)
        {
            target.transform.DOShakePosition(duration, strength, vibrato, randomness, false, true)
                .SetUpdate(true)
                .SetLink(target)
                .OnComplete(() => onComplete?.Invoke())
                .Play();
        }
    }
}
