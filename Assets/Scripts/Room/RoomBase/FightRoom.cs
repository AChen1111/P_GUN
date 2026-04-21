using System.Collections.Generic;
using UnityEngine;

public abstract class FightRoom : Room
{
    [Header("波数")]
    [SerializeField] private int waveCount = 1;

    [Header("是否已进入过房间")]
    [SerializeField] private bool hasEnter = false;

    private static HashSet<EnemyBase> enemyList = new HashSet<EnemyBase>();
    
    // 当前波剩余敌人数
    private int enemyCount = 0;
    // 房间剩余波数
    private int remainWaveCount = 0;

    // 当前正在进行战斗流程的房间（给 Enemy 死亡回调使用）
    public static FightRoom currentFightRoom;


    /// <summary>
    /// 所有波次结束回调，给子类扩展
    /// </summary>
    protected virtual void OnFightAllWavesEnd() {}

    /// <summary>
    /// 子类负责生成一波敌人，并返回该波敌人数。
    /// </summary>
    protected abstract int SpawnWaveEnemies();

    /// <summary>
    /// 将新生成的敌人注册到当前房间敌人集合。
    /// </summary>
    protected void RegisterSpawnedEnemy(EnemyBase enemy)
    {
        if (enemy == null) return;
        enemyList.Add(enemy);
    }


    /// <summary>
    /// 房间初始化时开启门生成，并初始化波数。
    /// </summary>
    protected override void OnRoomInitialized()
    {
        needGenerateDoors = true;
        doorStateIsOpen = true;
        remainWaveCount = Mathf.Max(1, waveCount);
    }

    /// <summary>
    /// 玩家首次进入后，启动战斗流程。
    /// </summary>
    protected override void OnPlayerEnteredRoom(Collider2D other)
    {
        if (hasEnter) return;

        hasEnter = true;
        currentFightRoom = this;

        //关闭门
        foreach (var door in doorsList)
        {
            door.CloseDoor();
        }

        ResetWaveCount(remainWaveCount);
        StartNextWave();
    }



    /// <summary>
    /// 重置当前波敌人数；若为 0 则直接结算该波。
    /// </summary>
    public void ResetEnemyCount(int count)
    {
        enemyCount = Mathf.Max(0, count);
        TryCompleteCurrentWave();
    }

    /// <summary>
    /// 外部可重置剩余波数（安全值 >= 0）。
    /// </summary>
    public void ResetWaveCount(int count)
    {
        remainWaveCount = Mathf.Max(0, count);
    }

    /// <summary>
    /// 敌人死亡后减少计数，降到 0 时结算该波。
    /// </summary>
    public void DecreaseEnemyCount()
    {
        if (enemyCount <= 0) return;

        enemyCount--;
        TryCompleteCurrentWave();
    }

    /// <summary>
    /// 供 Enemy 调用：将击杀事件路由到当前战斗房间。
    /// </summary>
    public static void NotifyEnemyDefeated(EnemyBase enemy)
    {
        var ownerRoom = enemy != null && enemy.OwnerFightRoom != null ? enemy.OwnerFightRoom : currentFightRoom;
        ownerRoom?.DecreaseEnemyCount();
        enemyList.Remove(enemy);
    }

    /// <summary>
    /// 开始下一波：先生成，再写入该波敌人数。
    /// </summary>
    private void StartNextWave()
    {
        if (remainWaveCount <= 0) return;

        ResetEnemyCount(SpawnWaveEnemies());
    }

    /// <summary>
    /// 当当前波敌人数归零时结算波次。
    /// </summary>
    private void TryCompleteCurrentWave()
    {
        if (enemyCount > 0) return;
        CompleteCurrentWave();
    }

    /// <summary>
    /// 波次结算：若还有波次则继续，否则结束战斗。
    /// </summary>
    private void CompleteCurrentWave()
    {
        remainWaveCount--;
        Debug.Log("波次结束, 剩余波数: " + remainWaveCount);
        

        //如果剩余波数为0，则打开门
        if (remainWaveCount <= 0)
        {
            remainWaveCount = 0;
            OnFightAllWavesEnd();
            foreach (var door in doorsList)
            {
                door.OpenDoor(true);
            }
            return;
        }

        StartNextWave();
    }


    /// <summary>
    /// 获取距离玩家最近的敌人
    /// </summary>
    /// <param name="playerTransform">玩家位置</param>
    /// <returns>距离玩家最近的敌人</returns>
    public static EnemyBase GetNearestEnemy(Transform playerTransform)
    {
        if(enemyList.Count == 0 || playerTransform == null) return null;

        EnemyBase nearestEnemy = null;
        float nearestSqrDistance = float.MaxValue;
        List<EnemyBase> invalidEnemies = null;

        foreach (var enemy in enemyList)
        {
            // Unity 对象被 Destroy 后会表现为 null，需要过滤并清理
            if (enemy == null || enemy.transform == null || !enemy.gameObject.activeInHierarchy)
            {
                if (invalidEnemies == null) invalidEnemies = new List<EnemyBase>();
                invalidEnemies.Add(enemy);
                continue;
            }

            float sqrDistance = (enemy.transform.position - playerTransform.position).sqrMagnitude;
            if (sqrDistance < nearestSqrDistance)
            {
                nearestSqrDistance = sqrDistance;
                nearestEnemy = enemy;
            }
        }

        if (invalidEnemies != null)
        {
            foreach (var invalidEnemy in invalidEnemies)
            {
                enemyList.Remove(invalidEnemy);
            }
        }

        return nearestEnemy;
    }



    private void OnDisable()
    {
        if (currentFightRoom == this) currentFightRoom = null;
    }
}
