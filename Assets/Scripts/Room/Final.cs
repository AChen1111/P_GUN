using UnityEngine;

public class Final : MonoBehaviour {
    void Reset() {
        var collider = gameObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            GameUI.Instance.ShowWinPanel();
        }
    }
}