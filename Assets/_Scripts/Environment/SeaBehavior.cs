using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaBehavior : MonoBehaviour
{
    public SeaManager[] seas;

    public float timer = 0.0f;

    float currentTSMagnitude = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        seas = GameObject.FindObjectsOfType<SeaManager>();
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                StartCoroutine("BackToNormalCoroutine");
            }
        }
    }

    public void ChangeRoughness(float magnitude, float newTimer = 0.0f)
    {
        this.timer = newTimer;
        for (int i = 0; i < 9; ++i)
        {
            seas[i].timeScaleMagnitude = magnitude;
        }
        currentTSMagnitude = magnitude;
    }

    public void ChangeColor(float magnitude, float newTimer = 0.0f)
    {
        this.timer = newTimer;

        for (int i = 0; i < 9; ++i)
        {
            seas[i].colorMagnitude = magnitude;
        }
    }

    IEnumerator BackToNormalCoroutine()
    {
        while (currentTSMagnitude > 0)
        {
            currentTSMagnitude -= Time.deltaTime;
            ChangeRoughness(currentTSMagnitude);
            yield return new WaitForEndOfFrame();
        }
    }
}
