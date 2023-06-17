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
    }
}
