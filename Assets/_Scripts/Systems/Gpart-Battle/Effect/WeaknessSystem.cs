using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaknessSystem : MonoBehaviour
{
    public int decrease = 30;


    private void OnEnable()
    {
        ActionSystem.AttachPerformer<ApplyWeaknessGA>(ApplyWeaknessPerformer);
    }
    private void OnDisable()
    {
        ActionSystem.DetachPerformer<ApplyWeaknessGA>();
    }

    private IEnumerator ApplyWeaknessPerformer(ApplyWeaknessGA applyWeaknessGA)
    {
        CombatantView target = applyWeaknessGA.Target;
        target.RemoveStatusEffect(StatusEffectType.WEAKNESS,decrease);
        yield return null;
    }
}
