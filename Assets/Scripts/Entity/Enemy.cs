using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private void Reset() {
        gameObject.AddComponent<Rigidbody2D>();
        gameObject.AddComponent<CircleCollider2D>();
        gameObject.tag = "Enemy";
    }

    public GameObject bulletPrefab;
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    
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
    }
    private void Update() {
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
    private void Shoot() {
        var bullet = Instantiate(bulletPrefab, transform.position + Vector3.right, Quaternion.identity);
        bullet.SetActive(true);
    }
    IEnumerator ShootCoroutine() {
        isShoot = true;
        yield return s_ShootDelay;  
        Shoot();
        isShoot = false;
    }
}