using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [Header("角色贴图")]
    public SpriteRenderer sr;
    [Header("角色移动速度")]
    public float moveSpeed = 5f;
    [Header("武器节点")]
    public Transform Weapon;
    public Pistol pistol;
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
        //获取鼠标位置
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //获取鼠标位置与角色位置的向量
        Vector2 dir = (mousePosition - transform.position).normalized;  

        //转成欧拉角
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //设置武器旋转
        Weapon.localRotation = Quaternion.Euler(0, 0, angle);
        Weapon.localScale = new Vector3(1, dir.x > 0 ? 1 : -1,1);

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
            pistol.ShootDown(dir);
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
