using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    private RectTransform _refreshRadar;
    private RectTransform _refreshRadarHUD;
    private RectTransform _trailRadar;
    private Transform _rayCastOrigin;
    private WheelController _shipWheelController;
    [Range(-720, 720)]
    [SerializeField] int angularSpeed;
    [SerializeField] int radarDistance;
    [SerializeField] int pingInterval = 5;
    [SerializeField] GameObject rayCastOrigin;
    [SerializeField] GameObject Ship;
    [SerializeField] GameObject radarPingPrefab;
    private GameObject[] radarPings;
    private int currentRadarPing;
    private int limitPings = 0;

    private Vector3 cleanEuler = Vector3.zero;
    private Vector3 finalEuler = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        _refreshRadar = transform.Find("RadarRefreshRaycast") as RectTransform;
        _refreshRadarHUD = transform.Find("RadarRefreshHUD") as RectTransform;
        _trailRadar = transform.Find("Trail") as RectTransform;
        _rayCastOrigin = rayCastOrigin.GetComponent<Transform>();
        _shipWheelController = Ship.GetComponent<WheelController>();
        //angularSpeed = 180;
        radarPings = new GameObject[1000];
        currentRadarPing = 0;

        for (int i = 0;i<radarPings.Length;i++)
        {
            radarPings[i] = Instantiate(radarPingPrefab, gameObject.transform);
            radarPings[i].SetActive(false);
        }

        cleanEuler = _refreshRadar.eulerAngles;
        finalEuler = _refreshRadar.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {

        UpdateRadar();
        
    }

    private void UpdateRadar()
    {
        Vector3 shipRot = Ship.transform.eulerAngles;

        _refreshRadar.eulerAngles = cleanEuler;
        _refreshRadar.Rotate(Vector3.forward, angularSpeed * Time.deltaTime);
        cleanEuler = _refreshRadar.eulerAngles;

        finalEuler = cleanEuler;
        finalEuler.z -= shipRot.y;
        _refreshRadar.eulerAngles = finalEuler;

        _refreshRadarHUD.Rotate(Vector3.forward, angularSpeed * Time.deltaTime);
        _trailRadar.Rotate(Vector3.forward, angularSpeed * Time.deltaTime);

        //Limit the entry to the Detect function
        LimitDetecting();

    }

    private void LimitDetecting()
    {
        limitPings++;

        if (limitPings >= pingInterval)
        {
            //Detect the nearby walls, etc.
            DetectSorroundings();
            limitPings = 0;
        }
    }

    private void DetectSorroundings()
    {
        RaycastHit[] _raycastHitArray;
        _raycastHitArray = Physics.RaycastAll(_rayCastOrigin.position, RetrieveVectorWithAngle(_refreshRadar.eulerAngles.z), radarDistance);

        //DEBUG
        Debug.DrawRay(_rayCastOrigin.position, RetrieveVectorWithAngle(_refreshRadar.eulerAngles.z), Color.red);
        Debug.DrawLine(_rayCastOrigin.position, _rayCastOrigin.position + RetrieveVectorWithAngle(_refreshRadar.eulerAngles.z) * radarDistance, Color.blue);

        RaycastBehaviour(_raycastHitArray);
    }
    
    private void RaycastBehaviour(RaycastHit[] _raycastHitArray)
    {
        for (int i = 0; i < _raycastHitArray.Length; i++)
        {
            if (_raycastHitArray[i].collider == null) return;

            if (_raycastHitArray[i].collider.CompareTag("Wall"))
            {
                radarPings[currentRadarPing].SetActive(true);

                radarPings[currentRadarPing].transform.position = _raycastHitArray[i].point;

                RadarPing radarPingRef = radarPings[currentRadarPing].GetComponent<RadarPing>();
                radarPingRef.ChangeColorOfPing(Color.white);
            }
            else if (_raycastHitArray[i].collider.CompareTag("Soul"))
            {

                radarPings[currentRadarPing].SetActive(true);

                radarPings[currentRadarPing].transform.position = _raycastHitArray[i].point;

                RadarPing radarPingRef = radarPings[currentRadarPing].GetComponent<RadarPing>();
                radarPingRef.ChangeColorOfPing(Color.green);
            }

            currentRadarPing++;

            if (currentRadarPing >= radarPings.Length)
                currentRadarPing = 0;

        }
    }

    Vector3 RetrieveVectorWithAngle(float angle)
    {

        float angleInRad = angle * Mathf.Deg2Rad;

        return new Vector3(Mathf.Cos(angleInRad), 0.0f, Mathf.Sin(angleInRad));
    }
}
