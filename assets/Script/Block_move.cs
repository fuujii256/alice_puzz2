using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_move : MonoBehaviour
{
    Rigidbody2D rbody;
    public int advent_no;
    public int axisH;
    public int axisV;
    public bool rakka = false;   //落下モードの場合はtrue
    public bool player_freeze;  
    int axisH_old = 0;
    int axisV_old = 0;

    int mat_x;
    int mat_y;
    int old_mat_x;
    int old_mat_y;
    public bool seishi = false;   //このオブジェクトが静止しているか
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
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        Vector3 pos = this.transform.position;

        float temp_x = (pos.x +5.00f)/0.75f +1.0f;  //ブロックマトリクスの自分の座標を計算 
        mat_x = (int)temp_x;
        float temp_y = 10- (pos.y +4.175f)/0.75f;
        mat_y = (int)temp_y;
        
        //Debug.Log("x:"+mat_x+" y:"+mat_y);
        GameManager.block_matrix [old_mat_y,old_mat_x] = 0;
        GameManager.block_matrix [mat_y,mat_x] = advent_no;    //ブロックマトリクスに自分の番号を記録する 
        old_mat_x= mat_x;
        old_mat_y= mat_y;

        //if (player_control == true )
        //{
            ////int speed = 5;
            //axisH =(int)Input.GetAxisRaw("Horizontal");
            //axisV =(int)Input.GetAxisRaw ("Vertical");

            //if(axisH_old != axisH)
            //{
            //movePosition = pos;
            ////if (axisH == -1 && pos.x > -5.5f )
            //if (axisH == -1 && pos.x > -5.5f && GameManager.block_matrix [mat_y,mat_x -1] ==0)
            //    {
            //        transform.Translate (-0.25f, 0, 0);
            //        //rbody.velocity = new Vector2(-0.25f,0);

            //    }
            //    //if (axisH == 1  && pos.x < -1.5f )
            //    if (axisH == 1  && pos.x < -1.5f && GameManager.block_matrix [mat_y,mat_x +1] ==0)
                
            //    {
            //        transform.Translate ( 0.25f, 0, 0);
            //        //rbody.velocity = new Vector2(0.25f,0);
            //    }
            //}

        if (rakka == true)      //落下モードへ移行する指示があった場合、下方向へ力を加える
        {
            rakka = false; 
            rbody.drag = 0;             //下ボタンが押されたら、強制落下
            Vector3 force = new Vector3 (0.0f,-400.0f,0.0f);    // 力を設定
            rbody.AddForce (force);         // 力を加える
  
        }

        //静止判定
        if (GetComponent<Rigidbody2D>().IsSleeping()) {
            seishi = true;
        }
        else {
            seishi = false;
        }            
            //}

            ////this.transform.position = pos;
            //axisH_old = axisH;
            //axisV_old = axisV;
        //}     
          
    }
    //衝突した時に、一度だけ実行する
    void OnCollisionEnter2D(Collision2D coll) 
    {
        GameManager.player_control = false;
        GameManager.trigger =1; 
        Debug.Log("GameManager.trigger:"+GameManager.trigger);              //　衝突判定開始フラグをたてる
    }
}
