using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DrawCardsEffect : Effect
{
    [SerializeField] private int drawAmount;

    public override GameAction GetGameAction(List<CombatantView> targets,CombatantView caster)
    {
        DrawCardsGA drawCardsGA = new (drawAmount);
        Debug.Log("DrawCardsEffect triggered, creating DrawCardsGA with amount: " + drawAmount);
        return  drawCardsGA;
    }

}
