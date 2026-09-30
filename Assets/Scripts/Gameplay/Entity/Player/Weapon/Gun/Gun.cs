using Game.Pooling;
using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using Game.Core;

namespace Game.Gameplay
{
    [ViewControllerChild]
    public abstract class Gun : ViewController {
    /// <summary>
    /// 武器数据 ID。为空时默认使用脚本类名，例如 AK/Pistol。
    /// </summary>
    [SerializeField] private string weaponId;

    public string WeaponId => string.IsNullOrWhiteSpace(weaponId) ? GetType().Name : weaponId.Trim();

    /// <summary>
    /// 射击音频列表
    /// </summary>
    [NonSerialized] public List<AudioClip> shootSounds = new List<AudioClip>();

    /// <summary>
    /// 换弹音效地址, 来自 WeaponData.lua.
    /// </summary>
    private string reloadSoundKey;

    /// <summary>
    /// 射击音效地址列表, 来自 WeaponData.lua.
    /// </summary>
    private List<string> shootSoundKeys = new List<string>();

    /// <summary>
    /// 子弹预制体
    /// </summary>
    public abstract PlayerBullet BulletPrefab { get; }

    /// <summary>
    /// 射击点,用于决定子弹出生位置和枪口特效位置.
    /// </summary>
    [SerializeField] private Transform firePoint;

    // 枪口必须由武器预制体明确绑定, 避免把子弹预制体的原点当成世界坐标.
    protected Vector2 FirePointPosition => RequiredFirePoint.Position2D();

    protected Quaternion FirePointRotation => RequiredFirePoint.rotation;

    private Transform RequiredFirePoint => firePoint != null
        ? firePoint
        : throw new InvalidOperationException($"{name} 未绑定枪口 firePoint.");

    /// <summary>
    /// 音频源,所有武器公用一个
    /// </summary>
    public static AudioSource PlayerAudioSource { get; set;}

    /// <summary>
    /// 换子弹音频
    /// </summary>
    [NonSerialized] public AudioClip ReloadSound;

    /// <summary>
    /// 伤害信息
    /// </summary>
    [Header("伤害设置")]
    public int MinDamage;
    public int MaxDamage;
    public int Damage => UnityEngine.Random.Range(MinDamage, MaxDamage + 1);


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

    /// <summary>
    /// 本枪的 Lua 行为组件, 开火规则在对应模块里.
    /// </summary>
    private LuaBehaviourHost gunLua;

    /// <summary>
    /// 基类开火缺少 Lua 组件时只警告一次, 避免逐帧刷屏.
    /// </summary>
    private bool reportedMissingGunLua;

    public virtual BulletBag BulletBag => bulletBag;
    public GunClip GunClip => gunClip;
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
        gunLua = GetComponent<LuaBehaviourHost>();
        ApplyDataFromLuaTable();
        LoadSounds();

        if (clipSize != 0)
        {
            shootDuration = new ShootDuration(shootInterval);
            gunClip = new GunClip(clipSize);
            bulletBag = new BulletBag(MaxBulletBagNum);
        }

        void ApplyDataFromLuaTable()
        {
            // 数值来自 WeaponData.lua, 不再读 WeaponDatabase.
            var config = LuaDataRuntime.GetWeaponConfig(WeaponId);
            MinDamage = config.MinDamage;
            MaxDamage = config.MaxDamage;
            MaxBulletBagNum = config.MaxBulletBagNum;
            clipSize = config.ClipSize;
            shootInterval = config.ShootInterval;
            bulletSpeed = config.BulletSpeed;
            reloadSoundKey = config.ReloadSoundKey;
            shootSoundKeys = config.ShootSoundKeys;
        }
}

    /// <summary>
    /// 通过短名 key 从阶段缓存读取射击与换弹音效.
    /// </summary>
    private void LoadSounds()
    {
        // 场景激活前完成资源预加载, 枪械初始化只使用同步缓存.
        shootSounds = AddressableAssetAccess.List<AudioClip>(shootSoundKeys);
        ReloadSound = AddressableAssetAccess.Get<AudioClip>(reloadSoundKey);
    }

    /// <summary>
    /// 鼠标按下, 转发给枪械 Lua 模块.
    /// </summary>
    public virtual void ShootDown(Vector2 dir) {
        ForwardToLua("ShootDown", dir);
    }

    /// <summary>
    /// 鼠标抬起, 转发给枪械 Lua 模块.
    /// </summary>
    public virtual void ShootUp(Vector2 dir)
    {
        ForwardToLua("ShootUp", dir);
    }

    /// <summary>
    /// 鼠标按住, 转发给枪械 Lua 模块, 并传入本帧时间.
    /// </summary>
    public virtual void Shooting(Vector2 dir)
    {
        ForwardToLua("Shooting", dir, Time.deltaTime);
    }

    /// <summary>
    /// 把开火转发给 Lua 模块.
    /// 未换绑的旧预制体走子类覆盖, 基类只警告一次并保持空操作, 换绑完成后不再走到这里.
    /// </summary>
    private void ForwardToLua(string functionName, Vector2 dir)
    {
        if (!EnsureGunLua()) return;

        gunLua.CallLuaFunction(functionName, dir);
    }

    private void ForwardToLua(string functionName, Vector2 dir, float deltaTime)
    {
        if (!EnsureGunLua()) return;

        gunLua.CallLuaFunction(functionName, dir, deltaTime);
    }

    /// <summary>
    /// 校验 Lua 组件存在, 缺失时只警告一次并保持空操作.
    /// </summary>
    private bool EnsureGunLua()
    {
        if (gunLua != null)
        {
            return true;
        }

        if (!reportedMissingGunLua)
        {
            Debug.LogWarning($"{GetType().Name} 未挂 LuaComponet, 基类开火保持空操作; 请按待办清单换绑枪械预制体.", this);
            reportedMissingGunLua = true;
        }

        return false;
    }

    /// <summary>
    /// 尝试按射击间隔和弹药状态消耗一发, 供枪械 Lua 模块调用.
    /// </summary>
    public bool TryConsumeShot()
    {
        if (gunClip == null || shootDuration == null)
        {
            throw new InvalidOperationException($"{GetType().Name} 弹药组件未初始化.");
        }

        gunClip.CheckAmmo();
        if (!shootDuration.CanShoot || !gunClip.CanShoot)
        {
            return false;
        }

        shootDuration.RecordShootTime();
        gunClip.Shoot();
        return true;
    }

    /// <summary>
    /// 按方向发射一发子弹, 生成走 WeaponManager.
    /// </summary>
    public PlayerBullet FireBullet(Vector2 dir)
    {
        return GetBullet(dir);
    }

    /// <summary>
    /// 播放射击音效, 供枪械 Lua 模块调用.
    /// </summary>
    public void PlayFireSound(bool loop = false)
    {
        TryPlaySound(loop);
    }

    /// <summary>
    /// 播放指定音效, 例如抬起时的结束音.
    /// </summary>
    public void PlaySoundClip(AudioClip clip, bool loop = false)
    {
        TryPlaySound(clip, loop);
    }

    /// <summary>
    /// 停止共用音源.
    /// </summary>
    public void StopFireSound()
    {
        PlayerAudioSource?.Stop();
    }

    /// <summary>
    /// 播放枪口火光, 供枪械 Lua 模块调用.
    /// </summary>
    public void PlayFireVfx(Vector2 dir)
    {
        PlayGunFire(dir);
    }

    /// <summary>
    /// 获取开火点位置, 供枪械 Lua 模块计算弹道.
    /// </summary>
    public Vector2 GetFirePointPosition()
    {
        return FirePointPosition;
    }

    /// <summary>
    /// 获取射击间隔, 供枪械 Lua 模块使用.
    /// </summary>
    public float GetShootInterval()
    {
        return shootInterval;
    }

    /// <summary>
    /// 单次射击
    /// </summary>
    public virtual void Shoot(Vector2 dir)
    {
        if(gunClip.IsOutOfAmmo)
        {
            EventCenter.Trigger(CoreEvents.PlayerHeadMessageRequested, new PlayerHeadMessageEvent("没有子弹", 2f));
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
            EventCenter.Trigger(CoreEvents.PlayerHeadMessageRequested, new PlayerHeadMessageEvent("没有子弹", 2f));
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
        if (BulletBag != null) EventCenter.Trigger(GameplayEvents.BulletBagChanged, BulletBag);
        gunClip?.OnGunUsed();
    }

protected void PlayGunFire(Vector2 direction)
        {
            // 枪口火光经 WeaponManager 中转, Gun 不直接接触场景表现单例.
            WeaponManager.Instance.PlayGunFire(FirePointPosition, direction);
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
        int index = UnityEngine.Random.Range(0, n);
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

            var obj = WeaponManager.Instance.SpawnPlayerBullet(
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
