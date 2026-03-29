using System.Collections.Generic;
using QFramework;
using UnityEngine;
namespace QFramework.PG {

[ViewControllerChild]
public abstract class Gun : ViewController {
    public List<AudioClip> shootSounds = new List<AudioClip>();
    public abstract PlayerBullet BulletPrefab { get; }
    public abstract AudioSource PlayerAudioSource { get; }

    public virtual void ShootDown(Vector2 dir) {    
        
    }
    public virtual void ShootUp(Vector2 dir)
    {

    }
    public virtual void Shooting(Vector2 dir)
    {

    }
    public virtual void Shoot(Vector2 dir)
    {
        var obj = Instantiate(BulletPrefab);
        obj.transform.position = transform.position;
        obj.dir = dir;
        obj.gameObject.SetActive(true);
    }
    
    public virtual void Reload()
    {
        
    }
    
    /// <summary>
    /// 显示枪
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏枪
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 枪被使用时调用
    /// </summary>
    public virtual void OnGunUsed()
    {

    }
}
}
