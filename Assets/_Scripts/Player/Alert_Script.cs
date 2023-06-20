using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert_Script : MonoBehaviour
{
    public Transform player;
    public Camera camera;
    public float altura;

    private RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        rect = GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {
        rect.position = camera.WorldToScreenPoint(player.position + (Vector3.up)* altura);
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
