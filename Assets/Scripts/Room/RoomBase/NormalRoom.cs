using System.Collections.Generic;
using UnityEngine;
using Edgar.Unity;
using QFramework.PG;


public class NormalRoom : Room
{
    [Header("敌人预制体")]
    [SerializeField] private Enemy enemyPrefab;

    [Header("门预制体")]
    [SerializeField] private Door doorPrefab;

    [Header("敌人可能出现的位置坐标点")]
    [SerializeField] private List<Transform> enemyPoints = new List<Transform>();

    [Header("门坐标点")]
    [SerializeField] private List<Transform> doorPoints = new List<Transform>();

    [Header("波数")]
    [SerializeField] private int waveCount = 1;

    [Header("每波敌人数量范围")]
    [SerializeField] private Vector2Int enemyCountRange = new Vector2Int(1, 3);

    private int currentWaveCount = 1;
    //是否已进入过房间
    [Header("是否已进入过房间")]
    [SerializeField] private bool hasEnter = false;
    private bool eventSubscribed;

    protected override void OnRoomInitialized()
    {
        currentWaveCount = Mathf.Max(1, waveCount);
        GenerateDoors();
    }

    protected override void OnPlayerEnteredRoom(Collider2D other)
    {
        if (hasEnter) return;
        hasEnter = true;

        if (RoomPlayManager.Instance == null) return;

        RoomPlayManager.Instance.ResetWaveCount(currentWaveCount);

        SpawnWaveEnemies();
        RoomPlayManager.Instance.OnRoomCurrentWaveEndEvent += SpawnWaveEnemies;
        RoomPlayManager.Instance.OnRoomAllWavesEndEvent += OnRoomAllWavesEnd;
        eventSubscribed = true;
    }

    private void GenerateDoors()
    {
        if (doorPrefab == null) return;

        var spawnPositions = GetDoorSpawnPositionsFromEdgar();
        if (spawnPositions.Count == 0)
        {
            foreach (var point in doorPoints)
            {
                if (point == null) continue;
                spawnPositions.Add(point.position);
            }
        }

        foreach (var position in spawnPositions)
        {
            var door = Instantiate(doorPrefab, position, Quaternion.identity);
            door.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 从Edgar生成结果中读取当前房间门位置（优先使用）
    /// </summary>
    /// <summary>
    /// 从 Edgar 的房间实例中提取“实际使用的门”并换算为世界坐标。
    /// 关键点：door.DoorLine.GetPoints() 返回的是房间模板局部网格坐标，不是世界坐标。
    /// </summary>
    private List<Vector3> GetDoorSpawnPositionsFromEdgar()
    {
        // 返回值：每个门对应一个“世界坐标中心点”，用于实例化 Door 预制体。
        var result = new List<Vector3>();

        // 当前对象必须挂有 RoomInfoGrid2D，并且 Edgar 已经写入 RoomInstance。
        // 若未生成或组件缺失，返回空列表（上层会回退 doorPoints）。
        if (!TryGetComponent<RoomInfoGrid2D>(out var roomInfo) || roomInfo.RoomInstance == null)
        {
            return result;
        }

        var roomInstance = roomInfo.RoomInstance;
        var roomTemplateInstance = roomInstance.RoomTemplateInstance;
        // 房间模板实例为空时无法做局部->世界坐标转换，直接返回空。
        if (roomTemplateInstance == null)
        {
            return result;
        }

        // roomInstance.Doors 只包含本次关卡布局中“真正连接生效”的门。
        foreach (var door in roomInstance.Doors)
        {
            // 门可能占多个格子（例如宽门），这里拿到该门线上的所有格点（局部坐标）。
            var points = door.DoorLine.GetPoints();
            var localSum = Vector3.zero;
            var count = 0;

            foreach (var point in points)
            {
                // 将格子坐标转为格子中心坐标：+0.5f 避免落在格角。
                // 这里仍然是“房间模板局部坐标系”。
                localSum += new Vector3(point.x + 0.5f, point.y + 0.5f, 0f);
                count++;
            }

            if (count > 0)
            {
                // 多格门取中心点（平均值），单格门就是该格中心。
                var localCenter = localSum / count;
                // 只做一次标准局部->世界转换，避免重复叠加 roomInstance.Position 导致偏移。
                var worldCenter = roomTemplateInstance.transform.TransformPoint(localCenter);
                result.Add(worldCenter);
            }
        }

        return result;
    }

    /// <summary>
    /// 生成波次敌人
    /// </summary>
    private void SpawnWaveEnemies()
    {
        if (RoomPlayManager.Instance == null) return;

        var validPoints = new List<Transform>();
        foreach (var point in enemyPoints)
        {
            if (point != null) validPoints.Add(point);
        }

        if (enemyPrefab == null || validPoints.Count == 0)
        {
            RoomPlayManager.Instance.ResetEnemyCount(0);
            return;
        }

        //随机生成敌人数量
        var spawnCount = Random.Range(enemyCountRange.x, enemyCountRange.y + 1);
        RoomPlayManager.Instance.ResetEnemyCount(spawnCount);


        //实例化敌人
        for (int i = 0; i < spawnCount; i++)
        {
            var enemy = Instantiate(enemyPrefab, validPoints[i].position, Quaternion.identity);
            enemy.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 波次结束
    /// </summary>
    private void OnRoomAllWavesEnd()
    {
        UnsubscribeEvents();
        hasEnter = true;
    }

    /// <summary>
    /// 禁用时取消订阅事件
    /// </summary>
    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    /// <summary>
    /// 取消订阅事件
    /// </summary>
    private void UnsubscribeEvents()
    {
        if (!eventSubscribed || RoomPlayManager.Instance == null) return;

        RoomPlayManager.Instance.OnRoomCurrentWaveEndEvent -= SpawnWaveEnemies;
        RoomPlayManager.Instance.OnRoomAllWavesEndEvent -= OnRoomAllWavesEnd;
        eventSubscribed = false;
    }
}

