using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCardEffect : CardEffect
{
    [SerializeField]public CardPile handlePile = CardPile.DisCardPile;

    public override GameAction GetGameAction(Card card)
    {
        HandleCardGA handleCardGA = new HandleCardGA(card, handlePile);
        return handleCardGA;
    }
}
[System.Serializable]
public class HandleCardGA : GameAction
{
    public Card card;
    public CardPile pile;

    public HandleCardGA(Card card,CardPile cardPile)
    {
        this.card = card;
        pile = cardPile;
    }
}
