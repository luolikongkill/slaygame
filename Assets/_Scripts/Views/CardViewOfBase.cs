using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CardViewOfBase : MonoBehaviour
{
    [SerializeField] public SpriteRenderer Cardbg;

    [SerializeField] public TMP_Text title;
    [SerializeField] public TMP_Text description;
    [SerializeField] public TMP_Text mana;
    [SerializeField] public SpriteRenderer imageSR;
    public Card Card {get; private set; }
    
    public virtual void Setup(Card card)
    {
        Card = card;
        title.text = card.Title;
        description.text = card.Description;
        mana.text = card.Mana.ToString();
        imageSR.sprite = card.Image;
        
    }



}
