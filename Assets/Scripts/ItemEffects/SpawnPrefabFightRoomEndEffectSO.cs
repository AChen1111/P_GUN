using System;
using System.Threading.Tasks;
using Game.Animation;
using Game.Core;
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

        /// <summary>
        /// 执行 Execute 逻辑.
        /// </summary>
        public override async void Execute(FightRoom room)
        {
            try
            {
                await ExecuteAsync(room);
            }
            catch (Exception exception)
            {
                Debug.LogError($"{nameof(SpawnPrefabFightRoomEndEffectSO)}: 战斗结束生成失败, Error: {exception.Message}", this);
                throw;
            }
        }

        /// <summary>
        /// 异步解析预制体并生成奖励.
        /// </summary>
        private async Task ExecuteAsync(FightRoom room)
        {
            if (room == null) return;

            var spawner = ResolveSpawner(room);
            if (spawner == null)
            {
                Debug.LogError($"{nameof(SpawnPrefabFightRoomEndEffectSO)}: 房间 {room.name} 缺少 {nameof(ItemSpawner)}。", room);
                return;
            }

            var spawnPosition = room.GetRoomCenterPoint() + worldOffset;
            var selection = await ResolveSelectionAsync();
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
        private async Task<PrefabSelection> ResolveSelectionAsync()
        {
            if (spawnTable != null && spawnTable.TryGetRandomEntry(out var randomEntry))
            {
                return new PrefabSelection
                {
                    Prefab = await spawnTable.TryResolvePrefabAsync(randomEntry),
                    SpawnAnimEffect = randomEntry.spawnAnimEffect,
                    SpawnAnimDuration = randomEntry.spawnAnimDuration
                };
            }

            return new PrefabSelection
            {
                Prefab = await ResolveRuntimePrefabAsync(prefab)
            };
        }

        /// <summary>
        /// 旧 prefab 配置优先按 itemId 解析热更新预制体.
        /// </summary>
        private static async Task<GameObject> ResolveRuntimePrefabAsync(GameObject configuredPrefab)
        {
            var item = configuredPrefab != null ? configuredPrefab.GetComponent<Item>() : null;
            if (item == null)
            {
                return configuredPrefab;
            }

            if (!AddressableItemAddressCatalog.TryGetAddress(item.ItemId, out var address))
            {
                return configuredPrefab;
            }

            var loader = AddressableLoader.Instance;
            if (loader == null)
            {
                throw new InvalidOperationException($"{nameof(SpawnPrefabFightRoomEndEffectSO)} requires {nameof(AddressableLoader)} for item prefab replacement.");
            }

            return await loader.LoadAssetAsync<GameObject>(address);
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
