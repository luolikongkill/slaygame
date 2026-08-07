using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem cur;

    public Tooltip tooltip;

    private void Awake()
    {
        cur =this;
    }

    public  static void Show(string conteng, string header)
    {
        cur.tooltip.SetText(conteng, header);
        cur.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        cur.tooltip.gameObject.SetActive(false);
    }
}
