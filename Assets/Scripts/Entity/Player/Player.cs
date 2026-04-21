using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework.PG;
using QFramework;
namespace QFramework.PG {
    public partial class Player : ViewController
    {
        private Rigidbody2D rb;
        private Coroutine mDisplayTextCoroutine;
        [Header("角色贴图")]
        public SpriteRenderer sr;
        [Header("角色移动速度")]
        public float moveSpeed = 5f;
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

        public static Player Instance { get; private set; }
    
        void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            //默认不显示
            DisPlayText.gameObject.SetActive(false);
            AimPrefab.SetActive(false);

            Global.player = this;
            Instance = this;
            rb = GetComponent<Rigidbody2D>();
            ResolveAnimator();

            gun.Hide();
            gun = guns[0];
            gun.Show();

            _autoAimRefreshWait = new WaitForSeconds(AutoAimRefreshInterval);
            StartCoroutine(AutoAimRefreshRoutine());
        }

        void OnDestroy()
        {
            
            Instance = null;
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

            if(isSleep && rb.velocity.magnitude > 0.01f) {
                isSleep = false;
                if (animator != null)
                {
                    animator.SetBool("Sleep", false);
                    sleepTimer = 0f;
                }
            }
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

            //按下
            if(Input.GetMouseButtonDown(0)) {
                gun.ShootDown(dir);
            }
            
            //抬起
            if(Input.GetMouseButtonUp(0)) {
                gun.ShootUp(dir);
            }

            //按住
            if(Input.GetMouseButton(0)) {
                gun.Shooting(dir);
            }

            //换子弹
            if(Input.GetKeyDown(KeyCode.R)) {
                gun.Reload();
            }

            //前一把枪
            if(Input.GetKeyDown(KeyCode.Q)) {
                gun.Hide();
                currentGunIndex = (currentGunIndex - 1 + guns.Count) % guns.Count;
                gun = guns[currentGunIndex];
                gun.Show();
                gun.OnGunUsed();
            }

            //后一把枪
            if(Input.GetKeyDown(KeyCode.E)) {
                gun.Hide();
                currentGunIndex = (currentGunIndex + 1) % guns.Count;
                gun = guns[currentGunIndex];
                gun.Show();
                gun.OnGunUsed();
            }

            if(Input.GetKeyDown(KeyCode.Tab)) {
                SwitchAutoAim();
            }

            if(Input.GetKeyDown(KeyCode.M))
            {
                GameUI.Instance.SwicthMinMapState();
            }
        }

        public void Hurt() {
            if(!canHurt) return;
            
            //扣血判断
            Global.HP--;
            if(Global.HP <= 0) {
                Global.HP = 0;
                GameUI.Instance.ShowOverPanel();
                return;
            }
            Global.OnHPChange?.Invoke();

            //受击免疫
            canHurt = false;
            DOTweenAnimMgr.Play(
                AnimType.Blink, gameObject,
                onComplete:()=>{
                    canHurt = true;
            }
            );

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

        ///<summary>
        ///切换自动瞄准
        ///</summary>
        private void SwitchAutoAim() {
            canAutoAim = !canAutoAim;
            ShowDisPlayer("自动瞄准: " + (canAutoAim ? "开启" : "关闭"), 1f);
        }

    }
}