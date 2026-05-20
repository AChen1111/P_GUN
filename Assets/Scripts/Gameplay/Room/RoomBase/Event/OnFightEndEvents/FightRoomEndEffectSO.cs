using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    /// <summary>
    /// 战斗房间结束效果基类。
    /// 在 FightRoom 的战斗全部结束后按列表顺序执行。
    /// </summary>
    public abstract class FightRoomEndEffectSO : ScriptableObject
    {
        /// <summary>
        /// 执行 Execute 逻辑.
        /// </summary>
        public abstract void Execute(FightRoom room);
    }
}
