// MapView.cs
using System.Collections.Generic;
using UnityEngine;

public class MapView : MonoBehaviour
{
    [SerializeField] private Transform mapContainer;
    [SerializeField] private MapNodeUI nodePrefab; // 每层节点数量
    [SerializeField] private float layerSpacing = 150f; // 层与层之间的间距
    [SerializeField] private float nodeSpacing = 100f;  // 节点之间的水平间距

    private List<MapNodeUI> spawnedNodes = new List<MapNodeUI>();

    // 显示地图
    public void ShowMap()
    {
        ClearMap();
        GenerateMapUI();
        gameObject.SetActive(true);
    }

    // 生成地图UI,初始化
    private void GenerateMapUI()
    {
        foreach (MapLayer layer in MapManager.Instance.mapLayers)
        {
            foreach (RoomNode node in layer.nodes)
            {
                // 计算位置
                Vector3 position = new Vector3(  
                    node.y * layerSpacing,
                    node.x * nodeSpacing - (MapManager.Instance.nodesPerLayer * nodeSpacing / 2),
                    0
                );

                // 实例化节点

                MapNodeUI nodeUI = Instantiate(nodePrefab, mapContainer);
                nodeUI.transform.localPosition = position;
                nodeUI.NodeInit(node, OnNodeClicked);//**初始化节点UI，传入节点数据和点击回调*/
                spawnedNodes.Add(nodeUI);
                // Debug.Log("地图节点生成中");
            }       
        }
        // GenerateConnections();
        Debug.Log("地图生成完成");
    }
    private void GenerateConnections()
    {
        foreach (MapLayer layer in MapManager.Instance.mapLayers)//每层
        {
            foreach (RoomNode node in layer.nodes)//每点
            {
                foreach (int connectionIndex in node.connections)
                {
                    RoomNode connectedNode = MapManager.Instance.mapLayers[node.y + 1].nodes[connectionIndex];//下一个连接节点
                    // 在UI上绘制连接线
                    MapNodeUI nodeUI = spawnedNodes.Find(n => n.nodeData.x == node.x && n.nodeData.y == node.y);//的ui
                    MapNodeUI connectedNodeUI = spawnedNodes.Find(n => n.nodeData.x == connectedNode.x && n.nodeData.y == connectedNode.y);
                    if (nodeUI != null && connectedNodeUI != null)
                    {
                        // 创建连接线（可以是LineRenderer或者其他方式）
                        NodeConnectionUI connectionUI = new NodeConnectionUI();
                        connectionUI.SetupConnection(nodeUI, connectedNodeUI);
                    }
                }
            }
        }
    }

    // 点击节点
    private void OnNodeClicked(MapNodeUI nodeUI)
    {
        LayerNodeUpdate(nodeUI);
        //更新本层所有节点属性ui

        // 更新下一层可访问的节点
        UpdateAccessibleNodes(nodeUI);

        // 根据房间类型跳转
        EnterRoom(nodeUI.nodeData);

    }
    private void OnAccessNode(MapNodeUI nodeUI)
    {
        UpdateAccessibleNodes(nodeUI);
    }

    // 更新可访问节点，应该改为当前房间完成再更新，
    private void UpdateAccessibleNodes(MapNodeUI nodeUI)
    {
        Debug.Log($"更新可访问节点，当前节点：{nodeUI.nodeData.x}, {nodeUI.nodeData.y}, 可访问节点数: {nodeUI.nodeData.connections.Count}");
        // Debug.Log($"当前节点连接的下一层节点索引: {string.Join(", ", nodeUI.nodeData.connections)}");
        MapLayer nextLayer = MapManager.Instance.mapLayers[nodeUI.nodeData.y + 1];
        foreach (int nextIndex in nodeUI.nodeData.connections)
        {
            nextLayer.nodes[nextIndex].isAccessible = true;
            CurrentNodeUpdate(spawnedNodes.Find(n => n.nodeData.x == nextLayer.nodes[nextIndex].x && n.nodeData.y == nextLayer.nodes[nextIndex].y));
        }
    }

    // 进入房间
    private void EnterRoom(RoomNode node)
    {
        switch (node.roomType)
        {
            case RoomType.Normal:
            case RoomType.Elite:
            case RoomType.Boss:
                // 跳转到战斗场景，传入敌人配置
                MatchSetupSystem.Instance.GameStart();
                //**地图系统通信战斗系统，连接还没做，
                //此处传送房间类型，战斗系统根据房间类型生成对应的敌人，但是怪池子还没做，先不填房间type
                break;
            // case RoomType.Rest:
            //     // 打开休息界面
            //     RestView.Instance.Show();
            //     break;
            // case RoomType.Shop:
            //     // 打开商店界面
            //     ShopView.Instance.Show();
            //     break;
            // case RoomType.Event:
            //     // 打开事件界面
            //     EventView.Instance.Show();
            //     break;
        }
        Debug.Log($"进入了{node.roomType}房间");
        UIChangeSet.Instance.UIChange(2);// 切换到战斗界面
    }

    private void ClearMap()
    {
        foreach (var node in spawnedNodes)
        {
            Destroy(node.gameObject);
        }
        spawnedNodes.Clear();
    }
    private void CurrentNodeUpdate(MapNodeUI nodeUI)

    {
        //使节点node ui刷新
        nodeUI.visitedindicator.enabled = !nodeUI.nodeData.isAccessible;
        nodeUI.Nodebutton.interactable = nodeUI.nodeData.isAccessible && !nodeUI.nodeData.isVisited;


    }
    private void LayerNodeUpdate(MapNodeUI nodeUI)
    {
        
        //使本层所有节点node 属性ui刷新
        MapLayer CurrentLayer = MapManager.Instance.mapLayers[nodeUI.nodeData.y ];
        Debug.Log($"更新本层节点，节点数: {CurrentLayer.nodes.Count}");
        foreach (var node in CurrentLayer.nodes)
        {
            node.isAccessible = false;
            node.isVisited = false;
            CurrentNodeUpdate(spawnedNodes.Find(n => n.nodeData.x == node.x && n.nodeData.y == node.y));
        }
        nodeUI.nodeData.isVisited = true;
        nodeUI.accessibleindicator.enabled = nodeUI.nodeData.isVisited;
        
    }

}