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

    int mat_x;
    int mat_y;
    int old_mat_x;
    int old_mat_y;

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
    void FixedUpdate()
    {   
        Vector3 pos = this.transform.position;

        float temp_x = (pos.x +5.5f)/0.5f; 
        mat_x = (int)temp_x;
        float temp_y = 14- (pos.y +3.53f)/0.5f;
        mat_y = (int)temp_y;
        
        //Debug.Log("x:"+mat_x+" y:"+mat_y);
        GameManager.block_matrix [old_mat_y,old_mat_x] = 0;
        GameManager.block_matrix [mat_y,mat_x] = advent_no;    //ブロックマトリクスに自分の番号を記録する 
        old_mat_x= mat_x;
        old_mat_y= mat_y;

        if (player_control == true )
        {
            //int speed = 5;
            axisH =(int)Input.GetAxisRaw("Horizontal");
            axisV =(int)Input.GetAxisRaw ("Vertical");

            if(axisH_old != axisH)
            {
            movePosition = pos;
            //if (axisH == -1 && pos.x > -5.5f )
            if (axisH == -1 && pos.x > -5.5f && GameManager.block_matrix [mat_y,mat_x -1] ==0)
                {
                    transform.Translate (-0.25f, 0, 0);
                    //rbody.velocity = new Vector2(-0.25f,0);

                }
                //if (axisH == 1  && pos.x < -1.5f )
                if (axisH == 1  && pos.x < -1.5f && GameManager.block_matrix [mat_y,mat_x +1] ==0)
                
                {
                    transform.Translate ( 0.25f, 0, 0);
                    //rbody.velocity = new Vector2(0.25f,0);
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
