using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBeacon : ShipInteractable
{
    public BeaconController beaconController;
    public AudioSource beaconAudioSource;

    private void Update()
    {
        if (!isPlayerOn)
            return;
        if (Input.GetKeyDown(KeyCode.E) && player.CanInteract())
        {
            PlayerInteraction();
            return;
        }
    }

    public override void PlayerInteraction()
    {
        beaconController.ActivateBeacon();
        beaconAudioSource.Play();
    }
}
