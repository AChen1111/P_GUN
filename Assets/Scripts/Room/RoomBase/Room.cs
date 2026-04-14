using System.Collections.Generic;
using UnityEngine;
using Edgar.Unity;
using QFramework;
using QFramework.PG;


public abstract class Room : MonoBehaviour
{
	[Header("房间碰撞器")]
	public BoxCollider2D SelfBoxCollider2D;

	[Header("门设置")]
	[SerializeField] protected bool needGenerateDoors = false;
	[SerializeField] private Door doorPrefab;

	private bool doorsGenerated;


	/// <summary>
	/// 玩家进入房间处理
	/// </summary>
	protected virtual void OnPlayerEnteredRoom(Collider2D other) { }
	/// <summary>
	/// 房间初始化逻辑
	/// </summary>
	protected virtual void OnRoomInitialized() { }


	
	private void Start() {
		Debug.Log("房间初始化");
		InitRoom();
	}

	
	/// <summary>
	/// 初始化房间	
	/// </summary>
	public void InitRoom()
	{

		OnRoomInitialized();

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

		foreach (var door in roomInfo.RoomInstance.Doors)
		{
			var points = door.DoorLine.GetPoints();
			var localSum = Vector3.zero;
			var count = 0;

			foreach (var point in points)
			{
				localSum += new Vector3(point.x + 0.5f, point.y + 0.5f, 0f);
				count++;
			}

			if (count <= 0) continue;

			var localCenter = localSum / count;
			var worldCenter = roomTemplateInstance.transform.TransformPoint(localCenter);
			result.Add(worldCenter);
		}

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
			OnPlayerEnteredRoom(other);
		}
	}




	private void Reset() {
		SelfBoxCollider2D = gameObject.GetOrAddComponent<BoxCollider2D>();
		
		gameObject.tag = "Room";
		SelfBoxCollider2D.isTrigger = true;
	}
}	



