using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
public class CinematicEvents : MonoBehaviour
{

    private PlayableDirector director;

    // Start is called before the first frame update
    void Start()
    {
        director = GetComponent<PlayableDirector>();
        director.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (director.state != PlayState.Playing)
        {
            SceneManager.LoadScene(sceneBuildIndex: 1);
        }

    }
}
