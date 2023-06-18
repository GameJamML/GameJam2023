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
    private Rigidbody _rb;
    public float speed = 1.0f;
    public LayerMask layer;

    private bool canInteract = true;

    void Start()
    {
        // Assume there is only 1 camera in the scene
        cameraTransform = FindObjectOfType<Camera>().gameObject.transform;
    }

    private void Update()
    {
        if (!canInteract)
            return;

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

        Ray ray = new Ray(transform.position, velocity);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
        {
            if (hit.distance > 1.0f)
                transform.position += velocity * speed * Time.deltaTime;
        }

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
        if (!canInteract)
            return;

        //GetComponent<Rigidbody>().velocity = velocity * speed;
    }

    public void StopInteraction()
    {
        canInteract = false;
        velocity = new Vector3(0, 0, 0);
    }

    public void ResumeInteraction()
    {
        canInteract = true;
    }

    public bool CanInteract()
    {
        return canInteract;
    }
}
