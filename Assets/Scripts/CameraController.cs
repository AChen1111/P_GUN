using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;
    private void LateUpdate() {
        transform.position = player.transform.position + new Vector3(0, 0, -10);
    }
}