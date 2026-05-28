using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer arrowHead;
    [SerializeField] private  LineRenderer lineRenderer;
    private Vector3 startPoint;
    private void Update()
    {
        Vector3 endPosition = MouseUtil.GetMousePositionInWorldSpace();
        Vector3 direction = -(startPoint - arrowHead.transform.position).normalized;
        lineRenderer.SetPosition(1, endPosition-direction*0.5f);
        arrowHead.transform.position = endPosition;
        arrowHead.transform.right = direction;
        
    }
    public void SetupArrow(Vector3 startPosition)
    {
        this.startPoint = startPosition;
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, MouseUtil.GetMousePositionInWorldSpace());
    }

}
