using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioClip[] footstepsOnWood;
    private PlayerController playerControler;
    public string material;

    private void Start()
    {
        playerControler = GetComponent<PlayerController>();
    }

    void PlayFootstepSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.volume = Random.Range(0.9f, 1.0f);
        audioSource.pitch = Random.Range(0.9f, 1.1f);


        if (footstepsOnWood.Length > 0 && playerControler.canInteract == true)
        {
            audioSource.PlayOneShot(footstepsOnWood[Random.Range(0, footstepsOnWood.Length)]);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //switch (collision.gameObject.tag)
        //{
        //    case "Bushes":
        //    case "Grass":
        //    case "Wood":
        //    case "Dirt":
        //    case "Wet Soil":
        //        material = collision.gameObject.tag;
        //        break;

        //    default:
        //        break;
        //}
    }
}
