using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{

    [SerializeField] GameObject timeline;
    [SerializeField] GameObject headphones;

    private bool stopDetecting = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopDetecting)
        {
            if (Input.anyKeyDown)
            {
                timeline.SetActive(true);
                headphones.SetActive(true);
                gameObject.SetActive(false);
                stopDetecting = true;
            }
        }
        

    }
}
