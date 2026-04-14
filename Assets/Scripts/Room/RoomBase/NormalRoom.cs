using System.Collections.Generic;
using UnityEngine;
using QFramework.PG;


public class NormalRoom : Room
{
    [Header("敌人预制体")]
    [SerializeField] private Enemy enemyPrefab;

    [Header("敌人可能出现的位置坐标点")]
    [SerializeField] private List<Transform> enemyPoints = new List<Transform>();

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
        needGenerateDoors = true;
        currentWaveCount = Mathf.Max(1, waveCount);
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

