using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MapLayer.cs
[System.Serializable]
public class MapLayer
{
    public int layerIndex;         // 层数
    public List<RoomNode> nodes;   // 这一层的所有节点
}
