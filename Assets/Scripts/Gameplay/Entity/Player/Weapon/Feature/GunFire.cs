using UnityEngine;
using QFramework;

public class GunFire
{
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
