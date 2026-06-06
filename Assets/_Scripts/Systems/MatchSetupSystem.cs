using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//本来是地图连接战斗系统，后面直接使用地图click事件触发战斗，所以这个系统就变成了战斗初始化系统
public class MatchSetupSystem : Singleton<MatchSetupSystem>
{
    [SerializeField] public HeroData heroData;
    [SerializeField] public List<EnemyData> enemyDatas;
    [SerializeField] public List<PerkData> perkDatas;
    public HeroData CurrentHeroData { get;  set; }
    private bool isGameStarted = false;
    private void Start()
    {
        CurrentHeroData = heroData;
        
    }


    public void GameStart()
    {
        GameAfterInit();
        GameReset();
        
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
        EnemySystem.Instance.Reset();
        CardSystem.Instance.Reset();
        PerkSystem.Instance.Reset();
        //cleanup
    }

    private void GameAfterInit()
    {
        if( !isGameStarted) {isGameStarted = true;return;}
        CurrentHeroData.BattleDeck = CurrentHeroData.Deck;//未实现
        //do something before game start, like showing loading screen, playing music, etc.
        CurrentHeroData.CurrentHealth = HeroSystem.Instance.HeroView.CurrentHealth;
        // CurrentHeroData.Image = heroData.Image;
        CurrentHeroData.Deck = heroData.Deck;//未实现




    }
}
