using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using QFramework;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;
using Game.Gameplay.Save;

namespace Game.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
    public class Player : ViewController
    {
        private static readonly string[] AddressableWeaponAddresses =
        {
            "weapon/pistol",
            "weapon/ak",
            "weapon/awp",
            "weapon/bow",
            "weapon/laser",
            "weapon/mp5",
            "weapon/rocket_gun",
            "weapon/shotgun"
        };

        public UnityEngine.TextMesh DisPlayText;
        private Rigidbody2D rb;
        private Coroutine mDisplayTextCoroutine;
        [Header("角色贴图")]
        public SpriteRenderer sr;
        [Header("角色移动速度")]
        public float moveSpeed = 5f;

        [Header("速度增量")]
        public float speedUp = 0f;

        [Header("最大血量")]
        public int maxHp = 5;
        [Header("当前血量")]
        public int HP ;
        [Header("武器节点")]
        public Transform Weapon;
        [Header("武器列表")]
        public List<Gun> guns = new List<Gun>();
        [Header("准星")]
        public GameObject AimPrefab;
        [Header("当前枪")]
        public Gun gun;

        [Header("当前枪索引")]
        public int currentGunIndex = 0;

        [Header("自动瞄准")]
        public bool canAutoAim = false;

        [Header("伤害加成")]
        public float damageMultiplier = 1f;

        [Header("Buff管理器")]
        public BuffManager buffManager;

        [Header("动画器")]
        [SerializeField]private Animator animator;
        [SerializeField]private bool isSleep = false;
        private float sleepTimer = 0f;
        private const float SleepDuration = 3f;

        [Header("受击反馈")]
        [SerializeField] private Color hurtFlashColor = new Color(1f, 0.35f, 0.35f, 1f);
        [SerializeField] private float hurtFlashInterval = 0.06f;
        [SerializeField] private int hurtFlashLoops = 4;
        [SerializeField] private float hurtSlowTimeScale = 0.9f;
        [SerializeField] private float hurtSlowDuration = 0.18f;
        [SerializeField] private float hurtInvincibleDuration = 1f;
        [SerializeField] private float hurtKnockbackDistance = 0.65f;
        [SerializeField] private float hurtKnockbackDuration = 0.12f;

        [Header("冲刺")]
        // 冲刺参数暴露给 Inspector, 方便在不改代码的情况下调整手感.
        [SerializeField] private float dashDistance = 2.2f;
        [SerializeField] private float dashDuration = 0.12f;
        [SerializeField] private float dashCooldown = 0.6f;
        [SerializeField] private float dashTimeScale = 0.35f;
        [SerializeField] private float dashCollisionSkin = 0.02f;

        //受击免疫标识.
        bool canHurt = true;
        Color defaultSpriteColor = Color.white;
        bool hasDefaultSpriteColor;
        Coroutine hurtSlowCoroutine;
        Coroutine hurtInvincibleCoroutine;
        float hurtPreviousTimeScale = 1f;
        Vector2 hurtKnockbackVelocity;
        float hurtKnockbackTimer;

        Transform _autoAimTarget;
        bool isGameEnded;
        bool wasMouseCombatBlocked;
        bool weaponLoadoutReady;
        Task weaponLoadoutTask;

        // 冲刺状态独立于受击免疫, 避免和 canHurt 的受击后无敌时间互相污染.
        bool isDashing;
        bool isDashInvincible;
        bool dashTimeScaleApplied;
        float nextDashReadyTime;
        float dashPreviousTimeScale = 1f;
        float dashPreviousFixedDeltaTime = 0.02f;
        Coroutine dashCoroutine;
        Vector2 activeDashDirection;

        // 复用 Cast 结果数组, 避免冲刺期间每帧产生临时 GC.
        readonly RaycastHit2D[] dashCastHits = new RaycastHit2D[8];


        public int MaxHP => Mathf.Max(0, Mathf.RoundToInt(CalculateBuffedStat(StatType.MaxHp, maxHp)));
        public bool IsHPFull => HP >= MaxHP;

        #region Unity Lifecycle

        /// <summary>
        /// 初始化运行时依赖.
        /// </summary>
        void Awake()
        {
            PlayerRegistry.Register(this);
            ResolveBuffManager();
            Restart();
            EventCenter.AddListener(CoreEvents.PlayerDied, HandleGameEnded);
            EventCenter.AddListener(CoreEvents.GameWin, HandleGameEnded);

            animator = GetComponentInChildren<Animator>();
            //默认不显示
            DisPlayText.gameObject.SetActive(false);
            AimPrefab.SetActive(false);

            rb = GetComponent<Rigidbody2D>();
            ResolveSpriteRenderer();
            ResolveAnimator();
            CaptureDefaultVisualState();
            HideWeaponChildrenBeforeLoadout();

            weaponLoadoutTask = InitializeWeaponLoadoutAsync();

            void CaptureDefaultVisualState()
            {
                if (sr == null)
                    return;
                defaultSpriteColor = sr.color;
                hasDefaultSpriteColor = true;
            }

            void ResolveAnimator()
            {
                if (animator != null) return;

                animator = GetComponent<Animator>();
                if (animator == null)
                {
                    animator = GetComponentInChildren<Animator>();
                }

                if (animator == null)
                {
                    Debug.LogWarning("Player Animator 未绑定，动画将被跳过。");
                }
            }

            void ResolveBuffManager()
            {
                if (buffManager != null)
                    return;
                // BuffManager 只从当前玩家对象查找, 不在代码里动态创建 Manager.
                buffManager = GetComponent<BuffManager>();
            }

            void ResolveSpriteRenderer()
            {
                if (sr != null)
                    return;
                sr = GetComponent<SpriteRenderer>();
                if (sr == null)
                {
                    sr = GetComponentInChildren<SpriteRenderer>();
                }

                if (sr == null)
                {
                    Debug.LogWarning("Player SpriteRenderer 未绑定，受击闪烁将被跳过。");
                }
            }
}

        /// <summary>
        /// 初始化 Addressables 武器配置.
        /// </summary>
        private async Task InitializeWeaponLoadoutAsync()
        {
            try
            {
                await ApplyAddressableWeaponLoadoutAsync();
                SelectInitialGun();
                weaponLoadoutReady = true;
                gun?.OnGunUsed();
            }
            catch (Exception exception)
            {
                Debug.LogError($"{nameof(Player)}: 武器加载失败, Error: {exception.Message}", this);
                throw;
            }
        }

        /// <summary>
        /// 按玩家自己的武器地址列表加载并实例化武器.
        /// </summary>
        private async Task ApplyAddressableWeaponLoadoutAsync()
        {
            var loader = AddressableLoader.Instance;
            if (loader == null)
            {
                throw new InvalidOperationException($"{nameof(Player)} requires {nameof(AddressableLoader)} before weapon loadout.");
            }

            if (Weapon == null)
            {
                throw new InvalidOperationException($"{nameof(Player)} requires Weapon transform.");
            }

            ClearCurrentGunInstances();
            foreach (var address in AddressableWeaponAddresses)
            {
                var prefab = await loader.LoadAssetAsync<GameObject>(address);
                var instance = Instantiate(prefab, Weapon);
                instance.name = prefab.name;
                // 武器装载完成前保持隐藏, 避免加载过程中多把枪同时出现在玩家身上.
                instance.SetActive(false);
                var newGun = instance.GetComponent<Gun>();
                if (newGun == null)
                {
                    throw new InvalidOperationException($"{nameof(Player)} weapon prefab missing {nameof(Gun)} component, Prefab: {prefab.name}.");
                }

                guns.Add(newGun);
            }

            gun = null;
            currentGunIndex = Mathf.Clamp(currentGunIndex, 0, Mathf.Max(0, guns.Count - 1));
        }

        /// <summary>
        /// 清理 Player prefab 自带枪械, 防止旧枪仍被切换到.
        /// </summary>
        private void ClearCurrentGunInstances()
        {
            foreach (var oldGun in guns)
            {
                if (oldGun != null)
                {
                    // Destroy 会延迟到帧末, 先隐藏可避免首帧显示预制体自带枪械.
                    oldGun.gameObject.SetActive(false);
                    Destroy(oldGun.gameObject);
                }
            }

            guns.Clear();
        }

        /// <summary>
        /// 武器异步装载前隐藏预制体上已有的武器子物体.
        /// </summary>
        private void HideWeaponChildrenBeforeLoadout()
        {
            if (Weapon == null) return;

            for (var i = 0; i < Weapon.childCount; i++)
            {
                Weapon.GetChild(i).gameObject.SetActive(false);
            }
        }
        async void Start()
        {
            if (weaponLoadoutTask == null)
            {
                return;
            }

            await weaponLoadoutTask;
        }
        void Update()
        {
            if (isGameEnded)
            {
                //游戏结束后不再检测输入和枪体转向, 避免暂停界面中武器继续跟随鼠标.
                return;
            }

            var mouseCombatBlocked = GameplayCursorState.BlocksMouseCombat;
            Vector2 dir = Weapon != null ? Weapon.right : Vector2.right;
            Vector2 mouseDashDir = dir;

            if (!mouseCombatBlocked)
            {
                // 获取鼠标瞄准方向, 鼠标被 UI 接管时完全跳过瞄准刷新.
                var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                dir = (mousePosition - transform.position).normalized;
                mouseDashDir = dir;

                // 自动瞄准命中目标时, 优先使用锁定目标方向.
                AutoAim(ref dir);
                ApplyWeaponDirection(dir);
            }
            else
            {
                HideAutoAimIndicator();
            }

            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            // 冲刺输入在移动计算前处理, 确保本帧立刻进入冲刺状态.
            HandleDashInput(mouseDashDir, mouseCombatBlocked);

            if (isDashing)
            {
                // 冲刺位移由协程直接推进 Rigidbody2D.position, 普通速度在这段时间归零.
                rb.velocity = Vector2.zero;
            }
            else if (hurtKnockbackTimer > 0f)
            {
                // 受击后退使用真实时间计时, 避免慢动作影响后退距离.
                hurtKnockbackTimer -= Time.unscaledDeltaTime;
                rb.velocity = hurtKnockbackTimer > 0f ? hurtKnockbackVelocity : Vector2.zero;
            }
            else
            {
                rb.velocity = new Vector2(horizontal, vertical).normalized * CurrentMoveSpeed;
            }

            if (animator != null)
            {
                var motionSpeed = isDashing ? dashDistance / Mathf.Max(0.01f, dashDuration) : rb.velocity.magnitude;
                animator.SetFloat("Speed", motionSpeed);
            }

            #region 睡眠状态检测
            var hasMotionInput = isDashing || rb.velocity.magnitude >= 0.01f;
            if(!hasMotionInput && !isSleep) {
                sleepTimer += Time.deltaTime;
                if(sleepTimer >= SleepDuration) {
                    isSleep = true;
                    if (animator != null)
                    {
                        animator.SetBool("Sleep", true);
                    }
                }
            }

            if (isSleep && (hasMotionInput || Input.anyKey))
                ExitSleepState();
            #endregion


            //翻转
            var visualHorizontal = isDashing ? activeDashDirection.x : horizontal;
            if(visualHorizontal < 0)
            {
                sr.flipX = true;
            }
            else if(visualHorizontal > 0)
            {
                sr.flipX = false;
            }

            HandleMouseCombatBlockTransition(mouseCombatBlocked, dir);
            HandleCombatInput(dir, mouseCombatBlocked);

            void HandleMouseCombatBlockTransition(bool mouseCombatBlocked, Vector2 dir)
            {
                if (mouseCombatBlocked && !wasMouseCombatBlocked)
                {
                    // 连射武器在鼠标被 UI 接管时补一次抬起, 避免保留射击状态.
                    gun?.ShootUp(dir);
                }

                wasMouseCombatBlocked = mouseCombatBlocked;
            }

            void ApplyWeaponDirection(Vector2 dir)
            {
                // 武器朝向只由鼠标战斗输入驱动, Ctrl 和设置面板打开时保持原方向.
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Weapon.localRotation = Quaternion.Euler(0, 0, angle);
                Weapon.localScale = new Vector3(1, dir.x > 0 ? 1 : -1, 1);
            }
}

        /// <summary>
        /// 释放销毁时持有的运行时状态.
        /// </summary>
        void OnDestroy()
        {
            EventCenter.RemoveListener(CoreEvents.PlayerDied, HandleGameEnded);
            EventCenter.RemoveListener(CoreEvents.GameWin, HandleGameEnded);
            CancelDash();
            RestoreHurtSlowTimeScale();
            ResetVisualState();

            PlayerRegistry.Unregister(this);
        }

        /// <summary>
        /// 重置编辑器默认配置.
        /// </summary>
        private void Reset()
        {
            gameObject.tag = "Player";
        }

        #endregion

        #region Initialize
        void SelectInitialGun()
        {
            if (guns == null || guns.Count == 0)
            {
                gun = null;
                return;
            }

            currentGunIndex = Mathf.Clamp(currentGunIndex, 0, guns.Count - 1);
            for (int i = 0; i < guns.Count; i++)
            {
                if (guns[i] == null) continue;
                if (i == currentGunIndex)
                    guns[i].Show();
                else
                    guns[i].Hide();
            }

            gun = guns[currentGunIndex];
        }

        #endregion

        #region Input And Combat
        void HandleCombatInput(Vector2 dir, bool mouseCombatBlocked)
        {
            if (!weaponLoadoutReady || gun == null || guns.Count == 0)
            {
                return;
            }

            if (!mouseCombatBlocked)
            {
                if (Input.GetMouseButtonDown(0))
                    gun.ShootDown(dir);

                if (Input.GetMouseButtonUp(0))
                    gun.ShootUp(dir);

                if (Input.GetMouseButton(0))
                    gun.Shooting(dir);
            }

            if (Input.GetKeyDown(KeyCode.R))
                gun.Reload();

            if (Input.GetKeyDown(KeyCode.Q))
            {
                gun.Hide();
                currentGunIndex = (currentGunIndex - 1 + guns.Count) % guns.Count;
                gun = guns[currentGunIndex];
                gun.Show();
                gun.OnGunUsed();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                gun.Hide();
                currentGunIndex = (currentGunIndex + 1) % guns.Count;
                gun = guns[currentGunIndex];
                gun.Show();
                gun.OnGunUsed();
            }

            if (Input.GetKeyDown(KeyCode.Tab))
                SwitchAutoAim();

            if (Input.GetKeyDown(KeyCode.M))
                EventCenter.Trigger(CoreEvents.MiniMapToggleRequested);

            void SwitchAutoAim()
            {
                canAutoAim = !canAutoAim;
                if (!canAutoAim)
                {
                    ClearAutoAimTarget();
                }

                ShowDisPlayer("自动瞄准: " + (canAutoAim ? "开启" : "关闭"), 1f);
            }
}

        #endregion

        #region Dash

        /// <summary>
        /// 处理 Shift 冲刺输入.
        /// </summary>
        void HandleDashInput(Vector2 dashDirection, bool mouseCombatBlocked)
        {
            if (mouseCombatBlocked)
                return;

            if (!Input.GetKeyDown(KeyCode.LeftShift) && !Input.GetKeyDown(KeyCode.RightShift))
                return;

            if (!CanStartDash())
                return;

            dashDirection = ResolveDashDirection(dashDirection);
            if (dashDirection.sqrMagnitude <= 0.0001f)
                return;

            dashCoroutine = StartCoroutine(DashCoroutine(dashDirection));
        }

        /// <summary>
        /// 判断当前帧是否允许启动冲刺.
        /// </summary>
        bool CanStartDash()
        {
            if (isDashing)
                return false;

            // 受击击退和受击慢动作期间不允许冲刺, 避免两个 Time.timeScale 效果互相覆盖.
            if (hurtKnockbackTimer > 0f || hurtSlowCoroutine != null)
                return false;

            // UI 暂停时 Time.timeScale 为 0, 这里直接阻止冲刺.
            if (Mathf.Approximately(Time.timeScale, 0f))
                return false;

            return Time.unscaledTime >= nextDashReadyTime;
        }

        /// <summary>
        /// 修正冲刺方向, 避免鼠标和玩家重合时得到零向量.
        /// </summary>
        Vector2 ResolveDashDirection(Vector2 dashDirection)
        {
            if (dashDirection.sqrMagnitude > 0.0001f)
                return dashDirection.normalized;

            // 鼠标和玩家重合时使用武器朝向, 避免零方向导致冲刺无效.
            var fallbackDirection = Weapon != null ? (Vector2)Weapon.right : Vector2.right;
            return fallbackDirection.sqrMagnitude > 0.0001f ? fallbackDirection.normalized : Vector2.right;
        }

        /// <summary>
        /// 按真实时间推进冲刺位移和冲刺慢动作.
        /// </summary>
        IEnumerator DashCoroutine(Vector2 dashDirection)
        {
            isDashing = true;
            isDashInvincible = true;
            activeDashDirection = dashDirection;
            nextDashReadyTime = Time.unscaledTime + Mathf.Max(0f, dashCooldown);
            rb.velocity = Vector2.zero;
            ApplyDashTimeScale();
            ExitSleepState();

            var remainingDistance = Mathf.Max(0f, dashDistance);
            var remainingTime = Mathf.Max(0.01f, dashDuration);
            var dashSpeed = remainingDistance / remainingTime;

            while (remainingDistance > 0f && remainingTime > 0f)
            {
                var deltaTime = Time.unscaledDeltaTime;
                if (deltaTime <= 0f)
                {
                    yield return null;
                    continue;
                }

                var requestedDistance = Mathf.Min(remainingDistance, dashSpeed * deltaTime);
                var moveDistance = GetDashMoveDistance(dashDirection, requestedDistance);
                if (moveDistance <= 0f)
                    break;

                // 冲刺位移用真实时间手动推进, 避免 Time.timeScale 降低后玩家也被拖慢.
                rb.position += dashDirection * moveDistance;
                remainingDistance -= moveDistance;
                remainingTime -= deltaTime;

                // 实际移动距离小于请求距离时说明前方被阻挡, 本次冲刺提前结束.
                if (moveDistance + dashCollisionSkin < requestedDistance)
                    break;

                yield return null;
            }

            FinishDash();
            dashCoroutine = null;
        }

        /// <summary>
        /// 根据当前 Rigidbody2D 碰撞体形状计算本帧可移动距离.
        /// </summary>
        float GetDashMoveDistance(Vector2 dashDirection, float requestedDistance)
        {
            if (requestedDistance <= 0f)
                return 0f;

            var filter = new ContactFilter2D();
            filter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
            filter.useTriggers = false;

            // Rigidbody2D.Cast 使用当前碰撞体形状预判阻挡, 保持冲刺不穿墙.
            var hitCount = rb.Cast(dashDirection, filter, dashCastHits, requestedDistance + dashCollisionSkin);
            var moveDistance = requestedDistance;
            for (var i = 0; i < hitCount; i++)
            {
                var hit = dashCastHits[i];
                if (hit.collider == null)
                    continue;

                moveDistance = Mathf.Min(moveDistance, Mathf.Max(0f, hit.distance - dashCollisionSkin));
            }

            return moveDistance;
        }

        /// <summary>
        /// 应用冲刺慢动作, 并记录进入冲刺前的时间缩放.
        /// </summary>
        void ApplyDashTimeScale()
        {
            dashPreviousTimeScale = Time.timeScale;
            dashPreviousFixedDeltaTime = Time.fixedDeltaTime;
            dashTimeScaleApplied = true;
            var appliedTimeScale = Mathf.Clamp(dashTimeScale, 0.01f, 1f);
            Time.timeScale = appliedTimeScale;
            // 降低 timeScale 后同步缩短 fixedDeltaTime, 避免物理真实刷新频率下降导致冲刺卡顿.
            Time.fixedDeltaTime = dashPreviousFixedDeltaTime * appliedTimeScale;
        }

        /// <summary>
        /// 恢复冲刺慢动作, 但不覆盖暂停或其他系统已经写入的时间缩放.
        /// </summary>
        void RestoreDashTimeScale()
        {
            if (!dashTimeScaleApplied)
                return;

            var appliedTimeScale = Mathf.Clamp(dashTimeScale, 0.01f, 1f);
            if (Mathf.Approximately(Time.timeScale, appliedTimeScale))
            {
                Time.timeScale = dashPreviousTimeScale;
            }

            if (Mathf.Approximately(Time.fixedDeltaTime, dashPreviousFixedDeltaTime * appliedTimeScale))
            {
                Time.fixedDeltaTime = dashPreviousFixedDeltaTime;
            }

            dashTimeScaleApplied = false;
        }

        /// <summary>
        /// 结束冲刺并清理冲刺运行时状态.
        /// </summary>
        void FinishDash()
        {
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
            }

            isDashing = false;
            isDashInvincible = false;
            activeDashDirection = Vector2.zero;
            RestoreDashTimeScale();
        }

        /// <summary>
        /// 外部中断冲刺时统一清理协程, 无敌和时间缩放.
        /// </summary>
        void CancelDash()
        {
            if (dashCoroutine != null)
            {
                StopCoroutine(dashCoroutine);
                dashCoroutine = null;
            }

            if (!isDashing && !isDashInvincible && !dashTimeScaleApplied)
                return;

            FinishDash();
        }

        #endregion

        #region Sleep
        void ExitSleepState()
        {
            if (!isSleep) return;
            isSleep = false;
            sleepTimer = 0f;
            if (animator != null)
                animator.SetBool("Sleep", false);
        }

        #endregion

        #region Health
        public void Hurt()
        {
            Hurt(new DamageInfo(1, Vector2.zero));
        }
        public void Hurt(DamageInfo damageInfo)
        {
            if(!canHurt || isDashInvincible) return;
            if(damageInfo == null) damageInfo = new DamageInfo();

            ExitSleepState();
            VfxPool.Instance.Play(GetBloodVfxPosition(), damageInfo.SourceDirection, BloodVfxColorMode.Green);
            PlayHurtFeedback();
            StartHurtSlow();
            ApplyHurtKnockback(damageInfo.SourceDirection);

            //扣血判断
            HP = Mathf.Max(0, HP - Mathf.Max(1, damageInfo.Damage));
            PublishHPChanged();

            if(HP <= 0)
            {
                EventCenter.Trigger(CoreEvents.PlayerDied);
                return;
            }

            //受击免疫
            canHurt = false;
            StartHurtInvincible();

            Vector3 GetBloodVfxPosition()
            {
                var col = GetComponent<Collider2D>();
                if (col != null)
                {
                    return col.bounds.center;
                }

                if (sr != null)
                {
                    return sr.bounds.center;
                }

                return transform.position;
            }

            void StartHurtSlow()
            {
                if (hurtSlowCoroutine != null)
                    StopCoroutine(hurtSlowCoroutine);
                hurtSlowCoroutine = StartCoroutine(HurtSlowCoroutine());
            }

            void ApplyHurtKnockback(Vector2 sourceDirection)
            {
                if (rb == null)
                    return;
                var knockbackDir = sourceDirection.sqrMagnitude > 0.0001f ? sourceDirection.normalized : GetFallbackKnockbackDirection();
                var duration = Mathf.Max(0.01f, hurtKnockbackDuration);
                hurtKnockbackTimer = duration;
                hurtKnockbackVelocity = knockbackDir * (Mathf.Max(0f, hurtKnockbackDistance) / duration);
            }

            void StartHurtInvincible()
            {
                if (hurtInvincibleCoroutine != null)
                    StopCoroutine(hurtInvincibleCoroutine);
                hurtInvincibleCoroutine = StartCoroutine(HurtInvincibleCoroutine());
            }

            void PlayHurtFeedback()
            {
                //受击时先清理上一次闪烁, 再播放统一受击动画和闪烁.
                ResetVisualState();
                DOTweenAnimMgr.Play("Hurted", gameObject, hurtInvincibleDuration);
                PlayHurtFlash();
            }

    IEnumerator HurtSlowCoroutine()
    {
        hurtPreviousTimeScale = Time.timeScale;
        Time.timeScale = Mathf.Clamp(hurtSlowTimeScale, 0.01f, 1f);
        yield return new WaitForSecondsRealtime(Mathf.Max(0f, hurtSlowDuration));
        RestoreHurtSlowTimeScale();
        hurtSlowCoroutine = null;
    }

    IEnumerator HurtInvincibleCoroutine()
    {
        //受击免疫使用真实时间, 避免受 Time.timeScale 影响.
        yield return new WaitForSecondsRealtime(Mathf.Max(0f, hurtInvincibleDuration));
        canHurt = true;
        hurtInvincibleCoroutine = null;
    }

    Vector2 GetFallbackKnockbackDirection()
    {
        // 没有伤害来源方向时, 按角色面向反方向后退.
        if (sr == null)
            return Vector2.zero;
        return sr.flipX ? Vector2.right : Vector2.left;
    }

    void PlayHurtFlash()
    {
        if (sr == null)
            return;
        sr.DOKill(false);
        sr.color = hasDefaultSpriteColor ? defaultSpriteColor : Color.white;
        var loops = Mathf.Max(2, hurtFlashLoops * 2);
        DOTween.To(() => sr.color, color => sr.color = color, hurtFlashColor, hurtFlashInterval).SetTarget(sr).SetLoops(loops, LoopType.Yoyo).SetUpdate(true).OnComplete(() =>
        {
            if (sr != null)
                sr.color = hasDefaultSpriteColor ? defaultSpriteColor : Color.white;
        });
    }
}
        public void Restart()
        {
            isGameEnded = false;
            HP = MaxHP;
            PublishHPChanged();
        }
        private void HandleGameEnded()
        {
            isGameEnded = true;
            CancelDash();
            if (AimPrefab != null)
            {
                AimPrefab.SetActive(false);
            }
        }
        public int Heal(int amount)
        {
            if (amount <= 0 || IsHPFull) return 0;

            var oldHp = HP;
            HP = Mathf.Min(MaxHP, HP + amount);
            PublishHPChanged();
            return HP - oldHp;
        }
        public async void RestoreSaveData(PlayerSaveData data)
        {
            if (data == null) return;

            try
            {
                await RestoreSaveDataAsync(data);
            }
            catch (Exception exception)
            {
                Debug.LogError($"{nameof(Player)}: 恢复存档失败, Error: {exception.Message}", this);
                throw;
            }
        }

        /// <summary>
        /// 异步恢复玩家存档数据, 等待武器和背包效果资源加载完成.
        /// </summary>
        public async Task RestoreSaveDataAsync(PlayerSaveData data)
        {
            if (data == null) return;

            transform.position = data.position.ToVector3();

            RestoreBuffs(data);
            await RestoreInventoryAsync(data);
            await EnsureWeaponLoadoutReadyAsync();
            RestoreWeapons(data);

            currentGunIndex = Mathf.Clamp(data.currentGunIndex, 0, Mathf.Max(0, guns.Count - 1));
            SelectInitialGun();
            HP = Mathf.Clamp(data.hp, 0, MaxHP);
            PublishHPChanged();
            gun?.OnGunUsed();
        }

        /// <summary>
        /// 等待 Addressables 武器装配完成.
        /// </summary>
        private async Task EnsureWeaponLoadoutReadyAsync()
        {
            if (weaponLoadoutTask != null)
            {
                await weaponLoadoutTask;
            }

            if (!weaponLoadoutReady)
            {
                throw new InvalidOperationException($"{nameof(Player)} weapon loadout is not ready.");
            }
        }

        /// <summary>
        /// 恢复武器弹药数据.
        /// </summary>
        private void RestoreWeapons(PlayerSaveData data)
        {
            for (var i = 0; i < data.weapons.Count; i++)
            {
                var weaponData = data.weapons[i];
                if (weaponData == null)
                    continue;
                var targetGun = guns.Find(candidate => candidate != null && candidate.WeaponId == weaponData.weaponId);
                targetGun?.RestoreAmmo(weaponData.clipAmmo, weaponData.clipMaxAmmo, weaponData.bagAmmo, weaponData.bagMaxAmmo);
            }
        }

        /// <summary>
        /// 恢复 Buff 数据.
        /// </summary>
        private void RestoreBuffs(PlayerSaveData data)
        {
            var manager = buffManager != null ? buffManager : GetComponent<BuffManager>();
            manager?.RestoreSaveData(data.buffs, this);
        }

        /// <summary>
        /// 恢复背包数据并按需加载物品效果.
        /// </summary>
        private async Task RestoreInventoryAsync(PlayerSaveData data)
        {
            var inventory = GetComponent<PlayerInventory>();
            if (inventory == null)
            {
                return;
            }

            inventory.Clear();
            var database = DataBaseManager.Instance != null ? DataBaseManager.Instance.Items : ItemDatabase.RuntimeDatabase;
            for (var i = 0; i < data.inventory.Count; i++)
            {
                var stack = data.inventory[i];
                if (stack == null)
                    continue;
                inventory.RestoreStack(stack.itemId, stack.count, database, await ResolveItemEffectsAsync(stack.itemId));
            }
        }

        /// <summary>
        /// 通过物品 Addressables 地址加载背包效果配置.
        /// </summary>
        private static async Task<IReadOnlyList<ItemEffectBase>> ResolveItemEffectsAsync(int itemId)
        {
            if (!AddressableItemAddressCatalog.TryGetAddress(itemId, out var address))
            {
                throw new InvalidOperationException($"Missing addressable item address, ItemId: {itemId}.");
            }

            var loader = AddressableLoader.Instance;
            if (loader == null)
            {
                throw new InvalidOperationException($"{nameof(AddressableLoader)} must exist before restoring inventory effects.");
            }

            var prefab = await loader.LoadAssetAsync<GameObject>(address);
            var item = prefab.GetComponent<Item>();
            if (item == null)
            {
                throw new InvalidOperationException($"Item prefab missing {nameof(Item)} component, Address: {address}.");
            }

            return item.Effects;
        }
        private void PublishHPChanged()
        {
            EventCenter.Trigger(GameplayEvents.PlayerHPChanged, this);
        }

        /// <summary>
        /// Buff 属性变化后刷新玩家生命上限和 UI.
        /// </summary>
        /// <param name="previousMaxHp">变化前的最大生命.</param>
        public void OnBuffStatsChanged(int previousMaxHp)
        {
            var currentMaxHp = MaxHP;
            var hpChanged = HP > currentMaxHp;
            if (HP > currentMaxHp)
            {
                HP = currentMaxHp;
            }

            if (previousMaxHp != currentMaxHp || hpChanged)
            {
                PublishHPChanged();
            }
        }
        private void RestoreHurtSlowTimeScale()
        {
            if(hurtSlowCoroutine == null) return;
            if(Mathf.Approximately(Time.timeScale, Mathf.Clamp(hurtSlowTimeScale, 0.01f, 1f)))
                Time.timeScale = hurtPreviousTimeScale;
        }
        private void ResetVisualState()
        {
            transform.DOKill(false);

            if(sr == null) return;

            sr.DOKill(false);
            if(hasDefaultSpriteColor)
                sr.color = defaultSpriteColor;
            else
            {
                var color = sr.color;
                color.a = 1f;
                sr.color = color;
            }
        }

        #endregion

        #region Display Text

        ///<summary>
        ///显示头顶文字
        ///</summary>
        ///<param name="text">头顶文字</param>
        ///<param name="duration">显示时间</param>
        IEnumerator ShowDisPlayerCoroutine(string text,float duration)
        {
            DisPlayText.gameObject.SetActive(true);
            DisPlayText.text = text;
            yield return new WaitForSeconds(duration);
            DisPlayText.gameObject.SetActive(false);
            mDisplayTextCoroutine = null;
        }

        ///<summary>
        ///对外接口显示头顶文字
        ///</summary>
        ///<param name="text">头顶文字</param>
        ///<param name="duration">显示时间</param>
        public void ShowDisPlayer(string text,float duration)
        {
            if(mDisplayTextCoroutine != null)
            {
                StopCoroutine(mDisplayTextCoroutine);
                DisPlayText.gameObject.SetActive(false);
            }
            mDisplayTextCoroutine = StartCoroutine(ShowDisPlayerCoroutine(text,duration));
        }

        #endregion

        #region Auto Aim

        ///<summary>
        ///自动瞄准
        ///</summary>
        ///<param name="dir">瞄准方向</param>
        public void AutoAim(ref Vector2 dir)
        {
            if(!canAutoAim || FightRoom.currentFightRoom == null)
            {
                ClearAutoAimTarget();
                return;
            }

            RefreshAutoAimTarget();

            if (_autoAimTarget != null)
            {
                AimPrefab.SetActive(true);
                AimPrefab.transform.position = _autoAimTarget.position;
                dir = (_autoAimTarget.position - transform.position).normalized;
            }
            else
            {
                AimPrefab.SetActive(false);
            }

            void RefreshAutoAimTarget()
            {
                // 自动瞄准每帧刷新最近敌人, 保证目标切换和敌人死亡后的锁定状态及时更新.
                if (canAutoAim && FightRoom.currentFightRoom != null)
                {
                    var targetEnemy = FightRoom.GetNearestEnemy(transform);
                    _autoAimTarget = targetEnemy != null ? targetEnemy.transform : null;
                    return;
                }

                _autoAimTarget = null;
            }
}
        private void ClearAutoAimTarget()
        {
            // 自动瞄准关闭或脱离战斗时, 同步清理锁定目标和准星显示.
            _autoAimTarget = null;
            HideAutoAimIndicator();
        }
        private void HideAutoAimIndicator()
        {
            if (AimPrefab != null)
            {
                AimPrefab.SetActive(false);
            }
        }

        #endregion

        #region Speed

        public float CurrentMoveSpeed => Mathf.Max(0f, CalculateBuffedStat(StatType.MoveSpeed, moveSpeed));
        public float GetSpeed() => CurrentMoveSpeed;

        /// <summary>
        /// 增加速度
        /// </summary>
        /// <param name="value"></param>
        public void AddSpeedByValue(float value)
        {
            moveSpeed += value;
        }

        /// <summary>
        /// 设置速度
        /// </summary>
        /// <param name="value"></param>
        public void SetSpeed(float value)
        {
            moveSpeed = value;
        }

        #endregion

        #region Damage

        /// <summary>
        /// 根据当前玩家增伤系数计算最终子弹伤害.
        /// </summary>
        /// <param name="baseDamage">基础伤害.</param>
        /// <returns>最终伤害.</returns>
        public int CalculateBulletDamage(int baseDamage)
        {
            return Mathf.Max(0, Mathf.RoundToInt(CalculateBuffedStat(StatType.Attack, baseDamage)));
        }

        #endregion

        #region Helpers
        private float CalculateBuffedStat(StatType statType, float baseValue)
        {
            return buffManager != null ? buffManager.CalculateStat(statType, baseValue) : baseValue;
        }

        #endregion
    }
}
