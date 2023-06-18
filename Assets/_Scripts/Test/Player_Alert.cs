using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Alert : MonoBehaviour
{
    Alert_Script Alert;
    // Start is called before the first frame update
    void Start()
    {
       Alert = FindObjectOfType<Alert_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag  == "Wall" )
        {
            Alert.AlertAvaliable(true);
            Debug.Log("enter");
        }
    }

   private void OnTriggerExit(Collider other)
   {
       if (other.tag == "Wall" )
       {
           Alert.AlertAvaliable(false);
       }
   }
}
