using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.PG
{
    /// <summary>
    /// 开枪间隔特性
    /// </summary>
    public class ShootDuration
    {
        public float Duration { get; set; }//开枪间隔
        private float lastShootTime;//上一次开枪时间

        public ShootDuration(float duration)
        {
            Duration = duration;
        }

        public bool CanShoot => lastShootTime == 0f||Time.time - lastShootTime >= Duration;

        /// <summary>
        /// 记录开枪时间
        /// </summary>
        public void RecordShootTime()
        {
            lastShootTime = Time.time;
        }
    }
}
