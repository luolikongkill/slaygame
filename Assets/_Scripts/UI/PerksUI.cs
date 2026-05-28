using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerksUI : MonoBehaviour
{
    [SerializeField] private PerkUI perkUIPrefab;
    private readonly List<PerkUI> perkUIs = new List<PerkUI>();
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
}
