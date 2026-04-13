using UnityEngine;


public class DOTweenTest : MonoBehaviour
{
    public AnimType animType;

    void Start()
    {
        DOTweenAnimMgr.Play(animType, gameObject, 3f, () =>
        {
            Debug.Log("动画完成");  
        });
    }
}


