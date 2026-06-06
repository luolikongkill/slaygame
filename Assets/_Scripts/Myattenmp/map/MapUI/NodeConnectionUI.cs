using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeConnectionUI : MonoBehaviour
{
 [SerializeField] private Transform mapContainer;  // Canvas 下的父物体，用于存放连线
    [SerializeField] private Sprite lineSprite;       // 上面的白色方块 Sprite
    [SerializeField] private float lineWidth = 3f;    // 线条宽度（像素）

    public void SetupConnection(MapNodeUI from, MapNodeUI to)
    {
        RectTransform fromRect = from.GetComponent<RectTransform>();
        RectTransform toRect = to.GetComponent<RectTransform>();
        
        // 获取两个 UI 元素相对于 Canvas 的锚点位置（假设 Canvas 的 Render Mode 为 Overlay）
        Vector2 fromPos = fromRect.anchoredPosition;
        Vector2 toPos = toRect.anchoredPosition;
        
        Vector2 dir = toPos - fromPos;
        float distance = dir.magnitude;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        
        // 创建 Image 对象作为线条
        GameObject lineObj = new GameObject("Line");
        lineObj.transform.SetParent(mapContainer, false); // false 保持本地坐标
        Image lineImage = lineObj.AddComponent<Image>();
        lineImage.sprite = lineSprite;
        lineImage.color = Color.white; // 可改为其他颜色
        
        RectTransform rect = lineObj.GetComponent<RectTransform>();
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, distance);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, lineWidth);
        rect.anchoredPosition = fromPos + dir * 0.5f;
        rect.localRotation = Quaternion.Euler(0, 0, angle);
    }
}

