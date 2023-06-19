using UnityEngine;
public class FlagController : MonoBehaviour
{
    public float controllingSpeed = 0.5f;
    private float _flagHeight = 0.0f;
    private ShipController _ship;
    [SerializeField] private Transform _flagTop;
    [SerializeField] private Transform _flagBottom;
    [SerializeField] private Renderer _flagRenderer;
    [SerializeField] private string _flagTestureName = "_BaseMap";
    private int _texturePropertyID;
    private float _moveDistance;
    private Vector3 _flagBottomStartPos;

    void Awake()
    {
        _ship = FindObjectOfType<ShipController>();
        _ship.speedMagnitude = _flagHeight;
    }

    void Start()
    {
        _flagBottomStartPos = _flagBottom.position;
        _texturePropertyID = Shader.PropertyToID(_flagTestureName);
        _moveDistance = _flagTop.position.y - _flagBottomStartPos.y;
        Debug.Log("ID" + _texturePropertyID);

        /*
                string[] texturePropertyNames = _flagRenderer.material.GetTexturePropertyNames();

                // 输出纹理属性名称到控制台
                foreach (string propertyName in texturePropertyNames)
                {
                    Debug.Log("Texture Property Name: " + propertyName);
                }
        */
    }

    // Update is called once per frames
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (_flagHeight < 1.0f)
                _flagHeight += controllingSpeed * Time.deltaTime;
            else
                _flagHeight = 1.0f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (_flagHeight > 0)
                _flagHeight -= controllingSpeed * Time.deltaTime;
            else
                _flagHeight = 0;
        }
        _ship.speedMagnitude = _flagHeight;

        _flagBottom.position = new Vector3(_flagBottom.position.x, _flagBottomStartPos.y + _moveDistance * (1 - _flagHeight), _flagBottom.position.z);

        _flagRenderer.material.SetTextureOffset(_texturePropertyID, new Vector2(0, 1 - (_flagHeight)));
    }
}
