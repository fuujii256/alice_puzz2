using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarLight : MonoBehaviour
{
    GameObject block;
    int temp_cnt;
    //private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
    temp_cnt =300;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        temp_cnt--;
        if (temp_cnt <= 0)
        {
            Destroy(this.gameObject);
        }
            
    }
}
