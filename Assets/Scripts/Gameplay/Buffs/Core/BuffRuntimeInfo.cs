using Game.Pooling;
using System.Collections.Generic;
using Game.Core;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// 已转换成枚举的单条属性修正, 由 BuffManager 在添加 Buff 时解析.
    /// </summary>
    public struct ParsedStatModifier
    {
        public StatType StatType;
        public ModifierType ModifierType;
        public float Value;
    }

    /// <summary>
    /// 单个 Buff 实例的运行时状态.
    /// 配置来自 BuffData.lua, 行为预制体上的 LuaComponet 负责特殊生命周期.
    /// </summary>
    public class BuffRuntimeInfo
    {
        public Player owner;
        public Player Owner => owner;
        public Object Source;
        public BuffConfig Config;
        public LuaBehaviourHost Behavior;
        public BuffTag ParsedTag;
        public List<ParsedStatModifier> ParsedModifiers = new List<ParsedStatModifier>();
        public float Duration;
        public float RemainingTime;
        public float Interval;
        public float IntervalTimer;
        public bool IsPermanent;
        public int StackCount = 1;
        public int Index;
    }
}