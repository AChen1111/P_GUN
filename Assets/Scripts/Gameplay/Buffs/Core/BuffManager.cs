using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;
using Game.Gameplay.Save;

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

        public IReadOnlyList<BuffRuntimeInfo> ActiveBuffs => buffs;

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

            var previousMaxHp = GetOwnerMaxHp();
            if (buffInfoMap.TryGetValue(buff.Id, out var existing))
            {
                if (buff.IsPermanent)
                {
                    // 永久 Buff 重复获得时只增加层数, 保留 Lua 运行时状态.
                    existing.Source = source;
                    existing.StackCount += 1;
                    existing.IsPermanent = true;
                }
                else
                {
                    ResetBuffRuntimeInfo(existing, buff, source);
                }

                TriggerOnAdd(existing);
                NotifyOwnerStatsChanged(previousMaxHp);
                NotifyBuffsChanged();
                return existing;
            }

            var info = CreateBuffRuntimeInfo(buff, source);
            if (info == null) return null;

            info.Index = buffs.Count;
            buffs.Add(info);
            buffInfoMap[buff.Id] = info;
            TriggerOnAdd(info);
            NotifyOwnerStatsChanged(previousMaxHp);
            NotifyBuffsChanged();
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

            var previousMaxHp = GetOwnerMaxHp();
            TriggerOnRemove(info);
            info.LuaInstance?.Dispose();
            RemoveAt(info.Index);
            NotifyOwnerStatsChanged(previousMaxHp);
            NotifyBuffsChanged();
            return true;
        }

        /// <summary>
        /// 移除指定标签的所有 Buff, 用于净化等一次性批量效果.
        /// </summary>
        /// <param name="tag">目标 Buff 标签.</param>
        /// <returns>被移除的 Buff 数量.</returns>
        public int RemoveBuffsByTag(BuffTag tag)
        {
            var previousMaxHp = GetOwnerMaxHp();
            var removedCount = 0;

            for (var i = buffs.Count - 1; i >= 0; i--)
            {
                var info = buffs[i];
                if (info.Buff.Tag != tag) continue;

                TriggerOnRemove(info);
                info.LuaInstance?.Dispose();
                RemoveAt(info.Index);
                removedCount++;
            }

            if (removedCount <= 0) return 0;

            NotifyOwnerStatsChanged(previousMaxHp);
            NotifyBuffsChanged();
            return removedCount;
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
            var previousMaxHp = GetOwnerMaxHp();
            for (var i = buffs.Count - 1; i >= 0; i--)
            {
                TriggerOnRemove(buffs[i]);
                buffs[i].LuaInstance?.Dispose();
                RemoveAt(i);
            }

            buffInfoMap.Clear();
            NotifyOwnerStatsChanged(previousMaxHp);
            NotifyBuffsChanged();
        }

        public void RestoreSaveData(IEnumerable<BuffSaveData> savedBuffs, UnityEngine.Object source)
        {
            ClearBuffs();
            if (savedBuffs == null) return;

            foreach (var savedBuff in savedBuffs)
            {
                if (savedBuff == null) continue;

                var info = AddBuffById(savedBuff.buffId, source);
                if (info == null) continue;

                // 添加后覆盖计时和层数, 保留 Lua 实例初始化流程.
                info.RemainingTime = Mathf.Max(0f, savedBuff.remainingTime);
                info.StackCount = Mathf.Max(1, savedBuff.stackCount);
                info.IsPermanent = savedBuff.isPermanent;
            }

            NotifyBuffsChanged();
        }

        /// <summary>
        /// 按统一公式计算指定属性的最终值.
        /// </summary>
        /// <param name="statType">属性类型.</param>
        /// <param name="baseValue">基础值.</param>
        /// <returns>计算后的最终值.</returns>
        public float CalculateStat(StatType statType, float baseValue)
        {
            var flat = 0f;
            var percentAdd = 0f;
            var finalMul = 1f;

            for (var i = 0; i < buffs.Count; i++)
            {
                var stackCount = Mathf.Max(1, buffs[i].StackCount);
                var modifiers = buffs[i].Buff.Modifiers;
                for (var j = 0; j < modifiers.Count; j++)
                {
                    var modifier = modifiers[j];
                    if (modifier == null || modifier.StatType != statType) continue;

                    // 同一属性按固定值, 百分比, 最终倍率三个分区累计.
                    switch (modifier.ModifierType)
                    {
                        case ModifierType.Flat:
                            flat += modifier.Value * stackCount;
                            break;
                        case ModifierType.PercentAdd:
                            percentAdd += modifier.Value * stackCount;
                            break;
                        case ModifierType.FinalMul:
                            for (var stackIndex = 0; stackIndex < stackCount; stackIndex++)
                            {
                                finalMul *= modifier.Value;
                            }
                            break;
                    }
                }
            }

            return (baseValue + flat) * (1f + percentAdd) * finalMul;
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
            var luaManager = LuaManager.Instance;
            if (luaManager == null)
            {
                Debug.LogError($"{nameof(BuffManager)}: Root 场景未挂载 {nameof(LuaManager)}, 无法创建 Buff Lua 实例.", this);
                return null;
            }

            var luaInstance = luaManager.CreateBuffInstance(buff);
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
            info.StackCount = 1;
        }

    #endregion

    #region Stat Change

        /// <summary>
        /// 获取属性变化前的玩家最大生命, 用于变化后刷新 UI.
        /// </summary>
        /// <returns>玩家当前最大生命.</returns>
        private int GetOwnerMaxHp()
        {
            var target = owner != null ? owner : Global.player;
            return target != null ? target.MaxHP : 0;
        }

        /// <summary>
        /// 通知玩家 Buff 属性已经变化.
        /// </summary>
        /// <param name="previousMaxHp">变化前的最大生命.</param>
        private void NotifyOwnerStatsChanged(int previousMaxHp)
        {
            var target = owner != null ? owner : Global.player;
            target?.OnBuffStatsChanged(previousMaxHp);
        }

        /// <summary>
        /// 通知 UI 当前 Buff 列表或层数已经变化.
        /// </summary>
        private static void NotifyBuffsChanged()
        {
            EventCenter.Trigger(GameEvent.PlayerBuffsChanged);
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
