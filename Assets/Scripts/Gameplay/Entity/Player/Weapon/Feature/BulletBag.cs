using UnityEngine;

/// <summary>
/// 子弹袋特性：管理子弹数量并同步 UI
/// </summary>
public class BulletBag
{
    public int maxBullet;
    public int currentBullet;


    public BulletBag(int maxBullet)
    {
        this.maxBullet = maxBullet;
        currentBullet = maxBullet;
    }


    /// <summary>
    /// 是否还有子弹
    /// </summary>
    public bool HasBullet => currentBullet > 0;

    public Player owner { get; set; }

    /// <summary>
    /// 换弹
    /// </summary>
    ///<param name="gunClip">枪弹夹</param>
    ///<param name="reloadSound">换弹声音</param>
    public void Reload(GunClip gunClip,AudioClip reloadSound = null)
    {
        ///如果枪弹夹满弹或没有子弹，则不换弹
        if(gunClip.IsFull || !HasBullet) return;
        int needBullet = gunClip.maxAmmo - gunClip.currentAmmo;
        if(needBullet > currentBullet)
        {
            gunClip.currentAmmo += currentBullet;
            currentBullet = 0;
        }
        else
        {
            gunClip.currentAmmo += needBullet;
            currentBullet -= needBullet;
        }

        EventCenter.Trigger(GameEvent.BulletBagChanged, this);
        gunClip.Reload(reloadSound);
    }

}
