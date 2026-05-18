using UnityEngine;
using Game.Core;
using Game.Animation;
using Game.Items;
using Game.Gameplay;

namespace Game.ItemEffects
{
    [CreateAssetMenu(fileName = "ApplyBuffItemEffect", menuName = "PG/Item/Effects/Apply Buff", order = 3)]
    public class ApplyBuffItemEffect : ItemEffectBase
    {
        [SerializeField] private int buffId = 0;
        [SerializeField] private BuffDataBase buffDataBase = null;
        [SerializeField] private bool showHeadMessage = true;

        public override bool CanUse(ItemEffectContext ctx)
        {
            var player = Global.player;
            return player != null && ResolveBuff() != null && player.GetComponent<BuffManager>() != null;
        }

        public override void OnPick(ItemEffectContext ctx)
        {
            var player = Global.player;
            if (player == null) return;

            var targetBuff = ResolveBuff();
            if (targetBuff == null) return;

            var manager = player.GetComponent<BuffManager>();
            if (manager == null)
            {
                Debug.LogError("ApplyBuffItemEffect生效失败, Player预制体缺少BuffManager组件.", player);
                return;
            }

            var info = manager.AddBuff(targetBuff, ctx.SourceObject != null ? ctx.SourceObject : this);
            if (info == null || !showHeadMessage) return;

            EventCenter.Trigger(GameEvent.PlayerHeadMessageRequested, new PlayerHeadMessageEvent($"{info.Buff.BuffName} 生效", 1.5f));
        }

        private Buff ResolveBuff()
        {
            var database = buffDataBase != null ? buffDataBase : DataBaseManager.Instance?.Buffs;
            return database != null && database.TryGetById(buffId, out var targetBuff) ? targetBuff : null;
        }
    }
}
