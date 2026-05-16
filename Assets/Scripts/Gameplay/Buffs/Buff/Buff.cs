using System;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Gameplay
{
    /// <summary>
    /// Buff 基础配置数据, 由 BuffDataBase 统一保存并按 id 查询.
    /// </summary>
    [Serializable]
    public class Buff
    {
        [Header("Basic")]
        [Tooltip("Buff 的唯一 id, 用于数据库查询和道具配置.")]
        [SerializeField] private int id = 0;

        [Tooltip("Buff 的显示名称. 如果为空, 会使用 id.")]
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

        [Header("Modifiers")]
        [Tooltip("Buff 提供的属性修正列表, 按统一公式集中计算.")]
        [SerializeField] private List<StatModifier> modifiers = new List<StatModifier>();

        public int Id => id;
        public string BuffName => string.IsNullOrWhiteSpace(buffName) ? id.ToString() : buffName;
        public TextAsset LuaFile => luaFile;
        public float Duration => Mathf.Max(0f, duration);
        public bool IsPermanent => isPermanent;
        public float Interval => Mathf.Max(0f, interval);
        public IReadOnlyList<StatModifier> Modifiers => modifiers;

        /// <summary>
        /// 替换 Buff 的属性修正配置, 供编辑器导入器写入.
        /// </summary>
        /// <param name="newModifiers">新的属性修正列表.</param>
        public void ReplaceModifiers(IEnumerable<StatModifier> newModifiers)
        {
            modifiers.Clear();
            if (newModifiers == null) return;

            foreach (var modifier in newModifiers)
            {
                if (modifier == null) continue;
                modifiers.Add(modifier);
            }
        }

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

        /// <summary>
        /// 编辑器导入后修正非法参数.
        /// </summary>
        public void Validate()
        {
            duration = Mathf.Max(0f, duration);
            interval = Mathf.Max(0f, interval);
            modifiers.RemoveAll(modifier => modifier == null);
        }
    }
}
