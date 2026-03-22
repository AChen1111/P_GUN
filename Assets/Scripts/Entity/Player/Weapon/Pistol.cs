using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour {
    public PlayerBullet bullet;
    public List<AudioClip> shootSounds = new List<AudioClip>();
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void ShootDown(Vector2 dir)
    {
        //实例化子弹
        var obj = Instantiate(bullet);
        obj.transform.position = transform.position;

        //设置子弹方向
        obj.dir = dir;
        obj.gameObject.SetActive(true);

        //播放射击音效
        var randomIndex = Random.Range(0, shootSounds.Count);
        audioSource.PlayOneShot(shootSounds[randomIndex]);
        
    }
    public void ShootUp(Vector2 dir)
    {

    }
    public void Shooting(Vector2 dir)
    {

    }
}