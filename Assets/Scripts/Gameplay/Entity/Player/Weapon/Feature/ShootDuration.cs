using System;
using System.Collections;
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
    /// 开枪间隔特性
    /// </summary>
    public class ShootDuration
    {
        public float Duration { get; set; }//开枪间隔
        private float lastShootTime;//上一次开枪时间
        private readonly Func<float> timeProvider;

        public ShootDuration(float duration, Func<float> timeProvider = null)
        {
            Duration = duration;
            // 默认使用 Unity 正常游戏时间, 敌人可以传入局部时间避免影响玩家武器手感.
            this.timeProvider = timeProvider ?? (() => Time.time);
        }

        public bool CanShoot => lastShootTime == 0f||timeProvider.Invoke() - lastShootTime >= Duration;

        /// <summary>
        /// 记录开枪时间
        /// </summary>
        public void RecordShootTime()
        {
            lastShootTime = timeProvider.Invoke();
        }
    }
}
