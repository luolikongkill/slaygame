using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetRewardUI : Singleton<GetRewardUI>
{
    public RewardViewCreator rewardViewCreator;
    [SerializeField] private GameObject rewardPanel;
    public GameObject rewardbox;

    public Button SkipButton;
    public Button[] rewardButton;


    public void Init(List<CardData> CardDatas)
    {
        CloseUI();
        rewardViewCreator.RCardpool = CardDatas;
    }

    void Start()
    {
        SetOnSkipClicked();
    }
    public void Setup(RoomType rewardType)
    {

        RewardCount rewardCount = new ();
        rewardCount.Init(rewardType);

        foreach (var button in rewardButton)
        {
            button.gameObject.SetActive(false);
        }
        for (int i = 0; i < rewardCount.Rcardcount; i++)
        {
            var option = rewardButton[i];
            
            option.gameObject.SetActive(true);
            option.GetComponent<RewardButton>().init("card");

            option.onClick.AddListener(()=>{ ShowCardReward();option.Select();option.gameObject.SetActive(false) ;CloseRewardbox();});
        }
        for (int i = 0; i < rewardCount.Rperkcount; i++)
        {
            int j = rewardCount.Rcardcount;
            var option = rewardButton[j];
            
            option.gameObject.SetActive(true);
            option.GetComponent<RewardButton>().init("perk");

            option.onClick.AddListener(()=>{ GetPerkReward();option.Select();option.gameObject.SetActive(false) ;});
            j++;
        }
        OpenUI();

    }

    private void ShowCardReward()
    {
        //justic the type of reward;
        rewardViewCreator.Setup(1);

    }

    private void GetPerkReward()
    {
        PerkSystem.Instance.AddPerk(new(MatchSetupSystem.Instance.CurrentHeroData.InitperkDatas[0]));
    }

    public void SetOnSkipClicked()
    {
        SkipButton.onClick.AddListener(()=>
        {
            UIChangeSet.Instance.UIChange(1);
            CloseUI();

        });
        CloseUI();
    }


    public void CloseUI()
    {
        rewardPanel.SetActive(false);
    }
    public void CloseRewardbox()
    {
        rewardbox.SetActive(false);
    }
    public void OpenUI()
    {
        rewardPanel.SetActive(true);
        rewardbox.SetActive(true);
    }


}

public class RewardCount
{
    public RoomType Type;
    public int Rcardcount;
    public int Rperkcount;
    public int goldcount;
    public int other;

    public void Init(RoomType roomType)
    {
        this.Type = roomType;
        switch (Type)
        {
            case RoomType.Normal:
                Rcardcount = 1;
                Rperkcount = 0;
                goldcount = 0;
                break;

            case RoomType.Elite:
                Rcardcount = 1;
                Rperkcount = 1;
                goldcount = 1;
                break;
            case RoomType.Boss:
                Rcardcount = 1;
                Rperkcount = 2;
                goldcount = 2;
                break;
            
            default:
                break;
        }
    }
}