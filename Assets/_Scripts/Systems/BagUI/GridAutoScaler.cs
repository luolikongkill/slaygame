using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridAutoScaler : MonoBehaviour
{
    [Header("Origion Card Size")]
    public float designWidth = 325f;
    public float designSpacingX = 56f;
    public int columnCount = 4;


    void Start()
    {
        RectTransform parentpanel = transform.parent.GetComponent<RectTransform>();
        float availableWidth = parentpanel.rect.width;

        float totalDesignWith = columnCount * designWidth +(columnCount - 1)* designSpacingX;
        float scale = (availableWidth - 20f)/totalDesignWith;

        transform.localScale = new Vector3(scale,scale,1f);

    }
}
