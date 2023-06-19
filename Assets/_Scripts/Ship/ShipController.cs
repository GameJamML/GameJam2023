using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipController : MonoBehaviour
{
    [Header("Ship ForwadSpeed")]
    [SerializeField, Range(0, 1)] private float _speedMagnitude = 1.0f;
    [SerializeField] private float _maxSpeed = 10;

    [Header("Ship Rotation")]
    [SerializeField, Range(-1, 1)] private float _rotateInput = 0.0f;
    [SerializeField] public float _rotationSpeed = 5f;

    private Rigidbody _rb;
    bool crashed = false;
    float crashTime = 3.0f;
    float currentCrashtime = 0.0f;
    FlagController flagController;

    public float speedMagnitude
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
        flagController = FindObjectOfType<FlagController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PhysicalWall"))
        {
            crashed = true;
            currentCrashtime = 0;
            flagController.flagHeight = 0;
            speedMagnitude = 0;
        }
    }
    void Update()
    {
        if (crashed)
        {
            currentCrashtime += Time.deltaTime;

            if (currentCrashtime >= crashTime)
                crashed = false;
        }
        // Movement test
        //speedMagnitude = speedMagnitude + Input.GetAxis("Vertical") * Time.deltaTime;

        //// Rotation test
        //rotateInput = Input.GetAxis("Horizontal");

        //Debug.Log(rotateInput);
        // Update Angle
        if (rotateInput != 0)
            transform.Rotate(new Vector3(0, _rotateInput * _rotationSpeed * Time.deltaTime, 0));

        
    }

    void FixedUpdate()
    {
        if (crashed)
            return;
        // Get forward direction
        Vector3 forwardDirection = transform.forward;

        // Calculate velocity
        _rb.velocity = forwardDirection * _speedMagnitude * _maxSpeed;
        _rb.angularVelocity = Vector3.zero;
    }
}
