using UnityEngine;
using Game.Core;
using Game.Gameplay;
using Game.Items;

namespace Game.ItemEffects
{
    /// <summary>
    /// 拾取后移除玩家身上所有负面 Buff.
    /// </summary>
    [CreateAssetMenu(fileName = "CleanseNegativeBuffItemEffect", menuName = "PG/Item/Effects/Cleanse Negative Buff", order = 4)]
    public class CleanseNegativeBuffItemEffect : ItemEffectBase
    {
        [SerializeField] private bool showHeadMessage = true;

        public override void OnPick(ItemEffectContext ctx)
        {
            var player = Global.player;
            if (player == null) return;

            var manager = player.GetComponent<BuffManager>();
            if (manager == null)
            {
                Debug.LogError("CleanseNegativeBuffItemEffect生效失败, Player预制体缺少BuffManager组件.", player);
                return;
            }

            var removedCount = manager.RemoveBuffsByTag(BuffTag.Negative);
            if (!showHeadMessage) return;

            var message = removedCount > 0 ? $"净化了 {removedCount} 个负面状态" : "没有可净化的负面状态";
            EventCenter.Trigger(GameEvent.PlayerHeadMessageRequested, new PlayerHeadMessageEvent(message, 1.5f));
        }
    }
}
