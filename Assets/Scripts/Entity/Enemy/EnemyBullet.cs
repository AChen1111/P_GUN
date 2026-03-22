using UnityEngine;

public class EnemyBullet : MonoBehaviour {
    public Rigidbody2D rb;
    public Vector2 dir;
    public float speed = 10f;
    
    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<Player>().Hurt();
            Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("Grid")) {
            Destroy(gameObject);
        }
    }
    private void Reset() {
        gameObject.AddComponent<CircleCollider2D>();
    }
    private void Update() {
        rb.velocity = dir * speed;
    }
}