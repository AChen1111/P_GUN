using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using QFramework.PG;

public class PlayerBullet : MonoBehaviour {
    public Vector2 dir;
    public float speed = 15f;
    public Rigidbody2D rb;
    public int damage;
    private bool hasHit;




    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        gameObject.layer = LayerMask.NameToLayer("PlayerBullet");
    }

    private void OnEnable() {
        StartCoroutine(AutoDestroyIfNotHit());
    }

    ///<summary>
    ///碰撞检测
    ///</summary>
    ///<param name="other">碰撞对象</param>
    private void OnCollisionEnter2D(Collision2D other) {
        //LogHit("Collision", other.gameObject);
        HandleHit(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        LogHit("Trigger", other.gameObject);
        HandleHit(other.gameObject);
    }


    private void Reset() {
        gameObject.AddComponent<CircleCollider2D>();
    }
    
    ///<summary>
    ///固定更新
    ///</summary>
    private void FixedUpdate() {
        rb.velocity = dir * speed;
    }

    private IEnumerator AutoDestroyIfNotHit() {
        yield return new WaitForSeconds(3f);
        if(!hasHit && gameObject != null) {
            Destroy(gameObject);
        }
    }

    private void HandleHit(GameObject target) {
        if(hasHit || target == null) return;

        if(target.CompareTag("Enemy")) {
            
            hasHit = true;
            
            var audioSource = target.GetComponent<AudioSource>();

            if(audioSource != null) {
                GlobalAudioPlay.Instance.PlayerAudioSourceByClip(WeaponGlobal.Instance.hitSoundOnBody);
                Debug.Log("audioSource is not null__");
            }
            else
            {
                Debug.Log("audioSource is null__");
            }
            
            DamageInfo damageInfo = new DamageInfo();
            damageInfo.Damage = damage;

            target.GetComponent<EnemyBase>()?.Hurt(damageInfo);
            Destroy(gameObject);
            return;
        }

        var wallLayer = LayerMask.NameToLayer("Wall");
        var isWall = target.CompareTag("Wall") || (wallLayer != -1 && target.layer == wallLayer);
        if(isWall) {
            hasHit = true;
            GlobalAudioPlay.Instance.PlayerAudioSourceByClip(WeaponGlobal.Instance.hitSoundOnWall);
            Destroy(gameObject);
        }
    }

    private void LogHit(string hitType, GameObject target) {
        if(target == null) return;

        var layerName = LayerMask.LayerToName(target.layer);
        var parentName = target.transform.parent != null ? target.transform.parent.name : "null";
        Debug.Log($"[PlayerBullet] {hitType} hit name={target.name}, tag={target.tag}, layer={target.layer}({layerName}), parent={parentName}");
    }
}