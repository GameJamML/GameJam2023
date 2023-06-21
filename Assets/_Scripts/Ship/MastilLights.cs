using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MastilLights : MonoBehaviour
{

    public List<Light> beaconLight;

    int flickeringIterations = 0;
    public bool parpadeo_Active = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ActivateLights(int iterations)
    {
        if (parpadeo_Active)
            return;
        flickeringIterations = iterations;
        parpadeo_Active = true;
        StartCoroutine("FlickeringCoroutine");
    }

    // Update is called once per frame
    void Update()
    {   
        
      
    }

    IEnumerator FlickeringCoroutine()
    {
        for (int i = 0; i < flickeringIterations; ++i)
        {
            for (int j = 0; j < beaconLight.Count; j++)
            {
                beaconLight[j].intensity = beaconLight[j].intensity == 15 ? 100 : 15;
                beaconLight[j].innerSpotAngle = 60.0f;
                beaconLight[j].spotAngle = 60.0f;
            }
            yield return new WaitForSeconds(Random.Range(0.10f, 0.25f));
        }
        for (int j = 0; j < beaconLight.Count; j++)
        {
            beaconLight[j].spotAngle = 120.0f;
            beaconLight[j].innerSpotAngle = 120.0f;
        }
        parpadeo_Active = false;
    }
}
