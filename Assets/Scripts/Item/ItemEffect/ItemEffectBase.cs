using UnityEngine;

namespace QFramework.PG
{
    /// <summary>
    /// 道具效果执行时的上下文，便于扩展参数而不改方法签名。
    /// </summary>
    public struct ItemEffectContext
    {
        /// <summary>触发本次效果的 ItemSO（字段名避免与类型同名，防止部分编译器对初始化器解析出错）。</summary>
        public ItemSO SourceItem;

        /// <summary>
        /// 触发拾取/打开时的世界坐标（例如宝箱在此位置生成掉落物）。
        /// </summary>
        public Vector3 WorldPosition;
    }

    /// <summary>
    /// 道具效果基类（ScriptableObject，便于在 ItemSO 中引用与做子资产）。
    /// </summary>
    public abstract class ItemEffectBase : ScriptableObject
    {
        /// <summary>
        /// 拾取并应用效果时调用。
        /// </summary>
        public abstract void OnPick(ItemEffectContext ctx);
    }
}
