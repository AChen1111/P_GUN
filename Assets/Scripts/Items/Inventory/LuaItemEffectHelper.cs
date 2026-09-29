using Game.Core;
using UnityEngine;

namespace Game.Items
{
    /// <summary>
    /// 供道具效果 Lua 模块调用的消息助手, 隔离泛型事件在 Lua 侧的调用.
    /// </summary>
    public static class LuaItemEffectHelper
    {
        /// <summary>
        /// 在玩家头顶显示提示文本.
        /// </summary>
        /// <param name="text">提示内容.</param>
        /// <param name="seconds">显示秒数.</param>
        public static void ShowHeadMessage(string text, float seconds)
        {
            EventCenter.Trigger(CoreEvents.PlayerHeadMessageRequested, new PlayerHeadMessageEvent(text, seconds));
        }
    }
}