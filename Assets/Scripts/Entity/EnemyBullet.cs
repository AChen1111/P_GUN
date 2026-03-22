using UnityEngine;

public class EnemyBullet : MonoBehaviour {
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
        transform.Translate(Time.deltaTime * 10f * Vector2.left);
    }
}