using UnityEngine;

/// <summary>
/// 单个 Buff 实例的运行时状态.
/// </summary>
public class BuffRuntimeInfo
{
    public Buff Buff;
    public float Duration;
    public float RemainingTime;
    public float Interval;
    public float IntervalTimer;
    public bool IsPermanent;
    public int Index;//性能优化用
}
