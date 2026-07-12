using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//本来是地图连接战斗系统，后面直接使用地图click事件触发战斗，所以这个系统就变成了战斗初始化系统
public class MatchSetupSystem : Singleton<MatchSetupSystem>
{
    [SerializeField] public HeroData heroData;
    [SerializeField] public List<EnemyData> enemyDatas;
    [SerializeField] public List<PerkData> perkDatas;
    public   HeroData  CurrentHeroData ;
    // public   HeroData  CurrentHeroData2 ;
    private bool isGameStarted = false;

    public void GameStart()
    {
        if(!isGameStarted)
        {
            ReGame();
            isGameStarted = true;
        }
        else
        GameAfterInit();
        GameReset();
        
        ReWardSystem.Instance.Setup(CurrentHeroData.Deck);
        HeroSystem.Instance.Setup(CurrentHeroData);//
        EnemySystem.Instance.Setup(enemyDatas);//1
        CardSystem.Instance.Setup(CurrentHeroData.BattleDeck);//1
        PerkSystem.Instance.Setup(perkDatas);//
        DrawCardsGA drawCardsGA = new (5);
        ActionSystem.Instance.Perform(drawCardsGA);

    }

    public void GameReset()
    {

        //  HeroSystem.Instance.Reset();直接在本地数据传入新数据，HeroData会自己更新，不需要重置
        // ActionSystem.Instance.Reset();
        ReWardSystem.Instance.Reset();
        Debug.Log("ActionClear");
        EnemySystem.Instance.Reset();
        CardSystem.Instance.Reset();
        PerkSystem.Instance.Reset();
        ManaSystem.Instance.Reset();
        //cleanup
    }
    //游戏角色更新
    private void GameAfterInit()
    {
        Interactions.Instance.GameIsOver = false;
        CurrentHeroData.BattleDeck = CurrentHeroData.Deck;
        Debug.Log("战斗卡牌添加完成");
        if(isGameStarted)
        CurrentHeroData.CurrentHealth = HeroSystem.Instance.HeroView.CurHealth;
    }
    //开局游戏角色初始化
    public void ReGame()
    {
        // CurrentHeroData2 = new ();
        // CurrentHeroData2 = heroData;
        // // CurrentHeroData2 = new ();
        // // CurrentHeroData2.Init(heroData);
        CurrentHeroData = heroData.GetClone();
        CurrentHeroData.BattleDeck = CurrentHeroData.Deck;
        Debug.Log("ReGame sucess");
        isGameStarted = false;
        BagManager.Instance.Init(CurrentHeroData.Deck);
    }
}
