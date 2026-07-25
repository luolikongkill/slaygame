using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Hero")]      
public class HeroData : ScriptableObject
{
    [field: SerializeField] public Sprite Image { get;  set; }
    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] public int CurrentHealth { get;  set; }
    [field: SerializeField] public List<CardData> Deck { get;  set; }
    [field: SerializeField] public List<CardData> BattleDeck { get;  set; }
    [field: SerializeField] public  List<CardData> AllDeck { get; private set; }
    [field: SerializeField] public List<PerkData> InitperkDatas{ get; private set; }
    public void Init(HeroData heroData)
    {
        Image = heroData.Image;
        Health = heroData.Health;
        CurrentHealth = heroData.CurrentHealth;
        Deck = heroData.Deck;
        BattleDeck = heroData.BattleDeck;
        AllDeck = heroData.AllDeck;
    }
    public HeroData GetClone()
    {
        HeroData newHeroData = ScriptableObject.Instantiate(this);
        newHeroData.Deck = new List<CardData>();
        foreach(var carddata in this.Deck)
        {
            CardData newdata = ScriptableObject.Instantiate(carddata);
            newHeroData.Deck.Add(newdata);
        }
        newHeroData.Image = this.Image;
        newHeroData.Health = this.Health;
        newHeroData.CurrentHealth = this.CurrentHealth;
        Debug.Log("clone suc");
        return newHeroData;
    }
    public void DeckAdd(CardData cardData)
    {
        this.Deck.Add(cardData);
        BagManager.Instance.BagAdd(cardData);
    }
}
