using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkSystem : Singleton<PerkSystem>
{
    [SerializeField] public PerksUI perksUI;
    public readonly List<Perk> perks = new ();
    public void AddPerk(Perk perk)
    {
        perks.Add(perk);
        perksUI.AddPerkUI(perk);
        perk.OnAdd();
    }
    public void RemovePerk(Perk perk)
    {
        perk.OnRemove();
        perks.Remove(perk);
        perksUI.RemovePerkUI(perk);
        
    }
    public void Setup(List<PerkData> perkDatas)
    {
        foreach (var perkData in perkDatas)
        {
            AddPerk(new Perk(perkData));
        }
    }
    public void Reset()
    {
        ClearAllPerks();   
        Debug.Log("PerkSystem has been reset.");
    }
    public void ClearAllPerks()
    {
        Debug.Log("perks.count before reset: " + perks.Count);
        for(int i = perks.Count - 1; i >= 0; i--)
        {
            RemovePerk(perks[i]);
        }
        perks.Clear();
        // perksUI.ClearAll();
        Debug.Log("perks.count after reset: " + perks.Count);
    }

}
