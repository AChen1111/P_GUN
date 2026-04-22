using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using QFramework.PG;

public class EnemyBullet : MonoBehaviour {
    public Vector2 dir;
    public float speed = 10f;
    public Rigidbody2D rb;
    [SerializeField] private float lifeTime = 3f;
    private bool hasHit;
    private Coroutine autoRecycleCoroutine;

    [Header("击中玩家音效")]
    public List<AudioClip> hitSoundsOnPlayer = new List<AudioClip>();
    private AudioClip hitSoundOnPlayer => hitSoundsOnPlayer[Random.Range(0, hitSoundsOnPlayer.Count)];
    [Header("击中墙壁音效")]
    public List<AudioClip> hitSoundsOnWall = new List<AudioClip>();
    private AudioClip hitSoundOnWall => hitSoundsOnWall[Random.Range(0, hitSoundsOnWall.Count)];

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        gameObject.layer = LayerMask.NameToLayer("EnemyBullet");
    }

    /// <summary>
    /// 每次从对象池取出敌人子弹时调用，重置方向、命中状态和生命周期。
    /// </summary>
    public void Init(Vector2 shootDir) {
        dir = shootDir;
        hasHit = false;

        StopAutoRecycleCoroutine();
        autoRecycleCoroutine = StartCoroutine(AutoRecycleIfNotHit());

        if (rb == null) {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        HandleHit(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        LogHit("Trigger", other.gameObject);
        HandleHit(other.gameObject);
    }

    private void Reset() {
        gameObject.AddComponent<CircleCollider2D>();
    }

    private void FixedUpdate() {
        if (rb == null) return;
        rb.velocity = dir * speed;
    }

    private IEnumerator AutoRecycleIfNotHit() {
        yield return new WaitForSeconds(lifeTime);
        autoRecycleCoroutine = null;

        if (!hasHit && gameObject != null) {
            Recycle();
        }
    }

    private void HandleHit(GameObject target) {
        if (hasHit || target == null) return;

        if (target.CompareTag("Player")) {
            target.GetComponent<Player>()?.Hurt(new DamageInfo(1, dir));
            hasHit = true;

            var audioSource = target.GetComponent<AudioSource>();
            if (audioSource != null && hitSoundsOnPlayer.Count > 0) {
                audioSource.PlayOneShot(hitSoundOnPlayer);
            }

            Recycle();
            return;
        }

        var wallLayer = LayerMask.NameToLayer("Wall");
        var isWall = target.CompareTag("Wall") || (wallLayer != -1 && target.layer == wallLayer);
        if (isWall) {
            hasHit = true;
            if (hitSoundsOnWall.Count > 0) {
                GlobalAudioPlay.Instance.PlayerAudioSourceByClip(hitSoundOnWall);
            }
            Recycle();
        }
    }

    /// <summary>
    /// 敌人子弹结束生命周期时归还对象池，而不是 Destroy。
    /// </summary>
    private void Recycle() {
        hasHit = true;
        StopAutoRecycleCoroutine();
        StopMove();
        EnemyBulletPool.Instance.Release(this);
    }

    /// <summary>
    /// 清掉刚体速度，避免回收后再次启用时继承旧速度。
    /// </summary>
    public void StopMove() {
        if (rb != null) {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnDisable() {
        StopAutoRecycleCoroutine();
        StopMove();
    }

    private void StopAutoRecycleCoroutine() {
        if (autoRecycleCoroutine == null) return;

        StopCoroutine(autoRecycleCoroutine);
        autoRecycleCoroutine = null;
    }

    private void LogHit(string hitType, GameObject target) {
        if (target == null) return;

        var layerName = LayerMask.LayerToName(target.layer);
        var parentName = target.transform.parent != null ? target.transform.parent.name : "null";
        Debug.Log($"[EnemyBullet] {hitType} hit name={target.name}, tag={target.tag}, layer={target.layer}({layerName}), parent={parentName}");
    }
}
