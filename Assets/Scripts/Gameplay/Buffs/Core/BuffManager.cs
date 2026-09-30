using Game.Pooling;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Core;
using Game.Gameplay.Save;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// 玩家身上的 Buff 运行时管理器.
    /// 配置来自 BuffData.lua, 层数与持续时间的容器和属性公式留在本类,
    /// 特殊生命周期由行为预制体上的 LuaComponet 承担.
    /// </summary>
    public class BuffManager : MonoBehaviour
    {
        private readonly List<BuffRuntimeInfo> buffs = new List<BuffRuntimeInfo>();
        private readonly Dictionary<int, BuffRuntimeInfo> buffInfoMap = new Dictionary<int, BuffRuntimeInfo>();
        private readonly Dictionary<int, LuaBehaviourHost> behaviorPrefabsById = new Dictionary<int, LuaBehaviourHost>();

        private Player owner;

        public IReadOnlyList<BuffRuntimeInfo> ActiveBuffs => buffs;

        /// <summary>
        /// 初始化运行时依赖.
        /// </summary>
        private void Awake()
        {
            owner = GetComponent<Player>();
        }

        /// <summary>
        /// 注册击杀事件监听.
        /// </summary>
        private void OnEnable()
        {
            EventCenter.AddListener(GameplayEvents.EnemyDefeated, HandleEnemyDefeated);
        }

        /// <summary>
        /// 注销击杀事件监听.
        /// </summary>
        private void OnDisable()
        {
            EventCenter.RemoveListener(GameplayEvents.EnemyDefeated, HandleEnemyDefeated);
        }

        /// <summary>
        /// 把击杀事件转发给每个带行为预制体的 Buff 的 OnKill.
        /// </summary>
        private void HandleEnemyDefeated(EnemyDefeatedEvent payload)
        {
            var enemyGameObject = payload != null && payload.Enemy != null ? payload.Enemy.gameObject : null;
            for (var i = 0; i < buffs.Count; i++)
            {
                buffs[i].Behavior?.CallLuaFunction("OnKill", enemyGameObject);
            }
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;

            for (var i = buffs.Count - 1; i >= 0; i--)
            {
                UpdateBuff(buffs[i], deltaTime);
            }

            void UpdateBuff(BuffRuntimeInfo info, float frameDeltaTime)
            {
                if (!info.IsPermanent)
                {
                    info.RemainingTime -= frameDeltaTime;

                    if (info.RemainingTime <= 0f)
                    {
                        RemoveBuffById(info.Config.Id);
                        return;
                    }
                }

                TriggerInterval(info, frameDeltaTime);
            }

            void TriggerInterval(BuffRuntimeInfo info, float frameDeltaTime)
            {
                if (info.Interval <= 0f) return;

                info.IntervalTimer += frameDeltaTime;

                while (info.IntervalTimer >= info.Interval && buffInfoMap.ContainsKey(info.Config.Id))
                {
                    info.IntervalTimer -= info.Interval;
                    TriggerOnInterval(info);
                }
            }
        }

        /// <summary>
        /// 释放销毁时持有的运行时状态.
        /// </summary>
        private void OnDestroy()
        {
            ClearBuffs();
        }

        #region Public API

        /// <summary>
        /// 通过 id 添加 Buff, 配置从 BuffData.lua 读取, 缺行直接报错.
        /// </summary>
        /// <param name="buffId">Buff id.</param>
        /// <returns>Buff 运行时信息.</returns>
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
            var config = LuaDataRuntime.GetBuffConfig(buffId);
            return AddBuff(config, source);
        }

        /// <summary>
        /// 直接添加 Buff 配置. 如果 Buff 已存在, 永久 Buff 叠层, 非永久 Buff 重置持续时间.
        /// </summary>
        /// <param name="config">Buff 配置.</param>
        /// <param name="source">Buff 来源对象.</param>
        /// <returns>Buff 运行时信息.</returns>
        public BuffRuntimeInfo AddBuff(BuffConfig config, UnityEngine.Object source)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            var previousMaxHp = GetOwnerMaxHp();
            if (buffInfoMap.TryGetValue(config.Id, out var existing))
            {
                if (config.IsPermanent)
                {
                    // 永久 Buff 重复获得时只增加层数.
                    existing.Source = source;
                    existing.StackCount += 1;
                    existing.IsPermanent = true;
                }
                else
                {
                    ResetBuffRuntimeInfo(existing, config, source);
                }

                TriggerOnAdd(existing);
                NotifyOwnerStatsChanged(previousMaxHp);
                NotifyBuffsChanged();
                return existing;
            }

            var info = CreateBuffRuntimeInfo(config, source);
            info.Index = buffs.Count;
            buffs.Add(info);
            buffInfoMap[config.Id] = info;
            // 预加载缓存可能同步完成, 必须先登记 Buff 再挂接行为, OnAdd 只由挂接入口调用一次.
            AttachBehaviorObjectAsync(info);
            NotifyOwnerStatsChanged(previousMaxHp);
            NotifyBuffsChanged();
            return info;
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
            RemoveBehaviorObject(info);
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
                if (info.ParsedTag != tag) continue;

                TriggerOnRemove(info);
                RemoveBehaviorObject(info);
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
                TriggerOnTrigger(info);
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
                RemoveBehaviorObject(buffs[i]);
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

                // 添加后覆盖计时和层数, 保留行为预制体的初始化流程.
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
                var modifiers = buffs[i].ParsedModifiers;
                for (var j = 0; j < modifiers.Count; j++)
                {
                    var modifier = modifiers[j];
                    if (modifier.StatType != statType) continue;

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

        #region Create And Reset

        /// <summary>
        /// 创建运行时信息, 解析标签和属性修正, 并异步挂接行为预制体.
        /// </summary>
        private BuffRuntimeInfo CreateBuffRuntimeInfo(BuffConfig config, UnityEngine.Object source)
        {
            var info = new BuffRuntimeInfo
            {
                owner = owner != null ? owner : PlayerRegistry.Current,
                Source = source,
                Config = config,
            };

            ParseTagAndModifiers(config, info);
            ResetBuffRuntimeInfo(info, config, source);
            return info;
        }

        /// <summary>
        /// 把配置里的字符串枚举转换成本地枚举, 非法值直接报错.
        /// </summary>
        private static void ParseTagAndModifiers(BuffConfig config, BuffRuntimeInfo info)
        {
            if (!Enum.TryParse(config.Tag, out BuffTag tag))
            {
                throw new InvalidOperationException($"Buff {config.Id} 的 tag 非法: {config.Tag}.");
            }

            info.ParsedTag = tag;

            for (var i = 0; i < config.Modifiers.Count; i++)
            {
                var entry = config.Modifiers[i];
                if (!Enum.TryParse(entry.Stat, out StatType statType))
                {
                    throw new InvalidOperationException($"Buff {config.Id} 的 stat 非法: {entry.Stat}.");
                }

                if (!Enum.TryParse(entry.ModifierType, out ModifierType modifierType))
                {
                    throw new InvalidOperationException($"Buff {config.Id} 的 modifierType 非法: {entry.ModifierType}.");
                }

                info.ParsedModifiers.Add(new ParsedStatModifier
                {
                    StatType = statType,
                    ModifierType = modifierType,
                    Value = entry.Value,
                });
            }
        }

        /// <summary>
        /// 重置 Buff 运行时计时数据.
        /// </summary>
        private static void ResetBuffRuntimeInfo(BuffRuntimeInfo info, BuffConfig config, UnityEngine.Object source)
        {
            info.Source = source;
            info.Duration = config.Duration;
            info.RemainingTime = config.Duration;
            info.Interval = config.Interval;
            info.IntervalTimer = 0f;
            info.IsPermanent = config.IsPermanent;
            info.StackCount = 1;
        }

        #endregion

        #region Behavior Object

        /// <summary>
        /// 异步加载行为预制体并挂接到 Buff 上, 加载期间 Buff 已被移除则立即回池.
        /// </summary>
        private async void AttachBehaviorObjectAsync(BuffRuntimeInfo info)
        {
            if (string.IsNullOrEmpty(info.Config.BehaviorPrefabKey)) return;

            try
            {
                var behavior = await GetBehaviorObjectAsync(info.Config);
                if (!buffInfoMap.TryGetValue(info.Config.Id, out var current) || current != info)
                {
                    // 异步加载完成前 Buff 已移除, 将实例立即归还行为池.
                    BuffBehaviorPool.Instance.Release(behavior);
                    return;
                }

                info.Behavior = behavior;
                TriggerOnAdd(info);
            }
            catch (Exception exception)
            {
                Debug.LogError($"{nameof(BuffManager)}: Buff 行为预制体加载失败, Buff: {info.Config.Name}, Error: {exception.Message}", this);
            }
        }

        /// <summary>
        /// 取出或加载行为预制体, 并从对象池取一个实例.
        /// </summary>
        private async Task<LuaBehaviourHost> GetBehaviorObjectAsync(BuffConfig config)
        {
            var pool = BuffBehaviorPool.Instance;
            if (pool == null)
            {
                throw new InvalidOperationException($"{nameof(BuffBehaviorPool)} 必须摆放在游戏场景中.");
            }

            var loader = AddressableLoader.Instance;
            if (loader == null)
            {
                throw new InvalidOperationException($"{nameof(AddressableLoader)} 必须先初始化.");
            }

            if (!behaviorPrefabsById.TryGetValue(config.Id, out var prefab))
            {
                var prefabGameObject = await loader.LoadAssetAsync<GameObject>(config.BehaviorPrefabKey);
                prefab = prefabGameObject != null ? prefabGameObject.GetComponent<LuaBehaviourHost>() : null;
                if (prefab == null)
                {
                    throw new InvalidOperationException($"Buff 行为预制体缺少 LuaComponet, 地址: {config.BehaviorPrefabKey}.");
                }

                behaviorPrefabsById[config.Id] = prefab;
            }

            return pool.Get(prefab);
        }

        /// <summary>
        /// 把行为对象归还对象池.
        /// </summary>
        private void RemoveBehaviorObject(BuffRuntimeInfo info)
        {
            var behavior = info.Behavior;
            info.Behavior = null;
            if (behavior == null) return;

            var pool = BuffBehaviorPool.Instance;
            if (pool == null)
            {
                Debug.LogError($"{nameof(BuffManager)}: {nameof(BuffBehaviorPool)} 不存在, 无法回收 Buff 行为对象.", this);
                return;
            }

            pool.Release(behavior);
        }

        /// <summary>
        /// 把归属玩家, Buff id 和层数写进行为对象的实例表.
        /// </summary>
        private static void InjectBehaviorFields(BuffRuntimeInfo info)
        {
            var behavior = info.Behavior;
            if (behavior == null) return;

            behavior.SetLuaField("owner", info.Owner);
            behavior.SetLuaField("buffId", info.Config.Id);
            behavior.SetLuaField("buffName", info.Config.Name);
            behavior.SetLuaField("stackCount", Mathf.Max(1, info.StackCount));
        }

        #endregion

        #region Stat Change

        /// <summary>
        /// 获取属性变化前的玩家最大生命, 用于变化后刷新 UI.
        /// </summary>
        private int GetOwnerMaxHp()
        {
            var target = owner != null ? owner : PlayerRegistry.Current;
            return target != null ? target.MaxHP : 0;
        }

        /// <summary>
        /// 通知玩家 Buff 属性已经变化.
        /// </summary>
        private void NotifyOwnerStatsChanged(int previousMaxHp)
        {
            var target = owner != null ? owner : PlayerRegistry.Current;
            target?.OnBuffStatsChanged(previousMaxHp);
        }

        /// <summary>
        /// 通知 UI 当前 Buff 列表或层数已经变化.
        /// </summary>
        private static void NotifyBuffsChanged()
        {
            EventCenter.Trigger(GameplayEvents.PlayerBuffsChanged);
        }

        #endregion

        #region Trigger

        /// <summary>
        /// 触发行为对象的添加回调, 并刷新注入数据.
        /// </summary>
        private static void TriggerOnAdd(BuffRuntimeInfo info)
        {
            InjectBehaviorFields(info);
            info.Behavior?.CallLuaFunction("OnAdd");
        }

        /// <summary>
        /// 触发行为对象的移除回调.
        /// </summary>
        private static void TriggerOnRemove(BuffRuntimeInfo info)
        {
            info.Behavior?.CallLuaFunction("OnRemove");
        }

        /// <summary>
        /// 触发行为对象的固定间隔回调, 每次触发前刷新层数.
        /// </summary>
        private static void TriggerOnInterval(BuffRuntimeInfo info)
        {
            if (info.Behavior == null) return;

            InjectBehaviorFields(info);
            info.Behavior.CallLuaFunction("OnInterval");
        }

        /// <summary>
        /// 触发行为对象的主动回调.
        /// </summary>
        private static void TriggerOnTrigger(BuffRuntimeInfo info)
        {
            info.Behavior?.CallLuaFunction("OnTrigger");
        }

        #endregion

        #region Remove Helpers

        /// <summary>
        /// 使用尾部交换的方式移除指定索引的 Buff.
        /// </summary>
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
            buffInfoMap.Remove(removedInfo.Config.Id);
        }

        #endregion
    }
}
