using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    /// <summary>
    /// Buff 配置集合.
    /// 该资产只保存 Buff 列表, 并提供 id 查询.
    /// </summary>
    [CreateAssetMenu(fileName = "BuffDataBase", menuName = "PG/Buff/Buff DataBase", order = 1)]
    public class BuffDataBase : ScriptableObjectDatabase<BuffDataBase, int, Buff>
    {
        [SerializeField] private List<Buff> buffs = new List<Buff>();

        protected override List<Buff> DataValues => buffs;

        protected override bool TryGetKey(Buff buff, out int key)
        {
            if (buff == null)
            {
                key = default;
                return false;
            }

            key = buff.Id;
            return true;
        }
    }
}
