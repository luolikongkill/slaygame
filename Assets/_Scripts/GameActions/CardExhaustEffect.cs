using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardExhaustEffect : CardEffect
{
    public override GameAction GetGameAction(Card card)
    {
        HandleCardGA discardGA = new (card,CardPile.Another);
        return discardGA;
    }
}



