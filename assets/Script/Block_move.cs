using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_move : MonoBehaviour
{
    Rigidbody2D rbody;
    public bool player_control;
    public bool player_freeze;  
    int axisH_old = 0;
    public bool isFalling = true;
    public float y;
    // Start is called before the first frame update
    void Start()
    {
        //Rigidbodyを取得
        rbody = this.GetComponent<Rigidbody2D>();


        //FreezePositionXをオンにする
        rbody.constraints = RigidbodyConstraints2D.FreezePositionX; 
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation; 

        player_control = true;   //初期のみ操作可能  
        player_freeze = false;
    }

    // Update is called once per frame
    void Update()
    {        
        if (player_control == true )
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
    }
    //衝突した時に、一度だけ実行する
    void OnCollisionEnter2D(Collision2D coll) 
    {
        player_control = false;
    }
}
