using System.Collections.Generic;
using UnityEngine;
using QFramework;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    /// <summary>
    /// 蝙蝠远程敌人, 先追踪玩家, 计时结束后播放攻击动画并发射子弹.
    /// </summary>
    public class EnemyBat : EnemyBase
    {
        [Header("攻击资源")]
        [SerializeField] private EnemyBullet bulletPrefab;
        [SerializeField] private List<AudioClip> shootSounds = new List<AudioClip>();

        [Header("攻击参数")]
        [SerializeField] private float followBeforeAttackTime = 1.5f;
        [SerializeField] private float attackShootDelay = 0.15f;
        [SerializeField] private float attackLockDuration = 0.35f;
        [SerializeField] private float bulletSpawnDistance = 0.5f;

        private float stateTimer;
        private bool hasShot;

        protected override WeaponType WeaponType => WeaponType.Gun;

        protected override void OnInit()
        {
            OwnerFightRoom = FightRoom.currentFightRoom;
            stateTimer = 0f;
            hasShot = false;
        }

        protected override void RegisterFSM(FSM<EnemyState> fsm)
        {
            fsm.State(EnemyState.Follow)
                .OnEnter(() => stateTimer = 0f)
                .OnUpdate(UpdateFollow)
                .OnExit(() => stateTimer = 0f);

            fsm.State(EnemyState.Attack)
                .OnEnter(BeginAttack)
                .OnUpdate(UpdateAttack)
                .OnExit(EndAttack);

            fsm.StartState(EnemyState.Follow);
        }

        protected override void OnHurt(DamageInfo damageInfo)
        {
            var damage = damageInfo == null ? 0 : damageInfo.Damage;
            ApplyDamage(damage);
        }

        protected override void OnDead()
        {
            FightRoom.NotifyEnemyDefeated(this);
        }

        private void UpdateFollow()
        {
            stateTimer += Time.deltaTime;
            DoFollow();

            if (stateTimer >= followBeforeAttackTime)
            {
                FSM.ChangeState(EnemyState.Attack);
            }
        }

        private void BeginAttack()
        {
            stateTimer = 0f;
            hasShot = false;
            StopMove();
            SetAnimatorSpeed(0f);
            PlayAttackAnimation();
        }

        private void UpdateAttack()
        {
            stateTimer += Time.deltaTime;

            if (!hasShot && stateTimer >= attackShootDelay)
            {
                hasShot = true;
                ShootAtPlayer();
            }

            if (stateTimer >= attackLockDuration)
            {
                FSM.ChangeState(EnemyState.Follow);
            }
        }

        private void EndAttack()
        {
            stateTimer = 0f;
            hasShot = false;
        }

        private void DoFollow()
        {
            if (Global.player == null)
            {
                StopMove();
                SetAnimatorSpeed(0f);
                return;
            }

            var direction = (Global.player.transform.position - transform.position).normalized;
            FaceDirection(direction);

            if (Rb != null)
            {
                Rb.velocity = direction * MoveSpeed;
            }

            SetAnimatorSpeed(MoveSpeed);
        }

        private void ShootAtPlayer()
        {
            if (bulletPrefab == null || Global.player == null) return;

            var direction = (Global.player.transform.position - transform.position).normalized;
            var spawnPosition = transform.position + (Vector3)(direction * bulletSpawnDistance);
            var bullet = EnemyBulletPool.Instance.Get(bulletPrefab, spawnPosition, Quaternion.identity, direction);
            if (bullet == null) return;

            PlayShootSound();
        }

        private void PlayShootSound()
        {
            if (AudioSource == null || shootSounds == null || shootSounds.Count == 0) return;

            AudioSource.PlayOneShot(shootSounds[Random.Range(0, shootSounds.Count)]);
        }

        private void FaceDirection(Vector2 direction)
        {
            if (Sr == null) return;

            if (direction.x < 0f) Sr.flipX = true;
            else if (direction.x > 0f) Sr.flipX = false;
        }
    }
}
