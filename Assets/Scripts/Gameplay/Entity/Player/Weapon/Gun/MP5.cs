using UnityEngine;
using QFramework;

public class MP5 : Gun
{
    public PlayerBullet PlayerBullet;
    public UnityEngine.AudioSource SelfAudioSource;

	public override PlayerBullet BulletPrefab => PlayerBullet;

	public override void ShootDown(Vector2 dir)
	{
		if(gunClip.IsOutOfAmmo || !gunClip.CanShoot) return;
		TryPlaySound(true);
	}

    public override void Shooting(Vector2 dir)
    {
		gunClip.CheckAmmo();
		if(shootDuration.CanShoot && gunClip.CanShoot) {
			if(!PlayerAudioSource.isPlaying) {
				TryPlaySound(true);
			}
			shootDuration.RecordShootTime();
			gunClip.Shoot();
			var obj = GetBullet(dir);
			gunFireEffect.Show(FirePointPosition, dir);
		} 
		else if(gunClip.IsOutOfAmmo || gunClip.IsReloading) {
			PlayerAudioSource.Stop();
		}
    }

	public override void ShootUp(Vector2 dir)
	{
		PlayerAudioSource.Stop();
	}

	public override void OnGunUsed()
	{
		base.OnGunUsed();
		PlayerAudioSource.Stop();
	}
}
