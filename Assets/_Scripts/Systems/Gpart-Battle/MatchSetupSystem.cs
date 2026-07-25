using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//本来是地图连接战斗系统，后面直接使用地图click事件触发战斗，所以这个系统就变成了战斗初始化系统
public class MatchSetupSystem : Singleton<MatchSetupSystem>
{
    [SerializeField] public HeroData heroData;
    public   HeroData  CurrentHeroData ;
    public bool isGameStarted = false;
    public bool isPlayerDied = false;

    public void GameStart(RoomType roomType)
    {
        if(!isGameStarted)
        {
            ReGame();
            isGameStarted = true;
        }
        else
        GameAfterInit();
        GameReset();
        


        ReWardSystem.Instance.Setup(CurrentHeroData.AllDeck);

        HeroSystem.Instance.Setup(CurrentHeroData);//
        EnemyPoolSystem.Instance.Setup(roomType);

        CardSystem.Instance.Setup(CurrentHeroData.BattleDeck);//1
        DrawCardsGA drawCardsGA = new (5);
        
        ActionSystem.Instance.Perform(drawCardsGA);

    }

    public void GameReset()
    {


        ReWardSystem.Instance.Reset();
        Debug.Log("ActionClear");
        EnemyPoolSystem.Instance.enemySystem.Reset();
        CardSystem.Instance.Reset();
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
        CurrentHeroData = heroData.GetClone();
        CurrentHeroData.BattleDeck = CurrentHeroData.Deck;
        Debug.Log("ReGame sucess");
        isGameStarted = false;
        BagManager.Instance.Init(CurrentHeroData.Deck);
        PerkSystem.Instance.Init(CurrentHeroData.InitperkDatas);
    }

    public void GameOver()
    {
        
    }
}
