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

        game_Time_Cnt += Time.deltaTime;


        if (game_Time_Cnt > 3.0f )
        {
            //ブロックの新規生成
            int rnd = Random.Range(1, 6);
            float x = -3.76f;
            float y = 3.24f;
            float z = 0.0f;
            Vector3 v3 = new Vector3(x,y,z);
            switch(rnd)
            {
                case 1:
                    Instantiate(block_1_Prefab,v3, Quaternion.Euler(0, 0, 0));     
                    break;
                case 2:
                    Instantiate(block_2_Prefab,v3, Quaternion.Euler(0, 0, 0));           
                    break;
                case 3:
                    Instantiate(block_3_Prefab,v3, Quaternion.Euler(0, 0, 0));  
                    break;
                case 4:
                    Instantiate(block_4_Prefab,v3, Quaternion.Euler(0, 0, 0));  
                    break; 
                case 5:
                    Instantiate(block_5_Prefab,v3, Quaternion.Euler(0, 0, 0));  
                    break;
            }                           
            

            game_Time += game_Time_Cnt;
            game_Time_Cnt = 0.0f;
        }

        

    }
}
