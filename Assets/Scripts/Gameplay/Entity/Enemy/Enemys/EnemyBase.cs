using System;
using UnityEngine;
using DG.Tweening;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    /// <summary>
    /// 敌人基类
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D), typeof(AudioSource), typeof(ItemSpawner))]
    public abstract class EnemyBase : MonoBehaviour, Game.Pooling.IPoolable {

        #region 子类实现
        protected abstract void OnInit();
        protected abstract WeaponType WeaponType { get; }
        protected abstract IEnemyAttack AttackModule { get; }
        #endregion

        [Header("基础属性")]
        [SerializeField] protected int MaxHp = 3;
        [SerializeField] protected int CurrentHp = 3;
        [SerializeField] protected float MoveSpeed = 2.5f;
        [SerializeField] protected int AttackDamage = 1;
        [SerializeField] bool isDead = false;
        private bool isInited = false;

        [Header("碰撞间距")]
        [SerializeField] protected float playerStopDistance = 0.9f;

        [Header("组件引用")]
        [SerializeField] SpriteRenderer sr;
        [SerializeField] Animator animator;
        [SerializeField] Rigidbody2D rb;
        [SerializeField] Collider2D col;
        [SerializeField] AudioSource audioSource;
        Color defaultSpriteColor = Color.white;
        bool hasDefaultSpriteColor;
        private Coroutine deathRecycleCoroutine;

        [Header("动画参数")]
        [SerializeField] private string speedParameterName = "Speed";
        [SerializeField] private string attackTriggerName = "Attack";
        [SerializeField] private string deadTriggerName = "Dead";
        [SerializeField] private string idleStateName = "idle";
        [SerializeField] private string moveStateName = "run";
        [SerializeField] private string attackStateName = "attack";
        [SerializeField] private string deadStateName = "dead";

        [Header("受击反馈")]
        [SerializeField] private Color hurtFlashColor = new Color(1f, 0.35f, 0.35f, 1f);
        [SerializeField] private float hurtFlashInterval = 0.06f;
        [SerializeField] private int hurtFlashLoops = 4;

        [Header("伤害数字")]
        // 配置保存短名, 资源来自阶段预加载缓存.

        [SerializeField, AddressableKey(AddressableAssetKind.Prefab)] private string damageTextPrefabKey = string.Empty;
        private DamageText damageTextPrefab => AddressableAssetAccess.Component<DamageText>(damageTextPrefabKey);
        [SerializeField] private Vector3 damageTextOffset = new Vector3(0f, 0.6f, 0f);

        [Header("死亡回收")]
        [SerializeField] private float deathRecycleDelay = 3f;

        [Header("死亡掉落")]
        [SerializeField] private ItemSpawner itemSpawner;
        private float itemDropChance;

        [Header("所属房间")]
        public FightRoom OwnerFightRoom;
        [Header("音频播放")]
        public AudioPlay audioPlay;

        [Header("行为参数, 由 EnemyData.lua 在生成时写入")]
        // 序列化默认值供编辑模式绘制视锥, 运行时仍由 EnemyData 覆盖.
        [SerializeField] protected float attackAngle = 120f;
        protected float separationRadius = 1.8f;
        protected float separationWeight = 1.5f;
        protected float attackInterval = 1f;
        [SerializeField] protected float attackRange = 6f;

        #region EnemyBrain 桥接成员
        // EnemyBrain 在同一程序集内读取运行时状态与写入速度, 子类不得绕过这些成员直接写 velocity.
        internal bool BrainIsDead => isDead;
        internal float BrainMoveSpeed => MoveSpeed;
        internal float BrainPlayerStopDistance => playerStopDistance;
        internal float BrainSeparationRadius => separationRadius;
        internal float BrainSeparationWeight => separationWeight;
        internal float BrainAttackRange => attackRange;
        internal float BrainAttackAngle => attackAngle;
        private Vector2 brainFacingDirection = Vector2.right;
        internal Vector2 BrainFacingDirection => brainFacingDirection;
        internal float BrainAttackInterval => attackInterval;
        internal Vector2 BrainCollisionCenter => col != null ? col.bounds.center : transform.position;
        internal float BrainCollisionRadius => col != null ? Mathf.Min(col.bounds.extents.x, col.bounds.extents.y) : 0.3f;

        /// <summary>
        /// 行为层写入刚体速度的唯一入口.
        /// </summary>
        internal void ApplyBrainVelocity(Vector2 velocity)
        {
            if (rb != null)
            {
                rb.linearVelocity = velocity;
            }
        }

        /// <summary>
        /// 行为层设置移动动画速度参数.
        /// </summary>
        internal void SetBrainAnimatorSpeed(float speed)
        {
            SetAnimatorSpeed(speed);
        }

        /// <summary>
        /// 行为层设置面朝, 翻转精灵并通知子类刷新朝向相关组件.
        /// </summary>
        internal void SetBrainFacing(Vector2 direction)
        {
            if (direction.sqrMagnitude <= 0.0001f)
            {
                return;
            }

            // 攻击视锥使用完整二维朝向, 精灵按水平分量翻转.
            brainFacingDirection = direction.normalized;
            if (sr == null) return;

            if (direction.x < 0f)
            {
                sr.flipX = true;
            }
            else if (direction.x > 0f)
            {
                sr.flipX = false;
            }

            OnFacingChanged();
        }

        /// <summary>
        /// 在 Scene 视图持续画出与出手判定一致的攻击视锥.
        /// </summary>
        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            // 仅在 Scene 视图绘制, 避免 Game 视图打开 Gizmos 时遮挡战斗画面.
            if (UnityEditor.SceneView.currentDrawingSceneView == null) return;
#endif
            DrawAttackRangeGizmos();
        }

        /// <summary>
        /// 子类可替换攻击范围的 Scene 绘制, 例如 Slime 使用圆形自爆范围.
        /// </summary>
        protected virtual void DrawAttackRangeGizmos()
        {
            if (attackRange <= 0f || attackAngle <= 0f) return;
            var sprite = sr != null ? sr : GetComponent<SpriteRenderer>();
            var facing = Application.isPlaying ? brainFacingDirection
                : sprite != null && sprite.flipX ? Vector2.left : Vector2.right;
            var halfAngle = Mathf.Clamp(attackAngle * 0.5f, 0f, 180f);
            var centerAngle = Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg;
            var origin = transform.position;
            var color = new Color(1f, 0.78f, 0.12f, 0.85f);
            Gizmos.color = color;
            Vector3 previous = origin;
            const int segments = 32;
            for (var index = 0; index <= segments; index++)
            {
                var angle = (centerAngle - halfAngle + attackAngle * index / segments) * Mathf.Deg2Rad;
                var point = origin + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * attackRange;
                if (index == 0) Gizmos.DrawLine(origin, point);
                else Gizmos.DrawLine(previous, point);
                if (index == segments) Gizmos.DrawLine(origin, point);
                previous = point;
            }
        }

        /// <summary>
        /// 面朝变化后的钩子, 例如近战检测盒跟随翻转.
        /// </summary>
        protected virtual void OnFacingChanged() { }
        #endregion

        /// <summary>
        /// 行为大脑, 由 Init 创建, 刚体速度只由它写入.
        /// </summary>
        private EnemyBrain brain;

        protected SpriteRenderer Sr => sr;
        protected Animator Animator => animator;
        protected Rigidbody2D Rb => rb;
        protected Collider2D Col => col;
        protected AudioSource AudioSource => audioSource;
        protected bool IsDead => isDead;
        // 敌人子类统一读取局部时间, 保证子弹时间只影响敌人侧逻辑.
        protected float EnemyDeltaTime => GameplayTime.EnemyDeltaTime;
        protected float EnemyTime => GameplayTime.EnemyTime;
        protected float EnemyTimeScale => GameplayTime.EnemyTimeScale;

        /// <summary>
        /// 重置编辑器默认配置.
        /// </summary>
        private void Reset() {
            sr = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
            audioSource = GetComponent<AudioSource>();
            itemSpawner = GetComponent<ItemSpawner>();
        }


        /// <summary>
        /// 初始化运行时依赖.
        /// </summary>
        private void Awake() {
            ///初始化组件
            sr = GetComponent<SpriteRenderer>();
            if (sr == null)
                sr = GetComponentInChildren<SpriteRenderer>();
            animator = GetComponent<Animator>();
            if (animator == null)
                animator = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
            audioSource = GetComponent<AudioSource>();
            ResolveItemSpawner();
            CaptureDefaultVisualState();
            gameObject.tag = "Enemy";
            ResetRuntimeState();

            void ResolveItemSpawner()
            {
                if (itemSpawner != null)
                    return;
                itemSpawner = GetComponent<ItemSpawner>();
                if (itemSpawner == null)
                {
                    throw new InvalidOperationException($"{nameof(EnemyBase)} requires {nameof(ItemSpawner)} on enemy prefab.");
                }
            }

            void CaptureDefaultVisualState()
            {
                if (sr == null)
                    return;
                defaultSpriteColor = sr.color;
                hasDefaultSpriteColor = true;
            }
}
        private void Start() {
            Init();
        }
        protected virtual void OnStart(){}
        private void Update()
        {
            if (isDead) return;

            ApplyAnimatorTimeScale();
            brain?.Tick(EnemyDeltaTime);
            OnUpdate();
        }
        protected virtual void OnUpdate(){}
        private void FixedUpdate()
        {
            if (isDead) return;

            OnFixedUpdate();
        }
        protected virtual void OnFixedUpdate(){}

        /// <summary>
        /// 释放销毁时持有的运行时状态.
        /// </summary>
        protected virtual void OnDestroy()
        {
            ResetAnimatorPlaybackSpeed();
            brain = null;
        }



        #region 对外接口
        /// <param name="damageInfo">伤害信息</param>
        public void Hurt(DamageInfo damageInfo){
            if(isDead) return;
            if(damageInfo == null) damageInfo = new DamageInfo();

            OnHurt(damageInfo);
            ShowDamageText(damageInfo.Damage);
            VfxPool.Instance.Play(GetBloodVfxPosition(), damageInfo.SourceDirection);
            HurtAnim();

            if(CurrentHp <= 0) {
                Dead();
            }

            void ShowDamageText(int damage)
            {
                if (damage <= 0 || damageTextPrefab == null)
                    return;
                // 敌人只传递伤害值和基准位置, 具体随机字号和飘字动画交给 DamageText prefab.
                var damageTextPool = DamageTextPool.Instance;
                if (damageTextPool == null)
                {
                    Debug.LogError($"{nameof(DamageTextPool)} is missing in scene.", this);
                    return;
                }

                damageTextPool.Play(damageTextPrefab, damage, GetBloodVfxPosition() + damageTextOffset);
            }
}
        protected virtual void HurtAnim()
        {
            // 受击时先清理上一次闪烁, 再播放统一受击动画和闪烁.
            ResetVisualState();
            DOTweenAnimMgr.Play("Hurted", gameObject,0.5f);
            PlayHurtFlash();

            void PlayHurtFlash()
            {
                if (sr == null)
                    return;
                sr.DOKill(false);
                sr.color = defaultSpriteColor;
                var loops = Mathf.Max(2, hurtFlashLoops * 2);
                DOTween.To(() => sr.color, color => sr.color = color, hurtFlashColor, hurtFlashInterval).SetTarget(sr).SetLoops(loops, LoopType.Yoyo).OnComplete(() =>
                {
                    if (sr != null)
                        sr.color = defaultSpriteColor;
                });
            }
}
        /// <param name="damageInfo">伤害信息</param>
        public void Dead(){
            if(isDead) return;
            isDead = true;
            StopMove();
            SetAnimatorSpeed(0f);
            // 死亡后强制离开行为状态, 清掉路径和视野记忆.
            brain?.ResetForPoolOrDeath();

            if(col != null) {
                col.enabled = false;
            }

            PlayDeathAnimation();
            OnDead();
            StartDeathRecycle();

            void StartDeathRecycle()
            {
                StopDeathRecycle();
                deathRecycleCoroutine = StartCoroutine(DeathRecycleCoroutine());
            }

            void PlayDeathAnimation()
            {
                TrySetAnimatorTrigger(deadTriggerName);
                PlayStateIfDifferent(deadStateName, true);
            }

    System.Collections.IEnumerator DeathRecycleCoroutine()
    {
        yield return new WaitForSeconds(deathRecycleDelay);
        deathRecycleCoroutine = null;
        EnemyPool.Instance.Release(this);
    }
}

        /// <summary>
        /// 默认受伤逻辑, 子类只在有特殊受伤行为时重写.
        /// </summary>
        /// <param name="damageInfo">伤害信息.</param>
        protected virtual void OnHurt(DamageInfo damageInfo) {
            var damage = damageInfo == null ? 0 : damageInfo.Damage;
            ApplyDamage(damage);
        }

        /// <summary>
        /// 默认死亡逻辑, 负责通知房间, 广播击杀事件并尝试掉落物品.
        /// </summary>
        protected virtual void OnDead() {
            FightRoom.NotifyEnemyDefeated(this);
            EventCenter.Trigger(GameplayEvents.EnemyDefeated, new EnemyDefeatedEvent(this));
            TryDropItem();

            void TryDropItem()
            {
                if (itemSpawner == null || itemDropChance <= 0f)
                    return;
                if (!itemSpawner.HasDropEntries)
                    return;
                if (UnityEngine.Random.value > itemDropChance)
                    return;
                SpawnDropItemAsync(transform.position);
            }
}

        /// <summary>
        /// 异步生成死亡掉落物.
        /// </summary>
        private async void SpawnDropItemAsync(Vector3 spawnPosition)
        {
            try
            {
                await itemSpawner.SpawnItemAsync(spawnPosition);
            }
            catch (Exception exception)
            {
                Debug.LogError($"{nameof(EnemyBase)}: 死亡掉落生成失败, Error: {exception.Message}", this);
                throw;
            }
        }
        public void Init() {
            if(isInited) return;
            isInited = true;
            // 房间归属优先用生成方写入的引用, 直接摆场景时回退到当前战斗房间.
            if (OwnerFightRoom == null) {
                OwnerFightRoom = FightRoom.currentFightRoom;
            }
            OnInit();
            brain = new EnemyBrain(this, AttackModule);
            OnStart();
        }
        protected void ApplyDamage(int damage) {
            if(damage <= 0) return;
            //Debug.Log("ApplyDamage: " + damage + " CurrentHp: " + CurrentHp);
            CurrentHp -= damage;
        }
        public void OnSpawnFromPool() {
            ResetRuntimeState();
            Init();
        }
        public void OnRecycleToPool() {
            PrepareForPoolRelease();
        }
        public void SetOwnerFightRoom(FightRoom ownerFightRoom) {
            OwnerFightRoom = ownerFightRoom;
        }

        /// <summary>
        /// 应用数据库里的基础属性配置, 生成时调用以覆盖 prefab 默认值.
        /// </summary>
        public void ApplyConfig(EnemyConfig config) {
            if(config.MaxHp > 0) MaxHp = config.MaxHp;
            if(config.MoveSpeed > 0f) MoveSpeed = config.MoveSpeed;
            if(config.Damage > 0) AttackDamage = config.Damage;
            itemDropChance = Mathf.Clamp01(config.ItemDropChance);

            // 攻击视锥与分离参数供行为层使用, 数值同样来自 EnemyData.lua.
            separationRadius = config.SeparationRadius;
            separationWeight = config.SeparationWeight;
            attackInterval = config.AttackInterval;
            attackRange = config.AttackRange;
            attackAngle = config.AttackAngle;

            CurrentHp = MaxHp;
        }

        /// <summary>
        /// 回收到对象池前调用，清理移动和房间引用，避免下一次复用带入旧状态。
        /// </summary>
        public void PrepareForPoolRelease() {
            StopDeathRecycle();
            StopMove();
            ResetVisualState();
            OwnerFightRoom = null;
            // 回池后清空行为状态, 复用出来的敌人不带上一次的记忆.
            brain?.ResetForPoolOrDeath();
        }

        /// <summary>
        /// 清理敌人的运行时状态。状态机必须清空，否则复用后会残留上一轮注册的状态。
        /// </summary>
        private void ResetRuntimeState() {
            isDead = false;
            isInited = false;
            CurrentHp = MaxHp;
            StopMove();
            ResetVisualState();
            ResetAnimatorState();
            brainFacingDirection = sr != null && sr.flipX ? Vector2.left : Vector2.right;

            if(col != null) {
                col.enabled = true;
            }

            void ResetAnimatorState()
            {
                if (animator == null)
                    return;
                ResetAnimatorPlaybackSpeed();
                TryResetAnimatorTrigger(attackTriggerName);
                TryResetAnimatorTrigger(deadTriggerName);
                SetAnimatorSpeed(0f);
                animator.Rebind();
                animator.Update(0f);
            }
}
        protected void ResetVisualState() {
            transform.DOKill(false);

            if(sr == null) return;

            sr.DOKill(false);
            if(hasDefaultSpriteColor)
                sr.color = defaultSpriteColor;
            else {
                var color = sr.color;
                color.a = 1f;
                sr.color = color;
            }
        }
        protected void StopMove() {
            if(rb != null) {
                rb.linearVelocity = Vector2.zero;
            }
        }
        /// <summary>
        /// 按敌人局部时间倍率缩放动画, 让攻击事件和视觉节奏一起变慢.
        /// </summary>
        private void ApplyAnimatorTimeScale() {
            if(animator == null) return;

            animator.speed = EnemyTimeScale;
        }
        /// <summary>
        /// 回收或销毁前恢复动画速度, 避免对象池复用时继承子弹时间状态.
        /// </summary>
        private void ResetAnimatorPlaybackSpeed() {
            if(animator == null) return;

            animator.speed = 1f;
        }

        /// <summary>
        /// 设置移动动画速度参数, 让所有敌人复用同一套动画机字段.
        /// </summary>
        protected void SetAnimatorSpeed(float speed) {
            if(animator == null) return;

            if(HasAnimatorParameter(speedParameterName, AnimatorControllerParameterType.Float)) {
                animator.SetFloat(speedParameterName, speed);
            }

            PlayStateIfDifferent(speed > 0.1f ? moveStateName : idleStateName);
        }

        /// <summary>
        /// 播放攻击动画, 子类只负责决定何时攻击.
        /// </summary>
        protected void PlayAttackAnimation() {
            TrySetAnimatorTrigger(attackTriggerName);
            PlayStateIfDifferent(attackStateName, true);
        }
        private void StopDeathRecycle() {
            if(deathRecycleCoroutine == null) return;

            StopCoroutine(deathRecycleCoroutine);
            deathRecycleCoroutine = null;
        }
        private void TrySetAnimatorTrigger(string triggerName) {
            if(!HasAnimatorParameter(triggerName, AnimatorControllerParameterType.Trigger)) return;

            animator.ResetTrigger(triggerName);
            animator.SetTrigger(triggerName);
        }
        private void TryResetAnimatorTrigger(string triggerName) {
            if(!HasAnimatorParameter(triggerName, AnimatorControllerParameterType.Trigger)) return;

            animator.ResetTrigger(triggerName);
        }
        private bool HasAnimatorParameter(string parameterName, AnimatorControllerParameterType parameterType) {
            if(animator == null || string.IsNullOrEmpty(parameterName)) return false;

            foreach(var parameter in animator.parameters) {
                if(parameter.type == parameterType && parameter.name == parameterName) return true;
            }

            return false;
        }
        private void PlayStateIfDifferent(string stateName, bool restart = false) {
            if(animator == null || string.IsNullOrEmpty(stateName)) return;
            if(!animator.HasState(0, Animator.StringToHash(stateName))) return;
            if(!restart && animator.GetCurrentAnimatorStateInfo(0).shortNameHash == Animator.StringToHash(stateName)) return;

            animator.Play(stateName, 0, 0f);
        }
        private Vector3 GetBloodVfxPosition() {
            if(col != null) {
                return col.bounds.center;
            }

            if(sr != null) {
                return sr.bounds.center;
            }

            return transform.position;
        }
        #endregion
    }
}
