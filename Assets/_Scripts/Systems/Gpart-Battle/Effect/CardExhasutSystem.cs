using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardExhaustSystem : MonoBehaviour
{
    void OnEnable()
    {
        ActionSystem.AttachPerformer<HandleCardGA>(CardExhaustPerformer);
    }

    void OnDisable()
    {
        ActionSystem.DetachPerformer<HandleCardGA>();
    }

    private IEnumerator CardExhaustPerformer(HandleCardGA cardExhaustGA )
    {
        CardSystem.Instance.hand.Remove(cardExhaustGA.card);

        yield return null;
    }
}
