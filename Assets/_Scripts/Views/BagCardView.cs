using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BagCardView : MonoBehaviour
{
    [SerializeField] public TMP_Text title;
    [SerializeField] public TMP_Text description;
    [SerializeField] public TMP_Text mana;
    [SerializeField] public SpriteRenderer image;
    [SerializeField] public Image image1;


    public void Setup(Card card)
    {
        title.text = card.Title;
        description.text = card.Description;
        mana.text = card.Mana.ToString();
        image1.sprite = card.Image;
    }

}
