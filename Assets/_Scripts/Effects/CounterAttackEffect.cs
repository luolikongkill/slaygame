using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAttackEffect : Effect
{
    [SerializeField] private int damageAmount;
    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster)
    {
        if(ManaSystem.Instance==null)
        {
            Debug.LogError("ManaSystem instance is null. Ensure ManaSystem is initialized before using CounterAttackEffect.");
            return null;
        }
        if(ManaSystem.Instance.TurnCounter == 7)
        { 
            DealDamageGA counterAttackGA = new DealDamageGA(damageAmount, targets, caster);
            Debug.Log("CounterAttackEffect triggered on turn " + ManaSystem.Instance.TurnCounter);
            return counterAttackGA;   
        }
        else return null;
    }
}
