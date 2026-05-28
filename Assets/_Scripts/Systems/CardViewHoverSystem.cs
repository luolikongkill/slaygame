using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CardViewHoverSystem : Singleton<CardViewHoverSystem>
{
    [SerializeField] private CardView CardViewHover;
    public void Show(Card card, Vector3 position)
    {
        CardViewHover.gameObject.SetActive(true);
        CardViewHover.Setup(card);
        CardViewHover.transform.position = position;

    }
    
    public void Hide ()
    {
        CardViewHover.gameObject.SetActive(false);
    }
 
}
