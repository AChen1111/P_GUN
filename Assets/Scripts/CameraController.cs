using UnityEngine;

public class CameraController : MonoBehaviour {
    private void LateUpdate() {
        if(Global.player == null) return;
        transform.position = Global.player.transform.position + new Vector3(0, 0, -10);
    }
}