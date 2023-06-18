using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    ShipController ship;
    float currentRotation = 0.0f;

    public float rotateSpeed = 0.2f;
    void Awake()
    {
        ship = FindObjectOfType<ShipController>();
        ship.rotateInput = currentRotation;
    }

    private void OnDisable()
    {
        currentRotation = 0;
        ship.rotateInput = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            currentRotation -= rotateSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            currentRotation += rotateSpeed * Time.deltaTime;
        }
        else
        {
            currentRotation = Mathf.Lerp(currentRotation, 0, rotateSpeed * 2 *Time.deltaTime);
        }

        if (currentRotation > 1)
            currentRotation = 1;
        if (currentRotation < -1)
            currentRotation = -1;
        ship.rotateInput = currentRotation;
    }
}
