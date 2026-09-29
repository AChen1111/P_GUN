using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// 分离向量计算: 分离半径内的同伴按距离产生推力, 只推开不聚集.
    /// </summary>
    public static class EnemySeparation
    {
        private static readonly int EnemyLayerMask = LayerMask.GetMask("EnemyLayer");

        // 单个敌人的邻居检测缓冲, 超出的邻居本次不参与分离.
        private static readonly Collider2D[] OverlapBuffer = new Collider2D[16];

        /// <summary>
        /// 计算分离向量, 越近的同伴推力越大.
        /// </summary>
        /// <param name="self">发起计算的敌人.</param>
        /// <param name="separationRadius">分离半径, 来自 EnemyData.</param>
        /// <returns>分离向量, 没有邻居时为零向量.</returns>
        public static Vector2 ComputeSeparation(EnemyBase self, float separationRadius)
        {
            if (self == null || separationRadius <= 0f)
            {
                return Vector2.zero;
            }

            var selfPosition = self.transform.position;
            var count = Physics2D.OverlapCircleNonAlloc(selfPosition, separationRadius, OverlapBuffer, EnemyLayerMask);

            var separation = Vector2.zero;
            for (var i = 0; i < count; i++)
            {
                var neighborCollider = OverlapBuffer[i];
                if (neighborCollider == null)
                {
                    continue;
                }

                var neighbor = neighborCollider.GetComponentInParent<EnemyBase>();
                if (neighbor == null || neighbor == self || neighbor.BrainIsDead)
                {
                    continue;
                }

                // 中心距用 transform.position 计算, 不用碰撞体外形.
                var toNeighbor = (Vector2)(neighbor.transform.position - selfPosition);
                var distance = toNeighbor.magnitude;
                if (distance <= 0.0001f || distance >= separationRadius)
                {
                    continue;
                }

                // 远离邻居而非朝向邻居, 距离越近推开越强.
                separation -= (1f - distance / separationRadius) * (toNeighbor / distance);
            }

            return separation;
        }
    }
}
