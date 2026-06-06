using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Hero")]      
public class HeroData : ScriptableObject
{
    [field: SerializeField] public Sprite Image { get;  set; }
    [field: SerializeField] public int Health { get;  set; }
    [field: SerializeField] public int CurrentHealth { get;  set; }
    [field: SerializeField] public List<CardData> Deck { get;  set; }
    [field: SerializeField] public List<CardData> BattleDeck { get;  set; }
    [field: SerializeField] public static List<CardData> AllDeck { get; private set; }
}
