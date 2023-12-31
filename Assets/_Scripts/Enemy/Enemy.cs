using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _currentState = 0;
    [SerializeField] private float[] _statePos;

    SphereCollider collider;

    public int phaseSubstate = -1;

    public float counter = 0.0f;

    MastilLights mastil;

    ShipController shipController;

    SeaBehavior sea;

    bool firstCrashSound = false;

    public AudioSource ambientAudioSource;
    public AudioClip stalkingClip1;

    public GameObject finalWalls;
    public GameObject[] wallswalls;
    public GameObject blackImage;

    bool teleportedPlayer = false;

    public int currentState
    {
        get
        {
            return _currentState;
        }
        set
        {
            if (value >= 0 && value <= 4)
            {
                _currentState = value;
                phaseSubstate = 0;
            }
        }
    }

    private void Awake()
    {
        collider = GetComponent<SphereCollider>();
        mastil = FindObjectOfType<MastilLights>();
        shipController = FindObjectOfType<ShipController>();
        sea = FindObjectOfType<SeaBehavior>();
    }

    void Update()
    {
        StateBehavior();
        if (Input.GetKeyDown(KeyCode.L))
        {
            ProgressState();
        }
    }

    public void ProgressState()
    {
        _currentState++;
        phaseSubstate = 0;
        counter = 0.0f;

    }

    public void StateBehavior()
    {
        switch (_currentState)
        {
            case 0:
                //No se ha cogido ningún objetivo.
                //El perseguidor no aparece en el radar.
                //No hay ningún sonido de tensión
                collider.enabled = false;
                break;
            case 1:
                {
                    //Empieza tras coger el primer objetivo, al cogerlo se emite un sonido de tensión(un rugido, algo estridente, )
                    //La primera vez que le das al radar la criatura aparece como un punto rojo en la lejanía, las siguientes pueden ser punto rojo, verde o ninguno.
                    //Hay un sonido continuo de tensión
                    collider.enabled = true;
                    
                    switch (phaseSubstate)
                    {
                        case 0:
                            transform.localPosition = new Vector3(0, 0, -180);
                            break;
                        case 1:
                            if (counter >= 1.0f)
                            {
                                transform.localPosition = new Vector3(0, 0, -110);
                            }
                            else counter += Time.deltaTime;
                            break;
                        case 2:
                            collider.enabled = false;
                            break;
                    }

                    break;
                }
            case 2:
                //Empieza tras coger el segundo objetivo.
                //El perseguidor aparece en el radar como un punto rojo o no aparece.
                //Ya no esta en el borde del radar, cuando aparece, aparece dentro del área.
                collider.enabled = true;

                switch (phaseSubstate)
                {
                    case 0:
                        transform.localPosition = new Vector3(0, 0, -160);
                        mastil.ActivateLights(12);
                        break;
                    case 1:
                        if (counter >= 1.0f)
                        {
                            transform.localPosition = new Vector3(0, 0, -90);
                        }
                        else counter += Time.deltaTime;
                        break;
                    case 2:
                        collider.enabled = false;
                        break;
                }

                break;
            case 3:
                collider.enabled = true;
                //Empieza tras coger el tercer objetivo.
                //El perseguidor siempre aparece en el radar como un punto rojo más cerca que antes.
                switch (phaseSubstate)
                {
                    case 0:
                        transform.localPosition = new Vector3(0, 0, -140);
                        mastil.ActivateLights(16);
                        sea.ChangeRoughness(0.5f, 14.0f);
                        break;
                    case 1:
                        if (counter >= 1.0f)
                        {
                            transform.localPosition = new Vector3(0, 0, -90);
                        }
                        else counter += Time.deltaTime;
                        break;
                    case 2:
                        if (counter >= 1.0f)
                        {
                            transform.localPosition = new Vector3(0, 0, -40);
                            ambientAudioSource.clip = stalkingClip1;
                            if (!ambientAudioSource.isPlaying)
                                ambientAudioSource.Play();
                        }
                        else counter += Time.deltaTime;
                        break;
                    case 3:
                        if (counter >= 2.0f && !firstCrashSound)
                        {
                            firstCrashSound = true;
                            collider.enabled = false;
                            shipController.CrashShip();
                        }
                        else counter += Time.deltaTime;
                        break;
                    case 4:
                        collider.enabled = false;
                        break;
                }
                break;
            case 4:
                // Desactivar todas las paredes
                // Activar paredes final
                collider.enabled = true;
                switch (phaseSubstate)
                {
                    case 0:
                        if (teleportedPlayer)
                            return;
                        GameObject[] walls = GameObject.FindGameObjectsWithTag("PhysicalWall");

                        foreach (var wall in walls)
                        {
                            wall.SetActive(false);
                        }

                        GameObject shipGO = FindObjectOfType<PlayerController>().gameObject;

                        finalWalls.transform.position = new Vector3(shipGO.transform.position.x, shipGO.transform.position.y-4.0f, shipGO.transform.position.z);

                        foreach (var wall in wallswalls)
                        {
                            wall.gameObject.SetActive(true);
                        }
                        
                        teleportedPlayer = true;

                        mastil.ActivateLights(18);
                        sea.ChangeRoughness(1.0f, 32.0f);
                        sea.ChangeColorProgressively(1.0f);
                        transform.localPosition = new Vector3(0, 0, -140);

                        break;
                    case 1:
                        if (counter >= 1.0f)
                        {
                            transform.localPosition = new Vector3(0, 0, -90);
                            ambientAudioSource.clip = stalkingClip1;
                            if (!ambientAudioSource.isPlaying)
                                ambientAudioSource.Play();
                        }
                        else counter += Time.deltaTime;
                        break;
                    case 2:
                        if (counter >= 1.0f)
                        {
                            transform.localPosition = new Vector3(0, 0, -40);
                        }
                        else counter += Time.deltaTime;
                        break;
                    case 3:
                        if (counter >= 2.0f && !firstCrashSound)
                        {
                            firstCrashSound = true;
                            collider.enabled = false;
                            shipController.CrashShip();
                            blackImage.SetActive(true);
                            StartCoroutine("ChangeToFinalScene");

                        }
                        else counter += Time.deltaTime;
                        break;
                }
                break;
        }
    }

    IEnumerator ChangeToFinalScene()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(sceneBuildIndex: 2);
    }
}
