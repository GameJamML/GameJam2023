using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableRadar : ShipInteractable
{
    public Radar radarController;
    float radarCooldown = 3.0f;
    float currentCooldown = 3.0f;
    private void Update()
    {
        if (!isPlayerOn)
            return;

        if (currentCooldown >= radarCooldown)
        {
            if (Input.GetKeyDown(KeyCode.E) && player.CanInteract())
            {
                PlayerInteraction();
                return;
            }
        }
        else
            currentCooldown += Time.deltaTime;

    }

    public override void PlayerInteraction()
    {
        radarController.ObjectiveSignal();
        currentCooldown = 0.0f;
    }
}
