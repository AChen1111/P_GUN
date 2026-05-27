using System;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Buff 可影响的属性类型.
    /// </summary>
    public enum StatType
    {
        MoveSpeed,
        Attack,
        Defense,
        MaxHp
    }

    /// <summary>
    /// 属性修正的计算分区.
    /// Final = (Base + FlatSum) * (1 + PercentAddSum) * FinalMulProduct
    /// </summary>
    public enum ModifierType
    {
        Flat,
        PercentAdd,
        FinalMul
    }

    /// <summary>
    /// 单条属性修正数据, 由 Buff 配置持有.
    /// </summary>
    [Serializable]
    public class StatModifier
    {
        [Tooltip("本条修正影响的属性.")]
        [SerializeField] private StatType statType = StatType.MoveSpeed;

        [Tooltip("本条修正参与的计算分区.")]
        [SerializeField] private ModifierType modifierType = ModifierType.Flat;

        [Tooltip("本条修正的数值.")]
        [SerializeField] private float value = 0f;

        public StatModifier()
        {
        }

        public StatModifier(StatType statType, ModifierType modifierType, float value)
        {
            this.statType = statType;
            this.modifierType = modifierType;
            this.value = value;
        }

        public StatType StatType => statType;
        public ModifierType ModifierType => modifierType;
        public float Value => value;
    }
}
