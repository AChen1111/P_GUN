using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// 近战敌人的前方检测器, 需要挂在敌人子物体并使用 Trigger 碰撞器.
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    public class MeleeAttackDetector : MonoBehaviour
    {
        [SerializeField] private EnemyMelee owner;

        private readonly Collider2D[] overlapResults = new Collider2D[8];
        private Collider2D detectorCollider;
        private Player currentPlayer;

        public bool HasPlayer => currentPlayer != null;

        /// <summary>
        /// 初始化运行时依赖.
        /// </summary>
        private void Awake()
        {
            detectorCollider = GetComponent<Collider2D>();
            detectorCollider.isTrigger = true;

            if (owner == null)
            {
                owner = GetComponentInParent<EnemyMelee>();
            }
        }

        /// <summary>
        /// 由近战敌人初始化归属关系, 便于对象池复用后重新绑定.
        /// </summary>
        /// <param name="enemy">所属近战敌人.</param>
        public void Init(EnemyMelee enemy)
        {
            owner = enemy;
        }

        /// <summary>
        /// 处理 2D 触发进入事件.
        /// </summary>
        private void OnTriggerEnter2D(Collider2D other)
        {
            TryRequestAttack(other);
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            TryRequestAttack(other);
        }

        /// <summary>
        /// 处理 2D 触发离开事件.
        /// </summary>
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            var player = other.GetComponentInParent<Player>();
            if (player == currentPlayer)
            {
                currentPlayer = null;
            }
        }

        /// <summary>
        /// 在攻击结算帧重新检测玩家是否仍在攻击范围内.
        /// </summary>
        /// <param name="player">检测到的玩家.</param>
        /// <returns>玩家是否仍在检测器碰撞体内.</returns>
        public bool TryGetPlayerInRange(out Player player)
        {
            player = null;
            if (detectorCollider == null) return false;

            // 最后一帧以碰撞体实时重叠结果为准, 避免玩家离开后仍被缓存目标扣血.
            var count = detectorCollider.OverlapCollider(new ContactFilter2D().NoFilter(), overlapResults);
            for (var i = 0; i < count; i++)
            {
                var result = overlapResults[i];
                if (result == null || !result.CompareTag("Player")) continue;

                player = result.GetComponentInParent<Player>();
                if (player != null)
                {
                    currentPlayer = player;
                    return true;
                }
            }

            currentPlayer = null;
            return false;
        }
        private void TryRequestAttack(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            var player = other.GetComponentInParent<Player>();
            if (player == null) return;

            currentPlayer = player;
            owner?.RequestAttack(player);
        }
    }
}
