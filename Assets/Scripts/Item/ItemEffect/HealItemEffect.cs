using UnityEngine;

namespace QFramework.PG
{
    /// <summary>
    /// 拾取后恢复 Global.HP，不超过上限；满血时提示且不增加。
    /// </summary>
    [CreateAssetMenu(fileName = "HealItemEffect", menuName = "PG/Item/Effects/Heal Item Effect", order = 1)]
    public class HealItemEffect : ItemEffectBase
    {
        [SerializeField] private int healAmount = 1;

        public override void OnPick(ItemEffectContext ctx)
        {

            var itemName = ctx.SourceItem != null && !string.IsNullOrEmpty(ctx.SourceItem.itemKey)
                ? ctx.SourceItem.itemKey
                : "回血道具";

            if (Global.HP >= Global.MaxHP)
            {
                GameUI.Instance.ShowMessageOnPlayerHead($"{itemName}：生命已满", 1.5f);
                return;
            }

            Global.HP = Mathf.Min(Global.MaxHP, Global.HP + healAmount);
            Global.OnHPChange?.Invoke();
            GameUI.Instance.ShowMessageOnPlayerHead($"{itemName}：恢复 {healAmount} 点生命", 1.5f);
        }
    }
}
