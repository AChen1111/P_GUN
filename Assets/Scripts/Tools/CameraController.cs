using UnityEngine;

public class CameraController : MonoBehaviour {
    [Header("跟随设置")]
    public Vector3 offset = new Vector3(0f, 0f, -10f);
    [Min(0.01f)] public float smoothTime = 0.2f;

    private Vector3 velocity;

    private void LateUpdate() {
        if(Global.player == null) return;

        Vector3 targetPos = Global.player.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
