using UnityEngine;
namespace QFramework.PG {
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
            UpdateUI();
        }
        
        /// <summary>
        /// 当前是否还有子弹可以射击
        /// </summary>
        public bool CanShoot => currentAmmo > 0;

        /// <summary>
        /// 发射一发，消耗一颗子弹
        /// </summary>
        public void Shoot()
        {
            currentAmmo--;
            UpdateUI();
        }

        /// <summary>
        /// 换弹，恢复至满弹
        /// </summary>
        public void Reload()
        {
            currentAmmo = maxAmmo;
            UpdateUI();
        }

        private void UpdateUI()
        {
            GameUI.Instance.UpdateBulletText(this);
        }

        /// <summary>
        /// 枪被使用时更新UI
        /// </summary>
        public void OnGunUsed()
        {
            UpdateUI();
        }
    }
}