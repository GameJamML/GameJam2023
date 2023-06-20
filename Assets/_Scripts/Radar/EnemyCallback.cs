using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCallback : PingCallback
{
    public AudioSource enemyAudioSource;
    public AudioClip[] enemyAudioClips;

    Enemy enemyScript;

    private void Awake()
    {
        enemyScript = FindObjectOfType<Enemy>();
    }
    public override void Callback()
    {
        if (!enemyAudioSource.isPlaying)
        {
            enemyScript.counter = 0.0f;
            enemyScript.StateBehavior();
            enemyAudioSource.pitch = Random.Range(0.8f, 1.1f);
            enemyAudioSource.clip = enemyAudioClips[enemyScript.currentState];
            enemyAudioSource.Play();
            enemyScript.phaseSubstate++;
        }
    }
}
