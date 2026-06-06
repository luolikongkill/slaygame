using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRewardIn : MonoBehaviour
{//后续修改稀有度以及随机数

    Dictionary<int, string> rewardDic = new Dictionary<int, string>()
    {
        {0,"Card"},
        {1,"Posion"},
        {2,"Perk"},
        {3,"Gold"}
    };
    public List<CardData> RewardCards = new List<CardData>();
    private List<CardData> pool = HeroData.AllDeck;
   public List<CardData> GenerateCardReward()
    {
        RewardCards.Clear();
        //cardwards
        for (int i = 0; i <= 2; i++)
        {
            RewardCards.Add(pool[Random.Range(0, pool.Count)]);
            pool.Remove(RewardCards[i]);
        }
        return RewardCards;
    }
    public int GenerateGoldReward()
    {
        return Random.Range(10, 50);
    }

}
