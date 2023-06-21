using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBehavior : MonoBehaviour
{
    public bool isCaptured = false;
    GameObject beaconObjective;
    Enemy enemy;

    float movingSpeed = 0.02f;

    private void Awake()
    {
        enemy = FindObjectOfType<Enemy>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!isCaptured)
            return;

        Vector3 direction = beaconObjective.transform.position - transform.position;
        direction.Normalize();
        transform.position += direction * movingSpeed; 

        if (Vector3.Distance(transform.position, beaconObjective.transform.position) <= 1.0f)
        {
            isCaptured = false;
            gameObject.SetActive(false);
            enemy.ProgressState();
        }
    }

    public void Capture(GameObject beacon)
    {
        if (isCaptured)
            return;

       

        beaconObjective = beacon;
        isCaptured = true;
    }
}
