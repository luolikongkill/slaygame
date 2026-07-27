using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ApplyBurnGA : GameAction
{
    public int BurnDamage { get; private set; }
    public CombatantView Target { get; private set; }
    public ApplyBurnGA( int burnDamage,CombatantView target)
    {
        Target = target;
        BurnDamage = burnDamage;
    }
}
