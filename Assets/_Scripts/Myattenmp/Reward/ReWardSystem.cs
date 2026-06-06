using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReWardSystem : Singleton<ReWardSystem>
{
    [SerializeField] private GameObject RewardScreen;
    private int EnemyCount {get; set;}
    void OnEnable()
    {
        ActionSystem.AttachPerformer<GetRewardGA>(GetRewardPerformer);
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<GetRewardGA>();
    }
    void Start()
    {
        RewardScreen.SetActive(false);
    }
    public void ReWardSet(int CurrentEnemyCount)
    {
        EnemyCount = CurrentEnemyCount;
        if (EnemyCount == 0)
        {
            //Reward the player
            Debug.Log("Player Rewarded");
            GetRewardGA getRewardGA = new GetRewardGA();
            ActionSystem.Instance.AddReaction(getRewardGA);
        }
    }
    private IEnumerator GetRewardPerformer(GetRewardGA getRewardGA)//we can add more modifiers
    {

            yield return new WaitForSeconds(0.5f);
            RewardScreen.SetActive(true);
            Interactions.Instance.GameIsOver = true;
    }
}
