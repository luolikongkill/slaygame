using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetRewardUI : Singleton<GetRewardUI>
{
    public RewardViewCreator rewardViewCreator;


    public GameObject rewardbox;

    [SerializeField] private CanvasGroup canvas;

    public Button SkipButton;
    private List<Button> rewardButton = new();

    public Button RCPrefab;


    public void Init(List<CardData> CardDatas)
    {
        CloseUI();
        rewardViewCreator.RCardpool = CardDatas;
        foreach(Button button in rewardButton)
        {
            button.onClick.RemoveAllListeners();

            Destroy(button);
        }
        rewardButton.Clear();
    }

    void Start()
    {
        SetOnSkipClicked();
    }
    public void Setup(RoomType rewardType)
    {

        RewardCount rewardCount = new ();
        rewardCount.Init(rewardType);

        foreach(Button button in rewardButton)
        {
            button.onClick.RemoveAllListeners();

            Destroy(button);
        }
        rewardButton.Clear();


        for (int i = 0; i < rewardCount.Rcardcount; i++)
        {


            Button button = Instantiate(RCPrefab,rewardbox.transform);
            button.GetComponent<RewardButton>().init("card");

            rewardButton.Add(button);


            button.onClick.AddListener(()=>{ ShowCardReward();button.Select();button.gameObject.SetActive(false) ;CloseRewardbox();});
        }


        for (int i = 0; i < rewardCount.Rperkcount; i++)
        {
            int j = rewardCount.Rcardcount;

            Button button = Instantiate(RCPrefab,rewardbox.transform);
            button.GetComponent<RewardButton>().init("perk");

            rewardButton.Add(button);

            button.onClick.AddListener(()=>{ GetPerkReward();button.Select();button.gameObject.SetActive(false) ;});
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
        canvas.alpha = 0;
        canvas.blocksRaycasts = false;
        canvas.interactable = false;

    }
    public void CloseRewardbox()
    {
        rewardbox.SetActive(false);
    }
    public void OpenUI()
    {
        canvas.alpha = 1;
        canvas.blocksRaycasts = true;
        canvas.interactable = true;

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