using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FlagController : MonoBehaviour
{
    public float controllingSpeed = 0.5f;
    public float flagHeight = 0.0f;
    private ShipController _ship;
    [SerializeField] private Transform _flagTop;
    [SerializeField] private Transform _flagBottom;
    [SerializeField] private Transform _smallFlagBottom;
    [SerializeField] private Renderer _flagRenderer;
    [SerializeField] private Renderer _flagRenderer2;

    [SerializeField] private Renderer _smallFlagRenderer;
    [SerializeField] private Renderer _smallFlagRenderer2;

    [SerializeField] private string _flagTestureName = "_BaseMap";
    private int _texturePropertyID;
    private float _moveDistance;
    private Vector3 _flagBottomStartPos;


    void Awake()
    {
        _ship = FindObjectOfType<ShipController>();
        _ship.speedMagnitude = flagHeight;

        _flagBottomStartPos = _flagBottom.position;
        _texturePropertyID = Shader.PropertyToID(_flagTestureName);
        _moveDistance = _flagTop.position.y - _flagBottomStartPos.y;
        Debug.Log("ID" + _texturePropertyID);
        UpdateFlag();
    }

    // Update is called once per frames
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (flagHeight < 1.0f)
                flagHeight += controllingSpeed * Time.deltaTime;
            else
                flagHeight = 1.0f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (flagHeight > 0)
                flagHeight -= controllingSpeed * Time.deltaTime;
            else
                flagHeight = 0;
        }
        UpdateFlag();
    }

    public void FlagCrash()
    {
        StartCoroutine("FlagCrashCoroutine");
    }

    IEnumerator FlagCrashCoroutine()
    {
        while (flagHeight > 0)
        {
            flagHeight -= 0.04f;
            UpdateFlag();
            _ship.speedMagnitude = 0;
            yield return new WaitForSeconds(0.05f);
        }

    }

    public void UpdateFlag()
    {
        _ship.speedMagnitude = flagHeight;

        _flagBottom.position = new Vector3(_flagBottom.position.x, _flagBottomStartPos.y + _moveDistance * (1 - flagHeight), _flagBottom.position.z);
        _smallFlagBottom.position = new Vector3(_flagBottom.position.x, _flagBottomStartPos.y + _moveDistance * (1 - flagHeight), _flagBottom.position.z);

        _flagRenderer.material.SetTextureOffset(_texturePropertyID, new Vector2(0, 1 - (flagHeight)));
        _flagRenderer2.material.SetTextureOffset(_texturePropertyID, new Vector2(0, 1 - (flagHeight)));

        _smallFlagRenderer.material.SetTextureOffset(_texturePropertyID, new Vector2(0, 1 - (flagHeight)));
        _smallFlagRenderer2.material.SetTextureOffset(_texturePropertyID, new Vector2(0, 1 - (flagHeight)));
    }
}
