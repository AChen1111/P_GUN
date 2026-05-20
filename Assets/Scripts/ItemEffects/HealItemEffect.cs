using UnityEngine;
using Game.Core;
using Game.Animation;
using Game.Items;
using Game.Gameplay;

namespace Game.ItemEffects
{
    /// <summary>
    /// 使用后恢复玩家 HP, 不超过上限; 满血时不允许消耗.
    /// </summary>
    [CreateAssetMenu(fileName = "HealItemEffect", menuName = "PG/Item/Effects/Heal Item Effect", order = 1)]
    public class HealItemEffect : ItemEffectBase
    {
        [SerializeField] private int healAmount = 1;

        /// <summary>
        /// 执行 CanUse 逻辑.
        /// </summary>
        public override bool CanUse(ItemEffectContext ctx)
        {
            var player = Global.player;
            return player != null && healAmount > 0 && !player.IsHPFull;
        }

        /// <summary>
        /// 执行 OnPick 逻辑.
        /// </summary>
        public override void OnPick(ItemEffectContext ctx)
        {
            var player = Global.player;
            if (player == null) return;

            if (player.IsHPFull)
            {
                EventCenter.Trigger(GameEvent.PlayerHeadMessageRequested, new PlayerHeadMessageEvent("生命已满", 1.5f));
                return;
            }

            var healedAmount = player.Heal(healAmount);
            EventCenter.Trigger(GameEvent.PlayerHeadMessageRequested, new PlayerHeadMessageEvent($"恢复 {healedAmount} 点生命", 1.5f));
        }
    }
}
