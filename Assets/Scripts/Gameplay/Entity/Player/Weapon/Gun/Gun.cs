using System.Collections.Generic;
using QFramework;
using UnityEngine;

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
public int Damage => Random.Range(MinDamage, MaxDamage + 1);


[Header("备弹设置")]
public int MaxBulletBagNum;
[Header("弹夹容量")]
[SerializeField] protected int clipSize;
[Header("射击间隔")]
[SerializeField] protected float shootInterval;

protected ShootDuration shootDuration;
protected GunClip gunClip;
protected BulletBag bulletBag;
protected GunFire gunFireEffect = new GunFire();

public virtual BulletBag BulletBag => bulletBag;

protected virtual void Awake()
{
    if (clipSize != 0)
    {
        shootDuration = new ShootDuration(shootInterval);
        gunClip = new GunClip(clipSize);
        bulletBag = new BulletBag(MaxBulletBagNum);
    }
}

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
    if(gunClip.IsOutOfAmmo)
    {
        EventCenter.Trigger(GameEvent.PlayerHeadMessageRequested, new PlayerHeadMessageEvent("没有子弹", 2f));
        return;
    }
    GetBullet(dir);
}

/// <summary>
/// 换子弹（默认实现含"没有子弹"提示）
/// </summary>
public virtual void Reload()
{
    if (bulletBag == null || gunClip == null) return;
    if (gunClip.IsOutOfAmmo && !bulletBag.HasBullet)
    {
        EventCenter.Trigger(GameEvent.PlayerHeadMessageRequested, new PlayerHeadMessageEvent("没有子弹", 2f));
        return;
    }
    bulletBag.Reload(gunClip, ReloadSound);
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
    if (BulletBag != null) EventCenter.Trigger(GameEvent.BulletBagChanged, BulletBag);
    gunClip?.OnGunUsed();
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
    //Debug.Log("minMaxDamage: " + MinDamage + " " + MaxDamage);
    //Debug.Log("damage: " + Damage);

    
    var obj = PlayerBulletPool.Instance.Get(
        BulletPrefab,
        BulletPrefab.transform.position,
        BulletPrefab.transform.rotation,
        dir,
        Damage
    );
    //Debug.Log("damage: " + Damage);
    return obj;
}
}
