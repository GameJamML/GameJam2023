using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MastilLights : MonoBehaviour
{

    public Light[] beaconLight;

    int off_Timer = 100;
    float off_Timer_resta = 0;
    public bool parpadeo_Active = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ActivateLights()
    {
        parpadeo_Active = true;
        StartCoroutine("FlickeringCoroutine");
    }

    // Update is called once per frame
    void Update()
    {   
        
      
    }

    IEnumerator FlickeringCoroutine()
    {
        for (int i = 0; i < 10; ++i)
        {
            for (int j = 0; j < 4; j++)
            {
                beaconLight[j].intensity = beaconLight[j].intensity == 0 ? 10000 : 0;
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
