using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家身上的 Buff 运行时管理器.
/// 负责保存当前 Buff, 更新计时器, 并按触发类型执行效果.
/// </summary>
public class BuffManager : MonoBehaviour
{
    [SerializeField] private BuffDataBase dataBase = null;
    
    /// <summary>
    /// 当前生效的 Buff 列表, 用于每帧顺序更新.
    /// </summary>
    private readonly List<BuffRuntimeInfo> buffs = new List<BuffRuntimeInfo>();

    /// <summary>
    /// Buff 到运行时信息的映射, 用于快速查找和去重.
    /// </summary>
    private readonly Dictionary<Buff, BuffRuntimeInfo> buffInfoMap = new Dictionary<Buff, BuffRuntimeInfo>();

    private void Update()
    {
        var deltaTime = Time.deltaTime;

        for (var i = buffs.Count - 1; i >= 0; i--)
        {
            UpdateBuff(buffs[i], deltaTime);
        }
    }

#region Public API

    /// <summary>
    /// 通过 id 添加 Buff.
    /// </summary>
    /// <param name="buffId">Buff id</param>
    /// <returns>Buff 运行时信息</returns>
    public BuffRuntimeInfo AddBuffById(int buffId)
    {
        if (dataBase == null)
        {
            Debug.LogWarning($"{nameof(BuffManager)}: 未设置 {nameof(BuffDataBase)}，无法通过 id 添加 Buff。", this);
            return null;
        }

        return AddBuff(dataBase.GetById(buffId));
    }

    /// <summary>
    /// 直接添加 Buff. 如果 Buff 已存在, 只重置持续时间并触发 OnStart.
    /// </summary>
    /// <param name="buff">Buff 配置</param>
    /// <returns>Buff 运行时信息</returns>
    public BuffRuntimeInfo AddBuff(Buff buff)
    {
        if (buff == null) return null;

        if (buffInfoMap.TryGetValue(buff, out var existing))
        {
            ResetBuffRuntimeInfo(existing, buff);
            TriggerOnStart(existing);
            return existing;
        }

        var info = CreateBuffRuntimeInfo(buff);

        info.Index = buffs.Count;
        buffs.Add(info);
        buffInfoMap[buff] = info;
        TriggerOnStart(info);
        return info;
    }

    /// <summary>
    /// 移除 Buff.
    /// </summary>
    /// <param name="buff">Buff</param>
    /// <returns>是否成功移除</returns>
    public bool RemoveBuff(Buff buff)
    {
        if (buff == null) return false;

        if (buffInfoMap.TryGetValue(buff, out var info))
        {
            TriggerOnEnd(info);
            RemoveAt(info.Index);
            return true;
        }

        return false;
    }

    /// <summary>
    /// 清空所有 Buff.
    /// </summary>
    public void ClearBuffs()
    {
        for (var i = buffs.Count - 1; i >= 0; i--)
        {
            TriggerOnEnd(buffs[i]);
            RemoveAt(i);
        }

        buffInfoMap.Clear();
    }

#endregion

#region Runtime

    /// <summary>
    /// 更新 Buff 的计时器和状态.
    /// </summary>
    /// <param name="info">Buff 运行时信息</param>
    /// <param name="deltaTime">时间增量</param>
    private void UpdateBuff(BuffRuntimeInfo info, float deltaTime)
    {
        if (!info.IsPermanent)
        {
            info.RemainingTime -= deltaTime;

            if (info.RemainingTime <= 0f)
            {
                RemoveBuff(info.Buff);
                return;
            }
        }

        TriggerDuringUpdate(info, deltaTime);
    }

    /// <summary>
    /// 按 Buff 的持续触发类型执行效果.
    /// </summary>
    /// <param name="info">Buff 运行时信息</param>
    /// <param name="deltaTime">时间增量</param>
    private void TriggerDuringUpdate(BuffRuntimeInfo info, float deltaTime)
    {
        switch (info.Buff.TriggerType)
        {
            case BuffTriggerType.Continuous:
                InvokeEffect(info);
                break;

            case BuffTriggerType.Interval:
                TriggerInterval(info, deltaTime);
                break;
        }
    }

    /// <summary>
    /// 按固定间隔触发 Buff 效果.
    /// </summary>
    /// <param name="info">Buff 运行时信息</param>
    /// <param name="deltaTime">时间增量</param>
    private void TriggerInterval(BuffRuntimeInfo info, float deltaTime)
    {
        if (info.Interval <= 0f) return;

        info.IntervalTimer += deltaTime;

        while (info.IntervalTimer >= info.Interval && buffInfoMap.ContainsKey(info.Buff))
        {
            info.IntervalTimer -= info.Interval;
            InvokeEffect(info);
        }
    }

#endregion

#region Create And Reset

    /// <summary>
    /// 创建 Buff 运行时信息.
    /// </summary>
    /// <param name="buff">Buff 配置</param>
    /// <returns>Buff 运行时信息</returns>
    private BuffRuntimeInfo CreateBuffRuntimeInfo(Buff buff)
    {
        var info = new BuffRuntimeInfo
        {
            Buff = buff
        };

        ResetBuffRuntimeInfo(info, buff);
        return info;
    }

    /// <summary>
    /// 重置 Buff 运行时计时数据.
    /// </summary>
    /// <param name="info">Buff 运行时信息</param>
    /// <param name="buff">Buff 配置</param>
    private void ResetBuffRuntimeInfo(BuffRuntimeInfo info, Buff buff)
    {
        info.Duration = buff.Duration;
        info.RemainingTime = buff.Duration;
        info.Interval = buff.Interval;
        info.IntervalTimer = 0f;
        info.IsPermanent = buff.IsPermanent;
    }

#endregion

#region Trigger

    /// <summary>
    /// 触发 Buff 的开始回调.
    /// </summary>
    /// <param name="info">Buff 运行时信息</param>
    private static void TriggerOnStart(BuffRuntimeInfo info)
    {
        info.Buff.OnStart(info);
    }

    /// <summary>
    /// 触发 Buff 的结束回调.
    /// </summary>
    /// <param name="info">Buff 运行时信息</param>
    private static void TriggerOnEnd(BuffRuntimeInfo info)
    {
        info.Buff.OnEnd(info);
    }

    /// <summary>
    /// 执行 Buff 的效果.
    /// </summary>
    /// <param name="info">Buff 运行时信息</param>
    private static void InvokeEffect(BuffRuntimeInfo info)
    {
        info?.Buff?.Effect?.Invoke(info);
    }

#endregion

#region Remove Helpers

    /// <summary>
    /// 使用尾部交换的方式移除指定索引的 Buff.
    /// </summary>
    /// <param name="index">索引</param>
    private void RemoveAt(int index)
    {
        var lastIndex = buffs.Count - 1;
        var removedInfo = buffs[index];
        var lastInfo = buffs[lastIndex];

        if (index != lastIndex)
        {
            buffs[index] = lastInfo;
            buffs[lastIndex] = removedInfo;

            removedInfo.Index = lastIndex;
            lastInfo.Index = index;
        }

        buffs.RemoveAt(lastIndex);
        buffInfoMap.Remove(removedInfo.Buff);
    }

#endregion
}
