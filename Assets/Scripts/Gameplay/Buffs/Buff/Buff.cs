using System;
using UnityEngine;

/// <summary>
/// 所有具体 Buff 的基础资产.
/// 具体 Buff 继承该类, 并重写 InitEffect, OnTrigger 或 OnEnd 来定义行为.
/// </summary>
public abstract class Buff : ScriptableObject
{
    [Header("Basic")]
    [Tooltip("Buff 的唯一 id, 用于数据库查询和道具配置.")]
    [SerializeField] private int id = 0;

    [Tooltip("Buff 的显示名称. 如果为空, 会使用资产名称.")]
    [SerializeField] private string buffName = string.Empty;

    [Header("Lifetime")]
    [Tooltip("Buff 的持续时间, 单位为秒. 当不是永久 Buff 时生效.")]
    [Min(0f)]
    [SerializeField] private float duration = 5f;

    [Tooltip("是否为永久 Buff. 开启后不会因为持续时间结束而自动移除.")]
    [SerializeField] private bool isPermanent = false;

    [Header("Trigger")]
    [Tooltip("Buff 存在期间的触发方式. Continuous 为每帧触发, Interval 为按固定间隔触发.")]
    [SerializeField] private BuffTriggerType triggerType = BuffTriggerType.Continuous;

    [Tooltip("固定间隔触发的时间间隔, 单位为秒. 仅在触发方式为 Interval 时生效.")]
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
    protected abstract void OnTrigger(BuffRuntimeInfo info);

    /// <summary>
    /// Buff 开始时的回调. 所有 Buff 被添加或重复调用时都会调用, 子类可以按需重写.
    /// </summary>
    public virtual void OnStart(BuffRuntimeInfo info)
    {
    }

    /// <summary>
    /// Buff 结束时的回调. 所有 Buff 被移除时都会调用, 子类可以按需重写.
    /// </summary>
    public virtual void OnEnd(BuffRuntimeInfo info)
    {
    }

    private void OnValidate()
    {
        duration = Mathf.Max(0f, duration);
        interval = Mathf.Max(0f, interval);
    }
}
