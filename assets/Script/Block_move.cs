using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_move : MonoBehaviour
{
    Rigidbody2D rbody;
    bool player_control = true;  
    int axisH_old = 0;
    bool isFalling = true;
    // Start is called before the first frame update
    void Start()
    {
        //Rigidbodyを取得
        rbody = this.GetComponent<Rigidbody2D>();
 
        //FreezePositionXをオンにする
        rbody.constraints = RigidbodyConstraints2D.FreezePositionX; 
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation; 

          

    }

    // Update is called once per frame
    void Update()
    {        
        if (player_control == true)
        {
            Vector3 pos = this.transform.position;
            int axisH =(int)Input.GetAxisRaw("Horizontal");
            if(axisH_old != axisH)
            {
             if (axisH == -1)
                {
                    pos.x -= 0.5f;
                }
                if(axisH == 1)
                {
                    pos.x += 0.5f;
                }
            }
            this.transform.position = pos;
            axisH_old = axisH;
        }
        //落下中か？
        isFalling = rbody.velocity.y < 0.0f;  
        if (isFalling ==false)
        {
            player_control = false;         //落下中以外は操作できない
        }      
    }
}
