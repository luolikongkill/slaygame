// MapManager.cs
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
//以后再自定义地图数据

    [Header("地图配置")]
    public int totalLayers = 15;   // 总层数（杀戮尖塔是15层）
    public int nodesPerLayer = 4;  // 每层最多4个节点
    public int bossLayerInterval = 5; // 每5层一个BOSS

    public List<MapLayer> mapLayers; // 生成好的完整地图
    public int currentLayer = 0;     // 当前所在层数
    public int currentNodeIndex = 0; // 当前所在节点索引



    // 生成完整地图（后面第二步实现）
    public void GenerateFullMap()
{
    mapLayers = new List<MapLayer>();

    for (int y = 0; y < totalLayers; y++)
    {
        MapLayer layer = new MapLayer();
        layer.layerIndex = y;
        layer.nodes = new List<RoomNode>();

        // 1. 生成这一层的节点数量（2-4个）
        int nodeCount = Random.Range(2, 5);

        // 2. 随机分配x坐标（均匀分布）
        List<int> xPositions = new List<int>();
        for (int i = 0; i < nodeCount; i++)
        {
            int x;
            do
            {
                x = Random.Range(0, nodesPerLayer);
            } while (xPositions.Contains(x));
            xPositions.Add(x);
        }
        xPositions.Sort();

        // 3. 创建节点并分配类型
        for (int i = 0; i < nodeCount; i++)
        {
            RoomNode node = new RoomNode();
            node.x = xPositions[i];
            node.y = y;
            node.connections = new List<int>();
            node.isVisited = false;
            node.isAccessible = (y == 0); // 第一层都可点击

            // 分配房间类型
            if ((y + 1) % bossLayerInterval == 0)
            {
                node.roomType = RoomType.Boss; // BOSS层只有一个BOSS房间
                nodeCount = 1; // BOSS层强制只有1个节点
            }
            else if (y == 0)
            {
                node.roomType = RoomType.Normal; // 第一层都是普通战斗
            }
            else
            {
                // 随机分配：普通70%，精英15%，休息10%，事件5%，以后再分配这个功能
                // float rand = Random.value;
                // // if (rand < 0.7f) node.roomType = RoomType.Normal;
                // // else if (rand < 0.85f) node.roomType = RoomType.Elite;
                // // else if (rand < 0.95f) node.roomType = RoomType.Rest;
                // // else node.roomType = RoomType.Event;
                node.roomType = RoomType.Normal; // 先全部设置为普通战斗，后面再细分
            }

            layer.nodes.Add(node);
        }

        mapLayers.Add(layer);
    }

    // 4. 生成层与层之间的连接
    GenerateConnections();
    Debug.Log("地图生成完成！");
}

// 生成节点连接
private void GenerateConnections()
{
    for (int y = 0; y < totalLayers - 1; y++)//对每个层进行连接生成，最后一层不需要连接
    {
        MapLayer currentLayer = mapLayers[y];
        MapLayer nextLayer = mapLayers[y + 1];
        int counter = 0;

        foreach (RoomNode currentNode in currentLayer.nodes)//对当前层的每个节点进行连接生成
        {
            counter++;
            // 每个节点连接下一层1-2个节点
            int connectionCount = Random.Range(1, 3);

            // 优先连接x坐标相近的节x
            List<int> possibleIndices = new List<int>();
            for (int i = 0; i < nextLayer.nodes.Count; i++)
            {
                if (Mathf.Abs(nextLayer.nodes[i].x - currentNode.x) <= 4) // x坐标相差不超过3
                {
                    possibleIndices.Add(i);
                }
            }
            if(possibleIndices.Count == 0&&counter>1)
            {
                Debug.LogWarning($" ({currentNode.x}, {currentNode.y})possibleIndices 0 ");
                var preNode = currentLayer.nodes[counter-2];
                if(preNode.connections.Count-1<0)
                    {
                        Debug.LogWarning("preNode.connections.Count-1");
                        break;

                    }
                else 
                {int lastConnection = preNode.connections[preNode.connections.Count-1];
                currentNode.connections.Add(lastConnection);
                }

            }

            // 随机选择连接
            for (int i = 0; i < (int)Random.Range(1, possibleIndices.Count)&&possibleIndices.Count>0;  i++)
            {
                currentNode.connections.Add(possibleIndices[i]);
            }
            
        }
    }
    Debug.Log("连接生成完成");
}
}