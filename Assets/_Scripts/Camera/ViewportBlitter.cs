using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[ExecuteAlways]
public class ViewportBlitter : MonoBehaviour
{
    [SerializeField] private UniversalRendererData rendererData = null;
    [SerializeField] public Camera renderCam;


    [SerializeField]
    public Vector2Int referenceResolution = new Vector2Int(640, 360);


    public int pixelMargin = 4;
    public float orthographicSize;
    public Vector2Int cameraResolution;
    private Vector2 pixelValue;
    public Vector2 renderOffsetInPixels;

    private PixelizeFeature pixelFeature;

    [Header("DEBUG ONLY")]
    [SerializeField]
    [Range(-1.0f, 1.0f)]
    private float marginX = 0;
    [SerializeField]
    [Range(-1.0f, 1.0f)]
    private float marginY = 0;

    [SerializeField]
    [Range(-1.0f, 1.0f)]
    private float pixelScaleX = 1;
    [SerializeField]
    [Range(-1.0f, 1.0f)]
    private float pixelScaleY = 1;

    void Start()
    {
        SetFeature();
    }

    void Update()
    {
        ReCenter();
    }

    private void ReCenter()
    { 
        //X AXIS
        if (renderOffsetInPixels.x >= 1 || renderOffsetInPixels.x <= -1)
        {
            float value = Mathf.Sign(renderOffsetInPixels.x) == -1 ? 1 : -1;

            renderOffsetInPixels.x += value;
            renderCam.transform.Translate((pixelValue.y * 10.0f) * -value, 0, 0);
        }

        //Y AXIS
        if (renderOffsetInPixels.y >= 1 || renderOffsetInPixels.y <= -1)
        {
            float value = Mathf.Sign(renderOffsetInPixels.y) == -1 ? 1 : -1;

            renderOffsetInPixels.y += value;
            renderCam.transform.Translate(0, (pixelValue.y * 10.0f) * -value, 0);
        }
    }
    public void PanViewport(float x, float y)
    {
        renderOffsetInPixels.x += x;
        renderOffsetInPixels.y -= y;

        marginX = pixelValue.x * ((pixelMargin / 2.0f) + renderOffsetInPixels.x);
        marginY = pixelValue.y * ((pixelMargin / 2.0f) + renderOffsetInPixels.y);


        SetMargin();
        //Debug.Log((pixelValue.y * 10) + ", "+marginY);
    }

    void CalculatePixelValue()
    {
        orthographicSize = renderCam.orthographicSize;

        pixelValue = new Vector2((float)((orthographicSize * 2.0f) / cameraResolution.x) * 0.10f, (float)((orthographicSize * 2.0f) / cameraResolution.y) * 0.10f);


    }

    void CameraCenterAndScale()
    {
        //Scale
        pixelScaleX = 1 - (pixelValue.x * pixelMargin);
        pixelScaleY = 1 - (pixelValue.y * pixelMargin);

        //Center
        marginX = pixelValue.x * (pixelMargin / 2.0f);
        marginY = pixelValue.y * (pixelMargin / 2.0f);
        //Debug.Log(marginX + ", " + marginY);
    }

    /*
     * RENDER FEATURE
     */
    private void SetFeature()
    {
        cameraResolution = new Vector2Int(referenceResolution.x + pixelMargin, referenceResolution.y + pixelMargin);
        pixelFeature.settings.cameraResolution = cameraResolution;
        pixelFeature.settings.screenResolution = new Vector2Int(Screen.width+ 12, Screen.height + 12);
        CalculatePixelValue();
        CameraCenterAndScale();
        //if (pixelFeature == null) return;


        //cameraResolution = new Vector2Int(referenceResolution.x, referenceResolution.y);
        pixelFeature.settings.margin = new Vector2(marginX, marginY);
        pixelFeature.settings.scale = new Vector2(pixelScaleX, pixelScaleY);
    }

    private void SetMargin()
    {
        pixelFeature.settings.margin = new Vector2(marginX, marginY);
    }

    private void TryGetFeature()
    {
        if (pixelFeature != null) return;

        ScriptableRendererFeature feature = rendererData.rendererFeatures.Find((f) => f.name == "PixelizeFeature");

        if(feature != null)
        {
            pixelFeature = feature as PixelizeFeature;
        }
    }

    private void OnValidate()
    {

        TryGetFeature();
        SetFeature();
        //Debug.Log("SUP0");
        rendererData.SetDirty();
    }

}
