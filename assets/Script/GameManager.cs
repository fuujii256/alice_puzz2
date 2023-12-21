using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public GameObject Block_1;
    public GameObject Block_2;
    public GameObject Block_3;
    public GameObject Block_4;
    public GameObject Block_5;
    public GameObject Block_6;
    public GameObject Block_SP; 
    float game_Time = 0.0f;
    float game_Time_Cnt = 0.0f;

    int axisV;

    int axisV_old;

    // Start is called before the first frame update
    void Start()
    {
    

    }

    // Update is called once per frame  
    void Update()
    {
        GameObject block_1_Prefab = Resources.Load<GameObject>("Block_1");
        GameObject block_2_Prefab = Resources.Load<GameObject>("Block_2");
        GameObject block_3_Prefab = Resources.Load<GameObject>("Block_3");
        GameObject block_4_Prefab = Resources.Load<GameObject>("Block_4");
        GameObject block_5_Prefab = Resources.Load<GameObject>("Block_5");

        //game_Time_Cnt += Time.deltaTime;


        //axisV =(int)Input.GetKeyDown("Vertical");
        
        if ( Input.GetKeyDown(KeyCode.Return))
        {
            //ブロックの新規生成
            int i = 0;
            while(i<2)
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
                GameObject instance = Instantiate(new_instance,v3, Quaternion.Euler(0, 0, 0))as GameObject;
                Block_move component = instance.AddComponent<Block_move>();
                component.advent_no = i;    //出現させるのは何個目のブロックか指定する  
            
           
                i++; 
            }     
            //game_Time += game_Time_Cnt;
            //game_Time_Cnt = 0.0f;
             //SaxisV_old = axisV;
        }

        

    }
}
