using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class ManaSystem : Singleton<ManaSystem>
{
    [SerializeField] private ManaUI manaUI;
    private const int MAX_MANA = 99 ;
    private int currentMana = MAX_MANA;
    public int TurnCounter = 0;


    public int cardlength => MatchSetupSystem.Instance.CurrentHeroData.BattleDeck.Count;
    public float refillmana;

    public bool isOverClocking;
    public int OverMana;



    public delegate void OverClocking(bool isOver);
    public static event OverClocking isOverClock;

    void OnEnable()
    {
        ActionSystem.AttachPerformer<SpendManaGA>(SpendManaPerformer);
        ActionSystem.AttachPerformer<RefillManaGA>(RefillManaPerformer);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.POST);
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<SpendManaGA>();
        ActionSystem.DetachPerformer<RefillManaGA>();
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.POST);
    }
    public void Reset()
    {
        currentMana += cardlength * 3;
        OverMana /= 2; 
        manaUI.UpdateManaText(currentMana, OverMana);
    }
    public bool HasEnoughMana(int mana)
    {
        // return currentMana - mana >= -MAX_MANA;
        return true;
    }


    private IEnumerator SpendManaPerformer(SpendManaGA spendManaGA)
    {
        if(!isOverClocking)
        {
            if(currentMana < spendManaGA.Amount)
            {
                isOverClocking = true;
                OverMana = spendManaGA.Amount - currentMana;
            }
        }
        else
        {
            if(OverMana < MAX_MANA)
            {
                OverMana += spendManaGA.Amount;
            }
            else
            {
                DealDamageGA dealDamageGA = new DealDamageGA(OverMana * MAX_MANA, new () {HeroSystem.Instance.HeroView},HeroSystem.Instance.HeroView);
                ActionSystem.Instance.AddReaction(dealDamageGA);
            }
            
        }

        currentMana -= spendManaGA.Amount;
        manaUI.UpdateManaText(currentMana, OverMana);

        // isOverClock(isOverClocking);
        yield return null;
    }
    private IEnumerator RefillManaPerformer(RefillManaGA refillManaGA)
    {
        if(!isOverClocking)
        {
            refillmana = cardlength * 2;
        }
        else
        {
            refillmana = cardlength * 2.5f;
        }
        currentMana += (int)refillmana;
        manaUI.UpdateManaText(currentMana, OverMana);
        // isOverClock(isOverClocking);
        
        yield return null;
    }
    private void EnemyTurnPreReaction(EnemyTurnGA enemyTurnGA)
    {
        RefillManaGA refillManaGA = new ();
        ActionSystem.Instance.AddReaction(refillManaGA);

        TurnCounter++;
    }



}
