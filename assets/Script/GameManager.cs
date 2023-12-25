using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class GameManager : MonoBehaviour
{
    public GameObject UnderWall;
    public GameObject GameOver;
    public GameObject start_text;
    public GameObject go_title;
    public GameObject scoreText;
    public GameObject hi_scoreText;
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
    float temp_x;
    float temp_y;

    public static int hi_score;
    public int score = 0;
    
    static public int Advent_num = 0;  //新規生成するブロックの背番号 0:wall 1:None 2:～ブロック
    
    static public int trigger = 0;
    public bool all_seishi = false; //全てのオブジェクトが静止しているか？
    public List<GameObject> blockList = new List<GameObject>();   //管理するブロックのリスト
    public List<GameObject> deleteList = new List<GameObject>();  //消去するブロックのリスト
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

    static public int [,] block_matrix_tag = new int [11,8]{
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
        GameOver.SetActive(false);  //ゲームオーバー文字を消す
        go_title.SetActive(false);  //タイトル画面へ戻るボタンを消す
        
        if (hi_score == 0)
        {
            hi_score = 100;  //ハイスコアの初期値
        }        
        scoreText.GetComponent<Text>().text = score.ToString();
        hi_scoreText.GetComponent<Text>().text = hi_score.ToString();

        
        Invoke("InactiveImage",2.0f);
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
        
        if ( ini_block )
        {
            ini_block = false;  //初期ブロック出力完了
            trigger = 0 ;       //消去判定用トリガーを初期化
            //ブロックの新規生成
            
            int i = 0;
            while(i<1)    //同時生成するブロックの数
            {

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

                if (block_matrix[0,3] == 0)
                {
                    block = Instantiate(new_instance,v3, Quaternion.Euler(0, 0, 0));
                    Block_move component = block.AddComponent<Block_move>();
                    component.advent_no = Advent_num;    //出現させるのは何個目のブロックか
                    component.advent_type = rnd +1;    //出現させるブロックの種類  
            
                    blockList.Add(block);
                    player_control = true;
                }
                else
                {
                    trigger =5;             //出現位置に既にブロックがあったら、ゲームオーバー処理へ
                    temp_cnt = 250;
                    Physics.gravity = new Vector3(0, -10, 0);  //重力を加える
                }
                i++; 
            }     
            
            Advent_num++;  //生成するブロックの背番号を更新　　0:wall 1:None 2:～ブロック

        }

        int list_num = blockList.Count-1 ;
        axisH =(int)Input.GetAxisRaw("Horizontal");

        //ブロックの座標・可動範囲    
        //x右端：-1.25
        //x左端：-5.00
        //y下端：-3.425
        //y上端： 3.2847

        //マトリクス上の現在位置を取得
        if ( blockList[list_num] !=null ) 
        {
            temp_x = ( blockList[list_num].transform.position.x +5.75f )/0.75f;
            mat_x = (int)temp_x;
            temp_y = 10.0f - (blockList[list_num].transform.position.y +4.175f )/0.75f;  //origin4.175f
            mat_y = (int)temp_y;

            //動作対象のブロックを左右に動かす
            if (player_control == true && axisH != axisH_old)
            {
                if (block_matrix_tag[mat_y , mat_x + axisH ] == 0)
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
        }

        //ブロックが全て静止したか確認する。ー＞ブロック消去フェーズへ
        if ( trigger == 1 )
        {
            all_seishi = false;
            int i = 0;
            while ( i< blockList.Count )
            {
                if (blockList[i] != null) 
                {
                    script = blockList[i].GetComponent<Block_move>();
                    if (script.seishi == true)
                    {
                        all_seishi = true;                    
                    }
                    else 
                    {
                        all_seishi = false;
                    }
                }
                i++;
            }
            if (all_seishi == true)
                {
                    trigger = 2;
                    temp_cnt=10;            //ブロックが静止してからの待機時間
                    //Debug.Log("trigger:"+trigger);
                }
    
        }

        if (trigger == 2)           //全てのオブジェクトが静止したら
        {
            temp_cnt--;
            if (temp_cnt <= 0) {
                //ini_block = true;
                trigger = 3;
            }
        }
        if (trigger == 3)           //３つ並んでいるか、チェックを開始する
        {
            //下の行からヨコのつながりを確認
            int k = 0;
            for (int i = 0; i < block_matrix.GetLength(1); i++)

            {

                //右から２つ目以降は確認不要（width-2）

                for (int j = 0; j < block_matrix.GetLength(0)-2; j++)

                {

                    //同じタグのキャンディが３つ並んでいたら。Ｘ座標がｊなので注意。

　　　　    //念のため、ふたつの式それぞれをカッコで囲んでいる。

                    if ((block_matrix_tag[j,i] > 1) && (block_matrix_tag[j,i] == block_matrix_tag[j+1,i]) && (block_matrix_tag[j, i] == block_matrix_tag[j + 2, i]))

                    {
                        Debug.Log("height_success3!");
                        score += 30;
                        UpdateScore();
                        //block_moveのisMatchingをtrueに
                        k = (int)block_matrix[j,i];
                        Debug.Log("success_block_no:"+k);                        
                        Block_move script1 = blockList[k].GetComponent<Block_move>();
                        script1.isMatching = true;

                        k = (int)block_matrix[j+1,i]; 
                        Debug.Log("success_block_no:"+k);   
                        Block_move script2 = blockList[k].GetComponent<Block_move>();
                        script2.isMatching = true;

                        k = (int)block_matrix[j+2,i]; 
                        Debug.Log("success_block_no:"+k);    
                        Block_move script3 = blockList[k].GetComponent<Block_move>();
                        script3.isMatching = true;
                    }

                }

            }

            //同様にｘ方向もチェック
            for (int i = 0; i < block_matrix.GetLength(0); i++)

            {

                //右から２つ目以降は確認不要（width-2）

                for (int j = 0; j < block_matrix.GetLength(1)-2; j++)

                {

                    //同じタグのキャンディが３つ並んでいたら。Ｘ座標がｊなので注意。

　　　　    //念のため、ふたつの式それぞれをカッコで囲んでいる。

                    if ((block_matrix_tag[i,j] > 1) && (block_matrix_tag[i,j] == block_matrix_tag[i,j+1]) && (block_matrix_tag[i, j] == block_matrix_tag[i , j +2]))

                    {
                        Debug.Log("width_success3!");
                        score +=30;
                        UpdateScore();
                        //block_moveのisMatchingをtrueに
                        k = (int)block_matrix[i,j];                        
                        script = blockList[k].GetComponent<Block_move>();
                        script.isMatching = true;

                        k = (int)block_matrix[i,j+1];   
                        script = blockList[k].GetComponent<Block_move>();
                        script.isMatching = true;

                        k = (int)block_matrix[i,j+2];    
                        script = blockList[k].GetComponent<Block_move>();
                        script.isMatching = true;
                    }

                }

            }

            //isMatching=trueのものをＬｉｓｔに入れる

            foreach (var item in blockList)
            {
                if (item != null)
                {
                    if (item.GetComponent<Block_move>().isMatching == true)
                        {
                            deleteList.Add(item);
                        }
                }        
            }

            Debug.Log("消去可能なブロック数："+deleteList.Count);

            if (deleteList.Count>0)         //消去可能なブロックはあるか？
            {
                //該当する配列をnullにして（内部管理）、キャンディを消去する（見た目）。
                foreach (var item in deleteList)
                {
                    temp_x = ( item.transform.position.x +5.75f )/0.75f;
                    mat_x = (int)temp_x;
                    temp_y = 10.0f - ( item.transform.position.y +4.175f )/0.75f;  //origin4.175f
                    mat_y = (int)temp_y;

                    Debug.Log("mat_x : mat_y:"+ mat_x +":"+ mat_y);
                    block_matrix[(int)mat_y, (int)mat_x] = 0;
                    block_matrix_tag[(int)mat_y, (int)mat_x] = 0;
                    
                    Destroy(item);
                }
            }
            else
            {
                //ブロック消去ルーチン完了時の処理 
                trigger = 0;  //もう消去可能なブロックがなければ、通常処理へ 
            }

            

            deleteList.Clear();         //消去処理が完了したら、リストをクリアする
            trigger = 4; 
            temp_cnt = 30;               //消去後はしばらく休止
        }

        if (trigger == 4)           //ブロック消去チェック後は小休止
        {
            temp_cnt--;
            if (temp_cnt <= 0) {
                ini_block = true;
                trigger = 1;            //もう一度全数静止チェックからやり直す
            }

        }

        if (trigger == 5)           //**ゲームオーバー
        {
            UnderWall.SetActive(false);  //床を抜く
            GameOver.SetActive(true);  //ゲームオーバー文字を表示
            go_title.SetActive(true);   //タイトル画面へ戻るボタンを表示

            temp_cnt--;
            if (temp_cnt <= 0) {
                ini_block = true;
                //trigger = 1;            //もう一度全数静止チェックからやり直す
            }

        }
        //For Debug
        //if (block.GetComponent<Rigidbody2D>().IsSleeping()) {
                //Debug.Log(block_matrix.ToString());
                //Debug.Log("sleeping...");
            //}
            //else
            //{
                //Debug.Log("block mooving");
            //}

        if ( Input.GetKeyDown(KeyCode.Space)) {   //ブロックマトリクスを表示
            //block_matrix.tagを表示
            Debug.Log("block_matrix.tagを表示");
            for (int i = 0;i < block_matrix.GetLength(0);i++)
            {                
                string str = "";
                for (int j = 0; j < block_matrix.GetLength(1); j++)
                {
                    str = str + block_matrix_tag[i, j] + " ";
                }
                Debug.Log(str);
            }
            //block_matrixを表示
            Debug.Log("block_matrixを表示");
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
    void InactiveImage()
    {
        start_text.SetActive(false);
    }
    void UpdateScore()
    {
        scoreText.GetComponent<Text>().text = score.ToString();

        if (score > hi_score) 
        {
            hi_score = score;
        } 

        hi_scoreText.GetComponent<Text>().text = hi_score.ToString();

    }
}
