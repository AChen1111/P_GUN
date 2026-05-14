using UnityEngine;
using Game.Core;
using Game.Animation;
using Game.Items;
using Game.Gameplay;

namespace Game.ItemEffects
{
    /// <summary>
    /// 拾取后恢复玩家 HP，不超过上限；满血时提示且不增加。
    /// </summary>
    [CreateAssetMenu(fileName = "HealItemEffect", menuName = "PG/Item/Effects/Heal Item Effect", order = 1)]
    public class HealItemEffect : ItemEffectBase
    {
        [SerializeField] private int healAmount = 1;

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
