using UnityEngine;

public class RadarPing : MonoBehaviour
{
    private float remainingLifetime;
    private float Lifetime;
    private SpriteRenderer _radarPingRenderer;
    Color pingColor;
    // Start is called before the first frame update

    private void OnEnable()
    {
        Lifetime = 4.0f;
        remainingLifetime = Lifetime;
    }
    void Start()
    {
        _radarPingRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_radarPingRenderer) 
            _radarPingRenderer.color = pingColor;

        if (remainingLifetime <= 0) gameObject.SetActive(false);
               
        remainingLifetime -= Time.deltaTime;

        pingColor.a = Mathf.Lerp(0.0f, Lifetime, remainingLifetime);       
    }

    public void ChangeColorOfPing(Color colorToChange)
    {
        pingColor = colorToChange;       
    }
}
