using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// 玩法时间工具, 当前只负责敌人侧的局部时间倍率.
    /// </summary>
    public static class GameplayTime
    {
        private const float NormalEnemyTimeScale = 1f;

        private static float enemyTimeScale = NormalEnemyTimeScale;
        private static float enemyTime;
        private static float enemyDeltaTime;
        private static int cachedFrame = -1;

        public static float EnemyTimeScale => enemyTimeScale;
        public static float EnemyTime
        {
            get
            {
                RefreshEnemyClock();
                return enemyTime;
            }
        }

        public static float EnemyDeltaTime
        {
            get
            {
                RefreshEnemyClock();
                return enemyDeltaTime;
            }
        }

        /// <summary>
        /// 设置敌人时间倍率, 只影响接入 GameplayTime 的敌人移动、计时和子弹.
        /// </summary>
        /// <param name="timeScale">敌人时间倍率.</param>
        public static void SetEnemyTimeScale(float timeScale)
        {
            enemyTimeScale = Mathf.Clamp(timeScale, 0.01f, NormalEnemyTimeScale);
        }

        /// <summary>
        /// 恢复敌人正常时间倍率.
        /// </summary>
        public static void ResetEnemyTimeScale()
        {
            enemyTimeScale = NormalEnemyTimeScale;
        }

        private static void RefreshEnemyClock()
        {
            if (cachedFrame == Time.frameCount)
                return;

            // 敌人局部时间仍尊重全局暂停, 但在玩家子弹时间中额外乘敌人倍率.
            enemyDeltaTime = Time.deltaTime * enemyTimeScale;
            enemyTime += enemyDeltaTime;
            cachedFrame = Time.frameCount;
        }
    }
}
