using UnityEngine;
using QFramework;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    /// <summary>
    /// 枪弹夹特性：管理弹药数量并同步 UI
    /// </summary>
    public class GunClip{
        public int maxAmmo = 30;    // 弹夹容量
        public int currentAmmo = 0; // 当前剩余弹药
        /// <summary>
        /// 以满弹状态初始化弹夹
        /// </summary>
        public GunClip(int maxAmmo)
        {
            this.maxAmmo = maxAmmo;
            currentAmmo = maxAmmo;
        }

        /// <summary>
        /// 是否正在换弹
        /// </summary>
        private bool isReloading = false;

        /// <summary>
        /// 当前是否还有子弹可以射击
        /// </summary>
        public bool IsInfinite => maxAmmo == -1;

        public bool CanShoot => (IsInfinite || currentAmmo > 0) && !isReloading;

        /// <summary>
        /// 弹药是否真正耗尽（不含换弹状态，用于判断是否停止声音）
        /// </summary>
        public bool IsOutOfAmmo => !IsInfinite && currentAmmo <= 0;

        /// <summary>
        /// 是否正处于换弹过程中
        /// </summary>
        public bool IsReloading => isReloading;

        /// <summary>
        /// 是否满弹
        /// </summary>
        public bool IsFull => currentAmmo == maxAmmo;


        /// <summary>
        /// 执行 CheckAmmo 逻辑.
        /// </summary>
        public void CheckAmmo()
        {
            if(IsOutOfAmmo)
            {
                EventCenter.Trigger(GameEvent.PlayerHeadMessageRequested, new PlayerHeadMessageEvent("没有备弹", 2f));
                GlobalAudioPlay.Instance.PlayerAudioSourceByPath("EmptyBulletSound");
                return;
            }
        }
        /// <summary>
        /// 发射一发，消耗一颗子弹
        /// </summary>
        public void Shoot()
        {
            if (IsInfinite) return;
            currentAmmo--;
            UpdateUI();
        }

        /// <summary>
        /// 换弹，恢复至满弹
        /// </summary>
        public void Reload(AudioClip reloadSound = null)
        {
            if(isReloading) return;

            isReloading = true;
            //设置回调,换弹完成后恢复满弹
            ActionKit.Sequence()
            .PlaySound(reloadSound)
            .Callback(() => {
                currentAmmo = maxAmmo;
                isReloading = false;
                UpdateUI();
            })
            .StartCurrentScene();
        }

        /// <summary>
        /// 执行 UpdateUI 逻辑.
        /// </summary>
        private void UpdateUI()
        {
            EventCenter.Trigger(GameEvent.BulletClipChanged, this);
        }

        /// <summary>
        /// 枪被使用时更新UI
        /// </summary>
        public void OnGunUsed()
        {
            UpdateUI();
        }

        /// <summary>
        /// 执行 RestoreAmmo 逻辑.
        /// </summary>
        public void RestoreAmmo(int currentAmmo, int maxAmmo)
        {
            // 读档恢复弹夹时直接覆盖数量, 并清理换弹状态.
            this.maxAmmo = maxAmmo;
            this.currentAmmo = IsInfinite ? -1 : Mathf.Clamp(currentAmmo, 0, Mathf.Max(0, maxAmmo));
            isReloading = false;
            UpdateUI();
        }
    }
}
