using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// 攻击视锥只限制出手, 不影响追踪玩家或绕墙寻路.
    /// </summary>
    public static class EnemyAttackCone
    {
        private static readonly int WallLayerMask = LayerMask.GetMask("Wall");

        public static bool ContainsPlayer(Vector3 origin, Vector2 facing, Player player, float range, float angle)
        {
            if (player == null || range <= 0f || angle <= 0f) return false;
            var delta = (Vector2)(player.transform.position - origin);
            var distance = delta.magnitude;
            if (distance > range) return false;
            if (distance <= 0.0001f) return true;
            var direction = delta / distance;
            var halfAngle = Mathf.Clamp(angle * 0.5f, 0f, 180f);
            if (Vector2.Dot(facing.normalized, direction) < Mathf.Cos(halfAngle * Mathf.Deg2Rad)) return false;
            // 墙体遮挡攻击路线, 敌人仍继续追踪当前玩家位置.
            return Physics2D.Raycast(origin, direction, distance, WallLayerMask).collider == null;
        }
    }
}
