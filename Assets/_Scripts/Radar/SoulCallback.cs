using UnityEngine;

public class SoulCallback : PingCallback
{
    public AudioClip clip;

    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GameObject.Find("Radar 1").GetComponent<AudioSource>();
    }

    public override void Callback()
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
