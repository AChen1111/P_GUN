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
    /// 该资产直接保存所有 Buff 参数, 并提供 id 查询.
    /// </summary>
    [CreateAssetMenu(fileName = "BuffDataBase", menuName = "PG/Buff/Buff DataBase", order = 1)]
    public class BuffDataBase : ScriptableObjectDatabase<BuffDataBase, int, Buff>
    {
        [SerializeField] private List<Buff> buffs = new List<Buff>();

        public IReadOnlyList<Buff> Buffs => buffs;

        /// <summary>
        /// 执行 ReplaceBuffs 逻辑.
        /// </summary>
        public void ReplaceBuffs(IEnumerable<Buff> newBuffs)
        {
            if (newBuffs != null)
            {
                foreach (var buff in newBuffs)
                {
                    buff?.Validate();
                }
            }

            ReplaceData(buffs, newBuffs);
        }

        protected override List<Buff> DataValues => buffs;

        /// <summary>
        /// 执行 TryGetKey 逻辑.
        /// </summary>
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
