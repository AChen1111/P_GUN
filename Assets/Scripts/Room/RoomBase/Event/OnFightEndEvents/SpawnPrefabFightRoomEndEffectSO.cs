using UnityEngine;

/// <summary>
/// 示例：战斗结束后生成一个预制体（例如宝箱）。
/// </summary>
[CreateAssetMenu(fileName = "SpawnPrefabFightRoomEndEffect", menuName = "PG/Room/Fight End Effects/Spawn Prefab", order = 1)]
public class SpawnPrefabFightRoomEndEffectSO : FightRoomEndEffectSO
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Vector3 worldOffset = Vector3.zero;
    [SerializeField] private bool parentToRoom = false;

    public override void Execute(FightRoom room)
    {
        if (room == null || prefab == null) return;

        var spawnPosition = room.GetRoomCenterPoint() + worldOffset;
        var parent = parentToRoom ? room.transform : null;
        var obj = Object.Instantiate(prefab, spawnPosition, Quaternion.identity, parent);

        var item = obj.GetComponent<Item>();
        if (item != null)
        {
            item.SetPickupEnabled(false);
        }

        DOTweenAnimMgr.Play(AnimType.Scale0To1, obj, onComplete:
        () =>
        {
            if (item == null) return;
            item.SetPickupEnabled(true);
        });
    }
}
