using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// 大型恶魔敌人, 在环形弹幕和追踪点射之间循环切换.
    /// </summary>
    public class EnemyBig : EnemyBase
    {
        [Header("攻击资源")]
        [SerializeField] private EnemyBullet bulletPrefab;
        [SerializeField] private List<AudioClip> shootSounds = new List<AudioClip>();

        [Header("动画参数")]
        [SerializeField] private string runBoolParameterName = "IsRun";

        [Header("状态1, 环形弹幕")]
        [SerializeField] private float radialStateDuration = 2f;
        [SerializeField] private float radialBurstInterval = 0.75f;
        [SerializeField] private int radialBulletCount = 12;
        [SerializeField] private float radialBulletSpawnDistance = 0.6f;

        [Header("状态2, 追踪玩家")]
        [SerializeField] private float chaseStateDuration = 4f;
        [SerializeField] private float playerSafeDistance = 3f;
        [SerializeField] private float aimedShotInterval = 1.2f;
        [SerializeField] private float aimedBulletSpawnDistance = 0.6f;

        private float stateTimer;
        private float radialBurstTimer;
        private float aimedShotTimer;

        protected override WeaponType WeaponType => WeaponType.Gun;

        protected override void OnInit()
        {
            if (bulletPrefab == null)
                throw new InvalidOperationException($"{nameof(EnemyBig)} requires {nameof(bulletPrefab)} on prefab.");

            OwnerFightRoom = FightRoom.currentFightRoom;
            stateTimer = 0f;
            radialBurstTimer = 0f;
            aimedShotTimer = 0f;
            SetRunAnimation(false);
        }

        protected override void RegisterFSM(FSM<EnemyState> fsm)
        {
            // Attack 表示状态1, 原地待机并重复释放环形弹幕.
            fsm.State(EnemyState.Attack)
                .OnEnter(BeginRadialState)
                .OnUpdate(UpdateRadialState)
                .OnExit(EndRadialState);

            // Follow 表示状态2, 朝玩家移动并定时点射.
            fsm.State(EnemyState.Follow)
                .OnEnter(BeginChaseState)
                .OnUpdate(UpdateChaseState)
                .OnExit(EndChaseState);

            fsm.StartState(EnemyState.Attack);
        }

        protected override void OnDead()
        {
            SetRunAnimation(false);
            base.OnDead();
        }

        private void BeginRadialState()
        {
            stateTimer = 0f;
            radialBurstTimer = 0f;
            StopMove();
            SetRunAnimation(false);
            ShootRadialBurst();
        }

        private void UpdateRadialState()
        {
            stateTimer += Time.deltaTime;
            radialBurstTimer += Time.deltaTime;

            if (radialBurstTimer >= Mathf.Max(0.01f, radialBurstInterval))
            {
                radialBurstTimer = 0f;
                ShootRadialBurst();
            }

            if (stateTimer >= radialStateDuration)
            {
                FSM.ChangeState(EnemyState.Follow);
            }
        }

        private void EndRadialState()
        {
            stateTimer = 0f;
            radialBurstTimer = 0f;
        }

        private void BeginChaseState()
        {
            stateTimer = 0f;
            aimedShotTimer = 0f;
            SetRunAnimation(true);
        }

        private void UpdateChaseState()
        {
            stateTimer += Time.deltaTime;
            aimedShotTimer += Time.deltaTime;

            if (MoveTowardPlayerUntilSafe())
                return;

            TryShootAtPlayerByInterval();

            if (stateTimer >= chaseStateDuration)
            {
                FSM.ChangeState(EnemyState.Attack);
            }
        }

        private void EndChaseState()
        {
            stateTimer = 0f;
            aimedShotTimer = 0f;
            StopMove();
            SetRunAnimation(false);
        }

        private void ShootRadialBurst()
        {
            var count = Mathf.Max(1, radialBulletCount);
            var angleStep = 360f / count;

            for (var i = 0; i < count; i++)
            {
                var angle = angleStep * i * Mathf.Deg2Rad;
                var direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
                var spawnPosition = transform.position + (Vector3)(direction * radialBulletSpawnDistance);
                EnemyBulletPool.Instance.Get(bulletPrefab, spawnPosition, Quaternion.identity, direction, AttackDamage);
            }

            PlayShootSound();
        }

        private void TryShootAtPlayerByInterval()
        {
            if (aimedShotTimer < Mathf.Max(0.01f, aimedShotInterval))
                return;

            aimedShotTimer = 0f;
            ShootAtPlayer();
        }

        private void ShootAtPlayer()
        {
            if (PlayerRegistry.Current == null)
                return;

            var direction = (Vector2)(PlayerRegistry.Current.transform.position - transform.position);
            if (direction.sqrMagnitude <= 0.0001f)
                return;

            direction.Normalize();
            FaceDirection(direction);

            var spawnPosition = transform.position + (Vector3)(direction * aimedBulletSpawnDistance);
            EnemyBulletPool.Instance.Get(bulletPrefab, spawnPosition, Quaternion.identity, direction, AttackDamage);
            PlayShootSound();
        }

        private bool MoveTowardPlayerUntilSafe()
        {
            if (PlayerRegistry.Current == null)
            {
                StopMove();
                SetRunAnimation(false);
                return false;
            }

            var toPlayer = (Vector2)(PlayerRegistry.Current.transform.position - transform.position);
            var safeDistance = Mathf.Max(0f, playerSafeDistance);
            if (toPlayer.sqrMagnitude <= safeDistance * safeDistance)
            {
                StopMove();
                SetRunAnimation(false);
                // 进入安全距离后立刻回到环形弹幕状态, 避免继续贴近玩家.
                FSM.ChangeState(EnemyState.Attack);
                return true;
            }

            if (toPlayer.sqrMagnitude <= 0.0001f)
            {
                StopMove();
                SetRunAnimation(false);
                return false;
            }

            var direction = toPlayer.normalized;
            if (Rb != null)
            {
                Rb.velocity = direction * MoveSpeed;
            }

            FaceDirection(direction);
            SetRunAnimation(MoveSpeed > 0.1f);
            return false;
        }

        private void FaceDirection(Vector2 direction)
        {
            if (Sr == null)
                return;

            if (direction.x < 0f)
                Sr.flipX = true;
            else if (direction.x > 0f)
                Sr.flipX = false;
        }

        private void SetRunAnimation(bool isRun)
        {
            if (Animator == null || string.IsNullOrEmpty(runBoolParameterName))
                return;

            foreach (var parameter in Animator.parameters)
            {
                if (parameter.type == AnimatorControllerParameterType.Bool && parameter.name == runBoolParameterName)
                {
                    Animator.SetBool(runBoolParameterName, isRun);
                    return;
                }
            }
        }

        private void PlayShootSound()
        {
            if (AudioSource == null || shootSounds == null || shootSounds.Count == 0)
                return;

            AudioSource.PlayOneShot(shootSounds[UnityEngine.Random.Range(0, shootSounds.Count)]);
        }
    }
}
