using System.Collections.Generic;
using QFramework;
using UnityEngine;
namespace QFramework.PG {
public partial class Pistol : Gun {
    public override PlayerBullet BulletPrefab => PlayerBullet;

    public override BulletBag BulletBag => _bulletBag;
    [Header("属性")]
    [SerializeField] private int _maxAmmo = 10;//最大弹药量
    private GunClip GunClip ;//枪弹夹
    private BulletBag _bulletBag;
    private GunFire GunFire = new GunFire();//枪火特效

    private void Awake()
    {
        GunClip = new GunClip(_maxAmmo);
        _bulletBag = new BulletBag(MaxBulletBagNum);
    }

    public override void Shoot(Vector2 dir)
    {
        GetBullet(dir);
        //播放射击音效
        TryPlaySound(false);
        GunFire.Show(BulletPrefab.Position2D(),dir);
    }

    public override void ShootDown(Vector2 dir) {
        if(GunClip.CanShoot) {
            GunClip.Shoot();
            Shoot(dir);
        }
    }

    public override void Reload() => BulletBag.Reload(GunClip,ReloadSound);
    public override void OnGunUsed() 
    {
        base.OnGunUsed();
        GunClip.OnGunUsed();
    }
}
}
