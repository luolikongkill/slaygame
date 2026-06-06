using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SerializeReferenceEditor;
public class Perk 
{
    public Sprite Image => data.Image;
    private readonly PerkData data;
    private readonly PerkCondition condition;
    private readonly AutoTargetEffect effect;
    public Perk(PerkData perkdata)
    {
        this.data = perkdata;
        condition = data.Condition;
        effect = data.AutoTargetEffects[0];
    }
    public void OnAdd()
    {
        condition.SubscribeCondition(Reaction);

    }
    public void OnRemove()
    {
        condition.UnsubscribeCondition(Reaction);
        Debug.Log("Perk removed: " );
    }
    private void Reaction(GameAction gameAction)
    {
        if(condition.SubConditionIsMet(gameAction))
        {
            List<CombatantView> targets = new List<CombatantView>();
            if(data.UseActionCasterAsTarget &&
               gameAction is IHaveCaster haveCaster)
            {
                targets.Add(haveCaster.Caster);
            }
            if(data.UseAutoTarget)
            {
                targets.AddRange(effect.TargetMode.GetTargets());
            }
            GameAction perkEffentAction = effect.Effect.GetGameAction(targets,HeroSystem.Instance.HeroView);
            ActionSystem.Instance.AddReaction(perkEffentAction);

        }
    }
}  
