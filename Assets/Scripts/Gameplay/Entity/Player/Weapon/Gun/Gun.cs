using System.Collections.Generic;
using QFramework;
using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    [ViewControllerChild]
    public abstract class Gun : ViewController {
    /// <summary>
    /// 武器数据 ID。为空时默认使用脚本类名，例如 AK/Pistol。
    /// </summary>
    [SerializeField] private string weaponId;

    /// <summary>
    /// 武器数据库。
    /// </summary>
    [SerializeField] private WeaponDatabase weaponDatabase;

    public string WeaponId => string.IsNullOrWhiteSpace(weaponId) ? GetType().Name : weaponId.Trim();

    /// <summary>
    /// 射击音频列表
    /// </summary>
    public List<AudioClip> shootSounds = new List<AudioClip>();

    /// <summary>
    /// 子弹预制体
    /// </summary>
    public abstract PlayerBullet BulletPrefab { get; }

    /// <summary>
    /// 射击点,用于决定子弹出生位置和枪口特效位置.
    /// </summary>
    [SerializeField] private Transform firePoint;

    protected Vector2 FirePointPosition => firePoint != null ? firePoint.Position2D() : BulletPrefab.Position2D();

    protected Quaternion FirePointRotation => firePoint != null ? firePoint.rotation : BulletPrefab.transform.rotation;

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
    [Header("子弹速度")]
    [SerializeField] protected int bulletSpeed;

    protected ShootDuration shootDuration;
    protected GunClip gunClip;
    protected BulletBag bulletBag;
    protected GunFire gunFireEffect = new GunFire();

    public virtual BulletBag BulletBag => bulletBag;
    public GunClip GunClip => gunClip;

    /// <summary>
    /// 执行 RestoreAmmo 逻辑.
    /// </summary>
    public void RestoreAmmo(int clipAmmo, int clipMaxAmmo, int bagAmmo, int bagMaxAmmo)
    {
        if (gunClip != null)
        {
            gunClip.RestoreAmmo(clipAmmo, clipMaxAmmo);
        }

        if (bulletBag != null)
        {
            bulletBag.RestoreAmmo(bagAmmo, bagMaxAmmo);
        }
    }

    /// <summary>
    /// 初始化运行时依赖.
    /// </summary>
    protected virtual void Awake()
    {
        ApplyDataFromDatabase();

        if (clipSize != 0)
        {
            shootDuration = new ShootDuration(shootInterval);
            gunClip = new GunClip(clipSize);
            bulletBag = new BulletBag(MaxBulletBagNum);
        }

        void ApplyDataFromDatabase()
        {
            var database = weaponDatabase != null ? weaponDatabase : DataBaseManager.Instance?.Weapons;
            if (database != null && database.TryGetById(WeaponId, out var data))
            {
                data.ApplyTo(this);
            }
            else
            {
                Debug.LogWarning($"Weapon {WeaponId} not found in database.");
            }
        }
}

    /// <summary>
    /// 执行 ApplyData 逻辑.
    /// </summary>
    public void ApplyData(WeaponData data)
    {
        shootSounds.Clear();
        if (data.shootSounds != null)
        {
            foreach (var sound in data.shootSounds)
            {
                if (sound != null)
                {
                    shootSounds.Add(sound);
                }
            }
        }

        ReloadSound = data.reloadSound;
        MinDamage = data.minDamage;
        MaxDamage = data.MaxDamage;
        MaxBulletBagNum = data.maxBulletBagNum;
        clipSize = data.clipSize;
        shootInterval = data.ShootInterval;
        bulletSpeed = data.bulletSpeed;
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

    /// <summary>
    /// 执行启动后的初始化逻辑.
    /// </summary>
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
        if (PlayerAudioSource == null || sound == null) return;

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
        if (PlayerAudioSource == null || shootSounds == null || shootSounds.Count == 0) return;

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
        if (BulletPrefab == null)
        {
            Debug.LogError($"{GetType().Name}: 子弹预制体为空,无法发射。", this);
            return null;
        }

        var obj = PlayerBulletPool.Instance.Get(
            BulletPrefab,
            FirePointPosition,
            FirePointRotation,
            dir,
            Damage,
            bulletSpeed
        );
        return obj;
    }
    }
}
