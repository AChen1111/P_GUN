using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using QFramework;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;
using Game.Gameplay.Save;

namespace Game.Gameplay
{
    [RequireComponent(typeof(BoxCollider2D))]
	public abstract class Room : MonoBehaviour
    {
		private static readonly List<Room> activeRooms = new List<Room>();

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
		[SerializeField] protected bool canGenerateItems = false;


		private bool doorsGenerated;
		protected List<Door> doorsList = new List<Door>();
		private string cachedSaveRoomId;

		// 随机生成的格子坐标, 由 RandomRoomGenerator 在实例化后写入.
		private Vector2Int? gridCell;
		// 有邻居的方向集合, 决定哪些墙边开门.
		private readonly HashSet<Vector2Int> openDoorDirections = new HashSet<Vector2Int>();

		public event Action<Room> RoomInitialized;
		public event Action<Room, Collider2D> PlayerEnteredRoom;
		public event Action<Room, Collider2D> PlayerExitedRoom;

		public static IReadOnlyList<Room> ActiveRooms => activeRooms;
		public static Room CurrentPlayerRoom { get; private set; }
		public string SaveRoomId
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(cachedSaveRoomId))
				{
					return cachedSaveRoomId;
				}

				// 随机生成的房间用 格子坐标 做 id, 同关卡同种子的地图 id 稳定.
				if (gridCell.HasValue)
				{
					cachedSaveRoomId = $"{GetType().Name}_{gridCell.Value.x}_{gridCell.Value.y}";
					return cachedSaveRoomId;
				}

				// 直接运行场景时没有生成上下文, 使用场景位置生成调试用 id.
				var roundedX = Mathf.RoundToInt(transform.position.x);
				var roundedY = Mathf.RoundToInt(transform.position.y);
				cachedSaveRoomId = $"{GetType().Name}_{roundedX}_{roundedY}_{gameObject.name}";
				return cachedSaveRoomId;
			}
		}
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

		/// <summary>
		/// 初始化运行时依赖.
		/// </summary>
		private void Awake() {
			if (!activeRooms.Contains(this))
			{
				activeRooms.Add(this);
			}

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
		/// 释放销毁时持有的运行时状态.
		/// </summary>
		private void OnDestroy()
		{
			activeRooms.Remove(this);
			if (CurrentPlayerRoom == this)
			{
				CurrentPlayerRoom = null;
			}
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

	    void GenerateDoors()
	    {
            if (doorsGenerated)
	            return;
            if (doorPrefab == null)
                throw new InvalidOperationException($"{name} 未绑定 Door 预制体.");

	        if (!gridCell.HasValue)
	        {
	            Debug.LogError($"{nameof(Room)}: 未写入生成上下文, 无法按锚点生成门.", this);
	            return;
	        }

        var floor = GetComponentsInChildren<Tilemap>().FirstOrDefault(tilemap => tilemap.name == "Floor");
        if (floor == null)
            throw new InvalidOperationException($"{name} 缺少 Floor Tilemap, 无法按格子生成门.");
        var bounds = floor.cellBounds;
        foreach (var direction in openDoorDirections)
	        {
	            var anchor = GetDoorAnchor(direction);
	            if (anchor == null)
	            {
	                Debug.LogError($"{nameof(Room)}: 缺少门锚点 {AnchorName(direction)}, 无法生成门.", this);
	                continue;
	            }

            // 两格宽门洞每格放一扇一格门, 战斗关门时不会只封住半边.
            var horizontal = direction.x != 0;
            var edge = horizontal
                ? (direction.x > 0 ? bounds.xMax - 1 : bounds.xMin)
                : (direction.y > 0 ? bounds.yMax - 1 : bounds.yMin);
            var middle = horizontal
                ? Mathf.FloorToInt((bounds.yMin + bounds.yMax - 1) * 0.5f)
                : Mathf.FloorToInt((bounds.xMin + bounds.xMax - 1) * 0.5f);
            for (var index = 0; index < 2; index++)
            {
                var cell = horizontal
                    ? new Vector3Int(edge, middle + index, 0)
                    : new Vector3Int(middle + index, edge, 0);
                var door = Instantiate(doorPrefab, floor.GetCellCenterWorld(cell), Quaternion.identity);
                door.gameObject.SetActive(true);
                door.SetDoorState(doorStateIsOpen);
                doorsList.Add(door);
            }
	        }

	        doorsGenerated = true;
	    }
}

		/// <summary>
		/// 写入生成上下文: 格子坐标和有邻居的方向, 由 RandomRoomGenerator 在实例化后调用.
		/// </summary>
		/// <param name="cell">本房间的格子坐标.</param>
		/// <param name="neighborDirections">有邻居的方向集合.</param>
		public void InitializeGenerationContext(Vector2Int cell, IEnumerable<Vector2Int> neighborDirections)
		{
			gridCell = cell;
			openDoorDirections.Clear();
			foreach (var direction in neighborDirections)
			{
				openDoorDirections.Add(direction);
			}
		}

		/// <summary>
		/// 按方向取门锚点, 预制体上需要预置 DoorAnchor_N/E/S/W 四个空物体.
		/// </summary>
		public Transform GetDoorAnchor(Vector2Int direction)
		{
			return transform.Find(AnchorName(direction));
		}

		/// <summary>
		/// 方向对应的锚点名.
		/// </summary>
		public static string AnchorName(Vector2Int direction)
		{
			if (direction == Vector2Int.up) return "DoorAnchor_N";
			if (direction == Vector2Int.right) return "DoorAnchor_E";
			if (direction == Vector2Int.down) return "DoorAnchor_S";
			if (direction == Vector2Int.left) return "DoorAnchor_W";
			throw new ArgumentException($"非法门方向: {direction}.", nameof(direction));
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
				{
					minimapData.SetVisited(true);
					minimapData.Highlight();
				}

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
			if (TryGetComponent<MinimapRoomData>(out var minimapData))
				minimapData.SetVisited(true);
		}
		public virtual void RestoreSaveData(RoomSaveData data)
		{
			if (data == null) return;

			// 读档只覆盖安全点状态, 不重放房间生成或掉落逻辑.
			Visited = data.visited;
			if (TryGetComponent<MinimapRoomData>(out var minimapData))
				minimapData.SetVisited(Visited);
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


		/// <summary>
		/// 重置编辑器默认配置.
		/// </summary>
		private void Reset() {
			SelfBoxCollider2D = GetComponent<BoxCollider2D>();

			gameObject.tag = "Room";
			SelfBoxCollider2D.isTrigger = true;
		}
    }
}
