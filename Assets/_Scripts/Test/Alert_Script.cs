using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert_Script : MonoBehaviour
{
    public Transform target_to_Camera;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(gameObject.transform.position - target_to_Camera.position);

        transform.position = new Vector3 (player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z );
    }

    public void AlertAvaliable(bool active){

        if (active == true)
        {
            gameObject.SetActive(true);
        }
       else
        {
            gameObject.SetActive(false);
        }
    }

}
