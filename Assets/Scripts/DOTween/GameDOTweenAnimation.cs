using UnityEngine;
using System.Collections.Generic;
using System;
using DG.Tweening;

namespace Game.Animation
{
    public class GameDOTweenAnimation : MonoBehaviour
    {
        [Header("动画")]
        public List<Sprite> sprites;
        [Header("动画时间")]
        public float duration = 0.2f;
        [Header("渲染器")]
        [SerializeField] private SpriteRenderer spriteRenderer;

        private Tween playingTween;

        [SerializeField] private bool isPlaying;

        /// <summary>
        /// 重置编辑器默认配置.
        /// </summary>
        private void Reset() {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// 初始化运行时依赖.
        /// </summary>
        private void Awake() {
            if (spriteRenderer == null) {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
        }


        /// <summary>
        /// 执行 Play 逻辑.
        /// </summary>
        public void Play(Action OnComplete)
        {
            if (isPlaying)
            {
                OnComplete?.Invoke();
                return;
            }

            if (spriteRenderer == null)
            {
                Debug.LogError("SpriteRenderer is null");
            }

            if (spriteRenderer == null || sprites == null || sprites.Count == 0 || duration <= 0f)
            {
                OnComplete?.Invoke();
                return;
            }

            if (playingTween != null && playingTween.IsActive())
            {
                playingTween.Kill(false);
            }

            if (sprites.Count == 1)
            {
                spriteRenderer.sprite = sprites[0];
                OnComplete?.Invoke();
                return;
            }

            isPlaying = true;
            float frameIndexFloat = 0f;
            playingTween = DOTween.To(
                    () => frameIndexFloat,
                    value =>
                    {
                        frameIndexFloat = value;
                        int frameIndex = Mathf.Clamp(Mathf.FloorToInt(frameIndexFloat), 0, sprites.Count - 1);
                        spriteRenderer.sprite = sprites[frameIndex];
                    },
                    sprites.Count,
                    duration)
                .SetEase(Ease.Linear)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    spriteRenderer.sprite = sprites[sprites.Count - 1];
                    isPlaying = false;
                    Debug.Log("OnComplete");
                    OnComplete?.Invoke();
                })
                .OnKill(() =>
                {
                    isPlaying = false;
                    playingTween = null;
                });
        }

        /// <summary>
        /// 执行 PreviewAnimation 逻辑.
        /// </summary>
        [ContextMenu("预览动画")]
        public void PreviewAnimation()
        {
            Debug.Log("预览动画");
            if (!Application.isPlaying) return;
            Play(() => {
                Debug.Log("动画完成");
            });
        }

        /// <summary>
        /// 执行 ResetSprite 逻辑.
        /// </summary>
        [ContextMenu("重置精灵图")]
        public void ResetSprite()
        {
            spriteRenderer.sprite = sprites[0];
        }
        /// <summary>
        /// 注销禁用时需要的监听.
        /// </summary>
        private void OnDisable()
        {
            if (playingTween != null && playingTween.IsActive())
            {
                playingTween.Kill(false);
            }
        }
    }
}
