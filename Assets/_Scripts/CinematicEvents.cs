using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CinematicEvents : MonoBehaviour
{
    private PlayableDirector director;

    private bool cinematicStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        director = GetComponent<PlayableDirector>();
        director.Pause();
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
