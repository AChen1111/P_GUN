using System.Collections.Generic;
using QFramework;
using UnityEngine;
namespace QFramework.PG {
public partial class Pistol : Gun {
    public override PlayerBullet BulletPrefab => PlayerBullet;
    public override AudioSource PlayerAudioSource => SelfAudioSource;
    [Header("属性")]
    [SerializeField] private int _maxAmmo = 10;//最大弹药量
    private GunClip GunClip ;//枪弹夹
    private GunFire GunFire = new GunFire();//枪火特效

    private void Awake()
    {
        GunClip = new GunClip(_maxAmmo);
    }

    public override void Shoot(Vector2 dir)
    {
        var obj = Instantiate(BulletPrefab);
        obj.transform.position = transform.position;

        //设置子弹方向
        obj.dir = dir;
        obj.gameObject.SetActive(true);

        //播放射击音效
        var randomIndex = Random.Range(0, shootSounds.Count);
        PlayerAudioSource?.PlayOneShot(shootSounds[randomIndex]);  
        GunFire.Show(BulletPrefab.Position2D(),dir);
    }

    public override void ShootDown(Vector2 dir) {
        if(GunClip.CanShoot) {
            GunClip.Shoot();
            Shoot(dir);
        }
    }

    public override void Reload() => GunClip.Reload(ReloadSound);
    public override void OnGunUsed() => GunClip.OnGunUsed();
}
}
