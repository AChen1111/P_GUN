using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework.PG;
using QFramework;
namespace QFramework.PG {
    public partial class Player : ViewController
    {
        private Rigidbody2D rb;
        [Header("角色贴图")]
        public SpriteRenderer sr;
        [Header("角色移动速度")]
        public float moveSpeed = 5f;
        [Header("武器节点")]
        public Transform Weapon;
        [Header("武器列表")]
        public List<Gun> guns = new List<Gun>();
        
        public Gun gun;
        public int currentGunIndex = 0;

        void Awake()
        {
            Global.player = this;
            rb = GetComponent<Rigidbody2D>();
            
            gun.Hide();
            gun = guns[0];
            gun.Show();
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

            //按下
            if(Input.GetMouseButtonDown(0)) {
                gun.ShootDown(dir);
            }
            
            //抬起
            if(Input.GetMouseButtonUp(0)) {
                gun.ShootUp(dir);
            }

            //按住
            if(Input.GetMouseButton(0)) {
                gun.Shooting(dir);
            }

            //换子弹
            if(Input.GetKeyDown(KeyCode.R)) {
                gun.Reload();
            }

            //前一把枪
            if(Input.GetKeyDown(KeyCode.Q)) {
                gun.Hide();
                currentGunIndex = (currentGunIndex - 1 + guns.Count) % guns.Count;
                gun = guns[currentGunIndex];
                gun.Show();
                gun.OnGunUsed();
            }

            //后一把枪
            if(Input.GetKeyDown(KeyCode.E)) {
                gun.Hide();
                currentGunIndex = (currentGunIndex + 1) % guns.Count;
                gun = guns[currentGunIndex];
                gun.Show();
                gun.OnGunUsed();
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
}