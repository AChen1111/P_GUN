using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using QFramework;
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
    public abstract class EnemyBase : MonoBehaviour, Game.Pooling.IPoolable {

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
        Color defaultSpriteColor = Color.white;
        bool hasDefaultSpriteColor;

        [Header("所属房间")]
        public FightRoom OwnerFightRoom;
        [Header("音频播放")]
        public AudioPlay audioPlay;

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
            if (sr == null)
                sr = GetComponentInChildren<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
            audioSource = GetComponent<AudioSource>();
            CaptureDefaultVisualState();
            gameObject.tag = "Enemy";
            ResetRuntimeState();
        }

        private void Start() {
            Init();
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
            if(damageInfo == null) damageInfo = new DamageInfo();

            //Debug.Log("Enemy Hurt");
            OnHurt(damageInfo);
            VfxPool.Instance.Play(GetBloodVfxPosition(), damageInfo.SourceDirection);
            HurtAnim();

            if(CurrentHp <= 0) {
                Dead();
            }
        }
        protected virtual void HurtAnim()
        {
            ResetVisualState();
            DOTweenAnimMgr.Play("Hurted", gameObject,0.5f);
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
            RegisterFSM(FSM);
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
        /// 回收到对象池前调用，清理移动和房间引用，避免下一次复用带入旧状态。
        /// </summary>
        public void PrepareForPoolRelease() {
            StopMove();
            ResetVisualState();
            OwnerFightRoom = null;
        }

        /// <summary>
        /// 清理敌人的运行时状态。状态机必须清空，否则复用后会残留上一轮注册的状态。
        /// </summary>
        private void ResetRuntimeState() {
            isDead = false;
            isInited = false;
            CurrentHp = MaxHp;
            FSM.Clear();
            StopMove();
            ResetVisualState();

            if(col != null) {
                col.enabled = true;
            }
        }

        private void CaptureDefaultVisualState() {
            if(sr == null) return;

            defaultSpriteColor = sr.color;
            hasDefaultSpriteColor = true;
        }

        private void ResetVisualState() {
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

        private void StopMove() {
            if(rb != null) {
                rb.velocity = Vector2.zero;
            }
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
