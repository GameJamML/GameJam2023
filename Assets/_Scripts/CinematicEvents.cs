using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum CinematicName
{
    Opening = 0,
    Ending
}

public class CinematicEvents : MonoBehaviour
{

    [SerializeField] private CinematicName cinematicName = CinematicName.Opening;

    private PlayableDirector director;

    private bool cinematicStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        director = GetComponent<PlayableDirector>();

        switch(cinematicName)
        {
            case CinematicName.Opening:
                director.Pause();
                break;
            case CinematicName.Ending:
                director.Play();
                break;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        switch (cinematicName)
        {
            case CinematicName.Opening:
                if (cinematicStarted)
                {
                    if (director.state != PlayState.Playing)
                    {
                        SceneManager.LoadScene(sceneBuildIndex: 1);
                    }
                }
                else
                {
                    if (Input.anyKeyDown)
                    {
                        director.Play();
                        cinematicStarted = true;
                    }
                }
                break;
            case CinematicName.Ending:
                if (cinematicStarted)
                {
                    if (director.state != PlayState.Playing)
                    {
                        Application.Quit();
                    }
                }
                break;
        }

        
    }
}
