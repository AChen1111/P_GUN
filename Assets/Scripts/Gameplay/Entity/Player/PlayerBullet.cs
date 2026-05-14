using System.Collections;
using UnityEngine;
public class PlayerBullet : MonoBehaviour, global::IPoolable {
    public Vector2 dir;
    public float speed = 15f;
    public Rigidbody2D rb;
    public int damage;
    [SerializeField] private float lifeTime = 3f;
    private bool hasHit = false;
    private AudioPlay _audioPlay;
    private Coroutine autoRecycleCoroutine;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        _audioPlay = GetComponent<AudioPlay>();
        gameObject.layer = LayerMask.NameToLayer("PlayerBullet");
    }

    /// <summary>
    /// 每次从对象池取出子弹时调用，重置上一轮使用留下的方向、伤害、命中状态和生命周期
    /// </summary>
    public void Init(Vector2 shootDir, int bulletDamage,int bulletSpeed) {
        dir = shootDir;
        damage = bulletDamage;
        speed = bulletSpeed;
    }

    /// <summary>
    /// 从对象池取出子弹时调用
    /// </summary>
    public void OnSpawnFromPool() {
        hasHit = false;
        autoRecycleCoroutine = StartCoroutine(AutoRecycleIfNotHit());
    }

    /// <summary>
    /// 回收子弹时调用
    /// </summary>
    public void OnRecycleToPool() {
        hasHit = true;
        StopAutoRecycleCoroutine();
        StopMove();
    }

    ///<summary>
    ///碰撞检测
    ///</summary>
    ///<param name="other">碰撞对象</param>
    private void OnCollisionEnter2D(Collision2D other) {
        HandleHit(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        HandleHit(other.gameObject);
    }


    
    ///<summary>
    ///固定更新
    ///</summary>
    private void FixedUpdate() {
        rb.velocity = dir * speed;
    }

    /// <summary>
    /// 自动回收协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator AutoRecycleIfNotHit() {
        yield return new WaitForSeconds(lifeTime);
        autoRecycleCoroutine = null;

        if(!hasHit) {
            hasHit = true;
            PlayerBulletPool.Instance.Release(this);
        }
    }


    /// <summary>
    /// 处理碰撞
    /// </summary>
    /// <param name="target"></param>
    private void HandleHit(GameObject target) {
        if(hasHit) return;

        if(target.CompareTag("Enemy")) {
            hasHit = true;

            //todo:改为从自身播放音效
            _audioPlay.Play();
            DamageInfo damageInfo = new DamageInfo(damage, dir);

            target.GetComponent<EnemyBase>()?.Hurt(damageInfo);
            PlayerBulletPool.Instance.Release(this);
            return;
        }

        var wallLayer = LayerMask.NameToLayer("Wall");
        var isWall = target.CompareTag("Wall") || target.layer == wallLayer;
        if(isWall) {
            hasHit = true;
            //todo:改为由墙体播放音效
            target.GetComponent<AudioPlay>().Play();
            PlayerBulletPool.Instance.Release(this);
        }
    }

    /// <summary>
    /// 回收到池中前清掉速度，避免下次启用时继承上一颗子弹的物理状态。
    /// </summary>
    private void StopMove() {
        rb.velocity = Vector2.zero;
    }

    private void StopAutoRecycleCoroutine() {
        if(autoRecycleCoroutine == null) return;

        StopCoroutine(autoRecycleCoroutine);
        autoRecycleCoroutine = null;
    }

}
