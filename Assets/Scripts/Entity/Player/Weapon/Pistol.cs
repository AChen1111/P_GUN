using System.Collections.Generic;
using QFramework;
using UnityEngine;
namespace QFramework.PG {
public partial class Pistol : ViewController {
    public List<AudioClip> shootSounds = new List<AudioClip>();
   

    public void ShootDown(Vector2 dir)
    {
        //实例化子弹
        var obj = Instantiate(PlayerBullet);
        obj.transform.position = transform.position;

        //设置子弹方向
        obj.dir = dir;
        obj.gameObject.SetActive(true);

        //播放射击音效
        var randomIndex = Random.Range(0, shootSounds.Count);
        SelfAudioSource.PlayOneShot(shootSounds[randomIndex]);
        
    }
    public void ShootUp(Vector2 dir)
    {

    }
    public void Shooting(Vector2 dir)
    {

    }
}
}
