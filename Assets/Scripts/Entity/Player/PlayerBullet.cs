using UnityEngine;

public class PlayerBullet : MonoBehaviour {
    public Vector2 dir;
    public float speed = 15f;
    public Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy")) {
            Destroy(other.gameObject);
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