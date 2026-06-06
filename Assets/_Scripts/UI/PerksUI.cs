using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerksUI : MonoBehaviour
{
    [SerializeField] private PerkUI perkUIPrefab;
    public readonly List<PerkUI> perkUIs = new List<PerkUI>();
    public void AddPerkUI(Perk perk)
    {
        PerkUI perkUI = Instantiate(perkUIPrefab, transform);
        perkUI.Setup(perk);
        perkUIs.Add(perkUI);
    }
    public void RemovePerkUI(Perk perk)
    {
        var perkUI = perkUIs.Find(p => p.Perk == perk);
        if (perkUI != null)
        {
            perkUIs.Remove(perkUI);
            Destroy(perkUI.gameObject);
        }
    }
    public void ClearAll()
    {
        int destroycount = 0;
        for(int i = perkUIs.Count - 1; i >= 0; i--)
        {
            Destroy(perkUIs[i].gameObject);
            destroycount++;
        }
        perkUIs.Clear();
        Debug.Log("销毁了" + destroycount + "个PerkUI");
    }

}
    



