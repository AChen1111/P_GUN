using UnityEngine;

[CreateAssetMenu(fileName = "ApplyBuffItemEffect", menuName = "PG/Item/Effects/Apply Buff", order = 3)]
public class ApplyBuffItemEffect : ItemEffectBase
{
    [SerializeField] private Buff buff = null;
    [SerializeField] private int buffId = 0 ;
    [SerializeField] private BuffDataBase buffDataBase = null;
    [SerializeField] private bool showHeadMessage = true;

    public override void OnPick(ItemEffectContext ctx)
    {
        var player = Global.player;
        if (player == null) return;

        var targetBuff = ResolveBuff();
        if (targetBuff == null) return;

        var manager = player.GetComponent<BuffManager>();
        if (manager == null)
        {
            manager = player.gameObject.AddComponent<BuffManager>();
        }

        var info = manager.AddBuff(targetBuff);
        if (info == null || !showHeadMessage) return;

        EventCenter.Trigger(GameEvent.PlayerHeadMessageRequested, new PlayerHeadMessageEvent($"{info.Buff.BuffName} 生效", 1.5f));
    }

    private Buff ResolveBuff()
    {
        if (buff != null) return buff;
        return buffDataBase.GetById(buffId);
    }
}
