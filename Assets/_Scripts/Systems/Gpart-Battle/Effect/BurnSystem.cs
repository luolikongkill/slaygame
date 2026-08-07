using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnSystem : MonoBehaviour
{
    [SerializeField] private GameObject burnVFX;

    private List<GameObject> vfxs => VFXManager.vfxs;
    public int decrease = 10;
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<ApplyBurnGA>(ApplyBurnPerformer);
    }
    private void OnDisable()
    {
        ActionSystem.DetachPerformer<ApplyBurnGA>();
    }
    private IEnumerator ApplyBurnPerformer(ApplyBurnGA applyBurnGA)
    {
        CombatantView target = applyBurnGA.Target;
        GameObject obj = Instantiate(burnVFX, target.transform.position, Quaternion.identity);
        vfxs.Add(obj);

        target.EffectDamage(applyBurnGA.BurnDamage);
        target.RemoveStatusEffect(StatusEffectType.BURN,decrease);
        yield return new WaitForSeconds(0.5f);

    }
        
       
}
