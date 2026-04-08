using UnityEngine;
using QFramework;
using System;

namespace QFramework.PG
{
	public partial class RoomPlayManager : ViewController
	{
		//单例
		public static RoomPlayManager Instance{get; private set;}
		private void Awake() {
			Instance = this;
		}

		//房间全敌人死亡事件
		public event Action OnRoomEnemysDiedEvent = () => {};
		public event Action OnPlayerEnterRoomEvent = () => {};


		//敌人数量
		private int _enemyCount = 0;
		
		//设置敌人数量
		public void ResetEnemyCount(int count)
		{
			_enemyCount = count;
			OnPlayerEnterRoomEvent?.Invoke();
		}

		public void DecreaseEnemyCount()
		{
			_enemyCount--;
			if(_enemyCount <= 0)
			{
				OnRoomEnemysDiedEvent?.Invoke();
			}
		}
	}
}
