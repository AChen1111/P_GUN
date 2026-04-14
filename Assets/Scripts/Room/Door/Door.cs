using UnityEngine;
using QFramework;
using System.Collections.Generic;
using System;

namespace QFramework.PG
{
	public partial class Door : ViewController
	{
		[Header("贴图")]
		public SpriteRenderer OpenDoorSR;
		public SpriteRenderer CloseDoorSR;

		[Header("碰撞器")]
		public BoxCollider2D doorCollider;
		[Header("是否打开")]
		public bool isOpen = true;


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
			OpenDoorSR.gameObject.SetActive(isOpen);
			CloseDoorSR.gameObject.SetActive(!isOpen);
		}

		/// <summary>
		/// 打开门
		/// </summary>
		public void OpenDoor()
		{
			isOpen = true;
			OpenDoorSR.gameObject.SetActive(true);
			CloseDoorSR.gameObject.SetActive(false);
			doorCollider.enabled = false;
		}

		/// <summary>
		/// 关闭门
		/// </summary>
		public void CloseDoor()
		{
			isOpen = false;
			OpenDoorSR.gameObject.SetActive(false);
			CloseDoorSR.gameObject.SetActive(true);
			doorCollider.enabled = true;
		}
	}
}
