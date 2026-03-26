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
}
}
