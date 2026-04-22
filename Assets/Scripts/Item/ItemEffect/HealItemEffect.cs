using UnityEngine;

namespace QFramework.PG
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

            var itemName = ctx.SourceItem != null && !string.IsNullOrEmpty(ctx.SourceItem.itemKey)
                ? ctx.SourceItem.itemKey
                : "回血道具";

            if (player.IsHPFull)
            {
                GameUI.Instance.ShowMessageOnPlayerHead($"{itemName}：生命已满", 1.5f);
                return;
            }

            var healedAmount = player.Heal(healAmount);
            GameUI.Instance.ShowMessageOnPlayerHead($"{itemName}：恢复 {healedAmount} 点生命", 1.5f);
        }
    }
}
