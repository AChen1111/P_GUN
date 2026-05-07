using UnityEngine;
using QFramework;
using System.Collections.Generic;
using System;

public class Door : ViewController
{
	[Header("贴图")]
	public SpriteRenderer CloseDoorSR;

	[Header("碰撞器")]
	public BoxCollider2D doorCollider;
	[Header("是否打开")]
	public bool isOpen = true;
	
	[Header("开门音效")]
	public AudioClip openDoorSound;
	public static bool isPlayingOpenDoorSound = false;


	/// <summary>
	/// 设置门的状态
	/// </summary>
	/// <param name="isOpen">是否打开</param>
	public void SetDoorState(bool isOpen)
	{
		this.isOpen = isOpen;
		
		if (isOpen)
		{
			OpenDoor();
		}
		else
		{
			CloseDoor();
		}
	}
	
	private void Awake() {
		CloseDoorSR.gameObject.SetActive(!isOpen);
	}

	/// <summary>
	/// 打开门
	/// </summary>
	public void OpenDoor(bool playSound = false)
	{
		isOpen = true;
		CloseDoorSR.gameObject.SetActive(false);
		doorCollider.enabled = false;
		

		//保证只会播放一次开门音效
		if(playSound && !isPlayingOpenDoorSound)
		{
			isPlayingOpenDoorSound = true;
			//播放开门音效,并添加回调
			GlobalAudioPlay.Instance.PlayerAudioSourceByClip(
				openDoorSound,
				onComplete:
					() => {
						isPlayingOpenDoorSound = false;
					}
			);
		}

		EventCenter.Trigger(GameEvent.DoorOpened, this);
	}

	/// <summary>
	/// 关闭门
	/// </summary>
	public void CloseDoor()
	{
		isOpen = false;
		CloseDoorSR.gameObject.SetActive(true);
		doorCollider.enabled = true;
		EventCenter.Trigger(GameEvent.DoorClosed, this);
	}
}
