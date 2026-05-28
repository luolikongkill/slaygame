using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Card
{
    public string Title => data.name;
    
    public string Description => data.Description;

    public Sprite Image => data.IMage;
    public Effect ManualTargetEffect => data.ManaualTargetEffect;
    public List<AutoTargetEffect> OtherEffects => data.OtherEffects;


    public int Mana { get; private set;}

    private readonly CardData data;

    public Card (CardData cardData)
    {
        data = cardData;
        Mana= cardData.Mana;
    }

}
