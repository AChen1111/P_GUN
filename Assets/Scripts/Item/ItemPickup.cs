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
        [SerializeField] private bool isActive = true;

        [SerializeField] private ItemSO _itemSO;

        private void Start() {
            Init(_itemSO, isActive);
        }

        private void Reset()
        {
            var c = GetComponent<Collider2D>();
            c.isTrigger = true;
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
        }

        public void Init(ItemSO itemSO, bool isActive = true)
        {
            _itemSO = itemSO;
            this.isActive = isActive;
            if (_itemSO != null && _itemSO.sprite != null && spriteRenderer != null)
            {
                spriteRenderer.sprite = _itemSO.sprite;
            }

            //添加到集合当中
            ItemWorldManager.Instance.AddItemPickup(this);
        }

        public void SetPickupEnabled(bool enabled) => isActive = enabled;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            if (_itemSO == null || !isActive) return;



            ///配置物品信息
            var ctx = new ItemEffectContext
            {
                SourceItem = _itemSO,
                WorldPosition = transform.position
            };

            //检查并执行物品效果
            if (_itemSO.effects != null)
            {
                foreach (var effect in _itemSO.effects)
                {
                    if (effect != null) effect.OnPick(ctx);
                }
            }

            //移除物品拾取物
            ItemWorldManager.Instance.RemoveItemPickup(this);
            Destroy(gameObject);

            //尝试播放音效(若有配置)
            if (_itemSO.pickupAudio != null)
            {
                GlobalAudioPlay.Instance.PlayerAudioSourceByClip(_itemSO.pickupAudio);
            }
            
        }

        private void OnTriggerStay2D(Collider2D other) {
            if (!other.CompareTag("Player")) return;
            if (_itemSO == null || !isActive) return;


            ///配置物品信息
            var ctx = new ItemEffectContext
            {
                SourceItem = _itemSO,
                WorldPosition = transform.position
            };

            //检查并执行物品效果
            if (_itemSO.effects != null)
            {
                foreach (var effect in _itemSO.effects)
                {
                    if (effect != null) effect.OnPick(ctx);
                }
            }
            //移除物品拾取物
            ItemWorldManager.Instance.RemoveItemPickup(this);
            Destroy(gameObject);

            //尝试播放音效(若有配置)
            if (_itemSO.pickupAudio != null)
            {
                GlobalAudioPlay.Instance.PlayerAudioSourceByClip(_itemSO.pickupAudio);
            }

        }


    }
}
