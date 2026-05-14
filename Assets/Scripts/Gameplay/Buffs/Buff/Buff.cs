using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    /// <summary>
    /// Buff 基础配置资产, 具体效果由绑定的 Lua 文件实现.
    /// </summary>
    [CreateAssetMenu(fileName = "Buff", menuName = "PG/Buff/Buff", order = 10)]
    public class Buff : ScriptableObject
    {
        [Header("Basic")]
        [Tooltip("Buff 的唯一 id, 用于数据库查询和道具配置.")]
        [SerializeField] private int id = 0;

        [Tooltip("Buff 的显示名称. 如果为空, 会使用资产名称.")]
        [SerializeField] private string buffName = string.Empty;

        [Tooltip("Buff 绑定的 Lua 文件, 文件需要返回包含生命周期方法的 table.")]
        [SerializeField] private TextAsset luaFile = null;

        [Header("Lifetime")]
        [Tooltip("Buff 的持续时间, 单位为秒. 当不是永久 Buff 时生效.")]
        [Min(0f)]
        [SerializeField] private float duration = 5f;

        [Tooltip("是否为永久 Buff. 开启后不会因为持续时间结束而自动移除.")]
        [SerializeField] private bool isPermanent = false;

        [Tooltip("固定间隔触发的时间间隔, 单位为秒. 大于 0 时启用 OnInterval.")]
        [Min(0f)]
        [SerializeField] private float interval = 1f;

        public int Id => id;
        public string BuffName => string.IsNullOrWhiteSpace(buffName) ? name : buffName;
        public TextAsset LuaFile => luaFile;
        public float Duration => Mathf.Max(0f, duration);
        public bool IsPermanent => isPermanent;
        public float Interval => Mathf.Max(0f, interval);

        /// <summary>
        /// 调用 Lua 的添加回调.
        /// </summary>
        /// <param name="info">Buff 运行时信息.</param>
        public void OnAdd(BuffRuntimeInfo info)
        {
            info?.LuaInstance?.OnAdd(info);
        }

        /// <summary>
        /// 调用 Lua 的移除回调.
        /// </summary>
        /// <param name="info">Buff 运行时信息.</param>
        public void OnRemove(BuffRuntimeInfo info)
        {
            info?.LuaInstance?.OnRemove(info);
        }

        /// <summary>
        /// 调用 Lua 的每帧更新回调.
        /// </summary>
        /// <param name="info">Buff 运行时信息.</param>
        /// <param name="deltaTime">时间增量.</param>
        public void OnUpdate(BuffRuntimeInfo info, float deltaTime)
        {
            info?.LuaInstance?.OnUpdate(info, deltaTime);
        }

        /// <summary>
        /// 调用 Lua 的固定间隔回调.
        /// </summary>
        /// <param name="info">Buff 运行时信息.</param>
        public void OnInterval(BuffRuntimeInfo info)
        {
            info?.LuaInstance?.OnInterval(info);
        }

        /// <summary>
        /// 调用 Lua 的主动触发回调.
        /// </summary>
        /// <param name="info">Buff 运行时信息.</param>
        public void OnTrigger(BuffRuntimeInfo info)
        {
            info?.LuaInstance?.OnTrigger(info);
        }

        private void OnValidate()
        {
            duration = Mathf.Max(0f, duration);
            interval = Mathf.Max(0f, interval);
        }
    }
}
