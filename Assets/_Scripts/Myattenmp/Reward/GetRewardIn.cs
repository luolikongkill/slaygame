using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRewardIn 
{
    public List<CardData> RewardCards = new List<CardData>();
    private List<CardData> pool ;
    public void RewardpoolUpdate(List<CardData> Deck)
    {
        pool = Deck;
    }
    public void RewardpoolUpdatev1(HeroDatav1 heroData)
    {
        pool = heroData.AllDeck;
    }
   public List<CardData> GenerateCardReward()
    {
        RewardCards.Clear();
        //cardwards
        for (int i = 0; i <= 2;)
        {
            CardData cdata = pool[Random.Range(0, pool.Count)];
            if(!RewardCards.Contains(cdata))
            {
                i++;
                RewardCards.Add(cdata);
            }
        }
        return RewardCards;
    }


}
