using System;
using UnityEngine;

/// <summary>
/// 所有具体 Buff 的基础资产.
/// 具体 Buff 继承该类, 并重写 InitEffect 或 OnTrigger 来定义效果.
/// </summary>
public abstract class Buff : ScriptableObject
{
    [Header("Basic")]
    [SerializeField] private int id = 0;
    [SerializeField] private string buffName = string.Empty;

    [Header("Lifetime")]
    [Min(0f)]
    [SerializeField] private float duration = 5f;
    [SerializeField] private bool isPermanent = false;

    [Header("Trigger")]
    [SerializeField] private BuffTriggerType triggerType = BuffTriggerType.OnApply;
    [Min(0f)]
    [SerializeField] private float interval = 1f;

    /// <summary>
    /// Buff 被触发时由 BuffManager 执行的效果.
    /// 具体 Buff 可以在 InitEffect 中设置该委托.
    /// </summary>
    public Action<BuffRuntimeInfo> Effect;

    public int Id => id;
    public string BuffName => string.IsNullOrWhiteSpace(buffName) ? name : buffName;
    public float Duration => Mathf.Max(0f, duration);
    public bool IsPermanent => isPermanent;
    public BuffTriggerType TriggerType => triggerType;
    public float Interval => Mathf.Max(0f, interval);

    protected virtual void OnEnable()
    {
        InitEffect();
    }

    /// <summary>
    /// 初始化效果委托. 如果 Buff 需要手动设置 Effect, 可以重写该方法.
    /// </summary>
    public virtual void InitEffect()
    {
        Effect = OnTrigger;
    }

    /// <summary>
    /// 默认效果入口. 简单 Buff 可以直接重写该方法.
    /// </summary>
    protected virtual void OnTrigger(BuffRuntimeInfo info)
    {
    }

    private void OnValidate()
    {
        duration = Mathf.Max(0f, duration);
        interval = Mathf.Max(0f, interval);
    }
}
