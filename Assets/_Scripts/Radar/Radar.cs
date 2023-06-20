using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    private RectTransform _refreshRadar;
    private RectTransform _refreshRadarHUD;
    private RectTransform _trailRadar;
    private Transform _rayCastOrigin;

    [Range(-720, 720)]
    [SerializeField] int angularSpeed;
    [SerializeField] int radarDistance;
    [SerializeField] int radarDistanceObjectives;
    [SerializeField] int pingInterval = 5;
    [SerializeField] GameObject rayCastOrigin;
    [SerializeField] GameObject Ship;
    [SerializeField] GameObject radarPingPrefab;
    private GameObject[] radarPings;
    private List<Collider> colliders;
    private int currentRadarPing;
    private int limitPings = 0;
    // Enmey
    [SerializeField] RectTransform enemyPoint;

    private Vector3 cleanEuler = Vector3.zero;
    private Vector3 finalEuler = Vector3.zero;

    private bool isDetectingObjectives = false;
    [SerializeField] private float objectivesPingMaxDuration = 1.0f;
    [SerializeField] private float objectivesPingDuration = 0.0f;
    [SerializeField] private RadarPing longPing;
    private RectTransform parentLongPing;

    // Start is called before the first frame update
    void Start()
    {
        _refreshRadar = transform.Find("RadarRefreshRaycast") as RectTransform;
        _refreshRadarHUD = transform.Find("RadarRefreshHUD") as RectTransform;
        _trailRadar = transform.Find("Trail") as RectTransform;
        _rayCastOrigin = rayCastOrigin.GetComponent<Transform>();

        parentLongPing = longPing.GetComponentInParent<RectTransform>();

        colliders = new List<Collider>();
        radarPings = new GameObject[100];
        currentRadarPing = 0;

        for (int i = 0; i < radarPings.Length; i++)
        {
            radarPings[i] = Instantiate(radarPingPrefab, gameObject.transform);
            radarPings[i].SetActive(false);
        }

        cleanEuler = _refreshRadar.eulerAngles;
        finalEuler = _refreshRadar.eulerAngles;
    }

    public void ObjectiveSignal()
    {
        isDetectingObjectives = true;
        objectivesPingDuration = 0.0f;
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
        float LastRot = (_refreshRadar.eulerAngles.z % 360) - 180;
        _refreshRadar.Rotate(Vector3.forward, angularSpeed * Time.deltaTime);
        cleanEuler = _refreshRadar.eulerAngles;
        float currentRot = (_refreshRadar.eulerAngles.z % 360) - 180;
        finalEuler = cleanEuler;
        finalEuler.z -= shipRot.y;
        _refreshRadar.eulerAngles = finalEuler;

        _refreshRadarHUD.Rotate(Vector3.forward, angularSpeed * Time.deltaTime);
        _trailRadar.Rotate(Vector3.forward, angularSpeed * Time.deltaTime);

        //Limit the entry to the Detect function
        LimitDetecting();

        if (LastRot < 0 && currentRot >= 0 && colliders.Count > 0)
        {
            colliders.Clear();
        }
    }

    private void LimitDetecting()
    {
        limitPings++;

        if (limitPings >= pingInterval)
        {
            //Detect the nearby walls, etc.
            DetectSorroundings();

            if (isDetectingObjectives)
                DetectObjectives();

            limitPings = 0;
        }
    }

    private void DetectObjectives()
    {
        RaycastHit[] _raycastHitArray;
        _raycastHitArray = Physics.RaycastAll(_rayCastOrigin.position, RetrieveVectorWithAngle(_refreshRadar.eulerAngles.z), radarDistanceObjectives);

        RaycastBehaviour(_raycastHitArray, true);

        if (objectivesPingDuration > objectivesPingMaxDuration)
        {
            isDetectingObjectives = false;
        }
        objectivesPingDuration += Time.deltaTime;
    }

    private void DetectSorroundings()
    {
        RaycastHit[] _raycastHitArray;
        _raycastHitArray = Physics.RaycastAll(_rayCastOrigin.position, RetrieveVectorWithAngle(_refreshRadar.eulerAngles.z), radarDistance);

        //DEBUG
        Debug.DrawRay(_rayCastOrigin.position, RetrieveVectorWithAngle(_refreshRadar.eulerAngles.z), Color.red);
        Debug.DrawLine(_rayCastOrigin.position, _rayCastOrigin.position + RetrieveVectorWithAngle(_refreshRadar.eulerAngles.z) * radarDistance, Color.blue);

        RaycastBehaviour(_raycastHitArray, false);
    }

    private void RaycastBehaviour(RaycastHit[] _raycastHitArray, bool isObjectives = false)
    {

        void ActiveRadarPing(Vector3 pos, int index, Color color)
        {
            radarPings[index].SetActive(true);

            radarPings[index].transform.position = pos;

            radarPings[index].GetComponent<RadarPing>().ChangeColorOfPing(color);
        }

        for (int i = 0; i < _raycastHitArray.Length; i++)
        {
            if (_raycastHitArray[i].collider == null || colliders.Contains(_raycastHitArray[i].collider)) return;

            if (isObjectives)
            {
                if (_raycastHitArray[i].collider.CompareTag("Soul"))
                {
                    colliders.Add(_raycastHitArray[i].collider);

                    ActiveRadarPing(_raycastHitArray[i].point, currentRadarPing, Color.green);

                    isDetectingObjectives = false;

                    if (_raycastHitArray[i].distance > radarDistance)
                    {
                        Vector3 dir = _raycastHitArray[i].transform.position - transform.position;
                        dir.z = 0.0f;
                        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                        //Debug.Log("Angle of objective -> " + angle);
                        PingLongDistance(angle);
                    }
                }
            }

            if (_raycastHitArray[i].collider.CompareTag("Wall"))
            {
                colliders.Add(_raycastHitArray[i].collider);

                ActiveRadarPing(_raycastHitArray[i].point, currentRadarPing, Color.white);
            }

            if (_raycastHitArray[i].collider.CompareTag("Enemy"))
            {
                Enemy enemy = _raycastHitArray[i].collider.GetComponent<Enemy>();

                switch (enemy.currentState)
                {
                    case 0:
                        break;
                    case 1:
                        //Empieza tras coger el primer objetivo, al cogerlo se emite un sonido de tensión(un rugido, algo estridente, )
                        //La primera vez que le das al radar la criatura aparece como un punto rojo en la lejanía, las siguientes pueden ser punto rojo, verde o ninguno.
                        //Hay un sonido continuo de tensión
                        //if (!enemyPoint.gameObject.activeSelf)
                        //    enemyPoint.gameObject.SetActive(true);
                        colliders.Add(_raycastHitArray[i].collider);
                        ActiveRadarPing(_raycastHitArray[i].point, currentRadarPing, Color.red);

                        //enemyPoint.eulerAngles = new Vector3(0, 0, 10);
                        break;
                    case 2:
                        //Empieza tras coger el segundo objetivo.
                        //El perseguidor aparece en el radar como un punto rojo o no aparece.
                        //Ya no esta en el borde del radar, cuando aparece, aparece dentro del área.
                        colliders.Add(_raycastHitArray[i].collider);
                        ActiveRadarPing(_raycastHitArray[i].point, currentRadarPing, Color.red);
                        break;
                    case 3:
                        //Empieza tras coger el tercer objetivo.
                        //El perseguidor siempre aparece en el radar como un punto rojo más cerca que antes.
                        colliders.Add(_raycastHitArray[i].collider);
                        ActiveRadarPing(_raycastHitArray[i].point, currentRadarPing, Color.red);
                        break;
                    case 4:
                        //Al llegar al último objetivo y utilizar el foco este no funciona. El objetivo se empieza a mover y es cuando la batería falla y ocurre el evento final.
                        break;
                }
            }
            //else if (_raycastHitArray[i].collider.CompareTag("Soul"))
            //{
            //    colliders.Add(_raycastHitArray[i].collider);

            //    radarPings[currentRadarPing].SetActive(true);

            //    radarPings[currentRadarPing].transform.position = _raycastHitArray[i].point;

            //    RadarPing radarPingRef = radarPings[currentRadarPing].GetComponent<RadarPing>();
            //    radarPingRef.ChangeColorOfPing(Color.green);
            //}

            PingCallback callback = _raycastHitArray[i].collider.gameObject.GetComponent<PingCallback>();
            if (callback != null)
                callback.Callback();

            if (++currentRadarPing >= radarPings.Length)
                currentRadarPing = 0;
        }
    }

    Vector3 RetrieveVectorWithAngle(float angle)
    {

        float angleInRad = angle * Mathf.Deg2Rad;

        return new Vector3(Mathf.Cos(angleInRad), 0.0f, Mathf.Sin(angleInRad));
    }

    private void PingLongDistance(float rot)
    {
        Vector3 shipRot = Ship.transform.eulerAngles;
        parentLongPing.eulerAngles = new Vector3(0.0f, 0.0f, rot + shipRot.y - 45);
        longPing.gameObject.SetActive(true);
    }
}