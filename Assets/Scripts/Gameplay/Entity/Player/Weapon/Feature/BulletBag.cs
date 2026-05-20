using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
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

        /// <summary>
        /// 执行 RestoreAmmo 逻辑.
        /// </summary>
        public void RestoreAmmo(int currentBullet, int maxBullet)
        {
            // 读档恢复备弹时直接覆盖数量, 无限弹药保持 -1.
            this.maxBullet = maxBullet;
            this.currentBullet = maxBullet < 0 ? -1 : Mathf.Clamp(currentBullet, 0, Mathf.Max(0, maxBullet));
            EventCenter.Trigger(GameEvent.BulletBagChanged, this);
        }

    }
}
