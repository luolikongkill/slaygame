using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHeroGA : GameAction , IHaveCaster
{
    public EnemyView Attacker { get; private set; }
    public CombatantView Caster {get; private set;}
    public AttackHeroGA(EnemyView attacker, CombatantView caster)
    {
        Attacker = attacker;
        Caster = caster;
    }
}
