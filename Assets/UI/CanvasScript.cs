using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    public CanvasGroup Instructions;
    private bool active = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown) active = false;

        if (active == false)
        {
            Instructions.alpha = Instructions.alpha - 0.001f;
        }

    }
}
