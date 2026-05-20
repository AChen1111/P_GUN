using UnityEngine;
using Game.Core;
using Game.Gameplay;
using Game.Items;

namespace Game.ItemEffects
{
    /// <summary>
    /// 使用后移除玩家身上所有负面 Buff, 没有负面 Buff 时不允许消耗.
    /// </summary>
    [CreateAssetMenu(fileName = "CleanseNegativeBuffItemEffect", menuName = "PG/Item/Effects/Cleanse Negative Buff", order = 4)]
    public class CleanseNegativeBuffItemEffect : ItemEffectBase
    {
        [SerializeField] private bool showHeadMessage = true;

        /// <summary>
        /// 执行 CanUse 逻辑.
        /// </summary>
        public override bool CanUse(ItemEffectContext ctx)
        {
            var manager = ResolveBuffManager();
            if (manager == null)
            {
                return false;
            }

            var activeBuffs = manager.ActiveBuffs;
            for (int i = 0; i < activeBuffs.Count; i++)
            {
                if (activeBuffs[i]?.Buff != null && activeBuffs[i].Buff.Tag == BuffTag.Negative)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 执行 OnPick 逻辑.
        /// </summary>
        public override void OnPick(ItemEffectContext ctx)
        {
            var player = Global.player;
            if (player == null) return;

            var manager = ResolveBuffManager();
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

        /// <summary>
        /// 执行 ResolveBuffManager 逻辑.
        /// </summary>
        private static BuffManager ResolveBuffManager()
        {
            var player = Global.player;
            return player != null ? player.GetComponent<BuffManager>() : null;
        }
    }
}
