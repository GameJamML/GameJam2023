using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconController : MonoBehaviour
{
    public GameObject beaconOrigin;
    public Light beaconLight;

    bool beaconActivated = false;

    float beaconTime = 3.0f;
    float currentTime = 0.0f;

    float coolDownTime = 2.0f;
    float currentCoolDowntime = 2.0f;
    public BoxCollider boxCollider;    

    public void Awake()
    {
        currentCoolDowntime = 5.0f;
    }

    private void Update()
    {
        if (!beaconActivated)
        {
            if (currentCoolDowntime < coolDownTime)
                currentCoolDowntime += Time.deltaTime;

            return;
        }

        if (currentTime < beaconTime)
        {
            currentTime += Time.deltaTime; 
            float timePercentage = 1.0f - (currentTime / beaconTime);

            beaconLight.intensity = 10000 * timePercentage;
        }
        else
        {
            currentCoolDowntime = 0.0f;
            currentTime = 0.0f;
            beaconActivated = false;
            beaconLight.intensity = 0;
        }

    }

    public void ActivateBeacon()
    {
        if ((currentCoolDowntime < coolDownTime) || beaconActivated)
            return;

        beaconActivated = true;
        boxCollider.enabled = true;
        beaconLight.intensity = 10000;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Soul"))
        {
            SoulBehavior behavior = other.gameObject.GetComponent<SoulBehavior>();
            behavior.Capture(beaconOrigin);
            ActivateBeacon();
        }
    }
}
