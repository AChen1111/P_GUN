using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using QFramework;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    public class Player : ViewController
    {
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


        public int MaxHP => Mathf.Max(0, Mathf.RoundToInt(CalculateBuffedStat(StatType.MaxHp, maxHp)));
        public bool IsHPFull => HP >= MaxHP;

        #region Unity Lifecycle

        void Awake()
        {
            Global.player = this;
            ResolveBuffManager();
            Restart();
            EventCenter.AddListener(GameEvent.PlayerDied, HandleGameEnded);
            EventCenter.AddListener(GameEvent.GameWin, HandleGameEnded);

            animator = GetComponentInChildren<Animator>();
            //默认不显示
            DisPlayText.gameObject.SetActive(false);
            AimPrefab.SetActive(false);

            rb = GetComponent<Rigidbody2D>();
            ResolveSpriteRenderer();
            ResolveAnimator();
            CaptureDefaultVisualState();

            SelectInitialGun();
        }

        void Start()
        {
            gun?.OnGunUsed();
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
            if(rb.velocity.magnitude < 0.01f && !isSleep) {
                sleepTimer += Time.deltaTime;
                if(sleepTimer >= SleepDuration) {
                    isSleep = true;
                    if (animator != null)
                    {
                        animator.SetBool("Sleep", true);
                    }
                }
            }

            if (isSleep && (rb.velocity.magnitude > 0.01f || InputCheck.IsAnyKeyHeld()))
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
        }

        void OnDestroy()
        {
            EventCenter.RemoveListener(GameEvent.PlayerDied, HandleGameEnded);
            EventCenter.RemoveListener(GameEvent.GameWin, HandleGameEnded);
            RestoreHurtSlowTimeScale();
            ResetVisualState();

            if (Global.player == this)
            {
                Global.player = null;
            }
        }

        private void Reset()
        {
            gameObject.AddComponent<Rigidbody2D>();
            gameObject.AddComponent<CircleCollider2D>();
            gameObject.tag = "Player";
        }

        #endregion

        #region Initialize

        private void ResolveAnimator()
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

        private void ResolveSpriteRenderer()
        {
            if (sr != null) return;

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

        private void ResolveBuffManager()
        {
            if (buffManager != null) return;

            // BuffManager 只从当前玩家对象查找, 不在代码里动态创建 Manager.
            buffManager = GetComponent<BuffManager>();
        }

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
                EventCenter.Trigger(GameEvent.MiniMapToggleRequested);
        }

        private void ApplyWeaponDirection(Vector2 dir)
        {
            // 武器朝向只由鼠标战斗输入驱动, Ctrl 和设置面板打开时保持原方向.
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Weapon.localRotation = Quaternion.Euler(0, 0, angle);
            Weapon.localScale = new Vector3(1, dir.x > 0 ? 1 : -1, 1);
        }

        private void HandleMouseCombatBlockTransition(bool mouseCombatBlocked, Vector2 dir)
        {
            if (mouseCombatBlocked && !wasMouseCombatBlocked)
            {
                // 连射武器在鼠标被 UI 接管时补一次抬起, 避免保留射击状态.
                gun?.ShootUp(dir);
            }

            wasMouseCombatBlocked = mouseCombatBlocked;
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
            if(!canHurt) return;
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
                EventCenter.Trigger(GameEvent.PlayerDied);
                return;
            }

            //受击免疫
            canHurt = false;
            StartHurtInvincible();
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

        private void PublishHPChanged()
        {
            EventCenter.Trigger(GameEvent.PlayerHPChanged, this);
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

        private void PlayHurtFeedback()
        {
            //受击时先清理上一次闪烁, 再播放统一受击动画和闪烁.
            ResetVisualState();
            DOTweenAnimMgr.Play("Hurted", gameObject, hurtInvincibleDuration);
            PlayHurtFlash();
        }

        private void PlayHurtFlash()
        {
            if(sr == null) return;

            sr.DOKill(false);
            sr.color = hasDefaultSpriteColor ? defaultSpriteColor : Color.white;

            var loops = Mathf.Max(2, hurtFlashLoops * 2);
            DOTween.To(() => sr.color, color => sr.color = color, hurtFlashColor, hurtFlashInterval)
                .SetTarget(sr)
                .SetLoops(loops, LoopType.Yoyo)
                .SetUpdate(true)
                .OnComplete(() => {
                    if(sr != null) sr.color = hasDefaultSpriteColor ? defaultSpriteColor : Color.white;
                });
        }

        private void StartHurtInvincible()
        {
            if(hurtInvincibleCoroutine != null)
                StopCoroutine(hurtInvincibleCoroutine);

            hurtInvincibleCoroutine = StartCoroutine(HurtInvincibleCoroutine());
        }

        private void ApplyHurtKnockback(Vector2 sourceDirection)
        {
            if(rb == null) return;

            var knockbackDir = sourceDirection.sqrMagnitude > 0.0001f
                ? sourceDirection.normalized
                : GetFallbackKnockbackDirection();

            var duration = Mathf.Max(0.01f, hurtKnockbackDuration);
            hurtKnockbackTimer = duration;
            hurtKnockbackVelocity = knockbackDir * (Mathf.Max(0f, hurtKnockbackDistance) / duration);
        }

        private Vector2 GetFallbackKnockbackDirection()
        {
            // 没有伤害来源方向时, 按角色面向反方向后退.
            if(sr == null) return Vector2.zero;
            return sr.flipX ? Vector2.right : Vector2.left;
        }

        private IEnumerator HurtInvincibleCoroutine()
        {
            //受击免疫使用真实时间, 避免受 Time.timeScale 影响.
            yield return new WaitForSecondsRealtime(Mathf.Max(0f, hurtInvincibleDuration));
            canHurt = true;
            hurtInvincibleCoroutine = null;
        }

        private void StartHurtSlow()
        {
            if(hurtSlowCoroutine != null)
                StopCoroutine(hurtSlowCoroutine);

            hurtSlowCoroutine = StartCoroutine(HurtSlowCoroutine());
        }

        private IEnumerator HurtSlowCoroutine()
        {
            hurtPreviousTimeScale = Time.timeScale;
            Time.timeScale = Mathf.Clamp(hurtSlowTimeScale, 0.01f, 1f);
            yield return new WaitForSecondsRealtime(Mathf.Max(0f, hurtSlowDuration));

            RestoreHurtSlowTimeScale();
            hurtSlowCoroutine = null;
        }

        private void RestoreHurtSlowTimeScale()
        {
            if(hurtSlowCoroutine == null) return;
            if(Mathf.Approximately(Time.timeScale, Mathf.Clamp(hurtSlowTimeScale, 0.01f, 1f)))
                Time.timeScale = hurtPreviousTimeScale;
        }

        private void CaptureDefaultVisualState()
        {
            if(sr == null) return;

            defaultSpriteColor = sr.color;
            hasDefaultSpriteColor = true;
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
            if(!canAutoAim) return;
            if(FightRoom.currentFightRoom == null) return;

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
        }

        private void RefreshAutoAimTarget()
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

        ///<summary>
        ///切换自动瞄准
        ///</summary>
        private void SwitchAutoAim()
        {
            canAutoAim = !canAutoAim;
            ShowDisPlayer("自动瞄准: " + (canAutoAim ? "开启" : "关闭"), 1f);
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

        private Vector3 GetBloodVfxPosition()
        {
            var col = GetComponent<Collider2D>();
            if(col != null)
            {
                return col.bounds.center;
            }

            if(sr != null)
            {
                return sr.bounds.center;
            }

            return transform.position;
        }

        #endregion
    }
}
