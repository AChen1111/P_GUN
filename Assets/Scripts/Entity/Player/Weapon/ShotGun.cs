using UnityEngine;
using QFramework;

namespace QFramework.PG
{
	public partial class ShotGun : Gun
	{
        public override PlayerBullet BulletPrefab => PlayerBullet;
        public override AudioSource PlayerAudioSource => SelfAudioSource;

        [Header("属性")]
        [SerializeField] private float _duration = 1f;
        [SerializeField] private int _maxAmmo = 8;
        private ShootDuration _shootDuration;
        private GunClip _gunClip;

        private void Awake()
        {
            _shootDuration = new ShootDuration(_duration);
            _gunClip = new GunClip(_maxAmmo);
        }

        public void Shoot(Vector2 pos,Vector2 dir,bool playSound = true)
		{
			var obj = Instantiate(BulletPrefab);
			obj.transform.position = pos;
			obj.dir = dir;
			obj.gameObject.SetActive(true);
			if(playSound && shootSounds.Count > 0)
			{
				var randomIndex = Random.Range(0, shootSounds.Count);
				PlayerAudioSource?.PlayOneShot(shootSounds[randomIndex]);
			}
		}

		public override void ShootDown(Vector2 dir)
        {
			if(!_shootDuration.CanShoot || !_gunClip.CanShoot) return;
			_shootDuration.RecordShootTime();
			_gunClip.Shoot();
            var angle = dir.ToAngle();
			var originPos = transform.parent.Position2D();
			var radius = (BulletPrefab.Position2D() - originPos).magnitude;
			//生成五个方向的子弹
			for(int i = 0; i < 5; i++)
			{
				int j = i % 2 == 0 ? 1 : -1;
				var angle2 = angle + j*i*2;
				if(i == 0)
				{
					angle2 = angle;
				}
				var dir2 = angle2.AngleToDirection2D();
				var pos = originPos + dir2 * radius;
				Shoot(pos,dir2.normalized,i==0);
			}
        }
        public override void Shooting(Vector2 dir)
        {
            ShootDown(dir);
        }
        public override void Reload() => _gunClip.Reload();
	}
}
