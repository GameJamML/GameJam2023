using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Alert_Script : MonoBehaviour
{
    public Transform player;
    public Camera mainCamera;
    public float altura;
    private Image _image;
    public Sprite sprite1;
    public Sprite sprite2;
    private Sprite _tempSprite;
    public float timeFadeIn = 0.3f;
    private RectTransform rect;
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        rect = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        _image.sprite = sprite1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(player.position + (Vector3.up) * altura);
        rect.anchoredPosition = new Vector2(screenPoint.x, screenPoint.y) - canvas.pixelRect.size / 2f;
    }

    public void AlertAvaliable(bool active)
    {

        if (active == true)
        {
            gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(nameof(Fadein));
        }
        else
        {

            StopAllCoroutines();
            StartCoroutine(nameof(Fadeout));
        }
    }

    public void ChangeSprites(int number)
    {
        switch (number)
        {
            case 0:
                _image.sprite = sprite1;
                //StartCoroutine(nameof(ChangeSpriteCoroutine));
                break;
            case 1:
                _image.sprite = sprite2;
                //StartCoroutine(nameof(ChangeSpriteCoroutine));
                break;
            default:
                break;
        }
    }
    IEnumerator Fadein()
    {
        float elapsedTime = 0f;
        Color startColor, endColor;

        startColor = _image.color;
        endColor = new Color(1, 1, 1, 1);

        while (elapsedTime < timeFadeIn)
        {
            float t = elapsedTime / timeFadeIn;
            _image.color = Color.Lerp(startColor, endColor, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator Fadeout()
    {
        float elapsedTime = 0f;
        Color startColor, endColor;

        startColor = _image.color;
        endColor = new Color(1, 1, 1, 0);

        while (elapsedTime < timeFadeIn)
        {
            float t = elapsedTime / timeFadeIn;
            _image.color = Color.Lerp(startColor, endColor, t);

            elapsedTime += Time.deltaTime;
            yield return null;

        }
        gameObject.SetActive(false);
    }

    IEnumerator ChangeSpriteCoroutine()
    {
        Debug.Log("hello");
        // Fade out
        float elapsedTime = 0f;
        Color startColor, endColor;

        startColor = _image.color;
        endColor = new Color(1, 1, 1, 0);

        float tempFadeIn = timeFadeIn;

        while (elapsedTime < tempFadeIn)
        {
            float t = elapsedTime / tempFadeIn;
            _image.color = Color.Lerp(startColor, endColor, t);

            elapsedTime += Time.deltaTime;

            Debug.Log("elapsedTime:" + elapsedTime);
            Debug.Log("tempFadeIn:" + tempFadeIn);

            yield return null;
        }

        Debug.Log("_tempSprite.name");
        // Change image
        _image.sprite = _tempSprite;

        // Fade in
        elapsedTime = 0f;
        startColor = _image.color;
        endColor = new Color(1, 1, 1, 1);

        while (elapsedTime < tempFadeIn)
        {
            float t = elapsedTime / tempFadeIn;
            _image.color = Color.Lerp(startColor, endColor, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}

