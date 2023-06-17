using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{

    public GameObject flag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            flag.transform.Rotate(new Vector3(0, 1, 0), 1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            flag.transform.Rotate(new Vector3(0, 1, 0), -1);
        }
    }
}
