using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ApplyWeaknessGA : GameAction
{
    public int stackcount {get; private set;}
    public CombatantView Target { get; private set; }

    public ApplyWeaknessGA( int stackcount,CombatantView target)
    {
        Target = target;
        this.stackcount = stackcount;
    }
}
