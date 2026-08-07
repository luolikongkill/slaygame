using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleEffectSystem : MonoBehaviour
{
    public Dictionary<StatusEffectType, int> statusEffects = new Dictionary<StatusEffectType, int>();
    public Dictionary<StatusEffectType,DamageChane> damageChanes = new Dictionary<StatusEffectType,DamageChane>();
    public Dictionary<StatusEffectType,DamageChane> attackChanes = new Dictionary<StatusEffectType,DamageChane>();


    void Start()
    {
        damageChanes.Add(StatusEffectType.VULNERABLE,null);
        attackChanes.Add(StatusEffectType.WEAKNESS,null);
    }
    public void AddChanes(StatusEffectType type)
    {
        DamageChane chane = StatusEffectFactory.CreateModifier(type);
        if(chane == null) return;
        if(damageChanes.ContainsKey(type))damageChanes[type] = chane;
        else
        {
            attackChanes[type] = chane;
        }
    }

    public void RemoveChanes(StatusEffectType type)
    {
        if(damageChanes.ContainsKey(type))
            damageChanes[type] = null;
        else attackChanes[type] = null;
    }



    public int DamageSet(int damageAmount)
    {
        /*foreach(var deffect in damageChanes)
        {
            damageAmount = deffect.DamageChange(damageAmount);
        }
        */
        foreach(DamageChane chane in damageChanes.Values)
        {
            if(chane != null)
            damageAmount = chane.DamageChange(damageAmount);
        }
        return damageAmount;
    }

    public int AttackSet(int attack)
    {
        foreach(DamageChane chane in attackChanes.Values)
        {
            if(chane != null)
            attack = chane.DamageChange(attack);
        }
        return attack;
    }
}
