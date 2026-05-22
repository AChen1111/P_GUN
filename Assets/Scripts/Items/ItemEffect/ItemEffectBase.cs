using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;

namespace Game.Items
{
    /// <summary>
    /// 道具效果执行时的上下文.
    /// </summary>
    public struct ItemEffectContext
    {
        /// <summary>触发本次效果的物体.</summary>
        public GameObject SourceObject;

        /// <summary>触发时的世界坐标.</summary>
        public Vector3 WorldPosition;
    }

    /// <summary>
    /// 道具效果基类 (ScriptableObject).
    /// </summary>
    public abstract class ItemEffectBase : ScriptableObject
    {
        public virtual bool CanUse(ItemEffectContext ctx)
        {
            // 默认效果没有额外使用条件, 具体效果可按玩法规则阻止消耗.
            return true;
        }
        public abstract void OnPick(ItemEffectContext ctx);
    }
}
