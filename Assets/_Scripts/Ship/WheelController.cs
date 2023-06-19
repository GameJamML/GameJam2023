using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    ShipController ship;
    public GameObject Timon;
    public float currentRotation = 0.0f;

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
            if (Timon.transform.eulerAngles.y < 225)
            {
                Timon.transform.Rotate(0.1f, 0.0f, 0.0f, Space.Self);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            currentRotation += rotateSpeed * Time.deltaTime;
            if (Timon.transform.eulerAngles.y > 135)
            {
                Timon.transform.Rotate(-0.1f, 0.0f, 0.0f, Space.Self);
                }
        }
        else
        {
            currentRotation = Mathf.Lerp(currentRotation, 0, rotateSpeed * 2 *Time.deltaTime);
            if (Timon.transform.eulerAngles.y > 181)
            {
                Timon.transform.Rotate(-0.1f, 0.0f, 0.0f, Space.Self);
            }
            else if (Timon.transform.eulerAngles.y < 179)
            {
                Timon.transform.Rotate(0.1f, 0.0f, 0.0f, Space.Self);
            }
            else
            {
                Timon.transform.Rotate(0, 0.0f, 0.0f, Space.Self);
            }
        }

        if (currentRotation > 1)
            currentRotation = 1;
        if (currentRotation < -1)
            currentRotation = -1;
        ship.rotateInput = currentRotation;
    }
}
