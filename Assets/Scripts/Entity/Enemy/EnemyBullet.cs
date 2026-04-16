using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using QFramework.PG;

public class EnemyBullet : MonoBehaviour {
    public Vector2 dir;
    public float speed = 10f;
    public Rigidbody2D rb;
    private bool hasHit;

    [Header("击中玩家音效")]
    public List<AudioClip> hitSoundsOnPlayer = new List<AudioClip>();
    private AudioClip hitSoundOnPlayer => hitSoundsOnPlayer[Random.Range(0, hitSoundsOnPlayer.Count)];
    [Header("击中墙壁音效")]
    public List<AudioClip> hitSoundsOnWall = new List<AudioClip>();
    private AudioClip hitSoundOnWall => hitSoundsOnWall[Random.Range(0, hitSoundsOnWall.Count)];

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        gameObject.layer = LayerMask.NameToLayer("EnemyBullet");
    }

    private void OnEnable() {
        StartCoroutine(AutoDestroyIfNotHit());
    }

    private void OnCollisionEnter2D(Collision2D other) {
        HandleHit(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        LogHit("Trigger", other.gameObject);
        HandleHit(other.gameObject);
    }

    private void Reset() {
        gameObject.AddComponent<CircleCollider2D>();
    }

    private void FixedUpdate() {
        rb.velocity = dir * speed;
    }

    private IEnumerator AutoDestroyIfNotHit() {
        yield return new WaitForSeconds(3f);
        if (!hasHit && gameObject != null) {
            Destroy(gameObject);
        }
    }

    private void HandleHit(GameObject target) {
        if (hasHit || target == null) return;

        if (target.CompareTag("Player")) {
            target.GetComponent<Player>()?.Hurt();
            hasHit = true;

            var audioSource = target.GetComponent<AudioSource>();
            if (audioSource != null) {
                audioSource.PlayOneShot(hitSoundOnPlayer);
            }

            Destroy(gameObject);
            return;
        }

        var wallLayer = LayerMask.NameToLayer("Wall");
        var isWall = target.CompareTag("Wall") || (wallLayer != -1 && target.layer == wallLayer);
        if (isWall) {
            hasHit = true;
            GlobalAudioPlay.Instance.PlayerAudioSourceByClip(hitSoundOnWall);
            Destroy(gameObject);
        }
    }

    private void LogHit(string hitType, GameObject target) {
        if (target == null) return;

        var layerName = LayerMask.LayerToName(target.layer);
        var parentName = target.transform.parent != null ? target.transform.parent.name : "null";
        Debug.Log($"[EnemyBullet] {hitType} hit name={target.name}, tag={target.tag}, layer={target.layer}({layerName}), parent={parentName}");
    }
}
