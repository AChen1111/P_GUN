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

        public enum DoorState
        {
            Open,
			Close,
        }
	
		public DoorState State{get; set;} = DoorState.Open;

		private void Start()
		{
			OpenDoorSR.gameObject.SetActive(State == DoorState.Open);
			CloseDoorSR.gameObject.SetActive(State == DoorState.Close);
			doorCollider.enabled = State == DoorState.Close;
		}

		/// <summary>
		/// 启用时订阅玩家进入房间事件,和离开房间事件
		/// </summary>
		private void OnEnable() {
			RoomPlayManager.Instance.OnPlayerEnterRoomEvent += CloseDoor;
			RoomPlayManager.Instance.OnRoomEnemysDiedEvent += OpenDoor;
		}
		/// <summary>
		/// 禁用时取消订阅玩家进入房间事件
		/// </summary>
		private void OnDisable() {
			RoomPlayManager.Instance.OnPlayerEnterRoomEvent -= CloseDoor;
			RoomPlayManager.Instance.OnRoomEnemysDiedEvent -= OpenDoor;
		}

	
		private void SetDoorState(DoorState state)
		{
			State = state;

		}

		/// <summary>
		/// 打开门
		/// </summary>
		public void OpenDoor()
		{
			SetDoorState(DoorState.Open);
			OpenDoorSR.gameObject.SetActive(true);
			CloseDoorSR.gameObject.SetActive(false);
			doorCollider.enabled = false;
		}

		/// <summary>
		/// 关闭门
		/// </summary>
		public void CloseDoor()
		{
			SetDoorState(DoorState.Close);
			OpenDoorSR.gameObject.SetActive(false);
			CloseDoorSR.gameObject.SetActive(true);
			doorCollider.enabled = true;
		}
	}
}
