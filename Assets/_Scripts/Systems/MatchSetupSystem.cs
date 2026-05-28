using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MatchSetupSystem : MonoBehaviour
{
    [SerializeField] private HeroData heroData;
    [SerializeField] private List<EnemyData> enemyDatas;
    [SerializeField] private List<PerkData> perkDatas;

    private void Start()
    {
        HeroSystem.Instance.Setup(heroData);
        EnemySystem.Instance.Setup(enemyDatas);
        CardSystem.Instance.Setup(heroData.Deck);
        foreach (var perkData in perkDatas)
        {
            PerkSystem.Instance.AddPerk(new Perk(perkData));
        }
        DrawCardsGA drawCardsGA = new (5);
        ActionSystem.Instance.Perform(drawCardsGA);

    }
}
