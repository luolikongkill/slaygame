using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FadeAnimManager : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public GameObject[] panel;

    public void Init()
    {
        for (int i = 0; i < panel.Length; i++)
        {
            panel[i].SetActive(false);
        }
    }

    public void PlayAnim(int index)
    {
        panel[index].SetActive(true);
        panel[index].GetComponent<Animator>().Play("anim");
    }
}
