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

        private Collider2D detectorCollider;
        private Player currentPlayer;

        public bool HasPlayer => currentPlayer != null;

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

        private void OnTriggerEnter2D(Collider2D other)
        {
            TryRequestAttack(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            TryRequestAttack(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            var player = other.GetComponent<Player>();
            if (player == currentPlayer)
            {
                currentPlayer = null;
            }
        }

        private void TryRequestAttack(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            var player = other.GetComponent<Player>();
            if (player == null) return;

            currentPlayer = player;
            owner?.RequestAttack(player);
        }
    }
}
