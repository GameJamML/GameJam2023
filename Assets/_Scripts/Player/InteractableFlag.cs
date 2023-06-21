using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableFlag : ShipInteractable
{
    public FlagController flagController;
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
        flagController.enabled = true;
        Alert.ChangeSprites(1);
    }

    public override void PlayerLeave()
    {
        isOpen = false;
        player.ResumeInteraction();
        flagController.enabled = false;
        Alert.ChangeSprites(0);
    }
}
