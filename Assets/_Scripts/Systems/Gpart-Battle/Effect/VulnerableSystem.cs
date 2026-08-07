using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulnerableSystem : MonoBehaviour
{
    public int decrease = 30;


    private void OnEnable()
    {
        ActionSystem.AttachPerformer<ApplyVulnerableGA>(ApplyVulnerablePerformer);
    }
    private void OnDisable()
    {
        ActionSystem.DetachPerformer<ApplyVulnerableGA>();
    }

    private IEnumerator ApplyVulnerablePerformer(ApplyVulnerableGA applyVulnerableGA)
    {
        CombatantView target = applyVulnerableGA.Target;
        target.RemoveStatusEffect(StatusEffectType.VULNERABLE,decrease);
        yield return null;
    }
}
