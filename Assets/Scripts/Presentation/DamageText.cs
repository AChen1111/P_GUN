using System;
using DG.Tweening;
using Game.Pooling;
using TMPro;
using UnityEngine;

namespace Game.Presentation
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TextMeshPro))]
    public class DamageText : MonoBehaviour, IPoolable
    {
        [Header("Text")]
        [SerializeField] private TextMeshPro text;
        [SerializeField] private Vector2 fontSizeRange = new Vector2(32f, 44f);

        [Header("Motion")]
        [SerializeField] private Vector2 randomOffset = new Vector2(0.18f, 0.08f);
        [SerializeField] private float riseDistance = 0.65f;
        [SerializeField] private float duration = 0.55f;
        [SerializeField] private float punchScale = 1.18f;

        private Color defaultColor = Color.white;
        private Vector3 defaultScale = Vector3.one;
        private Sequence sequence;

        public Action<DamageText> OnComplete { get; set; }

        /// <summary>
        /// 初始化运行时依赖.
        /// </summary>
        private void Awake()
        {
            ResolveText();
            CaptureDefaults();
        }

        /// <summary>
        /// 重置编辑器默认配置.
        /// </summary>
        private void Reset()
        {
            ResolveText();
            CaptureDefaults();
        }

        /// <summary>
        /// 执行 OnSpawnFromPool 逻辑.
        /// </summary>
        public void OnSpawnFromPool()
        {
            ResetState();
        }

        /// <summary>
        /// 执行 OnRecycleToPool 逻辑.
        /// </summary>
        public void OnRecycleToPool()
        {
            ResetState();
        }

        /// <summary>
        /// 执行 Play 逻辑.
        /// </summary>
        public void Play(int damage, Vector3 basePosition)
        {
            ResolveText();
            ResetState();

            // 伤害数字只接收最终伤害值, 位置扰动和表现参数由 prefab 自己控制.
            var offset = new Vector3(
                UnityEngine.Random.Range(-randomOffset.x, randomOffset.x),
                UnityEngine.Random.Range(-randomOffset.y, randomOffset.y),
                0f
            );
            transform.position = basePosition + offset;

            text.text = damage.ToString();
            text.fontSize = UnityEngine.Random.Range(fontSizeRange.x, fontSizeRange.y);

            var color = defaultColor;
            color.a = 1f;
            text.color = color;
            transform.localScale = defaultScale;

            var targetPosition = transform.position + Vector3.up * riseDistance;
            sequence = DOTween.Sequence();
            sequence.SetTarget(this);
            sequence.Join(transform.DOMove(targetPosition, duration).SetEase(Ease.OutQuad));
            sequence.Join(transform.DOScale(defaultScale * punchScale, duration * 0.35f).SetLoops(2, LoopType.Yoyo));
            sequence.Join(DOTween.To(
                () => text.color.a,
                alpha => {
                    var nextColor = text.color;
                    nextColor.a = alpha;
                    text.color = nextColor;
                },
                0f,
                duration
            ).SetEase(Ease.InQuad));
            sequence.OnComplete(() => {
                sequence = null;
                OnComplete?.Invoke(this);
            });
        }

        /// <summary>
        /// 执行 ResolveText 逻辑.
        /// </summary>
        private void ResolveText()
        {
            if(text != null) return;

            text = GetComponent<TextMeshPro>();
        }

        /// <summary>
        /// 执行 CaptureDefaults 逻辑.
        /// </summary>
        private void CaptureDefaults()
        {
            if(text != null) {
                defaultColor = text.color;
            }

            defaultScale = transform.localScale;
        }

        /// <summary>
        /// 执行 ResetState 逻辑.
        /// </summary>
        private void ResetState()
        {
            // 对象池复用时必须清理 Tween, 避免上一轮淡出或回调影响下一次显示.
            if(sequence != null) {
                sequence.Kill(false);
                sequence = null;
            }

            DOTween.Kill(this, false);
            transform.DOKill(false);

            if(text == null) return;

            text.DOKill(false);
            text.color = defaultColor;
            transform.localScale = defaultScale;
        }
    }
}
