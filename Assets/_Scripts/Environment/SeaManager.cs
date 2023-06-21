using System.Collections;
using UnityEngine;

public class SeaManager : MonoBehaviour
{
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;
    [SerializeField] private Vector4 _startTimeScales;
    [SerializeField] private Vector4 _endTimeScales;
    [SerializeField, Range(0, 1)] private float _colorMagnitude;
    [SerializeField, Range(0, 1)] private float _timeScaleMagnitude;
    private float _lastColorMagnitude;
    private float _lastTimeScaleMagnitude;
    [SerializeField] private float _transitionDuration = 1f;

    private Renderer _renderer;
    private bool toEnd = false;

    public float colorMagnitude
    {
        get
        {
            return _colorMagnitude;
        }
        set
        {
            if (value >= 0 || value <= 1)
            {
                _colorMagnitude = value;
                UpdateMaterial();
            }
        }
    }

    public float timeScaleMagnitude
    {
        get
        {
            return _timeScaleMagnitude;
        }
        set
        {
            if (value >= 0 || value <= 1)
            {
                _timeScaleMagnitude = value;
                UpdateMaterial();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // comment this func in release
        UpdateMaterial();
    }

    private void UpdateMaterial()
    {
        if (_lastColorMagnitude != _colorMagnitude)
        {
            _lastColorMagnitude = _colorMagnitude;
            Color currentColor = Color.Lerp(_startColor, _endColor, _colorMagnitude);
            _renderer.material.SetColor("_Color", currentColor);
        }
        if (_lastTimeScaleMagnitude != _timeScaleMagnitude)
        {
            _lastTimeScaleMagnitude = _timeScaleMagnitude;
            Vector4 currentTimeScale = Vector4.Lerp(_startTimeScales, _endTimeScales, _timeScaleMagnitude);
            _renderer.material.SetVector("_TimeScales", currentTimeScale);
        }
    }

    public void BeQuiet()
    {
        StopCoroutine(nameof(ChangeStateCoroutine));
        toEnd = false;
        StartCoroutine(nameof(ChangeStateCoroutine));
    }

    public void BeRought()
    {
        StopCoroutine(nameof(ChangeStateCoroutine));
        toEnd = true;
        StartCoroutine(nameof(ChangeStateCoroutine));
    }

    IEnumerator ChangeStateCoroutine()
    {
        float elapsedTime = 0f;
        Color startColor, endColor;
        Vector4 startTimeScale, endTimeScale;

        if (toEnd)
        {
            startColor = _renderer.material.color;
            endColor = _endColor;
            startTimeScale = _startTimeScales;
            endTimeScale = _endTimeScales;
        }
        else
        {
            startColor = _renderer.material.color;
            endColor = _startColor;
            startTimeScale = _endTimeScales;
            endTimeScale = _startTimeScales;
        }

        while (elapsedTime < _transitionDuration)
        {
            float t = elapsedTime / _transitionDuration;

            Vector4 currentTimeScale = Vector4.Lerp(startTimeScale, endTimeScale, t);
            Color currentColor = Color.Lerp(startColor, endColor, t);
            _renderer.material.SetColor("_Color", currentColor);
            _renderer.material.SetVector("_TimeScales", currentTimeScale);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _renderer.material.SetColor("_Color", endColor);
        _renderer.material.SetVector("_TimeScales", endTimeScale);
    }
}
