using UnityEngine;

public class ClothPointConnector : MonoBehaviour
{
    public GameObject[] connectedObjects; // 存储要连接的游戏对象
    public int[] connectedVertices; // 存储要连接的布料顶点的索引

    private Cloth clothComponent; // 布料组件的引用

    void Start()
    {
        clothComponent = GetComponent<Cloth>(); // 获取布料组件的引用

        // 确保连接的游戏对象和布料顶点索引数组长度一致
        if (connectedObjects.Length != connectedVertices.Length)
        {
            Debug.LogError("连接的游戏对象和顶点索引数组长度不一致！");
            return;
        }

        // 遍历连接的游戏对象和顶点索引数组
        for (int i = 0; i < connectedObjects.Length; i++)
        {
            GameObject connectedObject = connectedObjects[i];
            int vertexIndex = connectedVertices[i];

            // 检查布料顶点索引是否在有效范围内
            if (vertexIndex < 0 || vertexIndex >= clothComponent.vertices.Length)
            {
                Debug.LogError("顶点索引超出布料范围！");
                continue;
            }

            // 将布料顶点连接到游戏对象
            clothComponent.vertices[vertexIndex] = connectedObject.transform.position;
            clothComponent.SetEnabledFading(false);
        }

        // 更新布料
        //clothComponent.vertices = clothComponent.vertices;
    }
}
