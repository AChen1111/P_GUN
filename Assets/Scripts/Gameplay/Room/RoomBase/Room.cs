using System;
using System.Collections.Generic;
using UnityEngine;
using Edgar.Unity;
using QFramework;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;
using Game.Gameplay.Save;

namespace Game.Gameplay
{
    public abstract class Room : MonoBehaviour
    {
		[Header("房间碰撞器")]
		public BoxCollider2D SelfBoxCollider2D;

		[Header("门设置")]
		[SerializeField] protected bool needGenerateDoors = false;
		[SerializeField] private Door doorPrefab;
		[SerializeField] protected bool doorStateIsOpen = true;

		[Header("房间中心点")]
		[SerializeField] private Transform roomCenterPoint;

		[Header("物品生成器")]
		public ItemSpawner itemSpawner;
		protected bool canGenerateItems = false;


		private bool doorsGenerated;
		protected List<Door> doorsList = new List<Door>();
		private string cachedSaveRoomId;

		public event Action<Room> RoomInitialized;
		public event Action<Room, Collider2D> PlayerEnteredRoom;
		public event Action<Room, Collider2D> PlayerExitedRoom;

		public static Room CurrentPlayerRoom { get; private set; }
		public string SaveRoomId => ResolveSaveRoomId();
		public bool Visited { get; private set; }
		public virtual bool Cleared => false;
		public IReadOnlyList<Door> Doors => doorsList;

		/// <summary>
		/// 玩家进入房间处理
		/// </summary>
		protected virtual void OnPlayerEnteredRoom(Collider2D other) { }
		/// <summary>
		/// 玩家离开房间处理
		/// </summary>
		protected virtual void OnPlayerExitedRoom(Collider2D other) { }
		/// <summary>
		/// 房间初始化逻辑
		/// </summary>
		protected virtual void OnRoomInitialized() { }

		private void Awake() {
			//查看有无生成器
			itemSpawner = GetComponent<ItemSpawner>();
			if(itemSpawner == null) {
				canGenerateItems = false;
			} else {
				canGenerateItems = true;
			}
		}

		private void Start() {
			//Debug.Log("房间初始化");
			InitRoom();
		}


		/// <summary>
		/// 初始化房间
		/// </summary>
		public void InitRoom()
		{

			OnRoomInitialized();
			RoomInitialized?.Invoke(this);

			//如果需要生成门，则生成门
			if (needGenerateDoors)
			{
				GenerateDoors();
			}
		}


		/// <summary>
		/// 生成门
		/// </summary>
		private void GenerateDoors()
		{
			if (doorsGenerated || doorPrefab == null) return;

			var spawnPositions = GetDoorSpawnPositionsFromEdgar();

			foreach (var position in spawnPositions)
			{
				var door = Instantiate(doorPrefab, position, Quaternion.identity);
				door.gameObject.SetActive(true);
				door.SetDoorState(this.doorStateIsOpen);
				doorsList.Add(door);
			}

			doorsGenerated = true;
		}


		/// <summary>
		/// 从Edgar插件中获取门生成位置
		/// </summary>
		/// <returns>门生成位置列表</returns>
		private List<Vector3> GetDoorSpawnPositionsFromEdgar()
		{
			var result = new List<Vector3>();

			if (!TryGetComponent<RoomInfoGrid2D>(out var roomInfo) || roomInfo.RoomInstance == null)
			{
				return result;
			}

			var roomTemplateInstance = roomInfo.RoomInstance.RoomTemplateInstance;
			if (roomTemplateInstance == null)
			{
				return result;
			}

			//Debug.Log($"房间 {gameObject.name} Edgar门数量: {roomInfo.RoomInstance.Doors.Count}");

			var addedTiles = new HashSet<Vector3Int>();
			foreach (var door in roomInfo.RoomInstance.Doors)
			{
				foreach (var point in door.DoorLine.GetPoints())
				{
					var tile = new Vector3Int(point.x, point.y, point.z);
					if (!addedTiles.Add(tile)) continue;

					var localCenter = new Vector3(tile.x + 0.5f, tile.y + 0.5f, 0f);
					var worldCenter = roomTemplateInstance.transform.TransformPoint(localCenter);
					result.Add(worldCenter);
				}
			}

			//Debug.Log($"房间 {gameObject.name} 生成门位: {result.Count}");
			return result;
		}



		/// <summary>
		/// 检测玩家进入房间
		/// </summary>
		/// <param name="other">玩家的碰撞器</param>
		private void OnTriggerEnter2D(Collider2D other)
		{
			if(other.CompareTag("Player"))
			{
				// 玩家当前房间只记录安全点存档需要的稳定进度.
				Visited = true;
				CurrentPlayerRoom = this;

				if (TryGetComponent<MinimapRoomData>(out var minimapData))
					minimapData.Highlight();

				OnPlayerEnteredRoom(other);
				PlayerEnteredRoom?.Invoke(this, other);
			}
		}

		/// <summary>
		/// 检测玩家离开房间
		/// </summary>
		/// <param name="other">玩家的碰撞器</param>
		private void OnTriggerExit2D(Collider2D other)
		{
			if (other.CompareTag("Player"))
			{
				OnPlayerExitedRoom(other);
				PlayerExitedRoom?.Invoke(this, other);
			}
		}

		private string ResolveSaveRoomId()
		{
			if (!string.IsNullOrWhiteSpace(cachedSaveRoomId))
			{
				return cachedSaveRoomId;
			}

			if (TryGetComponent<RoomInfoGrid2D>(out var roomInfo) && roomInfo.RoomInstance != null)
			{
				var position = roomInfo.RoomInstance.Position;
				var template = roomInfo.RoomInstance.RoomTemplatePrefab;
				var templateName = template != null ? template.name : gameObject.name;
				cachedSaveRoomId = $"{GetType().Name}_{position.x}_{position.y}_{templateName}";
				return cachedSaveRoomId;
			}

			// 直接运行场景时可能没有 Edgar 房间信息, 使用场景位置生成调试用 id.
			var roundedX = Mathf.RoundToInt(transform.position.x);
			var roundedY = Mathf.RoundToInt(transform.position.y);
			cachedSaveRoomId = $"{GetType().Name}_{roundedX}_{roundedY}_{gameObject.name}";
			return cachedSaveRoomId;
		}

		public Vector3 GetRoomCenterPoint()
		{
			if(roomCenterPoint == null) return transform.position;
			return roomCenterPoint.position;
		}

		public static void SetCurrentPlayerRoom(Room room)
		{
			CurrentPlayerRoom = room;
		}

		public void MarkVisited()
		{
			Visited = true;
			CurrentPlayerRoom = this;
		}

		public virtual void RestoreSaveData(RoomSaveData data)
		{
			if (data == null) return;

			// 读档只覆盖安全点状态, 不重放房间生成或掉落逻辑.
			Visited = data.visited;
		}

		protected void SetDoorsOpen(bool isOpen)
		{
			for (var i = 0; i < doorsList.Count; i++)
			{
				if (doorsList[i] != null)
				{
					doorsList[i].SetDoorState(isOpen);
				}
			}
		}


		private void Reset() {
			SelfBoxCollider2D = gameObject.GetOrAddComponent<BoxCollider2D>();

			gameObject.tag = "Room";
			SelfBoxCollider2D.isTrigger = true;
		}
    }
}
