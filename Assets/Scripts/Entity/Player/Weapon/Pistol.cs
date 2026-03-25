using System.Collections.Generic;
using QFramework;
using UnityEngine;
namespace QFramework.PG {
public partial class Pistol : Gun {
    public override PlayerBullet BulletPrefab => PlayerBullet;
    public override AudioSource PlayerAudioSource => SelfAudioSource;
    public override void ShootDown(Vector2 dir) {
        //实例化子弹
        var obj = Instantiate(BulletPrefab);
        obj.transform.position = transform.position;

        //设置子弹方向
        obj.dir = dir;
        obj.gameObject.SetActive(true);

        //播放射击音效
        var randomIndex = Random.Range(0, shootSounds.Count);
        PlayerAudioSource?.PlayOneShot(shootSounds[randomIndex]);        
    }
}
}
