using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableFlag : ShipInteractable
{
    private bool isOpen = false;

    public override void PlayerInteraction()
    {
        Debug.Log("InteractableFlag!");
    }
}
