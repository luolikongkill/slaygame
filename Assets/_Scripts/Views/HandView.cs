using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using DG.Tweening;
using System.Linq;

public class HandView : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;
    public  List<CardView> cards= new();
    public IEnumerator AddCard(CardView cardView)
    {
        cards.Add(cardView);
        yield  return  UpdataCardPositions(0.15f);
    }
    public CardView RemoveCard(Card card)
    {
        CardView cardView = GetCardView(card);
        if (cardView == null) return null;
        
            cards.Remove(cardView);
          StartCoroutine(UpdataCardPositions(0.15f));
        
        return cardView;
    }
    private CardView GetCardView(Card card)
    {
        return cards.Where(cardview => cardview.Card == card).FirstOrDefault();
    }
    private IEnumerator UpdataCardPositions(float duration)
    {
        if (cards.Count ==0  ) yield break;
        float cardSpacing = 1f/10f;
        float firstCardPosition =0.5f- (cards.Count-1)*cardSpacing/2;
        Spline spline = splineContainer.Spline;
        for (int i=0;i<cards.Count;i++)
        {
            float p= firstCardPosition+i*cardSpacing;
            Vector3 splinePosition =spline.EvaluatePosition(p);//position
            Vector3 forward = spline.EvaluateTangent(p);//qie
            Vector3 up = spline.EvaluateUpVector(p);//fa
            Quaternion rotation = Quaternion.LookRotation(Vector3.Cross(-up, forward).normalized,up);
            cards[i].transform.DOMove(splinePosition + transform.position + 0.01f * i * Vector3.back, duration);
            cards[i].transform.DORotate(rotation.eulerAngles, duration);
        }
        yield return new WaitForSeconds(duration);
    }
}
 