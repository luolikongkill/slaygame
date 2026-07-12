using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Herov1")]      
public class HeroDatav1 : ScriptableObject
{
    [field: SerializeField] public Sprite Image { get;  set; }
    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] public int CurrentHealth { get;  set; }
    [field: SerializeField] public List<CardData> Deck { get;  set; }
    [field: SerializeField] public List<CardData> BattleDeck { get;  set; }
    [field: SerializeField] public  List<CardData> AllDeck { get; private set; }
    public void Init(HeroData heroData)
    {
        Image = heroData.Image;
        Health = heroData.Health;
        CurrentHealth = heroData.CurrentHealth;
        Deck = heroData.Deck;
        BattleDeck = heroData.BattleDeck;
        AllDeck = heroData.AllDeck;
    }
}
