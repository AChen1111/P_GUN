using UnityEngine;
using Game.Core;
using Game.Items;
using Game.Gameplay;

namespace Game.ItemEffects
{
    /// <summary>
    /// 使用后按配置的 buffId 给玩家添加 Buff, 配置来自 BuffData.lua.
    /// </summary>
    [CreateAssetMenu(fileName = "ApplyBuffItemEffect", menuName = "PG/Item/Effects/Apply Buff", order = 3)]
    public class ApplyBuffItemEffect : ItemEffectBase
    {
        [SerializeField] private int buffId = 0;
        [SerializeField] private bool showHeadMessage = true;

        public override bool CanUse(ItemEffectContext ctx)
        {
            var player = PlayerRegistry.Current;
            return player != null && buffId >= 0 && player.GetComponent<BuffManager>() != null;
        }

        public override void OnPick(ItemEffectContext ctx)
        {
            var player = PlayerRegistry.Current;
            if (player == null) return;

            var manager = player.GetComponent<BuffManager>();
            if (manager == null)
            {
                Debug.LogError("ApplyBuffItemEffect生效失败, Player预制体缺少BuffManager组件.", player);
                return;
            }

            var info = manager.AddBuffById(buffId, ctx.SourceObject != null ? ctx.SourceObject : this);
            if (info == null || !showHeadMessage) return;

            EventCenter.Trigger(CoreEvents.PlayerHeadMessageRequested, new PlayerHeadMessageEvent($"{info.Config.Name} 生效", 1.5f));
        }
    }
}