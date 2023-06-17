using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;

public class IngameDebug : MonoBehaviour
{
    //Text serializebles
    [SerializeField] TMP_Text textTMP;

    //Unity Screen Render Data singleton
    [SerializeField] private ViewportBlitter viewport;

    //Data Storage
    private Vector2 screenResolution;
    private Vector2Int referenceResolution;
    private Vector2 cameraResolution;
    private Vector2 renderOffsetInPixels;
    private float orthographicSize;
    private float pixelSize;

    //Post Processing feature

    void Start()
    {

    }

    void Update()
    {
        GetData();
        UpdateText();

    }

    private void GetData()
    {
        screenResolution = new Vector2(Screen.width, Screen.height);
        referenceResolution = viewport.referenceResolution;
        cameraResolution = viewport.cameraResolution;
        renderOffsetInPixels = viewport.renderOffsetInPixels;
        orthographicSize = viewport.orthographicSize;
        pixelSize = screenResolution.y / referenceResolution.y;
    }

    private void UpdateText()
    {
        textTMP.text = "Screen Resolution = " + screenResolution.x + "x" + screenResolution.y + "\n";
        textTMP.text += "Reference Resoloution = " + referenceResolution.x + "x" + referenceResolution.y + "\n";
        textTMP.text += "Camera Resolution = " + cameraResolution.x + "x" + cameraResolution.y + "\n";
        textTMP.text += "Render Offset In Pixels = " + renderOffsetInPixels + "\n";
        textTMP.text += "Orthographic Size = " + orthographicSize + "\n";
        textTMP.text += "Pixel Size = " + pixelSize;
    }

}
