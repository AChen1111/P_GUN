using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    /// <summary>
    /// 玩家身上的 Buff 运行时管理器.
    /// 负责保存当前 Buff, 更新计时器, 并调度 Lua 生命周期.
    /// </summary>
    public class BuffManager : MonoBehaviour
    {
        [SerializeField] private BuffDataBase dataBase = null;

        /// <summary>
        /// 当前生效的 Buff 列表, 用于每帧顺序更新.
        /// </summary>
        private readonly List<BuffRuntimeInfo> buffs = new List<BuffRuntimeInfo>();

        /// <summary>
        /// Buff id 到运行时信息的映射, 用于快速查找和去重.
        /// </summary>
        private readonly Dictionary<int, BuffRuntimeInfo> buffInfoMap = new Dictionary<int, BuffRuntimeInfo>();

        private Player owner;

        private void Awake()
        {
            owner = GetComponent<Player>();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;

            for (var i = buffs.Count - 1; i >= 0; i--)
            {
                UpdateBuff(buffs[i], deltaTime);
            }
        }

        private void OnDestroy()
        {
            ClearBuffs();
        }

    #region Public API

        /// <summary>
        /// 通过 id 添加 Buff.
        /// </summary>
        /// <param name="buffId">Buff id</param>
        /// <returns>Buff 运行时信息</returns>
        public BuffRuntimeInfo AddBuffById(int buffId)
        {
            return AddBuffById(buffId, null);
        }

        /// <summary>
        /// 通过 id 添加 Buff.
        /// </summary>
        /// <param name="buffId">Buff id.</param>
        /// <param name="source">Buff 来源对象.</param>
        /// <returns>Buff 运行时信息.</returns>
        public BuffRuntimeInfo AddBuffById(int buffId, UnityEngine.Object source)
        {
            var database = dataBase != null ? dataBase : DataBaseManager.Instance?.Buffs;
            if (database == null)
            {
                Debug.LogWarning($"{nameof(BuffManager)}: 未设置 {nameof(BuffDataBase)}, 无法通过 id 添加 Buff.", this);
                return null;
            }

            return database.TryGetById(buffId, out var buff) ? AddBuff(buff, source) : null;
        }

        /// <summary>
        /// 直接添加 Buff. 如果 Buff 已存在, 只重置持续时间并触发 OnAdd.
        /// </summary>
        /// <param name="buff">Buff 配置</param>
        /// <returns>Buff 运行时信息</returns>
        public BuffRuntimeInfo AddBuff(Buff buff)
        {
            return AddBuff(buff, null);
        }

        /// <summary>
        /// 直接添加 Buff. 如果 Buff 已存在, 只重置持续时间并触发 OnAdd.
        /// </summary>
        /// <param name="buff">Buff 配置.</param>
        /// <param name="source">Buff 来源对象.</param>
        /// <returns>Buff 运行时信息.</returns>
        public BuffRuntimeInfo AddBuff(Buff buff, UnityEngine.Object source)
        {
            if (buff == null) return null;

            if (buffInfoMap.TryGetValue(buff.Id, out var existing))
            {
                ResetBuffRuntimeInfo(existing, buff, source);
                TriggerOnAdd(existing);
                return existing;
            }

            var info = CreateBuffRuntimeInfo(buff, source);
            if (info == null) return null;

            info.Index = buffs.Count;
            buffs.Add(info);
            buffInfoMap[buff.Id] = info;
            TriggerOnAdd(info);
            return info;
        }

        /// <summary>
        /// 移除 Buff.
        /// </summary>
        /// <param name="buff">Buff</param>
        /// <returns>是否成功移除</returns>
        public bool RemoveBuff(Buff buff)
        {
            if (buff == null) return false;

            return RemoveBuffById(buff.Id);
        }

        /// <summary>
        /// 通过 id 移除 Buff.
        /// </summary>
        /// <param name="buffId">Buff id.</param>
        /// <returns>是否成功移除.</returns>
        public bool RemoveBuffById(int buffId)
        {
            if (!buffInfoMap.TryGetValue(buffId, out var info)) return false;

            TriggerOnRemove(info);
            info.LuaInstance?.Dispose();
            RemoveAt(info.Index);
            return true;
        }

        /// <summary>
        /// 主动触发指定 Buff.
        /// </summary>
        /// <param name="buffId">Buff id.</param>
        public void TriggerBuffById(int buffId)
        {
            if (buffInfoMap.TryGetValue(buffId, out var info))
            {
                info.Buff.OnTrigger(info);
            }
        }

        /// <summary>
        /// 清空所有 Buff.
        /// </summary>
        public void ClearBuffs()
        {
            for (var i = buffs.Count - 1; i >= 0; i--)
            {
                TriggerOnRemove(buffs[i]);
                buffs[i].LuaInstance?.Dispose();
                RemoveAt(i);
            }

            buffInfoMap.Clear();
        }

    #endregion

    #region Runtime

        /// <summary>
        /// 更新 Buff 的计时器和状态.
        /// </summary>
        /// <param name="info">Buff 运行时信息</param>
        /// <param name="deltaTime">时间增量</param>
        private void UpdateBuff(BuffRuntimeInfo info, float deltaTime)
        {
            if (!info.IsPermanent)
            {
                info.RemainingTime -= deltaTime;

                if (info.RemainingTime <= 0f)
                {
                    RemoveBuffById(info.Buff.Id);
                    return;
                }
            }

            info.Buff.OnUpdate(info, deltaTime);
            TriggerInterval(info, deltaTime);
        }

        /// <summary>
        /// 按固定间隔触发 Buff 效果.
        /// </summary>
        /// <param name="info">Buff 运行时信息</param>
        /// <param name="deltaTime">时间增量</param>
        private void TriggerInterval(BuffRuntimeInfo info, float deltaTime)
        {
            if (info.Interval <= 0f) return;

            info.IntervalTimer += deltaTime;

            while (info.IntervalTimer >= info.Interval && buffInfoMap.ContainsKey(info.Buff.Id))
            {
                info.IntervalTimer -= info.Interval;
                info.Buff.OnInterval(info);
            }
        }

    #endregion

    #region Create And Reset

        /// <summary>
        /// 创建 Buff 运行时信息.
        /// </summary>
        /// <param name="buff">Buff 配置</param>
        /// <returns>Buff 运行时信息</returns>
        private BuffRuntimeInfo CreateBuffRuntimeInfo(Buff buff, UnityEngine.Object source)
        {
            var luaInstance = LuaManager.GetOrCreate().CreateBuffInstance(buff);
            if (luaInstance == null)
            {
                Debug.LogError($"{nameof(BuffManager)}: 创建 Buff Lua 实例失败, Buff: {buff.BuffName}.", this);
                return null;
            }

            var info = new BuffRuntimeInfo
            {
                owner = owner != null ? owner : Global.player,
                Source = source,
                Buff = buff,
                LuaInstance = luaInstance
            };

            ResetBuffRuntimeInfo(info, buff, source);
            return info;
        }

        /// <summary>
        /// 重置 Buff 运行时计时数据.
        /// </summary>
        /// <param name="info">Buff 运行时信息</param>
        /// <param name="buff">Buff 配置</param>
        private void ResetBuffRuntimeInfo(BuffRuntimeInfo info, Buff buff, UnityEngine.Object source)
        {
            info.Source = source;
            info.Duration = buff.Duration;
            info.RemainingTime = buff.Duration;
            info.Interval = buff.Interval;
            info.IntervalTimer = 0f;
            info.IsPermanent = buff.IsPermanent;
        }

    #endregion

    #region Trigger

        /// <summary>
        /// 触发 Buff 的添加回调.
        /// </summary>
        /// <param name="info">Buff 运行时信息</param>
        private static void TriggerOnAdd(BuffRuntimeInfo info)
        {
            info.Buff.OnAdd(info);
        }

        /// <summary>
        /// 触发 Buff 的移除回调.
        /// </summary>
        /// <param name="info">Buff 运行时信息</param>
        private static void TriggerOnRemove(BuffRuntimeInfo info)
        {
            info.Buff.OnRemove(info);
        }

    #endregion

    #region Remove Helpers

        /// <summary>
        /// 使用尾部交换的方式移除指定索引的 Buff.
        /// </summary>
        /// <param name="index">索引</param>
        private void RemoveAt(int index)
        {
            var lastIndex = buffs.Count - 1;
            var removedInfo = buffs[index];
            var lastInfo = buffs[lastIndex];

            if (index != lastIndex)
            {
                buffs[index] = lastInfo;
                buffs[lastIndex] = removedInfo;

                removedInfo.Index = lastIndex;
                lastInfo.Index = index;
            }

            buffs.RemoveAt(lastIndex);
            buffInfoMap.Remove(removedInfo.Buff.Id);
        }

    #endregion
    }
}
