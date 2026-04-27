using System.Collections.Generic;
using UnityEngine;

namespace QFramework.PG
{
    /// <summary>
    /// 场景中的可交互物品：挂载效果列表，玩家触碰时依次执行效果。
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Item : MonoBehaviour
    {
        [Header("拾取状态")]
        [SerializeField] private bool isActive = true;

        [Header("效果列表")]
        [SerializeField] private List<ItemEffectBase> effects = new List<ItemEffectBase>();

        [Header("拾取音效")]
        [SerializeField] private AudioClip pickupAudio;

        [Header("DOTween动画器")]
        [SerializeField] private DOTweenAnimation _dotweenAnimation;

        [Header("是否销毁")]
        [SerializeField] private bool isDestroy = true;

        private bool hasPicked;

        private void Awake()
        {
            if (_dotweenAnimation == null)
            {
                _dotweenAnimation = GetComponent<DOTweenAnimation>();
            }
        }

        private void Reset()
        {
            var c = GetComponent<Collider2D>();
            c.isTrigger = true;
        }

        public void SetPickupEnabled(bool enabled) => isActive = enabled;

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnPlayerTrigger(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            OnPlayerTrigger(other);
        }

        private void OnPlayerTrigger(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            if (!isActive || hasPicked) return;

            hasPicked = true;
            isActive = false;

            if (_dotweenAnimation != null)
            {
                _dotweenAnimation.Play(ApplyEffectsAndDestroy);
                return;
            }

            ApplyEffectsAndDestroy();
        }

        private void ApplyEffectsAndDestroy()
        {
            var ctx = new ItemEffectContext
            {
                SourceObject = gameObject,
                WorldPosition = transform.position
            };

            if (effects != null)
            {
                foreach (var effect in effects)
                {
                    if (effect != null) effect.OnPick(ctx);
                }
            }

            ItemWorldManager.Instance?.RemoveItem(this);

            if (pickupAudio != null)
            {
                GlobalAudioPlay.Instance.PlayerAudioSourceByClip(pickupAudio);
            }

            if (isDestroy)
            {
                Destroy(gameObject);
            }
        }
    }
}
