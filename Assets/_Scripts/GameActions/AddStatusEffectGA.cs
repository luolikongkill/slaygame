using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AddStatusEffectGA : GameAction
{
    public StatusEffectType StatusEffectType { get; private set; }
    public int StackCount { get; private set; }
    public List<CombatantView> Targets { get; private set; }
    public CombatantView caster { get; private set; }

    public AddStatusEffectGA(StatusEffectType statusEffectType,CombatantView caster, int stackCount, List<CombatantView> targets)
    {
        StatusEffectType = statusEffectType;
        StackCount = stackCount;
        Targets = targets;
        this.caster = caster;
    }
    public AddStatusEffectGA(StatusEffectType statusEffectType, CombatantView caster,int stackCount, params CombatantView[] target)
    {
        this.caster = caster;
        StatusEffectType = statusEffectType;
        StackCount = stackCount;
        List<CombatantView> targets = new List<CombatantView>(target);
        Targets = targets;
    }
}
 