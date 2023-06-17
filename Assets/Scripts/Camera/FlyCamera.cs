using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCamera : MonoBehaviour
{
    public float panSpeed = 1;

    public float targetAngle = 45f;
    public float currentAngle = 0f;
    public float mouseSensitivity = 2f;
    public float rotationSpeed = 5f;

    private Vector2 pixelValue;

    ViewportBlitter blitter;

    private void Awake()
    {
        blitter = GetComponentInChildren<ViewportBlitter>();
    }

    private void Start()
    {

    }

    void Update()
    {
        Orbit();
        Pan();
    }

    private void Orbit()
    {
        float mouseX = Input.GetAxis("Mouse X");

        //Click and drag to rotate camera pivot
        if (Input.GetMouseButton(1))
        {
            targetAngle += mouseX * mouseSensitivity;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            targetAngle += 45;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            targetAngle -= 45;
        }
        else //Ince the mouse is let go, the camera pivot snaps to an increment o 45
        {
            targetAngle = Mathf.Round(targetAngle / 45);
            targetAngle *= 45;
        }

        //If the target angle exceeds 360 or falls below 0, it is corrected to stay between those values.
        if (targetAngle < 0)
        {
            targetAngle += 360;
        }
        if (targetAngle > 360)
        {
            targetAngle -= 360;
        }

        currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(30, currentAngle, 0);
    }

    private void Pan()
    {
        Vector2 pan = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            pan.x += 1 * panSpeed;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            pan.x -= 1 * panSpeed;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            pan.y -= panSpeed;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            pan.y += panSpeed;
        }

        blitter.PanViewport(pan.x * Time.deltaTime, pan.y * Time.deltaTime);
    }
}