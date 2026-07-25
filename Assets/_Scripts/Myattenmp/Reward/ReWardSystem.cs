using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReWardSystem : Singleton<ReWardSystem>
{

    private List<CardData> RCardDatas;
    void OnEnable()
    {
        // ActionSystem.AttachPerformer<GetRewardGA>(GetRewardPerformer);
        // ActionSystem.AttachPerformer<GameEndGA>(GameEndPerformer);
        EnemySystem.CanGetReward += GetReward;
    }
    void OnDisable()
    {
        // ActionSystem.DetachPerformer<GetRewardGA>();
        // ActionSystem.DetachPerformer<GameEndGA>();
        EnemySystem.CanGetReward -= GetReward;
    }
    public void Setup(List<CardData> cardDatas)
    {
        RCardDatas = cardDatas;
        GetRewardUI.Instance.Init(cardDatas);
    }
    public void Reset()
    {
        GetRewardUI.Instance.rewardViewCreator.Reset();
    }

        private void GetReward(RoomType roomType)
    {
        Interactions.Instance.GameIsOver = true;
        if(roomType == RoomType.Boss)
        {
            UIChangeSet.Instance.UIChange(roomType);
            return;
        }



        MapView.Instance.UpdateAccessibleNodes(MapView.Instance.currentnodeUI);


        Debug.Log("GetrewardGA");
        GetRewardUI.Instance.Setup(roomType);     
    }

    // private IEnumerator GetRewardPerformer(GetRewardGA getRewardGA)//we can add more modifiers
    // {

    //         yield return new WaitForSeconds(0.5f);
    //         Debug.Log("GetrewardGA");
    //         RewardViewCreator.Instance.Setup();
            
    // }

    // private IEnumerator GameEndPerformer(GameEndGA gameEndGA)
    // {
    //     //游戏结束执行者
    //     Interactions.Instance.GameIsOver = true;
    //     MapView.Instance.UpdateAccessibleNodes(MapView.Instance.currentnodeUI);
    //     Debug.Log("Updatenode and gameend");
    //     yield return new WaitForSeconds(0.1f);
    //     GetRewardGA getRewardGA = new GetRewardGA();
    //     ActionSystem.Instance.AddReaction(getRewardGA);
    //     yield return null;

    // }
}
