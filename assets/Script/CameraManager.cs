using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject SubScreen;

    public float isForceScrollSpeedX = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float x = SubScreen.transform.position.x - (isForceScrollSpeedX * Time.deltaTime);
        float y = SubScreen.transform.position.y + (isForceScrollSpeedX * Time.deltaTime);
        float z = SubScreen.transform.position.z;
        
        if(y > 10.0f)
        {
            x = 0.0f;
            y = 0.0f;
        } 

        Vector3 v3 = new Vector3(x,y,z);
        SubScreen.transform.position = v3;        
    }
}
