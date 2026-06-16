using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var hit = Physics2D.Raycast(transform.position,Vector2.right,5f);
            Debug.DrawRay(transform.position,Vector2.right*5f,Color.red);
            if(hit.collider != null)
            {
                var obj = hit.collider.gameObject;
                var sr = obj.GetComponent<SpriteRenderer>();
                if(sr != null)
                {
                    sr.color = new Color(0,255,0);
                }
            }
        }
    }
}
