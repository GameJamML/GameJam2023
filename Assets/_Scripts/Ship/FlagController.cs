using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    public float controllingSpeed = 0.5f;
    float flagHeight = 0.0f;

    ShipController ship;

    void Awake()
    {
        ship = FindObjectOfType<ShipController>();
        ship.speedMagnitude = flagHeight;
    }

    // Update is called once per frames
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (flagHeight < 1.0f)
            {
                flagHeight += controllingSpeed * Time.deltaTime;
            }
            else
                flagHeight = 1.0f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (flagHeight > 0)
            {
                flagHeight -= controllingSpeed * Time.deltaTime;
            }
            else
                flagHeight = 0;
        }
        ship.speedMagnitude = flagHeight;

    }
}
