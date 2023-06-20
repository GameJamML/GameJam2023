using UnityEngine;

public class Waves : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float frequency = 1f;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.localPosition;
    }

    private void Update()
    {
        float newY = startPos.y + amplitude * Mathf.Sin(frequency * Time.time);
        transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
        
        // Modificador Oleatge per Keys (Temporal)

        //if (Input.GetKeyDown(KeyCode.I)) levelWaves(1);
        //if (Input.GetKeyDown(KeyCode.O)) levelWaves(2);
        //if (Input.GetKeyDown(KeyCode.P)) levelWaves(3);

    }

    public void levelWaves(int level)
    {
        switch (level)
        {
            case 1:
                amplitude = 1f;
                frequency = 2f;
                break;
            case 2:
                amplitude = 1.5f;
                break;
            case 3:
                amplitude = 2f;
                frequency = 3f;
                break;
            default:
                break;
        }

    }
}
