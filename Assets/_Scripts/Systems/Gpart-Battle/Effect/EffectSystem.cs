using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSystem : MonoBehaviour
{
    void OnEnable()
    {
        ActionSystem.AttachPerformer<PerformEffectGA>(PerformEffectPerformer);
    }

    // Update is called once per frame
    void OnDisable()
    {
        ActionSystem.DetachPerformer<PerformEffectGA>();
    }
    private IEnumerator PerformEffectPerformer(PerformEffectGA performEffectGA)
    {
        GameAction effectAction = performEffectGA.Effect.GetGameAction(performEffectGA.Targets, HeroSystem.Instance.HeroView);
        ActionSystem.Instance.AddReaction(effectAction);
        yield return null;
    }
    
}
public static class StatusEffectFactory
{
    public static DamageChane CreateModifier(StatusEffectType type)
    {
        switch (type)
        {
// 你需要实现 ArmorModifier
            case StatusEffectType.BURN:
                // BURN 可能不是伤害修改器，而是持续伤害，这里可以返回 null 或者特殊处理
                return null; // 或者 return new BurnDamageModifier(stackCount) 如果它实现 IDamageModifier
            case StatusEffectType.VULNERABLE:
                return new VulnerableChane();
            case StatusEffectType.WEAKNESS:
                return new WeaknessChane();
            default:
                return null;
        }
    }
}
