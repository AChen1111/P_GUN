using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "PG/Item/Item Database", order = 0)]
public class ItemDatabase : ScriptableObject
{
    public const string DefaultResourcesPath = "ItemDatabase";

    [SerializeField] private List<ItemData> items = new List<ItemData>();

    private static ItemDatabase cachedDefault;
    private readonly Dictionary<int, ItemData> itemMap = new Dictionary<int, ItemData>();
    private int indexedItemCount = -1;

    public IReadOnlyList<ItemData> Items => items;

    public static ItemDatabase Default
    {
        get
        {
            if (cachedDefault == null)
            {
                cachedDefault = Resources.Load<ItemDatabase>(DefaultResourcesPath);
            }

            return cachedDefault;
        }
    }

    public static void SetDefault(ItemDatabase database)
    {
        cachedDefault = database;
    }

    private void OnEnable()
    {
        RebuildIndex();

        if (cachedDefault == null)
        {
            cachedDefault = this;
        }
    }

    private void OnValidate()
    {
        RebuildIndex();
    }

    public bool TryGetById(int id, out ItemData data)
    {
        EnsureIndex();
        return itemMap.TryGetValue(id, out data);
    }

    public ItemData GetById(int id)
    {
        if (TryGetById(id, out var data))
        {
            return data;
        }

        Debug.LogWarning($"{nameof(ItemDatabase)}: 未找到 itemId={id} 的物品数据。");
        return ItemData.CreateFallback(id, string.Empty);
    }

    public void ReplaceItems(IEnumerable<ItemData> newItems)
    {
        items.Clear();

        if (newItems != null)
        {
            items.AddRange(newItems);
        }

        RebuildIndex();
    }

    private void EnsureIndex()
    {
        if (indexedItemCount != items.Count)
        {
            RebuildIndex();
        }
    }

    private void RebuildIndex()
    {
        itemMap.Clear();
        indexedItemCount = items.Count;

        foreach (var item in items)
        {
            itemMap[item.itemId] = item;
        }
    }
}
