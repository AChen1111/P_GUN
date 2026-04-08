using System.Collections.Generic;
using UnityEngine;
using QFramework.PG;

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

    [Header("属性")]
    [SerializeField]private int hp = 3;//血量
    [SerializeField] private float _shootInterval = 0.2f;//射击间隔

    public enum EnemyState {
        Follow,
        Shoot,
    }
    public EnemyState state = EnemyState.Follow;
    //计时器
    public float timer = 0f;
    public float timerMax = 3f;


    private ShootDuration _shootDuration;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        _shootDuration = new ShootDuration(_shootInterval);
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
            if(_shootDuration.CanShoot) {
                _shootDuration.RecordShootTime();
                Shoot((Global.player.transform.position - transform.position).normalized);
            }
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

    public void Hurt(int damage)
    {
        hp -= damage;
        if(hp <= 0) {
            Destroy(gameObject);
            if(RoomPlayManager.Instance != null) {
                RoomPlayManager.Instance.DecreaseEnemyCount();
            }
        }
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
    
}