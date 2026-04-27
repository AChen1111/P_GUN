using UnityEngine;

/// <summary>
/// 战斗房间结束效果基类。
/// 在 FightRoom 的战斗全部结束后按列表顺序执行。
/// </summary>
public abstract class FightRoomEndEffectSO : ScriptableObject
{
    public abstract void Execute(FightRoom room);
}
