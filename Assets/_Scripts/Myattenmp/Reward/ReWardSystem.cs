using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReWardSystem : Singleton<ReWardSystem>
{
    private int EnemyCount {get; set;}
    private List<CardData> RCardDatas;
    void OnEnable()
    {
        ActionSystem.AttachPerformer<GetRewardGA>(GetRewardPerformer);
        ActionSystem.AttachPerformer<GameEndGA>(GameEndPerformer);
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<GetRewardGA>();
        ActionSystem.DetachPerformer<GameEndGA>();
    }
    public void Setup(List<CardData> cardDatas)
    {
        RCardDatas = cardDatas;
        RewardViewCreator.Instance.Init(cardDatas);
    }
    public void Reset()
    {
        RewardViewCreator.Instance.Reset();
    }

    private IEnumerator GetRewardPerformer(GetRewardGA getRewardGA)//we can add more modifiers
    {

            yield return new WaitForSeconds(0.5f);
            Debug.Log("GetrewardGA");
            RewardViewCreator.Instance.Setup();
            
    }
    private IEnumerator GameEndPerformer(GameEndGA gameEndGA)
    {
        //游戏结束执行者
        Interactions.Instance.GameIsOver = true;
        MapView.Instance.UpdateAccessibleNodes(MapView.Instance.currentnodeUI);
        Debug.Log("Updatenode and gameend");
        yield return new WaitForSeconds(0.5f);
        GetRewardGA getRewardGA = new GetRewardGA();
        ActionSystem.Instance.AddReaction(getRewardGA);
        yield return null;

    }
}
