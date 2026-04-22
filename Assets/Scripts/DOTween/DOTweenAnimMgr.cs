using System;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 基于 DOTween 的轻量动画入口：闪烁、小跳、位置抖动等，统一由 <see cref="Play"/> 分发。
/// 均为静态方法，无需挂脚本即可调用。
/// </summary>
public class DOTweenAnimMgr : MonoBehaviour
{
    /// <summary>原地上下跳动的单次振幅。</summary>
    const float JumpHeight = 0.35f;
    /// <summary>原地上下跳动次数。</summary>
    const int JumpBounceCount = 3;
    /// <summary>最后向左位移距离。</summary>
    const float JumpLeftDistance = 0.4f;
    /// <summary>闪烁时透明度下限（1 为完全不透明）。</summary>
    const float BlinkMinAlpha = 0.35f;
    /// <summary>闪烁循环次数：每次含「变暗 + 恢复」两段。</summary>
    const int BlinkSteps = 4;
    /// <summary>受击泛红时 G/B 通道相对原色的缩放（越小越红）。</summary>
    const float HurtTintGbScale = 0.28f;
    /// <summary>默认执行时间</summary>
    const float DefaultDuration = 3f;
    /// <summary>
    /// 按类型播放动画；target 为空或 duration 无效时仍会调用 onComplete。
    /// 播放前会 <c>DOKill(false)</c> 清理该物体 Transform 上已有 Tween，避免叠加。
    /// </summary>
    public static void Play(AnimType type, GameObject target, float duration = DefaultDuration, Action onComplete = null)
    {
        if (target == null || duration <= 0f)
        {
            onComplete?.Invoke();
            return;
        }

        target.transform.DOKill(false);

        switch (type)
        {
            case AnimType.None:
                onComplete?.Invoke();
                break;
            case AnimType.Blink:
                PlayBlink(target, duration, onComplete);
                break;
            case AnimType.Jump:
                PlayJump(target, duration, onComplete);
                break;
            case AnimType.Shake:
                PlayShake(target, duration, onComplete);
                break;
            case AnimType.Hurted:
                PlayHurted(target, duration, onComplete);
                break;
            default:
                onComplete?.Invoke();
                break;
        }
    }

    /// <summary>
    /// 透明度闪烁：仅作用 SpriteRenderer，结束强制还原不透明。
    /// </summary>
    static void PlayBlink(GameObject target, float duration = DefaultDuration, Action onComplete = null)
    {
        var sr = target.GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            float step = duration / (BlinkSteps * 2f);
            var seq = DOTween.Sequence();
            for (int i = 0; i < BlinkSteps; i++)
            {
                seq.Append(sr.DOFade(BlinkMinAlpha, step));
                seq.Append(sr.DOFade(1f, step));
            }
            seq.SetUpdate(true);
            seq.OnComplete(() =>
            {
                var c = sr.color;
                c.a = 1f;
                sr.color = c;
                onComplete?.Invoke();
            });
            seq.Play();
            return;
        }

        Debug.LogWarning($"[DOTweenAnimMgr] Blink：未找到 SpriteRenderer：{target.name}");
        onComplete?.Invoke();
    }

    /// <summary>
    /// 先在原地上下跳动几次，再向左跳一小段距离。
    /// </summary>
    static void PlayJump(GameObject target, float duration = DefaultDuration, Action onComplete = null)
    {
        var t = target.transform;
        float bounceDuration = duration * 0.6f;
        float moveDuration = duration - bounceDuration;
        float singleBounce = bounceDuration / (JumpBounceCount * 2f);

        Vector3 start = t.localPosition;
        Vector3 up = start + Vector3.up * JumpHeight;
        Vector3 leftEnd = start + Vector3.left * JumpLeftDistance;

        var seq = DOTween.Sequence();
        for (int i = 0; i < JumpBounceCount; i++)
        {
            seq.Append(t.DOLocalMoveY(up.y, singleBounce).SetEase(Ease.OutQuad));
            seq.Append(t.DOLocalMoveY(start.y, singleBounce).SetEase(Ease.InQuad));
        }

        seq.Append(t.DOLocalMove(leftEnd, Mathf.Max(0.05f, moveDuration)).SetEase(Ease.OutCubic));
        seq.SetUpdate(true)
            .OnComplete(() => onComplete?.Invoke());
        seq.Play();
    }

    /// <summary>localPosition 强化震动，提升受击感。</summary>
    static void PlayShake(GameObject target, float duration = DefaultDuration, Action onComplete = null)
    {
        var strength = new Vector3(0.35f, 0.35f, 0f);
        var tween = target.transform.DOShakePosition(duration, strength, 24, 100f, false, true)
            .SetUpdate(true)
            .OnComplete(() => onComplete?.Invoke());
        tween.Play();
    }

    /// <summary>
    /// 受击：SpriteRenderer 同步「泛红」与「透明度闪烁」，结束强制还原为播放前颜色且 alpha=1。
    /// </summary>
    static void PlayHurted(GameObject target, float duration = DefaultDuration, Action onComplete = null)
    {
        var sr = target.GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            sr.DOKill(false);

            Color orig = sr.color;
            Color redTint = new Color(1f, orig.g * HurtTintGbScale, orig.b * HurtTintGbScale, orig.a);

            float step = duration / (BlinkSteps * 2f);
            var seq = DOTween.Sequence();
            for (int i = 0; i < BlinkSteps; i++)
            {
                seq.Append(sr.DOColor(redTint, step));
                seq.Join(sr.DOFade(BlinkMinAlpha, step));
                seq.Append(sr.DOColor(orig, step));
                seq.Join(sr.DOFade(1f, step));
            }

            seq.SetUpdate(true);
            seq.OnComplete(() =>
            {
                var c = orig;
                c.a = 1f;
                sr.color = c;
                onComplete?.Invoke();
            });
            seq.Play();
            return;
        }

        Debug.LogWarning($"[DOTweenAnimMgr] Hurted：未找到 SpriteRenderer：{target.name}");
        onComplete?.Invoke();
    }
}



/// <summary>与 <see cref="DOTweenAnimMgr.Play"/> 配合的动画种类。</summary>
public enum AnimType
{
    /// <summary>不播放，仅触发完成回调。</summary>
    None,
    /// <summary>透明度明暗闪烁。</summary>
    Blink,
    /// <summary>小幅度侧向跳跃位移。</summary>
    Jump,
    /// <summary>位置抖动。</summary>
    Shake,
    /// <summary>受击动画。</summary>
    Hurted,
}
