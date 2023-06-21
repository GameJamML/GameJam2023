using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulManager : MonoBehaviour
{
    public SoulBehavior[] souls;

    public AudioClip[] soulClips;

    public InteractableRadar radar;

    int currentSoul = 0;
    // Start is called before the first frame update
    void Start()
    {
        radar = FindObjectOfType<InteractableRadar>();

        foreach (var soul in souls)
        {
            soul.transform.parent.gameObject.SetActive(false);
        }
        souls[0].transform.parent.gameObject.SetActive(true);
        radar.currentClip = soulClips[0];

    }

    public void NextSoul()
    {
        currentSoul++;
        if (currentSoul <= 3)
        {
            souls[currentSoul].transform.parent.gameObject.SetActive(true);
            radar.currentClip = soulClips[currentSoul];
        }
    }
}
