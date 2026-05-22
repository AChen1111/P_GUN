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
        [SerializeField] private int bulletCount = 5;
        [SerializeField] private float bulletSpreadStepAngle = 2f;

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

            void EndAttack()
            {
                stateTimer = 0f;
                hasShot = false;
            }

            void UpdateAttack()
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

            void BeginAttack()
            {
                stateTimer = 0f;
                hasShot = false;
                StopMove();
                SetAnimatorSpeed(0f);
                PlayAttackAnimation();
            }

            void UpdateFollow()
            {
                stateTimer += Time.deltaTime;
                DoFollow();
                if (stateTimer >= followBeforeAttackTime)
                {
                    FSM.ChangeState(EnemyState.Attack);
                }
            }

    void ShootAtPlayer()
    {
        if (bulletPrefab == null || PlayerRegistry.Current == null)
            return;
        var direction = ((Vector2)(PlayerRegistry.Current.transform.position - transform.position)).normalized;
        var spawnPosition = transform.position + (Vector3)(direction * bulletSpawnDistance);
        // 参考霰弹枪散射规则, 中心一发, 其余子弹按左右交替角度偏移.
        var baseAngle = direction.ToAngle();
        var count = Mathf.Max(1, bulletCount);
        for (var i = 0; i < count; i++)
        {
            var spreadSign = i % 2 == 0 ? 1 : -1;
            var bulletAngle = i == 0 ? baseAngle : baseAngle + spreadSign * i * bulletSpreadStepAngle;
            var bulletDirection = bulletAngle.AngleToDirection2D().normalized;
            EnemyBulletPool.Instance.Get(bulletPrefab, spawnPosition, Quaternion.identity, bulletDirection, AttackDamage);
        }

        PlayShootSound();
    }

    void DoFollow()
    {
        if (!FollowPlayerWithBodySpace(out var direction))
        {
            StopMove();
            SetAnimatorSpeed(0f);
            return;
        }

        FaceDirection(direction);
    }

    void FaceDirection(Vector2 direction)
    {
        if (Sr == null)
            return;
        if (direction.x < 0f)
            Sr.flipX = true;
        else if (direction.x > 0f)
            Sr.flipX = false;
    }

    void PlayShootSound()
    {
        audioPlay.Play();
    }
}
    }
}
