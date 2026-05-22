using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// 单个 Buff 实例的运行时状态.
    /// </summary>
    public class BuffRuntimeInfo
    {
        public Player owner;
        public Player Owner => owner;
        public Object Source;
        public Buff Buff;
        public IBuffScriptInstance ScriptInstance;
        public float Duration;
        public float RemainingTime;
        public float Interval;
        public float IntervalTimer;
        public bool IsPermanent;
        public int StackCount = 1;
        public int Index;
    }
}
