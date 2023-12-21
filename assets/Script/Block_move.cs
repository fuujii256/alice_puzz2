using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_move : MonoBehaviour
{
    Rigidbody2D rbody;
    public int advent_no;
    public int axisH;
    public int axisV;
    public bool player_control;
    public bool player_freeze;  
    int axisH_old = 0;
    int axisV_old = 0;

     public bool moveButtonJudge = false;  
    Vector3 movePosition;
    
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

            int speed = 5;
            axisH =(int)Input.GetAxisRaw("Horizontal");
            axisV =(int)Input.GetAxisRaw ("Vertical");

            if(axisH_old != axisH)
            {
            movePosition = pos;
            if (axisH == -1 && pos.x > -5.5f)
                {
                    transform.Translate (-0.25f, 0, 0);
                }
                if(axisH == 1  && pos.x < -1.5f)
                {
                    transform.Translate ( 0.25f, 0, 0);
                }
            }

            if(axisV_old != axisV)
            {
                if (axisV == -1)
                    {
                        rbody.drag = 0;             //下ボタンが押されたら、強制落下
                        Vector3 force = new Vector3 (0.0f,-200.0f,0.0f);    // 力を設定
                        rbody.AddForce (force);         // 力を加える
                        player_control = false;         //コントロールを不可にする
                    }
                    
            }

            //this.transform.position = pos;
            axisH_old = axisH;
            axisV_old = axisV;
        }     
          
    }
    //衝突した時に、一度だけ実行する
    void OnCollisionEnter2D(Collision2D coll) 
    {
        player_control = false;
    }
}
