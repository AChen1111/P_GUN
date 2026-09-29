using System;
using System.Collections.Generic;
using Game.Gameplay;
using XLua;

/// <summary>
/// P_GUN 的 xLua Hotfix 注入配置.
/// </summary>
internal static class PgunHotfixConfig
{
    /// <summary>
    /// 热修只覆盖逐帧逻辑留在 C# 的玩家和敌人, 补丁不应替换 Update 或 FixedUpdate.
    /// 首包构建前需要执行 XLua/Generate Code 和 XLua/Hotfix Inject In Editor.
    /// </summary>
    [Hotfix(HotfixFlag.IgnoreProperty | HotfixFlag.IgnoreCompilerGenerated)]
    private static readonly List<Type> HotfixTypes = new List<Type>
    {
        // 玩家主流程, 覆盖受击, 治疗和瞄准修正这类短方法.
        typeof(Player),

        // 敌人受击, 死亡, 掉落和对象池复用状态.
        typeof(EnemyBase),

        // 敌人行为换成 EnemyBrain 后再加入本名单.
    };
}