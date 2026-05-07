using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理世界中道具的生成：直接实例化道具预制体。
/// </summary>
public class ItemWorldManager : MonoBehaviour
{
    public static ItemWorldManager Instance { get; private set; }

    private HashSet<Item> items = new();

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 生成道具预制体
    /// </summary>
    public Item SpawnItem(
        GameObject prefab,
        Vector3 worldPosition,
        bool isActive = true,
        AnimType animType = AnimType.None,
        Action onComplete = null,
        bool deferPickupUntilAnimComplete = false)
    {
        if (prefab == null)
        {
            Debug.LogError($"{nameof(ItemWorldManager)}: prefab 为空。");
            return null;
        }

        var instance = Instantiate(prefab, worldPosition, Quaternion.identity, transform);
        instance.SetActive(true);

        var item = instance.GetComponent<Item>();
        if (item == null)
        {
            Debug.LogError($"{nameof(ItemWorldManager)}: 预制体 {prefab.name} 上缺少 {nameof(Item)} 组件。");
            Destroy(instance);
            return null;
        }

        var startActive = deferPickupUntilAnimComplete ? false : isActive;
        item.SetPickupEnabled(startActive);
        AddItem(item);

        Action wrapped = () =>
        {
            if (deferPickupUntilAnimComplete)
                item.SetPickupEnabled(isActive);
            onComplete?.Invoke();
        };
        DOTweenAnimMgr.Play(animType, instance, onComplete: wrapped);
        return item;
    }

    /// <summary>
    /// 延迟生成道具预制体
    /// </summary>
    public void SpawnItemDelay(
        GameObject prefab,
        Vector3 worldPosition,
        float delay,
        bool isActive = true,
        AnimType animType = AnimType.None,
        Action onComplete = null,
        bool deferPickupUntilAnimComplete = false)
    {
        StartCoroutine(SpawnItemDelayCoroutine(prefab, worldPosition, delay, isActive, animType, onComplete, deferPickupUntilAnimComplete));
    }

    private IEnumerator SpawnItemDelayCoroutine(
        GameObject prefab,
        Vector3 worldPosition,
        float delay,
        bool isActive,
        AnimType animType,
        Action onComplete,
        bool deferPickupUntilAnimComplete)
    {
        yield return new WaitForSeconds(delay);
        SpawnItem(prefab, worldPosition, isActive, animType, onComplete, deferPickupUntilAnimComplete);
    }

    public void RemoveItem(Item item)
    {
        if (item == null) return;
        items.Remove(item);
    }

    public void AddItem(Item item)
    {
        if (item == null) return;
        items.Add(item);
    }


    [ContextMenu("生成宝箱")]
    public void GenerateChest()
    {
        
    }
}
