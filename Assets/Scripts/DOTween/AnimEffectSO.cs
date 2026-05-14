using System;
using UnityEngine;

namespace Game.Animation
{
    /// <summary>
    /// 动画效果 SO 基类。每种动画效果继承此类，实现 Play 方法。
    /// SO 资产的 name 即为注册 key，运行时通过 name 自动建立映射。
    /// </summary>
    public abstract class AnimEffectSO : ScriptableObject
    {
        /// <summary>
        /// 播放动画效果。
        /// </summary>
        /// <param name="target">目标物体</param>
        /// <param name="duration">动画时长</param>
        /// <param name="onComplete">结束回调</param>
        public abstract void Play(GameObject target, float duration, Action onComplete);
    }
}
