using UnityEngine;
public class FinalRoom : Room
{
    [Header("最终房间贴图")]
    [SerializeField]private SpriteRenderer finalSR;
    
    override protected void OnRoomInitialized()
    {
        needGenerateDoors = true;
        finalSR.gameObject.SetActive(false);
    }


    protected override void OnPlayerEnteredRoom(Collider2D other)
    {
        finalSR.gameObject.SetActive(true);
    }
}
