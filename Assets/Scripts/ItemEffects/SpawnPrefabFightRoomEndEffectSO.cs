using System;
using Game.Animation;
using Game.Gameplay;
using Game.Items;
using UnityEngine;

namespace Game.ItemEffects
{
    /// <summary>
    /// 示例: 战斗结束后生成一个预制体, 例如宝箱.
    /// </summary>
    [CreateAssetMenu(fileName = "SpawnPrefabFightRoomEndEffect", menuName = "PG/Room/Fight End Effects/Spawn Prefab", order = 1)]
    public class SpawnPrefabFightRoomEndEffectSO : FightRoomEndEffectSO
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private ItemSpawnTableSO spawnTable;
        [SerializeField] private Vector3 worldOffset = Vector3.zero;
        [SerializeField] private string animEffectKey = "Scale0To1";
        public override void Execute(FightRoom room)
        {
            try
            {
                ExecuteInternal(room);
            }
            catch (Exception exception)
            {
                Debug.LogError($"{nameof(SpawnPrefabFightRoomEndEffectSO)}: 战斗结束生成失败, Error: {exception.Message}", this);
                throw;
            }
        }

        /// <summary>
        /// 解析预制体并生成奖励.
        /// </summary>
        private void ExecuteInternal(FightRoom room)
        {
            if (room == null) return;

            var spawner = ResolveSpawner(room);
            if (spawner == null)
            {
                Debug.LogError($"{nameof(SpawnPrefabFightRoomEndEffectSO)}: 房间 {room.name} 缺少 {nameof(ItemSpawner)}。", room);
                return;
            }

            var spawnPosition = room.GetRoomCenterPoint() + worldOffset;
            var selection = ResolveSelection();
            if (selection.Prefab == null)
            {
                Debug.LogWarning($"{nameof(SpawnPrefabFightRoomEndEffectSO)}: prefab 和 spawnTable 都为空。", this);
                return;
            }

            if (selection.SpawnAnimEffect != DOTweenAnimType.None)
            {
                spawner.SpawnItem(selection.Prefab, spawnPosition, selection.SpawnAnimEffect, selection.SpawnAnimDuration);
                return;
            }

            spawner.SpawnItem(selection.Prefab, spawnPosition, animEffectKey);
        }

        /// <summary>
        /// 解析战斗结束奖励预制体.
        /// </summary>
        private PrefabSelection ResolveSelection()
        {
            if (spawnTable != null && spawnTable.TryGetRandomEntry(out var randomEntry))
            {
                spawnTable.TryResolvePrefab(randomEntry, out var selectedPrefab);
                return new PrefabSelection
                {
                    Prefab = selectedPrefab,
                    SpawnAnimEffect = randomEntry.spawnAnimEffect,
                    SpawnAnimDuration = randomEntry.spawnAnimDuration
                };
            }

            return new PrefabSelection
            {
                // 战斗结束奖励直接使用 Inspector 中引用的 prefab, 不再按 itemId 加载 Addressables 资源.
                Prefab = prefab
            };
        }

        /// <summary>
        /// 查找房间内的物品生成器.
        /// </summary>
        private static ItemSpawner ResolveSpawner(FightRoom room)
        {
            var spawner = room.GetComponent<ItemSpawner>();
            return spawner != null ? spawner : room.GetComponentInChildren<ItemSpawner>();
        }

        /// <summary>
        /// 战斗结束奖励选择结果.
        /// </summary>
        private struct PrefabSelection
        {
            public GameObject Prefab;
            public DOTweenAnimType SpawnAnimEffect;
            public float SpawnAnimDuration;
        }
    }
}
