using System.Collections;
using UnityEngine;
using QFramework.PG;

public class PlayerBullet : MonoBehaviour {
    public Vector2 dir;
    public float speed = 15f;
    public Rigidbody2D rb;
    public int damage;
    [SerializeField] private float lifeTime = 3f;
    private bool hasHit;
    private Coroutine autoRecycleCoroutine;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        gameObject.layer = LayerMask.NameToLayer("PlayerBullet");
    }

    /// <summary>
    /// 每次从对象池取出子弹时调用，重置上一轮使用留下的方向、伤害、命中状态和生命周期。
    /// </summary>
    public void Init(Vector2 shootDir, int bulletDamage) {
        dir = shootDir;
        damage = bulletDamage;
        hasHit = false;

        StopAutoRecycleCoroutine();
        autoRecycleCoroutine = StartCoroutine(AutoRecycleIfNotHit());

        if(rb == null) {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    ///<summary>
    ///碰撞检测
    ///</summary>
    ///<param name="other">碰撞对象</param>
    private void OnCollisionEnter2D(Collision2D other) {
        //LogHit("Collision", other.gameObject);
        HandleHit(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        LogHit("Trigger", other.gameObject);
        HandleHit(other.gameObject);
    }


    private void Reset() {
        gameObject.AddComponent<CircleCollider2D>();
    }
    
    ///<summary>
    ///固定更新
    ///</summary>
    private void FixedUpdate() {
        if(rb == null) return;
        rb.velocity = dir * speed;
    }

    private IEnumerator AutoRecycleIfNotHit() {
        yield return new WaitForSeconds(lifeTime);
        autoRecycleCoroutine = null;

        if(!hasHit && gameObject != null) {
            Recycle();
        }
    }

    private void HandleHit(GameObject target) {
        if(hasHit || target == null) return;

        if(target.CompareTag("Enemy")) {
            
            hasHit = true;
            
            var audioSource = target.GetComponent<AudioSource>();

            if(audioSource != null) {
                GlobalAudioPlay.Instance.PlayerAudioSourceByClip(WeaponGlobal.Instance.hitSoundOnBody);
                Debug.Log("audioSource is not null__");
            }
            else
            {
                Debug.Log("audioSource is null__");
            }
            
            DamageInfo damageInfo = new DamageInfo();
            damageInfo.Damage = damage;

            target.GetComponent<EnemyBase>()?.Hurt(damageInfo);
            Recycle();
            return;
        }

        var wallLayer = LayerMask.NameToLayer("Wall");
        var isWall = target.CompareTag("Wall") || (wallLayer != -1 && target.layer == wallLayer);
        if(isWall) {
            hasHit = true;
            GlobalAudioPlay.Instance.PlayerAudioSourceByClip(WeaponGlobal.Instance.hitSoundOnWall);
            Recycle();
        }
    }

    /// <summary>
    /// 子弹结束生命周期时归还对象池，而不是 Destroy，避免频繁创建和销毁。
    /// </summary>
    private void Recycle() {
        hasHit = true;
        StopAutoRecycleCoroutine();
        StopMove();
        PlayerBulletPool.Instance.Release(this);
    }

    /// <summary>
    /// 回收到池中前清掉速度，避免下次启用时继承上一颗子弹的物理状态。
    /// </summary>
    public void StopMove() {
        if(rb != null) {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnDisable() {
        StopAutoRecycleCoroutine();
        StopMove();
    }

    private void StopAutoRecycleCoroutine() {
        if(autoRecycleCoroutine == null) return;

        StopCoroutine(autoRecycleCoroutine);
        autoRecycleCoroutine = null;
    }

    private void LogHit(string hitType, GameObject target) {
        if(target == null) return;

        var layerName = LayerMask.LayerToName(target.layer);
        var parentName = target.transform.parent != null ? target.transform.parent.name : "null";
        Debug.Log($"[PlayerBullet] {hitType} hit name={target.name}, tag={target.tag}, layer={target.layer}({layerName}), parent={parentName}");
    }
}
