using System.Collections.Generic;
using QFramework;
using UnityEngine;
namespace QFramework.PG {

[ViewControllerChild]
public abstract class Gun : ViewController {
    public List<AudioClip> shootSounds = new List<AudioClip>();
    public abstract PlayerBullet BulletPrefab { get; }
    public static AudioSource PlayerAudioSource { get; set;}
    public  AudioClip ReloadSound;

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
        GetBullet(dir);
    }
    
    public virtual void Reload()
    {
        
    }

    private void Start() {
        PlayerAudioSource  = WeaponGlobal.Instance.WeaponAudioSource;
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

    /// <summary>
    /// 尝试播放声音
    /// </summary>
    protected virtual void TryPlaySound(AudioClip sound,bool loop = false)
    {
        if(PlayerAudioSource.clip != null)
        {
        //停止当前播放的声音
        PlayerAudioSource.Stop();
        }

        //播放新声音
        PlayerAudioSource.clip = sound;
        PlayerAudioSource.loop = loop;
        PlayerAudioSource.Play();
    }
    
    /// <summary>
    /// 尝试播放声音(随机播放)
    /// </summary>
    protected virtual void TryPlaySound(bool loop = false)
    {
        if(PlayerAudioSource.clip != null)
        {
        //停止当前播放的声音
        PlayerAudioSource.Stop();
        }

        //播放新声音
        int n = shootSounds.Count;
        int index = Random.Range(0, n);
        PlayerAudioSource.clip = shootSounds[index];
        PlayerAudioSource.loop = loop;
        PlayerAudioSource.Play();
    }

    protected virtual PlayerBullet GetBullet(Vector2 dir)
    {
        var obj = Instantiate(BulletPrefab);
        obj.transform.position = BulletPrefab.transform.position;
        obj.dir = dir;
        obj.gameObject.SetActive(true);
        return obj;
    }
}
}
