using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
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

        [Header("子弹时间")]
        // 子弹时间只减慢敌人, 不修改全局 Time.timeScale, 这样玩家移动和开火保持正常.
        [FormerlySerializedAs("dashTimeScale")]
        [SerializeField] private float bulletTimeEnemyScale = 0.35f;
        [SerializeField] private float bulletTimeDuration = 1.5f;
        [SerializeField] private float bulletTimeCooldown = 3f;

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

        bool isBulletTimeActive;
        float bulletTimeEndTime;
        float nextBulletTimeReadyTime;
        Coroutine bulletTimeCoroutine;

        public int MaxHP => Mathf.Max(0, Mathf.RoundToInt(CalculateBuffedStat(StatType.MaxHp, maxHp)));
        public bool IsHPFull => HP >= MaxHP;
        public bool IsBulletTimeActive => isBulletTimeActive;
        public bool IsBulletTimeReady => !isBulletTimeActive && BulletTimeReadyRemainingTime <= 0f;
        public float BulletTimeActiveRemainingTime => GetBulletTimeActiveRemainingTime();
        public float BulletTimeReadyRemainingTime => GetBulletTimeReadyRemainingTime();
        public float BulletTimeEnemyScale => Mathf.Clamp(bulletTimeEnemyScale, 0.01f, 1f);

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

            if (!mouseCombatBlocked)
            {
                // 获取鼠标瞄准方向, 鼠标被 UI 接管时完全跳过瞄准刷新.
                var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                dir = (mousePosition - transform.position).normalized;

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

            HandleBulletTimeInput(mouseCombatBlocked);

            if (hurtKnockbackTimer > 0f)
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
                animator.SetFloat("Speed", rb.velocity.magnitude);
            }

            #region 睡眠状态检测
            var hasMotionInput = rb.velocity.magnitude >= 0.01f;
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
            if(horizontal < 0)
            {
                sr.flipX = true;
            }
            else if(horizontal > 0)
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
            CancelBulletTime();
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

            if (Input.GetMouseButtonDown(1))
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

        #region Bullet Time

        /// <summary>
        /// 按下 空格 时触发一段子弹时间, 持续结束后进入冷却.
        /// </summary>
        void HandleBulletTimeInput(bool mouseCombatBlocked)
        {
            if (Mathf.Approximately(Time.timeScale, 0f))
            {
                CancelBulletTime();
                return;
            }

            if (mouseCombatBlocked)
                return;

            if (!IsBulletTimeKeyDown())
                return;

            if (!CanStartBulletTime())
                return;

            bulletTimeCoroutine = StartCoroutine(BulletTimeCoroutine());
        }

        private bool IsBulletTimeKeyDown()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        private bool CanStartBulletTime()
        {
            if (isBulletTimeActive)
                return false;

            return Time.unscaledTime >= nextBulletTimeReadyTime;
        }

        private IEnumerator BulletTimeCoroutine()
        {
            var duration = Mathf.Max(0f, bulletTimeDuration);
            bulletTimeEndTime = Time.unscaledTime + duration;
            nextBulletTimeReadyTime = bulletTimeEndTime + Mathf.Max(0f, bulletTimeCooldown);

            StartBulletTime();
            yield return new WaitForSecondsRealtime(duration);
            StopBulletTime();
            bulletTimeCoroutine = null;
        }

        /// <summary>
        /// 启动子弹时间, 只写敌人时间倍率, 不影响玩家侧的移动和射击计时.
        /// </summary>
        private void StartBulletTime()
        {
            if (isBulletTimeActive)
                return;

            isBulletTimeActive = true;
            GameplayTime.SetEnemyTimeScale(BulletTimeEnemyScale);
            ExitSleepState();
        }

        /// <summary>
        /// 停止子弹时间并恢复敌人正常速度.
        /// </summary>
        private void StopBulletTime()
        {
            if (!isBulletTimeActive)
                return;

            isBulletTimeActive = false;
            GameplayTime.ResetEnemyTimeScale();
        }

        /// <summary>
        /// 外部中断玩家控制时统一停止协程并恢复敌人时间倍率.
        /// </summary>
        private void CancelBulletTime()
        {
            if (bulletTimeCoroutine != null)
            {
                StopCoroutine(bulletTimeCoroutine);
                bulletTimeCoroutine = null;
            }

            StopBulletTime();
        }

        /// <summary>
        /// 重开游戏或重新初始化玩家时清空子弹时间和冷却.
        /// </summary>
        private void ResetBulletTimeState()
        {
            CancelBulletTime();
            bulletTimeEndTime = 0f;
            nextBulletTimeReadyTime = 0f;
        }

        private float GetBulletTimeActiveRemainingTime()
        {
            if (!isBulletTimeActive)
                return 0f;

            return Mathf.Max(0f, bulletTimeEndTime - Time.unscaledTime);
        }

        private float GetBulletTimeReadyRemainingTime()
        {
            if (isBulletTimeActive)
                return 0f;

            return Mathf.Max(0f, nextBulletTimeReadyTime - Time.unscaledTime);
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
        public bool Hurt()
        {
            return Hurt(new DamageInfo(1, Vector2.zero));
        }
        public bool Hurt(DamageInfo damageInfo)
        {
            if(!canHurt) return false;
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
                return true;
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

            return true;
        }
        public void Restart()
        {
            isGameEnded = false;
            ResetBulletTimeState();
            HP = MaxHP;
            PublishHPChanged();
        }
        private void HandleGameEnded()
        {
            isGameEnded = true;
            CancelBulletTime();
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
