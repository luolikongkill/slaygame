using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public abstract class Effect 
{
    public abstract GameAction GetGameAction(List<CombatantView> targets, CombatantView caster) ;
}
[System.Serializable]
public abstract class CardEffect 
{
    public abstract GameAction GetGameAction(Card card) ;
}