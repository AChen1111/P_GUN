using UnityEngine;
using System.Collections.Generic;
using System;
using DG.Tweening;

public class DOTweenAnimation : MonoBehaviour
{
    [Header("动画")]
    public List<Sprite> sprites;
    [Header("动画时间")]
    public float duration = 0.2f;
    [Header("渲染器")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Tween playingTween;
    
    [SerializeField] private bool isPlaying;
    
    private void Reset() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Awake() {
        if (spriteRenderer == null) {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }


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

    [ContextMenu("预览动画")]
    public void PreviewAnimation()
    {
        Debug.Log("预览动画");
        if (!Application.isPlaying) return;
        Play(() => {
            Debug.Log("动画完成");
        });
    }

    [ContextMenu("重置精灵图")]
    public void ResetSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }
    private void OnDisable()
    {
        if (playingTween != null && playingTween.IsActive())
        {
            playingTween.Kill(false);
        }
    }
}
