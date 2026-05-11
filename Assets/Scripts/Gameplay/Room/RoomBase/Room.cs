using System;
using System.Collections.Generic;
using UnityEngine;
using Edgar.Unity;
using QFramework;
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

	public event Action<Room> RoomInitialized;
	public event Action<Room, Collider2D> PlayerEnteredRoom;
	public event Action<Room, Collider2D> PlayerExitedRoom;


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

	public Vector3 GetRoomCenterPoint()
	{
		if(roomCenterPoint == null) return transform.position;
		return roomCenterPoint.position;
	}


	private void Reset() {
		SelfBoxCollider2D = gameObject.GetOrAddComponent<BoxCollider2D>();
		
		gameObject.tag = "Room";
		SelfBoxCollider2D.isTrigger = true;
	}
}
