/// <summary>
/// Buff 效果触发时机.
/// </summary>
public enum BuffTriggerType
{
    /// <summary>Buff 被添加, 或同一个 Buff 被再次添加时触发一次.</summary>
    OnApply,

    /// <summary>Buff 被移除前触发一次.</summary>
    OnRemove,

    /// <summary>Buff 存在期间每帧触发.</summary>
    Continuous,

    /// <summary>Buff 存在期间按固定间隔触发.</summary>
    Interval
}
