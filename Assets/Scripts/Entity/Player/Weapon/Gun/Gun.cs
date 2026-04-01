using System.Collections.Generic;
using QFramework;
using UnityEngine;
namespace QFramework.PG {

[ViewControllerChild]
public abstract class Gun : ViewController {
    /// <summary>
    /// 射击音频列表
    /// </summary>
    public List<AudioClip> shootSounds = new List<AudioClip>();

    /// <summary>
    /// 子弹预制体
    /// </summary>
    public abstract PlayerBullet BulletPrefab { get; }

    /// <summary>
    /// 音频源,所有武器公用一个
    /// </summary>
    public static AudioSource PlayerAudioSource { get; set;}

    /// <summary>
    /// 换子弹音频
    /// </summary>
    public AudioClip ReloadSound;

    /// <summary>
    /// 伤害信息
    /// </summary>
    [Header("伤害设置")]
    public int MinDamage;
    public int MaxDamage;
    public int Damage => Random.Range(MinDamage, MaxDamage);


    /// <summary>
    /// 鼠标按下
    /// </summary>
    public virtual void ShootDown(Vector2 dir) {    
        
    }

    /// <summary>
    /// 鼠标抬起
    /// </summary>
    public virtual void ShootUp(Vector2 dir)
    {

    }

    /// <summary>
    /// 鼠标按住
    /// </summary>
    public virtual void Shooting(Vector2 dir)
    {

    }

    /// <summary>
    /// 单次射击
    /// </summary>
    public virtual void Shoot(Vector2 dir)
    {
        GetBullet(dir);
    }
    
    /// <summary>
    /// 换子弹
    /// </summary>
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

    /// <summary>
    /// 获取子弹
    /// </summary>
    protected virtual PlayerBullet GetBullet(Vector2 dir)
    {
        var obj = Instantiate(BulletPrefab);
        obj.transform.position = BulletPrefab.transform.position;
        obj.dir = dir;
        obj.damage = Damage;
        //Debug.Log("damage: " + Damage);
        obj.gameObject.SetActive(true);
        return obj;
    }
}
}
