using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [Header("角色贴图")]
    public SpriteRenderer sr;
    [Header("子弹预制体")]
    public GameObject bulletPrefab;
    [Header("角色移动速度")]
    public float moveSpeed = 5f;

    void Awake()
    {
        Global.player = this;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Reset() {
        gameObject.AddComponent<Rigidbody2D>();
        gameObject.AddComponent<CircleCollider2D>();
        gameObject.tag = "Player";
    }

    void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        
        rb.velocity = new Vector2(horizontal, vertical).normalized * moveSpeed;
        
        //翻转  
        if(horizontal < 0)
        {
            sr.flipX = true;
        }
        else if(horizontal > 0)
        {
            sr.flipX = false;
        }

        if(Input.GetMouseButtonDown(0)) {
            //获取鼠标位置
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var dir = (mousePosition - transform.position).normalized;
            //实例化子弹
            var bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position;
            //设置子弹方向
            bullet.GetComponent<PlayerBullet>().dir = dir;
            //激活子弹
            bullet.SetActive(true);
        }
    }

    public void Hurt() {
        Global.HP--;
        if(Global.HP <= 0) {
            Global.HP = 0;
            GameUI.Instance.ShowOverPanel();
            return;
        }
        Global.OnHPChange?.Invoke();
    }
}
