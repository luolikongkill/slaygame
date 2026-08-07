using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public string content;
    public string header;
    public void Setup(string str1,string str2)
    {
        content = str1;
        header = str2;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show(content,header);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }



}
