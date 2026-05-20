using UnityEngine;
using Game.Core;
using Game.Animation;
using Game.Items;
using Game.Gameplay;

namespace Game.ItemEffects
{
    /// <summary>
    /// 示例：战斗结束后生成一个预制体（例如宝箱）。
    /// </summary>
    [CreateAssetMenu(fileName = "SpawnPrefabFightRoomEndEffect", menuName = "PG/Room/Fight End Effects/Spawn Prefab", order = 1)]
    public class SpawnPrefabFightRoomEndEffectSO : FightRoomEndEffectSO
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private ItemSpawnTableSO spawnTable;
        [SerializeField] private string spawnTableAddress;
        [SerializeField] private Vector3 worldOffset = Vector3.zero;
        [SerializeField] private string animEffectKey = "Scale0To1";

        public override void Execute(FightRoom room)
        {
            if (room == null) return;

            var spawner = ResolveSpawner(room);
            if (spawner == null)
            {
                Debug.LogError($"{nameof(SpawnPrefabFightRoomEndEffectSO)}: 房间 {room.name} 缺少 {nameof(ItemSpawner)}。", room);
                return;
            }

            var spawnPosition = room.GetRoomCenterPoint() + worldOffset;

            var selectedPrefab = ResolveRuntimePrefab(prefab);
            var selectedAnimEffect = DOTweenAnimType.None;
            var selectedAnimDuration = 0f;
            var activeSpawnTable = ResolveSpawnTable();
            if (activeSpawnTable != null && activeSpawnTable.TryGetRandomEntry(out var randomEntry))
            {
                selectedPrefab = activeSpawnTable.TryResolvePrefab(randomEntry, out var resolvedPrefab) ? resolvedPrefab : null;
                selectedAnimEffect = randomEntry.spawnAnimEffect;
                selectedAnimDuration = randomEntry.spawnAnimDuration;
            }

            if (selectedPrefab == null)
            {
                Debug.LogWarning($"{nameof(SpawnPrefabFightRoomEndEffectSO)}: prefab 和 spawnTable 都为空。", this);
                return;
            }

            if (selectedAnimEffect != DOTweenAnimType.None)
            {
                spawner.SpawnItem(selectedPrefab, spawnPosition, selectedAnimEffect, selectedAnimDuration);
                return;
            }

            spawner.SpawnItem(selectedPrefab, spawnPosition, animEffectKey);
        }

        private ItemSpawner ResolveSpawner(FightRoom room)
        {
            var spawner = room.GetComponent<ItemSpawner>();
            return spawner != null ? spawner : room.GetComponentInChildren<ItemSpawner>();
        }

        private ItemSpawnTableSO ResolveSpawnTable()
        {
            if (string.IsNullOrWhiteSpace(spawnTableAddress))
            {
                return spawnTable;
            }

            var content = AddressableRuntimeContent.Instance;
            if (content == null)
            {
                // 允许直接从 GameScene Play, 此时使用 Inspector 中的本地生成表.
                return spawnTable;
            }

            if (content.TryGetAsset<ItemSpawnTableSO>(spawnTableAddress, out var runtimeSpawnTable))
            {
                return runtimeSpawnTable;
            }

            Debug.LogError($"{nameof(SpawnPrefabFightRoomEndEffectSO)}: 找不到已预加载的物品生成表, Address: {spawnTableAddress}.", this);
            return null;
        }

        /// <summary>
        /// 直连预制体作为编辑器兼容配置, Root 预加载完成后优先使用同 ID 的热更预制体.
        /// </summary>
        private static GameObject ResolveRuntimePrefab(GameObject configuredPrefab)
        {
            var item = configuredPrefab != null ? configuredPrefab.GetComponent<Item>() : null;
            var content = AddressableRuntimeContent.Instance;
            if (item != null && content != null && content.TryGetPrefabById("item", item.ItemId, out var runtimePrefab))
            {
                return runtimePrefab;
            }

            return configuredPrefab;
        }
    }
}
