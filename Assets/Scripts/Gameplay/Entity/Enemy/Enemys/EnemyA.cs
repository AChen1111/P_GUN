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
            shootDuration = new ShootDuration(shootInterval);
            OwnerFightRoom = FightRoom.currentFightRoom;
        }

        protected override void RegisterFSM(FSM<EnemyState> fsm)
        {
            // ── Follow 状态：追踪玩家，计时到达后切换到 Attack ──
            fsm.State(EnemyState.Follow)
                .OnEnter(() => stateTimer = 0f)
                .OnUpdate(() =>
                {
                    stateTimer += Time.deltaTime;
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
                    stateTimer += Time.deltaTime;
                    TryShootByInterval();
                    if (stateTimer >= attackDuration)
                        fsm.ChangeState(EnemyState.Follow);
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
            FightRoom.NotifyEnemyDefeated(this);
            EnemyPool.Instance.Release(this);
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
            //Debug.Log("DoShoot");
            var dirToPlayer = (Global.player.transform.position - transform.position).normalized;
            var spawnPos = transform.position + (Vector3)(dirToPlayer * 0.5f);
            var bullet = EnemyBulletPool.Instance.Get(bulletPrefab, spawnPos, Quaternion.identity, dirToPlayer);
            if (bullet == null) return;

            if (AudioSource != null && shootSounds != null && shootSounds.Count > 0)
            {
                AudioSource.PlayOneShot(shootSounds[Random.Range(0, shootSounds.Count)]);
            }
        }

        private void TryShootByInterval()
        {
            if (shootDuration == null) return;
            if (!shootDuration.CanShoot) return;

            shootDuration.RecordShootTime();
            DoShoot();
        }
    }
}
