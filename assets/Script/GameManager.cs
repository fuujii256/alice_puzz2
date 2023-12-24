using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;   

public class GameManager : MonoBehaviour
{
    //public GameObject Block_1;
    //public GameObject Block_2;
    //public GameObject Block_3;
    //public GameObject Block_4;
    //public GameObject Block_5;
    //public GameObject Block_6;
    //public GameObject Block_SP;
    private GameObject block; 

    //float game_Time = 0.0f;
    //float game_Time_Cnt = 0.0f;

    Block_move script;
    int axisH;
    int axisH_old;
    int axisV;
    int axisV_old;
    bool ini_block = true;   //ゲームスタート時、true の時は初期ブロックを強制生成
    static public bool player_control = false;   //プレイヤーがブロックを動かせる状態か？
    int mat_x;
    int mat_y;
    int temp_cnt;
    static public int Advent_num = 1;  //新規生成するブロックの背番号 0:wall 1:None 2:～ブロック
    
    static public int trigger = 0;
    public bool all_seishi = false; //全てのオブジェクトが静止しているか？
    public List<GameObject> blockList = new List<GameObject>();   //管理するブロックのリスト
    static public int [,] block_matrix = new int [11,8]{
        {1,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,1},
        {1,1,1,1,1,1,1,1}
    }; 

    // Start is called before the first frame update
    void Start()
    {
    

    }

    // Update is called once per frame  
    void FixedUpdate()
    {
        GameObject block_1_Prefab = Resources.Load<GameObject>("Block_1");
        GameObject block_2_Prefab = Resources.Load<GameObject>("Block_2");
        GameObject block_3_Prefab = Resources.Load<GameObject>("Block_3");
        GameObject block_4_Prefab = Resources.Load<GameObject>("Block_4");
        GameObject block_5_Prefab = Resources.Load<GameObject>("Block_5");

        //game_Time_Cnt += Time.deltaTime;


        //axisV =(int)Input.GetKeyDown("Vertical");
        
        if ( ini_block || Input.GetKeyDown(KeyCode.Return))
        {
            ini_block = false;  //初期ブロック出力完了
            trigger = 0 ;       //消去判定用トリガーを初期化
            //ブロックの新規生成
            
            int i = 0;
            while(i<1)    //同時生成するブロックの数
            {
                Advent_num++;  //生成するブロックの背番号を更新　　0:wall 1:None 2:～ブロック

                int rnd = Random.Range(1, 5);
                float x = -3.5f+ i*0.5f;
                float y = 3.24f;
                float z = 0.0f;
                Vector3 v3 = new Vector3(x,y,z);

                GameObject new_instance = block_1_Prefab;
        
                switch(rnd)
                {
                    case 1:
                        new_instance = block_1_Prefab;
                        break;
                    case 2:
                        new_instance = block_2_Prefab;
                        break;
                    case 3:
                        new_instance = block_3_Prefab;
                        break;
                    case 4:
                        new_instance = block_4_Prefab;
                        break;
                    case 5:
                        new_instance = block_5_Prefab;
                        break;
                }   
                block = Instantiate(new_instance,v3, Quaternion.Euler(0, 0, 0));
                Block_move component = block.AddComponent<Block_move>();
                component.advent_no = Advent_num;    //出現させるのは何個目のブロックか指定する  
            
                blockList.Add(block);
                player_control = true;

                i++; 
            }     
            

        }

        int list_num = blockList.Count-1 ;
        axisH =(int)Input.GetAxisRaw("Horizontal");

        //ブロックの座標・可動範囲    
        //x右端：-1.25
        //x左端：-5.00
        //y下端：-3.425
        //y上端： 3.2847

        //マトリクス上の現在位置を取得
        float temp_x = ( blockList[list_num].transform.position.x +5.75f )/0.75f;
        mat_x = (int)temp_x;
        float temp_y = 10.0f - (blockList[list_num].transform.position.y +4.175f )/0.75f;
        mat_y = (int)temp_y;

        //動作対象のブロックを左右に動かす
        if (player_control == true && axisH != axisH_old)
        {
            if (block_matrix[mat_y , mat_x + axisH ] == 0)
            {
                blockList[list_num].transform.Translate (0.75f*axisH, 0, 0);   //指定したブロックを動かす
            }
        }

        axisH_old = axisH;

        //下が押されたら強制落下させる
        axisV =(int)Input.GetAxisRaw("Vertical");
        if(axisV_old != axisV)
        {
            if ( axisV == -1 && player_control == true )
                {
                    script = blockList[list_num].GetComponent<Block_move>();
                    script.rakka = true;     //落下モードにさせる 
                    player_control = false;
                }
        }
        axisV_old = axisV;

        //ブロックが全て静止したか確認する。ー＞ブロック消去フェーズへ
        if ( trigger == 1 )
        {
            all_seishi = false;
            int i = 0;
            while ( i< blockList.Count )
            {
                script = blockList[i].GetComponent<Block_move>();
                if (script.seishi = true)
                {
                    all_seishi = true;                    
                }
                else 
                {
                    all_seishi = false;
                }
                i++;
            }
            if (all_seishi == true)
                {
                    trigger = 2;
                    temp_cnt=50;            //ブロックが静止してからの待機時間
                    Debug.Log("trigger:"+trigger);
                }
    
        }

        if (trigger == 2)           //全てのオブジェクトが静止したら
        {
            temp_cnt--;
            if (temp_cnt <= 0) {
                ini_block = true;
                trigger = 3;
            }
        }
        if (trigger == 3)
        {
            
        }


        //For Debug
        if (block.GetComponent<Rigidbody2D>().IsSleeping()) {
                //Debug.Log(block_matrix.ToString());
                //Debug.Log("sleeping...");
            }
            else
            {
                //Debug.Log("block mooving");
            }

        if ( Input.GetKeyDown(KeyCode.Space)) {   //ブロックマトリクスを表示
            for (int i = 0;i < block_matrix.GetLength(0);i++)
            {                
                string str = "";
                for (int j = 0; j < block_matrix.GetLength(1); j++)
                {
                    str = str + block_matrix[i, j] + " ";
                }
                Debug.Log(str);
            }
        }       

    }
}
