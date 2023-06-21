using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    ShipController ship;
    public GameObject Timon;
    public float currentRotation = 0.0f;
    private bool _active = false;
    public float rotateSpeed = 0.2f;

    public bool active
    {
        get
        {
            return _active;
        }
        set
        {
            
            _active = value;
            if (_active == false)
            {
                currentRotation = 0;
                ship.rotateInput = 0;
            }
        }
    }
    void Awake()
    {
        ship = FindObjectOfType<ShipController>();
        ship.rotateInput = currentRotation;
    }


    // Update is called once per frame
    void Update()
    {

        if (_active && Input.GetKey(KeyCode.A))
        {

            currentRotation -= rotateSpeed * Time.deltaTime;

            if (Timon.transform.localEulerAngles.y < 315)
            {
                Timon.transform.Rotate(0.1f, 0.0f, 0.0f, Space.Self);
            }
        }
        else if (_active && Input.GetKey(KeyCode.D))
        {
            currentRotation += rotateSpeed * Time.deltaTime;
            if (Timon.transform.localEulerAngles.y > 225)
            {
                Timon.transform.Rotate(-0.1f, 0.0f, 0.0f, Space.Self);
            }
        }
        else
        {
            currentRotation = Mathf.Lerp(currentRotation, 0, rotateSpeed * 2 * Time.deltaTime);
            ResetpositionWheel();
        }

        if (currentRotation > 1)
            currentRotation = 1;
        if (currentRotation < -1)
            currentRotation = -1;
        ship.rotateInput = currentRotation;
    }

    public void ResetpositionWheel()
    {
        if (Timon.transform.localEulerAngles.y > 271)
        {
            Timon.transform.Rotate(-0.1f, 0.0f, 0.0f, Space.Self);
        }
        else if (Timon.transform.localEulerAngles.y < 269)
        {
            Timon.transform.Rotate(0.1f, 0.0f, 0.0f, Space.Self);
        }
        else
        {
            Timon.transform.Rotate(0, 0.0f, 0.0f, Space.Self);
        }
    }

}
