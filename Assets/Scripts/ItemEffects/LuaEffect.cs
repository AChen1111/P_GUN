using UnityEngine;
using Game.Gameplay;
using Game.Items;

namespace Game.ItemEffects
{
    /// <summary>
    /// Lua脚本类效果：通过Lua脚本实现的道具效果。
    /// </summary>
    [CreateAssetMenu(fileName = "LuaEffect", menuName = "PG/Item/Effects/Lua Effect", order = 3)]
    public class LuaEffect : ItemEffectBase
    {
        [Tooltip("Lua脚本文本资产")]
        [Header("Lua Script")]
        public TextAsset luaScript;

        public override void OnPick(ItemEffectContext ctx)
        {
            LuaManager.Instance.InvokeItemEffectMethod(luaScript, nameof(OnPick), ctx);
        }
    }
}
