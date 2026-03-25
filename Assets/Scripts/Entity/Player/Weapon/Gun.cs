using System.Collections.Generic;
using QFramework;
using UnityEngine;
namespace QFramework.PG {
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
}
}
