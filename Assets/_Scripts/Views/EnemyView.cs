using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyView : CombatantView
{
    [SerializeField] private TMP_Text attackText;
    public int AttackPower { get;  set; }

    public EnemyData enemydata;
    public EnemyAction curaction;

    private int actioncount = 0;
    public void Setup(EnemyData enemyData)
    {
        this.enemydata = enemyData;
        actioncount = 0;

        AttackPower = enemyData.AttackPower;
        UpdateEnemyIntention();
        SetupBase(enemyData.Health, enemyData.Image);
    }
    private void UpdateAttackText()
    {
        attackText.text = "ATK:"+ AttackPower;
    }

    public void UpdateEnemyIntention()
    {
        curaction = enemydata.enemyActions[actioncount++%enemydata.enemyActions.Count];
        attackText.text = curaction.type.ToString() + curaction.count;
        AttackPower = curaction.count;
    }

    public GameAction PlayEnemyAction()
    {
        switch (curaction.type)
        {
            case EnemyActionType.Attack:
                return new AttackHeroGA(this,this);
            case EnemyActionType.Defense:         
                return new AddStatusEffectGA(StatusEffectType.ARMOR, AttackPower,this);
            case EnemyActionType.Burn:         
                return new AddStatusEffectGA(StatusEffectType.BURN, AttackPower,HeroSystem.Instance.HeroView);    
            default:
                break;
        }

         return null;

    }

}
