using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using QFramework.PG;
using QFramework;

/// <summary>
/// 敌人基类
/// </summary>
public abstract class EnemyBase : MonoBehaviour {

    #region 子类实现
    protected abstract void OnHurt(DamageInfo damageInfo);
    protected abstract void OnDead();
    protected abstract void OnInit();
    protected abstract WeaponType WeaponType { get; }
    protected abstract void RegisterFSM(FSM<EnemyState> fsm);
    #endregion

    [Header("基础属性")]
    [SerializeField] protected int MaxHp = 3;
    [SerializeField] protected int CurrentHp = 3;
    [SerializeField] protected float MoveSpeed = 2.5f;
    [SerializeField] bool isDead = false;
    private bool isInited = false;

    [Header("组件引用")]
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D col;
    [SerializeField] AudioSource audioSource;

    [Header("所属房间")]
    public FightRoom OwnerFightRoom;

    /// <summary>
    /// 状态机
    /// </summary>
    public FSM<EnemyState> FSM = new FSM<EnemyState>();

    protected SpriteRenderer Sr => sr;
    protected Rigidbody2D Rb => rb;
    protected Collider2D Col => col;
    protected AudioSource AudioSource => audioSource;
    protected bool IsDead => isDead;

    private void Reset() {
        gameObject.AddComponent<SpriteRenderer>();
        gameObject.AddComponent<Rigidbody2D>();
        gameObject.AddComponent<Collider2D>();
        gameObject.AddComponent<AudioSource>();


        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }


    private void Awake() {
        ///初始化组件
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        gameObject.tag = "Enemy";
        ///初始化属性
        CurrentHp = MaxHp;
    }

    private void Start() {
        Init();
        RegisterFSM(FSM);
        OnStart();
    }
    protected virtual void OnStart(){}

    private void Update()
    {
        FSM.Update();
        OnUpdate();
    }
    protected virtual void OnUpdate(){}
    private void FixedUpdate()
    {
        FSM.FixedUpdate();
        OnFixedUpdate();
    }
    protected virtual void OnFixedUpdate(){}

    protected virtual void OnDestroy()
    {
        OnFSMDestroy();
        FSM.Clear();
    }
    protected virtual void OnFSMDestroy(){
        FSM.Clear();
    }



    #region 对外接口
    /// <summary>
    /// 对外接口,执行受伤逻辑
    /// </summary>
    /// <param name="damageInfo">伤害信息</param>
    public void Hurt(DamageInfo damageInfo){
        if(isDead) return;
        OnHurt(damageInfo);

        if(CurrentHp <= 0) {
            Dead();
        }
    }

    /// <summary>
    /// 对外接口,执行死亡逻辑
    /// </summary>
    /// <param name="damageInfo">伤害信息</param>
    public void Dead(){
        if(isDead) return;
        isDead = true;
        OnDead();
    }

    /// <summary>
    /// 对外接口,执行初始化逻辑
    /// </summary>
    public void Init() {
        if(isInited) return;
        isInited = true;
        OnInit();
    }

    protected void ApplyDamage(int damage) {
        if(damage <= 0) return;
        CurrentHp -= damage;
    }
    #endregion
}