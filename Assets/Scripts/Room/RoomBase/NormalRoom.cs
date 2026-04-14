using System.Collections.Generic;
using UnityEngine;
public class NormalRoom : FightRoom
{
    [Header("敌人预制体")]
    [SerializeField] private Enemy enemyPrefab;

    [Header("敌人可能出现的位置坐标点")]
    [SerializeField] private List<Transform> enemyPoints = new List<Transform>();

    [Header("每波敌人数量范围")]
    [SerializeField] private Vector2Int enemyCountRange = new Vector2Int(1, 3);

    /// <summary>
    /// 生成波次敌人
    /// </summary>
    protected override int SpawnWaveEnemies()
    {
        var validPoints = new List<Transform>();
        foreach (var point in enemyPoints)
        {
            if (point != null) validPoints.Add(point);
        }

        if (enemyPrefab == null || validPoints.Count == 0)
        {
            return 0;
        }

        //随机生成敌人数量
        var spawnCount = Random.Range(enemyCountRange.x, enemyCountRange.y + 1);
        spawnCount = Mathf.Clamp(spawnCount, 0, validPoints.Count);

        //实例化敌人
        for (int i = 0; i < spawnCount; i++)
        {
            var enemy = Instantiate(enemyPrefab, validPoints[i].position, Quaternion.identity);
            enemy.OwnerFightRoom = this;
            enemy.gameObject.SetActive(true);
        }
        
        return spawnCount;
    }
}

