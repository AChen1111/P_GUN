using UnityEngine;
using QFramework;
using System.Collections.Generic;

namespace QFramework.PG
{
	public partial class Room : ViewController
	{
		private List<Vector3> EnemyPositions = new List<Vector3>();
		private List<Vector3> DoorPositions = new List<Vector3>();
		private List<Door> Doors = new List<Door>();
		public RoomConfig RoomConfig{get; set;}

		[Header("敌人预制体")]
		public GameObject EnemyPrefab;
		[Header("门预制体")]	
		public GameObject DoorPrefab;



		/// <summary>
		/// 添加敌人
		/// </summary>
		/// <param name="position">敌人位置</param>
		public void AddEnemy(Vector3 position)
		{
			EnemyPositions.Add(position);
		}

		/// <summary>
		/// 添加门
		/// </summary>
		/// <param name="position">门位置</param>
		public void AddDoor(Vector3 position)
		{
			DoorPositions.Add(position);
		}

		/// <summary>
		/// 生成敌人
		/// </summary>
		public void GenerateEnemies()
		{

			int num = Random.Range(1, EnemyPositions.Count + 1);
			RoomPlayManager.Instance.ResetEnemyCount(num);
			for(int i = 0; i < num; i++)
			{
				var obj = Instantiate(EnemyPrefab, EnemyPositions[i], Quaternion.identity);
				obj.SetActive(true);
			}
			
		}


		
		/// <summary>
		/// 生成门
		/// </summary>
		public void GenerateDoors()
		{
			foreach(var position in DoorPositions)
			{
				var obj = Instantiate(DoorPrefab, position, Quaternion.identity);
				obj.SetActive(true);

				Doors.Add(obj.GetComponent<Door>());
			}
		}
	
		/// <summary>
		/// 设置房间类型
		/// </summary>
		/// <param name="type">房间类型</param>
		private void SetRoomType(RoomTypes type)
		{
			RoomConfig.Type(type);
		}


		/// <summary>
		/// 初始化房间	
		/// </summary>
		public void InitRoom(int pos_x,int pos_y,RoomConfig roomConfig)
		{
			transform.position = new Vector3(pos_x,pos_y,0);
			SelfBoxCollider2D.size = new Vector2(roomConfig.Width - 2, roomConfig.Height - 2);
			RoomConfig = roomConfig;

			if(RoomConfig.roomType == RoomTypes.Normal)
			{
				GenerateDoors();
				Debug.Log("生成门");
			}

			if(EnemyPositions.Count > 0)
			{
				//排序敌人的位置,按照与玩家距离降序排序
				EnemyPositions.Sort((a, b) =>
				{
					return (Global.player.transform.position - a).sqrMagnitude.CompareTo((Global.player.transform.position - b).sqrMagnitude);
				});
			}
		}


		/// <summary>
		/// 检测玩家进入房间
		/// </summary>
		/// <param name="other">玩家的碰撞器</param>
		private void OnTriggerEnter2D(Collider2D other)
		{
			if(other.CompareTag("Player"))
			{
				if(RoomConfig.roomType == RoomTypes.Normal)
				{
					//设置波数
					RoomPlayManager.Instance.ResetWaveCount(RoomConfig.waveNum);
					//生成敌人
					GenerateEnemies();
					//订阅波次结束事件
					RoomPlayManager.Instance.OnRoomCurrentWaveEndEvent += GenerateEnemies;
					RoomPlayManager.Instance.OnRoomAllWavesEndEvent += OnRoomAllWavesEnd;
				}

			}
		}

		private void OnRoomAllWavesEnd()
		{
			//设置房间类型为完成
			RoomConfig.roomType = RoomTypes.Complete;

			//取消订阅波次结束事件
			RoomPlayManager.Instance.OnRoomCurrentWaveEndEvent -= GenerateEnemies;
			RoomPlayManager.Instance.OnRoomAllWavesEndEvent -= OnRoomAllWavesEnd;
		}


	}	
}


