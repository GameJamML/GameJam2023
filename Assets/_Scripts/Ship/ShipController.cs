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

    [Header("Foam Particles")]
    [SerializeField] float MultFrontFoamParticles;
    [SerializeField] float backFrontFoamParticles;
    [SerializeField] ParticleSystem leftFoamParticle;
    [SerializeField] ParticleSystem rightFoamParticle;
    [SerializeField] ParticleSystem backFoamParticle;

    private Rigidbody _rb;
    bool crashed = false;
    float crashTime = 3.0f;
    float currentCrashtime = 0.0f;
    FlagController flagController;

    public AudioClip crashClip;
    public AudioClip specialCrashClip;
    public AudioSource shipAudioSource;

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
            flagController.FlagCrash();
            speedMagnitude = 0;
            _rb.velocity = Vector3.zero;
            shipAudioSource.clip = crashClip;
            shipAudioSource.volume = Random.Range(0.9f, 1.0f);
            shipAudioSource.pitch = Random.Range(0.9f, 1.1f);
            shipAudioSource.Play();
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

        if (speedMagnitude > 0)
        {
            var leftFoamParticleEmission = leftFoamParticle.emission;
            var rightFoamParticleEmission = rightFoamParticle.emission;
            var backFoamParticleEmission = backFoamParticle.emission;

            leftFoamParticleEmission.rateOverTime = MultFrontFoamParticles * speedMagnitude;
            rightFoamParticleEmission.rateOverTime = MultFrontFoamParticles * speedMagnitude;
            backFoamParticleEmission.rateOverTime = backFrontFoamParticles * speedMagnitude;

            if (!leftFoamParticle.isPlaying)
            {
                leftFoamParticle.Play();
            }
            if (!rightFoamParticle.isPlaying)
            {
                rightFoamParticle.Play();
            }
            if (!backFoamParticle.isPlaying)
            {
                backFoamParticle.Play();
            }

        }
        else
        {
            if (leftFoamParticle.isPlaying)
            {
                leftFoamParticle.Stop();
            }
            if (rightFoamParticle.isPlaying)
            {
                rightFoamParticle.Stop();
            }
            if (backFoamParticle.isPlaying)
            {
                backFoamParticle.Stop();
            }
        }

        //// Rotation test
        //rotateInput = Input.GetAxis("Horizontal");

        //Debug.Log(rotateInput);
        // Update Angle
        if (rotateInput != 0)
            transform.Rotate(new Vector3(0, _rotateInput * _rotationSpeed * Time.deltaTime, 0));

        if (transform.localEulerAngles.x != 0 || transform.localEulerAngles.z != 0)
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
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

    public void CrashShip()
    {
        if (shipAudioSource.isPlaying)
            return;
        crashed = true;
        currentCrashtime = 0;
        flagController.FlagCrash();
        _rb.velocity = Vector3.zero;

        speedMagnitude = 0;
        shipAudioSource.clip = specialCrashClip;

        shipAudioSource.Play();
    }
}
