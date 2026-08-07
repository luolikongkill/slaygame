using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Card
{
    public string Title => curdata.Title;
    
    public string Description => curdata.Description;

    public Sprite Image => curdata.IMage;
    public Effect ManualTargetEffect => curdata.ManaualTargetEffect;
    public List<AutoTargetEffect> OtherEffects => curdata.OtherEffects;
    public CardEffect cardEffect => curdata.cardeffect;


    public int Mana => curdata.Mana;

    public  CardData curdata;
    public bool canchangeform ;

    public readonly CardData anotherdata;

    public readonly CardData data;

    public Card (CardData cardData)
    {
        data = cardData;
        curdata = data;
        if(cardData.HasAnthorForm)
        {
            anotherdata = cardData.anotherform;
            canchangeform = true;
        }
    }

    public void Setup()
    {
        curdata = data;
    }

    public void  CardChangeForm()
    {
        if(curdata == data)
        curdata = anotherdata;
        else curdata = data;
    }

}
