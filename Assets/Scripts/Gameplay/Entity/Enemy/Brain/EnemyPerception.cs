using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// 敌人视野判定: 视野半径, 面朝扇形, 墙体射线三关同时通过才算看得见玩家.
    /// </summary>
    public static class EnemyPerception
    {
        private static readonly int WallLayerMask = LayerMask.GetMask("Wall");

        /// <summary>
        /// 判断敌人是否看得见玩家.
        /// </summary>
        /// <param name="selfPosition">敌人位置.</param>
        /// <param name="facingDirection">面朝方向, 由精灵翻转得到.</param>
        /// <param name="player">目标玩家.</param>
        /// <param name="visionRadius">视野半径, 来自 EnemyData.</param>
        /// <param name="visionAngle">视野角度, 来自 EnemyData.</param>
        /// <returns>是否看得见.</returns>
        public static bool CanSeePlayer(Vector3 selfPosition, Vector2 facingDirection, Player player, float visionRadius, float visionAngle)
        {
            if (player == null || visionRadius <= 0f || visionAngle <= 0f)
            {
                return false;
            }

            var toPlayer = (Vector2)(player.transform.position - selfPosition);
            var distance = toPlayer.magnitude;
            if (distance > visionRadius || distance <= 0.0001f)
            {
                return false;
            }

            var direction = toPlayer / distance;

            // 扇形判定: 面朝方向与玩家方向的夹角不超过半角.
            var halfAngle = Mathf.Clamp(visionAngle * 0.5f, 0f, 180f);
            if (Vector2.Dot(facingDirection.normalized, direction) < Mathf.Cos(halfAngle * Mathf.Deg2Rad))
            {
                return false;
            }

            // 视线判定: 到玩家的射线不能被墙挡住.
            var wallHit = Physics2D.Raycast(selfPosition, direction, distance, WallLayerMask);
            return wallHit.collider == null;
        }
    }
}