using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public TMP_Text headert;
    public TMP_Text contentt;

    public LayoutElement layoutElement;
    public ContentSizeFitter contentSizeFitter;

    public int characterWrapLimit;

    public RectTransform rect;

    public void Awake()
    {
        rect = GetComponent<RectTransform>();
    }


    public void SetText(string content, string header)
    {
        headert.text = header;
        contentt.text = content;

        int headerLength = headert.text.Length;
        int contentLength = contentt.text.Length;

        layoutElement.enabled = (headerLength > characterWrapLimit||contentLength > characterWrapLimit) ? true : false;
        contentSizeFitter.enabled = !layoutElement.enabled;
        SetPosition();

    }

    void SetPosition()
    {
        Vector2 position = Input.mousePosition;

        // float pivotX = position.x / Screen.width;
        // float pivotY = position.y / Screen.height;

        // rect.pivot = new Vector2(pivotX,pivotY);

        transform.position = position;
    }
}
