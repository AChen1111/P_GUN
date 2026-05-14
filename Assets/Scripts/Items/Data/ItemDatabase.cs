using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "PG/Item/Item Database", order = 0)]
public class ItemDatabase : ScriptableObjectDatabase<ItemDatabase, int, ItemData>
{
    [SerializeField] private List<ItemData> items = new List<ItemData>();

    public IReadOnlyList<ItemData> Items => items;

    public void ReplaceItems(IEnumerable<ItemData> newItems)
    {
        ReplaceData(items, newItems);
    }

    protected override List<ItemData> DataValues => items;

    protected override bool TryGetKey(ItemData data, out int key)
    {
        key = data.itemId;
        return true;
    }
}
