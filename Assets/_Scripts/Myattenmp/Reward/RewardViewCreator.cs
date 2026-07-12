using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RewardViewCreator : Singleton<RewardViewCreator>
{
    [SerializeField] private GameObject rewardPanel;
    [SerializeField] private Transform cardContainer;
    [SerializeField] private RewardCardView RCardPrefab;
    // [SerializeField] private Button skipButton;
//暂时不做
    // private System.Action onSkip;
    private List<CardData> rewardCards = new ();
    private List<RewardCardView> RCardViews = new ();
    private CardData Chosencarddata;
    private List<CardData> RCardpool;



    // private void Awake()
    // {
    //     skipButton.onClick.AddListener(OnSkipClicked);
    // }
    public void Reset()
    {
        rewardPanel.SetActive(false);
        ClearView();
    }
    public void Init(List<CardData> CardDatas)
    {
        this.RCardpool = CardDatas;
    }
    public void Setup()
    {
        ClearView();
        GetRewardIn AllReward = new ();
        AllReward.RewardpoolUpdate(RCardpool);
        rewardCards=AllReward.GenerateCardReward();
        ShowRewards();
    }

    private void AddDeck()
    {
        MatchSetupSystem.Instance.CurrentHeroData.Deck.Add(Chosencarddata);
    }
    // 打开奖励UI并生成卡牌
    public void ShowRewards()
    {
        // 先清空之前生成的卡牌
        // 实例化3张奖励卡
        rewardPanel.SetActive(true);
        foreach (var carddata in rewardCards)
        {
            RewardCardView cardview = Instantiate(RCardPrefab, cardContainer);
            Debug.Log("生成成功");
            Card card1 = new Card(carddata);
            cardview.Setup(card1);
            RCardViews.Add(cardview);
        }
        Debug.Log("ShowRewards");

        // rewardPanel.SetActive(true);
    }

    // 玩家选了一张卡,加入卡组


    // 玩家点击跳过
    private void OnSkipClicked()
    {
        // onSkip?.Invoke();
        CloseUI();
    }

    // 关闭UI并清理
    private void CloseUI()
    {
        rewardPanel.SetActive(false);
    }
    public void ClearView()
    {
        CloseUI();
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
