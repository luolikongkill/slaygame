using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardViewCreator :  Singleton<CardViewCreator>
{
    [SerializeField] private RectTransform handCardParent;//handview的父物体
    [SerializeField] private CardView cardViewPrefabs;
    public CardView CreateCardView(Card card, Vector3 position, Quaternion rotation)
    {
        CardView cardView = Instantiate(cardViewPrefabs,position,rotation,handCardParent);
        cardView.transform.localScale= Vector3.zero;
        cardView.transform.DOScale(Vector3.one,0.15f);
        cardView.Setup(card);
        return cardView;
    }
}
