using UnityEngine;
using QFramework;
using System.Collections.Generic;
using System;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    public class Door : ViewController
    {
		[Header("贴图")]
		public SpriteRenderer CloseDoorSR;

		[Header("碰撞器")]
		public BoxCollider2D doorCollider;
		[Header("是否打开")]
		public bool isOpen = true;

		[Header("音频播放组件")]
		public AudioPlay audioPlay;


		/// <summary>
		/// 设置门的状态
		/// </summary>
		/// <param name="isOpen">是否打开</param>
		public void SetDoorState(bool isOpen)
		{
			this.isOpen = isOpen;

			if (isOpen)
			{
				isOpen = true;
				CloseDoorSR.gameObject.SetActive(false);
				doorCollider.enabled = false;
				EventCenter.Trigger(GameplayEvents.DoorOpened, this);
			}
			else
			{
				CloseDoor();
			}
		}

		/// <summary>
		/// 初始化运行时依赖.
		/// </summary>
		private void Awake() {
			CloseDoorSR.gameObject.SetActive(!isOpen);
			audioPlay = GetComponent<AudioPlay>();
		}

		/// <summary>
		/// 打开门
		/// </summary>
		public void OpenDoor(bool playSound = false)
		{
			isOpen = true;
			CloseDoorSR.gameObject.SetActive(false);
			doorCollider.enabled = false;

			audioPlay.Play();
			EventCenter.Trigger(GameplayEvents.DoorOpened, this);
		}

		/// <summary>
		/// 关闭门
		/// </summary>
		public void CloseDoor()
		{
			isOpen = false;
			CloseDoorSR.gameObject.SetActive(true);
			doorCollider.enabled = true;
			EventCenter.Trigger(GameplayEvents.DoorClosed, this);
		}
    }
}
