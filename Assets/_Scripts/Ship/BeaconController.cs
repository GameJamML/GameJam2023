using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconController : MonoBehaviour
{
    public GameObject beaconOrigin;

    bool beaconActivated = false;

    float beaconTime = 3.0f;
    float currentTime = 0.0f;

    float coolDownTime = 2.0f;
    float currentCoolDowntime = 2.0f;

    public MeshRenderer mesh;
    public BoxCollider boxCollider;    

    public void Awake()
    {
        mesh = gameObject.GetComponent<MeshRenderer>();
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
            currentTime += Time.deltaTime;
        else
        {
            currentCoolDowntime = 0.0f;
            currentTime = 0.0f;
            beaconActivated = false;
            mesh.enabled = false;
            boxCollider.enabled = false;
        }
        
    }

    public void ActivateBeacon()
    {
        if ((currentCoolDowntime < coolDownTime) || beaconActivated)
            return;

        beaconActivated = true;
        mesh.enabled = true;
        boxCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Soul"))
        {
            SoulBehavior behavior = other.gameObject.GetComponent<SoulBehavior>();
            behavior.Capture(beaconOrigin);
        }
    }
}
