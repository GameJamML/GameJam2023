using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    private Transform cameraTransform;
    private Vector3 direction;
    private Vector3 velocity;
    private Vector3 lastForward;
    private Rigidbody rigidbody;

    public float speed = 1.0f;
    void Start()
    {
        // Assume there is only 1 camera in the scene
        cameraTransform = FindObjectOfType<Camera>().gameObject.transform;
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        direction = new Vector3(0, 0, 0);

        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        velocity.x = Input.GetAxisRaw("Horizontal");
        velocity.z = Input.GetAxisRaw("Vertical");

        direction = cameraTransform.localToWorldMatrix * direction;
        direction.y = 0;
        direction.Normalize();

        velocity = cameraTransform.localToWorldMatrix * velocity;
        velocity.y = 0;
        velocity.Normalize();

        if (direction.magnitude > 0)
        {
            transform.forward = direction;
            lastForward = direction;
        }
        else
        {
            transform.forward = lastForward;
        }
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = velocity * speed;
    }
}
