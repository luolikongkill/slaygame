using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class EnemySystem : Singleton<EnemySystem>
{
    [SerializeField] private EnemyBoardView enemyBoardView;
    public List<EnemyView> Enemies => enemyBoardView.EnemyViews;

    void OnEnable()
    {
        ActionSystem.AttachPerformer<EnemyTurnGA>(EnemyTurnPerformer);
        ActionSystem.AttachPerformer<AttackHeroGA>(AttackHeroPerformer);
        ActionSystem.AttachPerformer<KillEnemyGA>(KillEnemoyPerformer);
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<EnemyTurnGA>();
        ActionSystem.DetachPerformer<AttackHeroGA>();
        ActionSystem.DetachPerformer<KillEnemyGA>();    
    }
    public void Setup(List<EnemyData> enemyDatas)
    {
        foreach (var enemyData in enemyDatas)
        {
            enemyBoardView.AddEnemy(enemyData);
        }
    }
    private IEnumerator EnemyTurnPerformer(EnemyTurnGA enemyTurnGA)
    {
        foreach (var enemy in enemyBoardView.EnemyViews)
        {
            int burnStacks = enemy.GetStatusEffectStacks(StatusEffectType.BURN);
                if (burnStacks > 0)
                {
                    ApplyBurnGA applyBurnGA = new (burnStacks, enemy);
                    ActionSystem.Instance.AddReaction(applyBurnGA);
                }
            AttackHeroGA attackHeroGA = new AttackHeroGA(enemy, enemy);
            ActionSystem.Instance.AddReaction(attackHeroGA);
        }
        yield return null;
    
    }
    private IEnumerator AttackHeroPerformer(AttackHeroGA attackHeroGA)
    {
       EnemyView attacker = attackHeroGA.Attacker;
       Tween tween = attacker.transform.DOMoveX(attacker.transform.position.x-1f, 0.15f);
       Debug.Log("Enemy Attacking");
       yield return tween.WaitForCompletion();
       attacker.transform.DOMoveX(attacker.transform.position.x+1f, 0.25f);
       Debug.Log("Enemy Returning");
       yield return new WaitForSeconds(0.5f);
       DealDamageGA dealDamageGA = new DealDamageGA(attacker.AttackPower, new () {HeroSystem.Instance.HeroView}, attackHeroGA.Caster);
       ActionSystem.Instance.AddReaction(dealDamageGA);
    
    }
    private IEnumerator KillEnemoyPerformer(KillEnemyGA killEnemyGA)
    {
        yield return enemyBoardView.RemoveEnemy(killEnemyGA.enemyView);
        
    }


}
