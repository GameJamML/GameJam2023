using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MastilLights : MonoBehaviour
{

    public Light[] beaconLight;

    int off_Timer = 100;
    float off_Timer_resta = 0;
    bool parpadeo_Active = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)/*&& currentCoolDowntime < coolDownTime*/)
        {
            parpadeo_Active = true;
        }
        
        if (Input.GetKeyDown(KeyCode.X)/*&& currentCoolDowntime < coolDownTime*/)
        {
            parpadeo_Active = false;
        }

        for (int i = 0; i < 2; i++)
        {
            if (parpadeo_Active == true)
            {
                if (off_Timer_resta < 4)
                {
                    off_Timer_resta += Time.deltaTime;
                }
                else
                {
                    off_Timer_resta = 0;
                    off_Timer--;
                }

                if (off_Timer % 2 == 0)
                {


                    beaconLight[i].intensity = beaconLight[i].intensity - 0.5f;
                    //beaconLight[i].SetActive(false);
                }
                else
                {
                    
                    beaconLight[i].intensity = beaconLight[i].intensity + 0.5f;
                    //beaconLight[i].SetActive(true);
                }
            }
            else
            {
                off_Timer_resta = 0;
                parpadeo_Active = false;
                //beaconLight[i].SetActive(true);
            }
        }
      
    }
}
