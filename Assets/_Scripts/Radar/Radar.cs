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
    [SerializeField] GameObject Ship;
    [SerializeField] GameObject radarPingPrefab;
    private List<Collider> colliders;
    private float colliderTimer;
    // Start is called before the first frame update
    void Start()
    {
        _refreshRadar = transform.Find("RadarRefresh") as RectTransform;
        _trailRadar = transform.Find("Trail") as RectTransform;
        _ship = Ship.GetComponent<Transform>();
        //angularSpeed = 180;
        colliders = new List<Collider>();
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

        //Detect the nearby walls, etc.
        DetectSorroundings();

        colliderTimer += Time.deltaTime;

        if(colliderTimer > 0.1f)
        {
            colliders.Clear();

            colliderTimer = 0.0f;
        }
    }

    private void DetectSorroundings()
    {
        RaycastHit[] _raycastHitArray;
        _raycastHitArray = Physics.RaycastAll(_ship.position, RetrieveVectorWithAngle(_refreshRadar.eulerAngles.z), radarDistance);

        Debug.DrawRay(_ship.position, RetrieveVectorWithAngle(_refreshRadar.eulerAngles.z), Color.red);
        Debug.DrawLine(_ship.position, _ship.position + RetrieveVectorWithAngle(_refreshRadar.eulerAngles.z) * radarDistance,Color.blue);

        for(int i = 0;i<_raycastHitArray.Length;i++)
        {
            if (_raycastHitArray[i].collider == null) return;

            if (!colliders.Contains(_raycastHitArray[i].collider))
            {

                colliders.Add(_raycastHitArray[i].collider);

                GameObject pingGO = Instantiate(radarPingPrefab, _raycastHitArray[i].point, radarPingPrefab.transform.rotation);
                pingGO.transform.SetParent(gameObject.transform);

                if (_raycastHitArray[i].collider.CompareTag("Wall"))
                {

                    RadarPing radarPingRef = pingGO.GetComponent<RadarPing>();
                    radarPingRef.ChangeColorOfPing(Color.white);


                }
                else if (_raycastHitArray[i].collider.CompareTag("Soul"))
                {
                    RadarPing radarPingRef = pingGO.GetComponent<RadarPing>();
                    radarPingRef.ChangeColorOfPing(Color.green);

                }
            }
        }
    }

    Vector3 RetrieveVectorWithAngle(float angle)
    {

        float angleInRad = angle * Mathf.Deg2Rad;

        return new Vector3(Mathf.Cos(angleInRad), 0.0f, Mathf.Sin(angleInRad));
    }
}
