using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private void Reset() {
        gameObject.AddComponent<Rigidbody2D>();
        gameObject.AddComponent<CircleCollider2D>();
        gameObject.tag = "Enemy";
    }
    [Header("引用组件")]
    public GameObject bulletPrefab;
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    
    public List<AudioClip> shootSounds = new List<AudioClip>();
    private AudioSource audioSource;


    public enum EnemyState {
        Follow,
        Shoot,
    }
    public EnemyState state = EnemyState.Follow;
    //计时器
    public float timer = 0f;
    public float timerMax = 3f;
    bool isShoot = false;
    
    private static readonly WaitForSeconds s_ShootDelay = new WaitForSeconds(0.2f);
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        //巡逻状态
        if(state == EnemyState.Follow) {
            if(timer >= timerMax) {
                state = EnemyState.Shoot;
                timer = 0f;
                timerMax = 1f;
            } else {
                timer += Time.deltaTime;
            }
            Follow();
        } 
        //射击状态
        else if(state == EnemyState.Shoot) {
            if(timer >= timerMax) {
                state = EnemyState.Follow;
                timer = 0f;
                timerMax = 3f;
            } else {
                timer += Time.deltaTime;
            }
            if(!isShoot)
                StartCoroutine(ShootCoroutine());
        }
    }

    private void Follow() {
        if(Global.player == null) return;
        //方向向量处理
        var dir = (Global.player.transform.position - transform.position).normalized;
        //朝向处理
        if(dir.x < 0) {
            sr.flipX = true;
        }
        else if(dir.x > 0) {
            sr.flipX = false;
        }

        rb.velocity = dir * 5f;
    }


    private void Shoot(Vector2 dirToPlayer) {
        var obj = Instantiate(bulletPrefab, transform.position + Vector3.right, Quaternion.identity);
        obj.transform.position = transform.position;
        obj.GetComponent<EnemyBullet>().dir = dirToPlayer;
        obj.SetActive(true);

        //播放射击音效
        var randomIndex = Random.Range(0, shootSounds.Count);
        audioSource.PlayOneShot(shootSounds[randomIndex]);
    }
    
    IEnumerator ShootCoroutine() {
        isShoot = true;
        yield return s_ShootDelay;  
        Shoot((Global.player.transform.position - transform.position).normalized);
        isShoot = false;
    }
}