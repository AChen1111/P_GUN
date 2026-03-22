using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    void Awake()
    {
        
    }

    private void Reset() {
        gameObject.AddComponent<Rigidbody2D>();
        gameObject.AddComponent<CircleCollider2D>();
        gameObject.tag = "Player";
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        transform.Translate(Time.deltaTime * 5f * new Vector2(horizontal, vertical));

        if(Input.GetMouseButtonDown(0)) {
            var bullet = Instantiate(bulletPrefab, transform.position + Vector3.right, Quaternion.identity);
            bullet.SetActive(true);
        }
    }


}
