using UnityEngine;
using QFramework;
using System.Collections.Generic;

public class WeaponGlobal : ViewController
{
    public SpriteRenderer GunFire;

	public static WeaponGlobal Instance { get; private set; }
	[Header("击中身体音效")]
	public List<AudioClip> hitSoundsOnBody = new List<AudioClip>();
	public AudioClip hitSoundOnBody => hitSoundsOnBody[Random.Range(0, hitSoundsOnBody.Count)];
	[Header("击中墙壁音效")]
	public List<AudioClip> hitSoundsOnWall = new List<AudioClip>();
	public AudioClip hitSoundOnWall => hitSoundsOnWall[Random.Range(0, hitSoundsOnWall.Count)];
	private void Awake() {
		Instance = this;
	}
	public AudioSource WeaponAudioSource;

}
