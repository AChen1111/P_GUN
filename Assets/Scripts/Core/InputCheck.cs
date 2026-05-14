using UnityEngine;

namespace Game.Core
{
    /// <summary>
    /// 基于 Unity Legacy <see cref="Input"/> 的通用按键检测（与项目中 Player 等用法一致）。
    /// </summary>
    public static class InputCheck
    {
        /// <summary>
        /// 当前是否有任意键盘键或鼠标键处于<strong>按住</strong>状态。
        /// 等价于 <see cref="Input.anyKey"/>（含鼠标键）。
        /// </summary>
        public static bool IsAnyKeyHeld() => Input.anyKey;

        /// <summary>
        /// <strong>本帧</strong>是否刚按下任意键盘键或鼠标键（仅首帧为 true）。
        /// 等价于 <see cref="Input.anyKeyDown"/>。
        /// </summary>
        public static bool IsAnyKeyDown() => Input.anyKeyDown;
    }
}
