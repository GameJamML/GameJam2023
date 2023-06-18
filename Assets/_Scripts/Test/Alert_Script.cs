using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert_Script : MonoBehaviour
{
    public Transform target_Camera;
    public GameObject Player;
    public bool Active = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(gameObject.transform.position - target_Camera.position);

        transform.position = new Vector3 ( Player.transform.position.x, Player.transform.position.y + 1.5f, Player.transform.position.z );
        
    }

    public void AlertAvaliable(bool active){
        Debug.Log("enterssssss");
        if (active == true)
        {
            gameObject.SetActive(true);
            Debug.Log("enter2");
        }
       else
        {
            gameObject.SetActive(false);
            Debug.Log("enter3");
        }
    }

}
