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


        game_Time_Cnt += Time.deltaTime;


        if (game_Time_Cnt > 1.0f )
        {

            GameObject block_1 = Instantiate(block_1_Prefab); 


            game_Time += game_Time_Cnt;
            game_Time_Cnt = 0.0f;
        }

        

    }
}
