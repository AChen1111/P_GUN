using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Buff 配置集合.
/// 该资产只保存 Buff 列表, 并提供 id 查询.
/// </summary>
[CreateAssetMenu(fileName = "BuffDataBase", menuName = "PG/Buff/Buff DataBase", order = 1)]
public class BuffDataBase : ScriptableObject
{
    [SerializeField] private List<Buff> buffs = new List<Buff>();

    private readonly Dictionary<int, Buff> buffMap = new Dictionary<int, Buff>();
    private int indexedBuffCount = -1;

    public bool TryGetById(int id, out Buff buff)
    {
        EnsureIndex();
        return buffMap.TryGetValue(id, out buff);
    }

    public Buff GetById(int id)
    {
        if (TryGetById(id, out var buff))
        {
            return buff;
        }

        Debug.LogWarning($"{nameof(BuffDataBase)}: 未找到 id={id} 的 Buff");
        return null;
    }

    private void EnsureIndex()
    {
        if (indexedBuffCount != buffs.Count)
        {
            RebuildIndex();
        }
    }

    private void RebuildIndex()
    {
        buffMap.Clear();
        indexedBuffCount = buffs.Count;

        foreach (var buff in buffs)
        {
            if (buff == null) continue;

            buffMap[buff.Id] = buff;
        }
    }
}
