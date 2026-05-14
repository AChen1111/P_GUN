using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject 数据库基类, 只负责维护 id 索引和导入后刷新.
/// </summary>
public abstract class ScriptableObjectDatabase<TDatabase, TKey, TValue> : ScriptableObject
    where TDatabase : ScriptableObjectDatabase<TDatabase, TKey, TValue>
{
    private Dictionary<TKey, TValue> indexMap;

    /// <summary>
    /// 数据列表, 用于构建查询索引.
    /// </summary>
    protected abstract List<TValue> DataValues { get; }

    /// <summary>
    /// 字典比较器, 子类可覆盖以实现字符串大小写不敏感等规则.
    /// </summary>
    protected virtual IEqualityComparer<TKey> KeyComparer => EqualityComparer<TKey>.Default;

    public bool TryGetById(TKey id, out TValue data)
    {
        return IndexMap.TryGetValue(id, out data);
    }

    /// <summary>
    /// 替换列表内容并重建索引, 保持导入后查询结果立即可用.
    /// </summary>
    protected void ReplaceData(List<TValue> targetList, IEnumerable<TValue> newValues)
    {
        targetList.Clear();

        if (newValues != null)
        {
            targetList.AddRange(newValues);
        }

        RebuildIndex();
    }

    /// <summary>
    /// 提取数据 id, 返回 false 表示该数据不进入索引.
    /// </summary>
    protected abstract bool TryGetKey(TValue data, out TKey key);

    private Dictionary<TKey, TValue> IndexMap
    {
        get
        {
            if (indexMap == null)
            {
                RebuildIndex();
            }

            return indexMap;
        }
    }

    private void RebuildIndex()
    {
        if (indexMap == null)
        {
            indexMap = new Dictionary<TKey, TValue>(KeyComparer);
        }

        indexMap.Clear();

        foreach (var data in DataValues)
        {
            if (!TryGetKey(data, out var key))
            {
                continue;
            }

            indexMap[key] = data;
        }
    }
}
