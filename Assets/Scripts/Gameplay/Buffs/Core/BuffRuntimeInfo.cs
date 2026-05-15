using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    /// <summary>
    /// 单个 Buff 实例的运行时状态.
    /// </summary>
    public class BuffRuntimeInfo
    {
        public Player owner = Global.player;
        public Player Owner => owner;
        public Object Source;
        public Buff Buff;
        public LuaBuffInstance LuaInstance;
        public float Duration;
        public float RemainingTime;
        public float Interval;
        public float IntervalTimer;
        public bool IsPermanent;
        public int Index;

        /// <summary>
        /// Lua 可读写的临时参数表, 用于保存 Buff 运行时自定义数据.
        /// </summary>
        public readonly Dictionary<string, object> Params = new Dictionary<string, object>();

        /// <summary>
        /// 设置 Lua 运行时参数.
        /// </summary>
        /// <param name="key">参数键.</param>
        /// <param name="value">参数值.</param>
        public void SetParam(string key, object value)
        {
            if (string.IsNullOrEmpty(key)) return;

            Params[key] = value;
        }

        /// <summary>
        /// 获取 Lua 运行时参数.
        /// </summary>
        /// <param name="key">参数键.</param>
        /// <returns>参数值.</returns>
        public object GetParam(string key)
        {
            return !string.IsNullOrEmpty(key) && Params.TryGetValue(key, out var value) ? value : null;
        }
    }
}
