using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RoomNode.cs
[System.Serializable]
public class RoomNode
{
    public int x;                  // 列坐标（每层从左到右）
    public int y;                  // 层坐标（从下到上，0是第一层）
    public RoomType roomType;      // 房间类型
    public bool isVisited;         // 是否已访问过
    public bool isAccessible;      // 当前是否可点击
    public List<int> connections;  // 连接的下一层节点索引
}