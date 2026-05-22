using System;
using System.Collections.Generic;
using Game.Gameplay;
using Game.Gameplay.Save;
using Game.Items;
using XLua;

/// <summary>
/// P_GUN 的 xLua Hotfix 注入配置.
/// </summary>
internal static class PgunHotfixConfig
{
    /// <summary>
    /// 主流程高风险类型, 首包构建前需要执行 XLua/Generate Code 和 XLua/Hotfix Inject In Editor.
    /// </summary>
    [Hotfix(HotfixFlag.IgnoreProperty | HotfixFlag.IgnoreCompilerGenerated)]
    private static readonly List<Type> HotfixTypes = new List<Type>
    {
        // 玩家主流程, 覆盖移动, 战斗输入, 受击, 武器装载和读档恢复.
        typeof(Player),

        // Buff 主流程, 覆盖添加, 移除, 层数, 属性计算和存档恢复.
        typeof(BuffManager),

        // 存档和房间生成主流程, 覆盖读写档兼容和 Edgar 生成恢复时序.
        typeof(SaveGameService),
        typeof(AddressableDungeonBootstrapper),
        typeof(Room),
        typeof(FightRoom),
        typeof(NormalRoom),
        typeof(InitRoom),
        typeof(FinalRoom),
        typeof(SaveRoom),

        // 敌人主流程, 覆盖受击, 死亡, 掉落和对象池复用状态.
        typeof(EnemyBase),
        typeof(EnemyA),
        typeof(EnemyBat),
        typeof(EnemyMelee),

        // 武器主流程, 覆盖射击, 换弹, 弹药恢复和具体枪械行为.
        typeof(Gun),
        typeof(AK),
        typeof(AWP),
        typeof(Bow),
        typeof(Laser),
        typeof(MP5),
        typeof(Pistol),
        typeof(RocketGun),
        typeof(ShotGun),

        // 道具和背包主流程, 覆盖拾取, 掉落, 堆叠, 使用和恢复.
        typeof(Item),
        typeof(ItemSpawner),
        typeof(PlayerInventory)
    };
}
