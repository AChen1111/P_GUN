using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
public class Player : ViewController
{
    public UnityEngine.TextMesh DisPlayText;

    private Rigidbody2D rb;
    private Coroutine mDisplayTextCoroutine;
    [Header("角色贴图")]
    public SpriteRenderer sr;
    [Header("角色移动速度")]
    public float moveSpeed = 5f;

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

    const float AutoAimRefreshInterval = 0.1f;

    [Header("动画器")]
    [SerializeField]private Animator animator;
    [SerializeField]private bool isSleep = false;
    private float sleepTimer = 0f;
    private const float SleepDuration = 3f;
    
    //受击免疫标识
    bool canHurt = true;

    WaitForSeconds _autoAimRefreshWait;
    Transform _autoAimTarget;

    
    public int MaxHP => Mathf.Max(0, maxHp);
    public bool IsHPFull => HP >= MaxHP;
    public event Action OnHPChange;

    void Awake()
    {
        Global.player = this;
        Restart();

        animator = GetComponentInChildren<Animator>();
        //默认不显示
        DisPlayText.gameObject.SetActive(false);
        AimPrefab.SetActive(false);

        rb = GetComponent<Rigidbody2D>();
        ResolveAnimator();

        SelectInitialGun();

        _autoAimRefreshWait = new WaitForSeconds(AutoAimRefreshInterval);
        StartCoroutine(AutoAimRefreshRoutine());
    }

    void Start()
    {
        gun?.OnGunUsed();
    }

    void OnDestroy()
    {
        if (Global.player == this)
        {
            Global.player = null;
        }
    }

    private void Reset() {
        gameObject.AddComponent<Rigidbody2D>();
        gameObject.AddComponent<CircleCollider2D>();
        gameObject.tag = "Player";
    }

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

    void Update()
    {
        //获取鼠标位置
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //获取鼠标位置与角色位置的向量
        Vector2 dir = (mousePosition - transform.position).normalized;  

        //自动瞄准命中目标时，优先使用锁定目标方向
        AutoAim(ref dir);

        //转成欧拉角
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //设置武器旋转
        Weapon.localRotation = Quaternion.Euler(0, 0, angle);
        Weapon.localScale = new Vector3(1, dir.x > 0 ? 1 : -1,1);

        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        
        rb.velocity = new Vector2(horizontal, vertical).normalized * moveSpeed;
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

        HandleCombatInput(dir);
    }

    void ExitSleepState()
    {
        if (!isSleep) return;
        isSleep = false;
        sleepTimer = 0f;
        if (animator != null)
            animator.SetBool("Sleep", false);
    }

    void HandleCombatInput(Vector2 dir)
    {
        if (Input.GetMouseButtonDown(0))
            gun.ShootDown(dir);

        if (Input.GetMouseButtonUp(0))
            gun.ShootUp(dir);

        if (Input.GetMouseButton(0))
            gun.Shooting(dir);

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
            GameUI.Instance.SwicthMinMapState();
    }

    public void Hurt() {
        Hurt(new DamageInfo(1, Vector2.zero));
    }

    public void Hurt(DamageInfo damageInfo) {
        if(!canHurt) return;
        if(damageInfo == null) damageInfo = new DamageInfo();
        
        ExitSleepState();
        VfxPool.Instance.Play(GetBloodVfxPosition(), damageInfo.SourceDirection, BloodVfxColorMode.Green);

        //扣血判断
        HP = Mathf.Max(0, HP - Mathf.Max(1, damageInfo.Damage));
        OnHPChange?.Invoke();

        if(HP <= 0) {
            GameUI.Instance.ShowOverPanel();
            return;
        }

        //受击免疫
        canHurt = false;
        DOTweenAnimMgr.Play(
            AnimType.Hurted, gameObject,1f,
            onComplete:()=>{
                canHurt = true;
        }
        );

    }

    public void Restart() {
        HP = MaxHP;
        OnHPChange?.Invoke();
    }

    public int Heal(int amount)
    {
        if (amount <= 0 || IsHPFull) return 0;

        var oldHp = HP;
        HP = Mathf.Min(MaxHP, HP + amount);
        OnHPChange?.Invoke();
        return HP - oldHp;
    }


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

    ///<summary>
    ///自动瞄准
    ///</summary>
    ///<param name="dir">瞄准方向</param>
    public void AutoAim(ref Vector2 dir)
    {
        if(!canAutoAim) return;
        if(FightRoom.currentFightRoom == null) return;

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

    IEnumerator AutoAimRefreshRoutine()
    {
        while (true)
        {
            if (canAutoAim && FightRoom.currentFightRoom != null)
            {
                var targetEnemy = FightRoom.GetNearestEnemy(transform);
                _autoAimTarget = targetEnemy != null ? targetEnemy.transform : null;
            }
            else
            {
                _autoAimTarget = null;
            }

            yield return _autoAimRefreshWait;
        }
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

    ///<summary>
    ///切换自动瞄准
    ///</summary>
    private void SwitchAutoAim() {
        canAutoAim = !canAutoAim;
        ShowDisPlayer("自动瞄准: " + (canAutoAim ? "开启" : "关闭"), 1f);
    }

    private Vector3 GetBloodVfxPosition() {
        var col = GetComponent<Collider2D>();
        if(col != null) {
            return col.bounds.center;
        }

        if(sr != null) {
            return sr.bounds.center;
        }

        return transform.position;
    }
}
