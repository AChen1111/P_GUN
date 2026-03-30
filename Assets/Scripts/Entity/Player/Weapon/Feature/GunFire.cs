using UnityEngine;

namespace QFramework.PG
{
    public class GunFire
    {
        public void Show(Vector2 pos,Vector2 dir)
        {
            Player.Instance.GunFire.Position2D(pos);
            Player.Instance.GunFire.transform.right = dir;
            Player.Instance.GunFire.Show();
            
            //3帧后隐藏
            ActionKit.DelayFrame(3,
                () => {
                    Player.Instance.GunFire.Hide();
                }
            ).StartCurrentScene();
        }
    }
}