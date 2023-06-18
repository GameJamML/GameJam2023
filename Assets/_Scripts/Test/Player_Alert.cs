using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Alert : MonoBehaviour
{
    public Alert_Script Alert;
    // Start is called before the first frame update
    void Start()
    {
        
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
