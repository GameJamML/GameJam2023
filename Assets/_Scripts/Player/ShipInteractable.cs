using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInteractable : MonoBehaviour
{

    protected bool isPlayerOn = false;
    protected PlayerController player;
    protected bool isOpen = false;
    //Alert_Script Alert;
    public Alert_Script Alert;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
       // Alert = FindObjectOfType<Alert_Script>();
    }
    
    private void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOn = true;
            Alert.AlertAvaliable(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOn = false;
            Alert.AlertAvaliable(false);
            if (isOpen)
                PlayerLeave(); // For Security reasons. Makes sure the interface closes if somehow you move out the trigger.
        }
    }

    public virtual void PlayerInteraction()
    {
    }

    public virtual void PlayerLeave()
    {
    }
}
