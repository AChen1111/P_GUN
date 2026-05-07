using System.Collections.Generic;
using System.Collections;
using UnityEngine;

/// <summary>
/// 场景中的可交互物品：玩家靠近显示描述，按 F 后播放拾取表现并执行效果列表。
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour
{
    private const string PickupTriggerName = "OnPickup";

    [Header("物品数据")]
    [SerializeField] private int itemId;
    [SerializeField] private ItemDatabase itemDatabase;

    [Header("拾取状态")]
    [SerializeField] private bool isActive = true;

    [Header("效果列表")]
    [SerializeField] private List<ItemEffectBase> effects = new List<ItemEffectBase>();

    [Header("拾取音效")]
    [SerializeField] private AudioClip pickupAudio;

    [Header("DOTween动画器")]
    [SerializeField] private DOTweenAnimation _dotweenAnimation;

    [Header("Animator动画器")]
    [SerializeField] private Animator _animator;
    [SerializeField] private float pickupAnimatorFallbackDelay = 0.6f;

    [Header("是否销毁")]
    [SerializeField] private bool isDestroy = true;

    private int playerColliderCount;
    private bool isPlayerInRange;
    private bool hasPicked;
    private bool effectsApplied;
    private Coroutine animatorFallbackCoroutine;

    private void Awake()
    {
        if (_dotweenAnimation == null)
        {
            _dotweenAnimation = GetComponent<DOTweenAnimation>();
        }

        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
    }

    private void Reset()
    {
        var c = GetComponent<Collider2D>();
        c.isTrigger = true;
        _dotweenAnimation = GetComponent<DOTweenAnimation>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isPlayerInRange || !isActive || hasPicked) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            PickUp();
        }
    }

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsPlayer(other)) return;

        playerColliderCount++;
        isPlayerInRange = true;
        ShowTip();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!IsPlayer(other)) return;

        playerColliderCount = Mathf.Max(0, playerColliderCount - 1);
        if (playerColliderCount > 0) return;

        isPlayerInRange = false;
        HideTip();
    }

    public void OnPickupAnimFinished()
    {
        ApplyEffectsAndDestroy();
    }

    private void PickUp()
    {
        if (!isActive || hasPicked) return;

        hasPicked = true;
        isActive = false;
        HideTip();
        EventCenter.Trigger(GameEvent.ItemPicked, this);

        if (TryPlayAnimatorPickup())
        {
            return;
        }

        if (_dotweenAnimation != null)
        {
            _dotweenAnimation.Play(ApplyEffectsAndDestroy);
            return;
        }

        ApplyEffectsAndDestroy();
    }

    private bool TryPlayAnimatorPickup()
    {
        if (_animator == null || !HasPickupTrigger(_animator)) return false;

        _animator.SetTrigger(PickupTriggerName);

        if (pickupAnimatorFallbackDelay > 0f)
        {
            animatorFallbackCoroutine = StartCoroutine(ApplyEffectsAfterDelay(pickupAnimatorFallbackDelay));
        }

        return true;
    }

    private IEnumerator ApplyEffectsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ApplyEffectsAndDestroy();
        animatorFallbackCoroutine = null;
    }

    private void ApplyEffectsAndDestroy()
    {
        if (effectsApplied) return;
        effectsApplied = true;

        if (animatorFallbackCoroutine != null)
        {
            StopCoroutine(animatorFallbackCoroutine);
            animatorFallbackCoroutine = null;
        }

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

    private void ShowTip()
    {
        if (!isActive || hasPicked) return;
        EventCenter.Trigger(GameEvent.ItemTipShown, ResolveItemData());
    }

    private void HideTip()
    {
        EventCenter.Trigger(GameEvent.ItemTipHidden);
    }

    private ItemData ResolveItemData()
    {
        var database = itemDatabase != null ? itemDatabase : ItemDatabase.Default;
        if (database != null && database.TryGetById(itemId, out var data))
        {
            return data;
        }

        return ItemData.CreateFallback(itemId, gameObject.name);
    }

    private static bool IsPlayer(Collider2D other)
    {
        return other != null && other.CompareTag("Player");
    }

    private static bool HasPickupTrigger(Animator animator)
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
}
