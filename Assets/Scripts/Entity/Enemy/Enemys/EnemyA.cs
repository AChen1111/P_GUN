using UnityEngine;
using QFramework;
using QFramework.PG;
using System.Collections.Generic;

public class EnemyA : EnemyBase
{
    [Header("攻击资源")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private List<AudioClip> shootSounds = new();

    [Header("攻击参数")]
    [SerializeField] private float shootInterval = 0.2f;
    [SerializeField] private float followDuration = 3f;
    [SerializeField] private float attackDuration = 1f;

    private ShootDuration shootDuration;
    private float stateTimer = 0f;

    protected override WeaponType WeaponType => WeaponType.Gun;

    protected override void OnInit()
    {
        shootDuration = new ShootDuration(shootInterval);
        OwnerFightRoom = FightRoom.currentFightRoom;
    }

    protected override void RegisterFSM(FSM<EnemyState> fsm)
    {
        // ── Follow 状态：追踪玩家，计时到达后切换到 Attack ──
        fsm.State(EnemyState.Follow)
            .OnCondition(() => fsm.CurrentStateId == EnemyState.Attack && stateTimer >= attackDuration)
            .OnEnter(() => stateTimer = 0f)
            .OnUpdate(() =>
            {
                stateTimer += Time.deltaTime;
                DoFollow();
            })
            .OnExit(() => stateTimer = 0f);

        // ── Attack 状态：原地射击，计时到达后切换回 Follow ──
        fsm.State(EnemyState.Attack)
            .OnCondition(() => fsm.CurrentStateId == EnemyState.Follow && stateTimer >= followDuration)
            .OnEnter(() => stateTimer = 0f)
            .OnUpdate(() =>
            {
                stateTimer += Time.deltaTime;
                if (shootDuration != null && shootDuration.CanShoot)
                {
                    shootDuration.RecordShootTime();
                    DoShoot();
                }
            })
            .OnExit(() => stateTimer = 0f);

        fsm.StartState(EnemyState.Follow);
    }

    protected override void OnHurt(DamageInfo damageInfo)
    {
        var damage = damageInfo == null ? 0 : damageInfo.Damage;
        ApplyDamage(damage);
    }

    protected override void OnDead()
    {
        if (Rb != null) Rb.velocity = Vector2.zero;
        OwnerFightRoom?.DecreaseEnemyCount();
        Destroy(gameObject);
    }

    private void DoFollow()
    {
        if (IsDead || Global.player == null)
        {
            if (Rb != null) Rb.velocity = Vector2.zero;
            return;
        }

        var dir = (Global.player.transform.position - transform.position).normalized;

        if (Sr != null)
        {
            if (dir.x < 0f) Sr.flipX = true;
            else if (dir.x > 0f) Sr.flipX = false;
        }

        if (Rb != null) Rb.velocity = dir * MoveSpeed;
    }

    private void DoShoot()
    {
        if (bulletPrefab == null || Global.player == null) return;

        var dirToPlayer = (Global.player.transform.position - transform.position).normalized;
        var obj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        var bullet = obj.GetComponent<EnemyBullet>();
        if (bullet != null) bullet.dir = dirToPlayer;
        obj.SetActive(true);

        if (AudioSource != null && shootSounds != null && shootSounds.Count > 0)
        {
            AudioSource.PlayOneShot(shootSounds[Random.Range(0, shootSounds.Count)]);
        }
    }
}
