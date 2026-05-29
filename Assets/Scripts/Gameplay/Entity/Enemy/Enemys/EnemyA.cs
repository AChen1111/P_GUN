using UnityEngine;
using QFramework;
using System.Collections.Generic;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    public class EnemyA : EnemyBase
    {
        [Header("攻击资源")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private List<AudioClip> shootSounds = new();

        [Header("攻击参数")]
        [SerializeField] private float shootInterval = 0.2f;
        [SerializeField] private float followDuration = 1f;
        [SerializeField] private float attackDuration = 0.1f;

        private ShootDuration shootDuration;
        private float stateTimer = 0f;

        protected override WeaponType WeaponType => WeaponType.Gun;
        protected override void OnInit()
        {
            // 远程敌人的射击间隔使用敌人局部时钟, 玩家武器仍保留正常时钟.
            shootDuration = new ShootDuration(shootInterval, () => EnemyTime);
            OwnerFightRoom = FightRoom.currentFightRoom;
        }
        protected override void RegisterFSM(FSM<EnemyState> fsm)
        {
            // ── Follow 状态：追踪玩家，计时到达后切换到 Attack ──
            fsm.State(EnemyState.Follow)
                .OnEnter(() => stateTimer = 0f)
                .OnUpdate(() =>
                {
                    stateTimer += EnemyDeltaTime;
                    DoFollow();
                    if (stateTimer >= followDuration)
                        fsm.ChangeState(EnemyState.Attack);
                })
                .OnExit(() => stateTimer = 0f);

            // ── Attack 状态：原地射击，计时到达后切换回 Follow ──
            fsm.State(EnemyState.Attack)
                .OnEnter(() =>
                {
                    stateTimer = 0f;
                    if (Rb != null) Rb.velocity = Vector2.zero;
                    if (shootDuration != null) shootDuration.Duration = shootInterval;
                })
                .OnUpdate(() =>
                {
                    stateTimer += EnemyDeltaTime;
                    TryShootByInterval();
                    if (stateTimer >= attackDuration)
                        fsm.ChangeState(EnemyState.Follow);
                })
                .OnExit(() => stateTimer = 0f);

            fsm.StartState(EnemyState.Follow);

            void TryShootByInterval()
            {
                if (shootDuration == null)
                    return;
                if (!shootDuration.CanShoot)
                    return;
                shootDuration.RecordShootTime();
                DoShoot();
            }

            void DoFollow()
            {
                if (IsDead)
                {
                    if (Rb != null)
                        Rb.velocity = Vector2.zero;
                    return;
                }

                if (FollowPlayerWithBodySpace(out var dir) && Sr != null)
                {
                    if (dir.x < 0f)
                        Sr.flipX = true;
                    else if (dir.x > 0f)
                        Sr.flipX = false;
                }
            }

    void DoShoot()
    {
        if (bulletPrefab == null || PlayerRegistry.Current == null)
            return;
        //Debug.Log("DoShoot");
        var dirToPlayer = (PlayerRegistry.Current.transform.position - transform.position).normalized;
        var spawnPos = transform.position + (Vector3)(dirToPlayer * 0.5f);
        var bullet = EnemyBulletPool.Instance.Get(bulletPrefab, spawnPos, Quaternion.identity, dirToPlayer, AttackDamage);
        if (bullet == null)
            return;
        if (AudioSource != null && shootSounds != null && shootSounds.Count > 0)
        {
            AudioSource.PlayOneShot(shootSounds[Random.Range(0, shootSounds.Count)]);
        }
    }
}
    }
}
