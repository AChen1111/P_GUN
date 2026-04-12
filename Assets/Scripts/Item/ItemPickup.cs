using UnityEngine;

namespace QFramework.PG
{
    /// <summary>
    /// 场景中的拾取物：绑定 ItemSO，触发时调用效果并销毁。
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class ItemPickup : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private ItemSO _itemSO;

        private void Reset()
        {
            var c = GetComponent<Collider2D>();
            c.isTrigger = true;
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
        }

        public void Init(ItemSO itemSO)
        {
            _itemSO = itemSO;
            if (_itemSO != null && _itemSO.sprite != null && spriteRenderer != null)
            {
                spriteRenderer.sprite = _itemSO.sprite;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            if (_itemSO == null) return;

            var ctx = new ItemEffectContext
            {
                SourceItem = _itemSO,
                WorldPosition = transform.position
            };
            if (_itemSO.effects != null)
            {
                foreach (var effect in _itemSO.effects)
                {
                    if (effect != null) effect.OnPick(ctx);
                }
            }

            Destroy(gameObject);

            if (_itemSO.pickupAudio != null)
            {
                GlobalAudioPlay.Instance?.PlayerAudioSourceByClip(_itemSO.pickupAudio);
            }
            
        }
    }
}
