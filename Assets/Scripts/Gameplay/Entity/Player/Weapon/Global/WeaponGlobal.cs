using UnityEngine;
using QFramework;
using System.Collections.Generic;

public class WeaponGlobal : ViewController
{
    public SpriteRenderer GunFire;

	public static WeaponGlobal Instance { get; private set; }
	private void Awake() {
		Instance = this;
	}
	public AudioSource WeaponAudioSource;

}
