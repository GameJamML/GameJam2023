using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableWheel : ShipInteractable
{
    public WheelController wheelController;
    private void Update()
    {
        if (!isPlayerOn)
            return;
        if (Input.GetKeyDown(KeyCode.E) && player.CanInteract())
        {
            PlayerInteraction();
            return;
        }

        if (!isOpen)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerLeave();
            return;
        }
    }

    public override void PlayerInteraction()
    {
        player.StopInteraction();
        isOpen = true;
        wheelController.enabled = true;
    }

    public override void PlayerLeave()
    {
        isOpen = false;
        player.ResumeInteraction();
        wheelController.enabled = false;
    }
}
