using UnityEngine;
using QFramework;


public abstract class Room : MonoBehaviour
{
	[Header("房间碰撞器")]
	public BoxCollider2D SelfBoxCollider2D;


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
	}

	/// <summary>
	/// 子类初始化逻辑
	/// </summary>
	protected virtual void OnRoomInitialized() { }

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

	/// <summary>
	/// 子类玩家进入处理
	/// </summary>
	protected virtual void OnPlayerEnteredRoom(Collider2D other) { }


	private void Reset() {
		SelfBoxCollider2D = gameObject.GetOrAddComponent<BoxCollider2D>();
		
		gameObject.tag = "Room";
		SelfBoxCollider2D.isTrigger = true;
	}
}	



