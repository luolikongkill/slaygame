using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RewardViewCreator : MonoBehaviour
{
    [SerializeField] private Transform cardContainer;
    [SerializeField] private RewardCardView RCardPrefab;
    // [SerializeField] private Button skipButton;
//暂时不做
    // private System.Action onSkip;
    private List<CardData> rewardCards = new ();
    private List<RewardCardView> RCardViews = new ();
    private CardData Chosencarddata;
    public List<CardData> RCardpool;


    public void Reset()
    {
        ClearView();
    }
    public void Init(List<CardData> CardDatas)
    {
        this.RCardpool = CardDatas;
    }
    public void Setup(int count)
    {
        ClearView();
        GetRewardIn AllReward = new ();
        AllReward.RewardpoolUpdate(RCardpool);
        rewardCards=AllReward.GenerateCardReward();
        ShowRewards();
    }

    // 打开奖励UI并生成卡牌
    public void ShowRewards()
    {
        // 先清空之前生成的卡牌
        // 实例化3张奖励卡
        foreach (var carddata in rewardCards)
        {
            RewardCardView cardview = Instantiate(RCardPrefab, cardContainer);
            Debug.Log("生成成功");
            Card card1 = new Card(carddata);
            cardview.Setup(card1);
            RCardViews.Add(cardview);
        }
        Debug.Log("ShowRewards");
    }


    public void ClearView()
    {

        if (RCardViews!=null)
        foreach (var view in RCardViews)
        {
            Destroy(view.gameObject);
        }
        RCardViews.Clear();
        if(rewardCards==null) return;
        rewardCards.Clear();
    }


}
