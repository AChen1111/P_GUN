using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    /// <summary>
    /// Buff 效果触发时机.
    /// </summary>
    public enum BuffTriggerType
    {
        /// <summary>Buff 存在期间每帧触发.</summary>
        Continuous,

        /// <summary>Buff 存在期间按固定间隔触发.</summary>
        Interval
    }
}
