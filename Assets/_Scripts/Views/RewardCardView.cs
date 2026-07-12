using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RewardCardView : CardViewOfBase
{
    private void OnMouseDown()
    {
        
    }
    private void OnMouseUp()
    {
        MatchSetupSystem.Instance.CurrentHeroData.DeckAdd(base.Card.data);
        RewardViewCreator.Instance.ClearView();

    } 

}
