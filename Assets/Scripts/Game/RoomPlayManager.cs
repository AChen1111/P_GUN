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
		public event Action OnRoomAllWavesEndEvent = () => {};
		public event Action OnPlayerEnterRoomEvent = () => {};
		public event Action OnRoomCurrentWaveEndEvent = () => {};

		//敌人数量
		private int _enemyCount = 0;
		//波数
		private int _waveCount = 0;
		
		//设置敌人数量
		public void ResetEnemyCount(int count)
		{
			_enemyCount = count;
			OnPlayerEnterRoomEvent?.Invoke();
		}

		/// <summary>
		/// 设置波数
		/// </summary>
		/// <param name="count">波数</param>
		public void ResetWaveCount(int waveCount)
		{
			_waveCount = waveCount;
		}

		public void DecreaseEnemyCount()
		{
			_enemyCount--;
			if(_enemyCount <= 0)
			{
				_waveCount--;
				Debug.Log("波次结束, 剩余波数: " + _waveCount);
				if(_waveCount <= 0)
				{
					_waveCount = 0;
					//所有波次结束
					OnRoomAllWavesEndEvent?.Invoke();
				}
				else
				{
					//本波结束，生成下一波
					OnRoomCurrentWaveEndEvent?.Invoke();
				}
			}
		}
	}
}
