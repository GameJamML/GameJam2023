using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInteractable : MonoBehaviour
{

    private bool isPlayerOn = false;

    private void Update()
    {
        if (!isPlayerOn)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerInteraction();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Stay");
        if (other.CompareTag("Player"))
        {
            isPlayerOn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Stay");
        if (other.CompareTag("Player"))
        {
            isPlayerOn = false;
        }
    }

    public virtual void PlayerInteraction()
    {
        // Interaciton for player
        Debug.Log("Default");
    }
}
