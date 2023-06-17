using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipController : MonoBehaviour
{
    [Header("Ship ForwadSpeed")]
    [SerializeField, Range(0, 1)] private float _speedMagnitude = 1.0f;
    [SerializeField] private float _maxSpeed = 10;

    [Header("Ship Rotation")]
    [SerializeField, Range(-1, 1)] private float _rotateInput = 0.0f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float stopThreshold = 0.1f;

    [SerializeField]
    private Quaternion targetRotation;

    private Rigidbody _rb;

    public float speedMagnitud
    {
        get
        {
            return _speedMagnitude;
        }
        set
        {
            if (value >= 0 && value <= 1)
                _speedMagnitude = value;
        }
    }

    public float rotateInput
    {
        get
        {
            return _rotateInput;
        }
        set
        {
            if (value >= -1 && value <= 1)
                _rotateInput = value;
        }
    }

    void Start()
    {
        // Set up rigidbody
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _rb.isKinematic = false;
    }

    void Update()
    {
        // Movement test
        speedMagnitud = speedMagnitud + Input.GetAxis("Vertical") * Time.deltaTime;

        // Rotation test
        rotateInput = Input.GetAxis("Horizontal");

        // Update Angle
        if (rotateInput != 0)
            transform.Rotate(new Vector3(0, _rotateInput * rotationSpeed * Time.deltaTime, 0));
    }

    void FixedUpdate()
    {
        // Get forward direction
        Vector3 forwardDirection = transform.forward;

        // Calculate velocity
        _rb.velocity = forwardDirection * _speedMagnitude * _maxSpeed;

        _rb.angularVelocity = Vector3.zero;
    }
}
