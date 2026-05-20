using UnityEngine;
using QFramework;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    public class GunFire
    {
        /// <summary>
        /// 执行 Show 逻辑.
        /// </summary>
        public void Show(Vector2 pos,Vector2 dir)
        {
            WeaponGlobal.Instance.GunFire.Position2D(pos);
            WeaponGlobal.Instance.GunFire.transform.right = dir;
            WeaponGlobal.Instance.GunFire.Show();

            //3帧后隐藏
            ActionKit.DelayFrame(3,
                () => {
                    WeaponGlobal.Instance.GunFire.Hide();
                }
            ).StartCurrentScene();
        }
    }
}
