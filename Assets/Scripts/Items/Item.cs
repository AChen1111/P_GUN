using System.Collections;
using DG.Tweening;
using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;

namespace Game.Items
{
    /// <summary>
    /// 场景中的可交互物品: 玩家靠近显示描述, 按 F 后收入背包或直接触发效果并播放拾取表现.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Item : MonoBehaviour, Game.Pooling.IPoolable
    {
        private const string PickupTriggerName = "OnPickup";

        [Header("物品数据")]
        [SerializeField] private int itemId;
        [SerializeField] private SpriteRenderer iconRenderer;

        [Header("拾取状态")]
        [SerializeField] private bool isActive = true;
        [Header("是否加入背包")]
        [SerializeField] private bool pickInBackBag = true;

        [Header("拾取音效")]
        // 配置保存短名, 资源来自阶段预加载缓存.

        [SerializeField, AddressableKey(AddressableAssetKind.AudioClip)] private string pickupAudioKey = string.Empty;
        private AudioClip pickupAudio => AddressableAssetAccess.Get<AudioClip>(pickupAudioKey);

        [Header("DOTween动画器")]
        [SerializeField] private GameDOTweenAnimation _dotweenAnimation;

        [Header("Animator动画器")]
        [SerializeField] private Animator _animator;
        [SerializeField] private float pickupAnimatorFallbackDelay = 0.6f;

        [Header("是否销毁")]
        [SerializeField] private bool isDestroy = true;

        private int playerColliderCount;
        private bool isPlayerInRange;
        private bool hasPicked;
        private bool inventoryAdded;
        private bool pickupPresentationFinished;
        private PlayerInventory playerInventoryInRange;
        private Coroutine animatorFallbackCoroutine;
        private LuaBehaviourHost itemLua;
        private Vector3 prefabLocalScale;

        public int ItemId => itemId;

        /// <summary>
        /// 初始化运行时依赖.
        /// </summary>
        private void Awake()
        {
            prefabLocalScale = transform.localScale;
            if (_dotweenAnimation == null)
            {
                _dotweenAnimation = GetComponent<GameDOTweenAnimation>();
            }

            if (_animator == null)
            {
                _animator = GetComponent<Animator>();
            }

            // 世界道具的拾取规则在预制体的 LuaComponet 模块里, 入背包的道具可以不挂.
            itemLua = GetComponent<LuaBehaviourHost>();
        }

        /// <summary>
        /// 重置编辑器默认配置.
        /// </summary>
        private void Reset()
        {
            var c = GetComponent<Collider2D>();
            c.isTrigger = true;
            _dotweenAnimation = GetComponent<GameDOTweenAnimation>();
            _animator = GetComponent<Animator>();
            iconRenderer = GetComponent<SpriteRenderer>();
        }
        private void Update()
        {
            if (!isPlayerInRange || !isActive || hasPicked) return;

            if (Input.GetKeyDown(KeyCode.F))
            {
                PickUp();
            }

            void PickUp()
            {
                if (!isActive || hasPicked)
                    return;
                if (!TryProcessPickup())
                {
                    return;
                }

                hasPicked = true;
                isActive = false;
                HideTip();
                EventCenter.Trigger(ItemEvents.ItemPicked, this);
                if (TryPlayAnimatorPickup())
                {
                    return;
                }

                if (_dotweenAnimation != null)
                {
                    _dotweenAnimation.Play(FinishPickupPresentation);
                    return;
                }

                FinishPickupPresentation();
            }

    PlayerInventory ResolvePlayerInventory()
    {
        // 物品只使用当前触发范围内的玩家背包, 避免跨模块依赖全局玩家引用.
        return playerInventoryInRange;
    }

    bool TryAddToInventory(PlayerInventory inventory)
    {
        if (inventoryAdded)
            return true;
        inventoryAdded = inventory.AddFromItem(this);
        return inventoryAdded;
    }

    bool TryProcessPickup()
    {
        if (!pickInBackBag)
        {
            ApplyPickupLua();
            return true;
        }

        var inventory = ResolvePlayerInventory();
        if (inventory == null)
        {
            Debug.LogError($"{nameof(Item)}拾取失败, Player缺少{nameof(PlayerInventory)}组件.", this);
            return false;
        }

        if (!TryAddToInventory(inventory))
        {
            return false;
        }

        // 入背包的道具也可以带 Lua 模块处理拾取时刻的规则, 没挂时跳过.
        if (itemLua != null)
        {
            itemLua.SetLuaField("itemId", itemId);
            itemLua.SetLuaField("sourceObject", gameObject);
            itemLua.CallLuaFunction("OnPickedUp");
        }

        return true;
    }

    void ApplyPickupLua()
    {
        // 非背包物品的效果完全由世界道具预制体上的 LuaComponet 决定.
        if (itemLua == null)
        {
            Debug.LogError($"{nameof(Item)}拾取失败, pickInBackBag=false 的道具必须挂 LuaComponet.", this);
            return;
        }

        itemLua.SetLuaField("itemId", itemId);
        itemLua.SetLuaField("sourceObject", gameObject);
        itemLua.CallLuaFunction("OnPickedUp");
    }

    bool TryPlayAnimatorPickup()
    {
        if (_animator == null || !HasPickupTrigger(_animator))
            return false;
        _animator.SetTrigger(PickupTriggerName);
        if (pickupAnimatorFallbackDelay > 0f)
        {
            animatorFallbackCoroutine = StartCoroutine(ApplyEffectsAfterDelay(pickupAnimatorFallbackDelay));
        }

        return true;
    }

    static bool HasPickupTrigger(Animator animator)
    {
        foreach (var parameter in animator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Trigger && parameter.name == PickupTriggerName)
            {
                return true;
            }
        }

        return false;
    }

    IEnumerator ApplyEffectsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        FinishPickupPresentation();
        animatorFallbackCoroutine = null;
    }
}

        /// <summary>
        /// 注销禁用时需要的监听.
        /// </summary>
        private void OnDisable()
        {
            HideTip();
        }
        public void SetPickupEnabled(bool enabled)
        {
            isActive = enabled;

            if (!isActive)
            {
                HideTip();
                return;
            }

            if (isPlayerInRange && !hasPicked)
            {
                ShowTip();
            }
        }
        public void OnSpawnFromPool()
        {
            // 上次生成动画可能中途回池, 每次取出都恢复预制体缩放.
            transform.DOKill(false);
            transform.localScale = prefabLocalScale;
            ResetPoolRuntimeState();
            ResetAnimatorState();

            void ResetAnimatorState()
            {
                if (_animator == null)
                    return;
                _animator.ResetTrigger(PickupTriggerName);
                _animator.Rebind();
                _animator.Update(0f);
            }
}
        public void OnRecycleToPool()
        {
            StopAnimatorFallbackCoroutine();
            transform.DOKill(false);
            HideTip();
            ResetPoolRuntimeState();
        }

        /// <summary>
        /// 处理 2D 触发进入事件.
        /// </summary>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!IsPlayer(other)) return;

            playerColliderCount++;
            isPlayerInRange = true;
            if (playerInventoryInRange == null)
            {
                playerInventoryInRange = other.GetComponentInParent<PlayerInventory>();
            }

            ShowTip();
        }

        /// <summary>
        /// 处理 2D 触发离开事件.
        /// </summary>
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!IsPlayer(other)) return;

            playerColliderCount = Mathf.Max(0, playerColliderCount - 1);
            if (playerColliderCount > 0) return;

            isPlayerInRange = false;
            playerInventoryInRange = null;
            HideTip();
        }
        public void OnPickupAnimFinished()
        {
            FinishPickupPresentation();
        }
        [ContextMenu("编辑器/从数据库同步物品图标")]
        public bool EditorApplyIconFromDatabase()
        {
            ResolveIconRenderer();

            if (iconRenderer == null)
            {
                Debug.LogWarning($"{nameof(Item)}: 未绑定 SpriteRenderer, 无法同步物品图标.", this);
                return false;
            }

            if (!TryResolveItemData(out var data))
            {
                Debug.LogWarning($"{nameof(Item)}: 未找到 itemId={itemId} 的物品数据, 无法同步物品图标.", this);
                return false;
            }

            // 编辑器工具只负责把数据库图标写回 Prefab, 运行时不再重复覆盖.
            iconRenderer.sprite = data.icon;
            return true;

            void ResolveIconRenderer()
            {
                if (iconRenderer != null)
                    return;
                // 默认使用同物体上的 SpriteRenderer, 让 Prefab 不需要额外层级绑定.
                iconRenderer = GetComponent<SpriteRenderer>();
            }
}
        private void FinishPickupPresentation()
        {
            if (pickupPresentationFinished) return;
            pickupPresentationFinished = true;

            if (animatorFallbackCoroutine != null)
            {
                StopAnimatorFallbackCoroutine();
            }

            if (pickupAudio != null)
            {
                GlobalAudioPlay.Instance.PlayerAudioSourceByClip(pickupAudio);
            }

            if (isDestroy)
            {
                ItemPool.Instance.Release(this);
            }
        }
        private void ShowTip()
        {
            if (!isActive || hasPicked) return;
            EventCenter.Trigger(ItemEvents.ItemTipShown, ResolveItemData());
        }
        private void HideTip()
        {
            EventCenter.Trigger(ItemEvents.ItemTipHidden);
        }
        private ItemData ResolveItemData()
        {
            return TryResolveItemData(out var data) ? data : default;
        }
        public ItemData GetItemData()
        {
            return ResolveItemData();
        }
        public bool TryGetItemData(out ItemData data)
        {
            // 背包入库前必须解析到正式配置, 避免把默认空数据写入运行时背包.
            return TryResolveItemData(out data);
        }
        private bool TryResolveItemData(out ItemData data)
        {
            // 显示数据来自 ItemData.lua, 图标取自按 id 缓存的 Sprite.
            try
            {
                var config = LuaDataRuntime.GetItemConfig(itemId);
                data = new ItemData(itemId, config.Name, config.Description, config.IconKey);
                return true;
            }
            catch (System.Exception exception)
            {
                Debug.LogError($"{nameof(Item)}: 读取 ItemData 失败, itemId={itemId}, Error: {exception.Message}", this);
                data = default;
                return false;
            }
        }
        private static bool IsPlayer(Collider2D other)
        {
            return other != null && other.CompareTag("Player");
        }
        private void ResetPoolRuntimeState()
        {
            isActive = true;
            playerColliderCount = 0;
            isPlayerInRange = false;
            hasPicked = false;
            inventoryAdded = false;
            pickupPresentationFinished = false;
            playerInventoryInRange = null;
        }
        private void StopAnimatorFallbackCoroutine()
        {
            if (animatorFallbackCoroutine == null) return;

            StopCoroutine(animatorFallbackCoroutine);
            animatorFallbackCoroutine = null;
        }
    }
}
