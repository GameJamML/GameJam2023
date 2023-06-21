using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alert_Script : MonoBehaviour
{
    public Transform player;
    public Camera camera;
    public float altura;
    private Image _image;
    public Sprite sprite1;
    public Sprite sprite2;
    public float timeFadeIn = 0.3f;
    private RectTransform rect;
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
       rect.position = camera.WorldToScreenPoint(player.position + (Vector3.up)* altura);
    }

    public void AlertAvaliable(bool active){

        if (active == true)
        {
            gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(nameof (Fadein));
        }
       else
        {

            StopAllCoroutines();
            StartCoroutine(nameof(Fadeout));
        }
    }

    public void ChangeSprites(int number)
    {
        switch (number )
        {
            case 0:
                _image.sprite = sprite1;
                break;
            case 1:
                _image.sprite = sprite2;
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
}

