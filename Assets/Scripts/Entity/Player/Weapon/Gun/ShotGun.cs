using UnityEngine;
using QFramework;

public class ShotGun : Gun
{
    public SpriteRenderer SR;
    public PlayerBullet PlayerBullet;
    public UnityEngine.AudioSource SelfAudioSource;

    public override PlayerBullet BulletPrefab => PlayerBullet;

    public void Shoot(Vector2 dir,bool playSound = true)
	{
		var obj = GetBullet(dir);
		
		if(playSound && shootSounds.Count > 0)
		{
			TryPlaySound(false);
            gunFireEffect.Show(BulletPrefab.Position2D(), dir);
		}
	}

	public override void ShootDown(Vector2 dir)
    {
		gunClip.CheckAmmo();
		if(!shootDuration.CanShoot || !gunClip.CanShoot) return;
		shootDuration.RecordShootTime();
		gunClip.Shoot();

        var angle = dir.ToAngle();
		var originPos = transform.parent.Position2D();
		var radius = (BulletPrefab.Position2D() - originPos).magnitude;

		for(int i = 0; i < 5; i++)
		{
			int j = i % 2 == 0 ? 1 : -1;
			var angle2 = angle + j*i*2;
			if(i == 0)
			{
				angle2 = angle;
			}
			var dir2 = angle2.AngleToDirection2D();
			Shoot(dir2.normalized,i==0);
		}
    }

    public override void Shooting(Vector2 dir)
    {
        ShootDown(dir);
    }
}
