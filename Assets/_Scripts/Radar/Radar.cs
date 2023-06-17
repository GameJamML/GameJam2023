using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{

    private RectTransform _refreshRadar;
    private RectTransform _trailRadar;
    private Transform _ship;
    [Range(-720, 720)]
    [SerializeField] int angularSpeed;
    [SerializeField] int radarDistance;
    [SerializeField] int pingInterval = 5;
    [SerializeField] GameObject Ship;
    [SerializeField] GameObject radarPingPrefab;
    private GameObject[] radarPings;
    private int currentRadarPing;
    private int limitPings = 0;
    // Start is called before the first frame update
    void Start()
    {
        _refreshRadar = transform.Find("RadarRefresh") as RectTransform;
        _trailRadar = transform.Find("Trail") as RectTransform;
        _ship = Ship.GetComponent<Transform>();
        //angularSpeed = 180;
        radarPings = new GameObject[1000];
        currentRadarPing = 0;

        for (int i = 0;i<radarPings.Length;i++)
        {
            radarPings[i] = Instantiate(radarPingPrefab, gameObject.transform);
            radarPings[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        UpdateRadar();
        
    }

    private void UpdateRadar()
    {
        //Rotate Refresh on radar, la linia vamos
        _refreshRadar.Rotate(Vector3.forward, angularSpeed * Time.deltaTime);
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
        _raycastHitArray = Physics.RaycastAll(_ship.position, RetrieveVectorWithAngle(_refreshRadar.eulerAngles.z), radarDistance);

        //DEBUG
        Debug.DrawRay(_ship.position, RetrieveVectorWithAngle(_refreshRadar.eulerAngles.z), Color.red);
        Debug.DrawLine(_ship.position, _ship.position + RetrieveVectorWithAngle(_refreshRadar.eulerAngles.z) * radarDistance, Color.blue);

        RaycastBehaviour(_raycastHitArray);
    }
    
    private void RaycastBehaviour(RaycastHit[] _raycastHitArray)
    {
        for (int i = 0; i < _raycastHitArray.Length; i++)
        {
            if (_raycastHitArray[i].collider == null) return;

            radarPings[currentRadarPing].SetActive(true);

            radarPings[currentRadarPing].transform.position = _raycastHitArray[i].point;

            if (_raycastHitArray[i].collider.CompareTag("Wall"))
            {
                RadarPing radarPingRef = radarPings[currentRadarPing].GetComponent<RadarPing>();
                radarPingRef.ChangeColorOfPing(Color.white);
            }
            else if (_raycastHitArray[i].collider.CompareTag("Soul"))
            {
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
