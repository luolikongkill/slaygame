using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class EnemySystem : MonoBehaviour
{
    [SerializeField] private EnemyBoardView enemyBoardView;

    public List<EnemyView> Enemies => enemyBoardView.EnemyViews;
    private RoomType curRoomType;

    public delegate void GameIsOver(RoomType roomType);
    public static event GameIsOver CanGetReward;

    void OnEnable()
    {
        ActionSystem.AttachPerformer<EnemyTurnGA>(EnemyTurnPerformer);
        ActionSystem.AttachPerformer<AttackHeroGA>(AttackHeroPerformer);
        ActionSystem.AttachPerformer<KillEnemyGA>(KillEnemyPerformer);
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<EnemyTurnGA>();
        ActionSystem.DetachPerformer<AttackHeroGA>();
        ActionSystem.DetachPerformer<KillEnemyGA>();    
    }
    public void Setup(List<EnemyData> enemyDatas, RoomType roomType)
    {
        this.curRoomType = roomType;
        foreach (var enemyData in enemyDatas)
        {
            enemyBoardView.AddEnemy(enemyData);
        }
    }
    public void Setup(EnemyGroup enemyGroup, RoomType roomType)
    {
        this.curRoomType = roomType;

        enemyBoardView.AddEnemy(enemyGroup);
        
        UIChangeSet.Instance.UIChange(2);

    }
    public void Reset()
    {
        enemyBoardView.RemoveAllEnemyView(enemyBoardView.EnemyViews);
        enemyBoardView.EnemyViews.Clear();
        if (enemyBoardView.EnemyViews.Count == 0)
        {
            Debug.Log("EnemySystem has been reset.");
        }
    }

    public void RefreshEnemyIntention()
    {
        foreach (var enemy in enemyBoardView.EnemyViews)
            enemy.UpdateEnemyIntention();
    }
    private IEnumerator EnemyTurnPerformer(EnemyTurnGA enemyTurnGA)
    {
        foreach (var enemy in enemyBoardView.EnemyViews)
        {
            effectdecrease(enemy);
            if(MatchSetupSystem.Instance.isPlayerDied||Interactions.Instance.GameIsOver)
                break;
            
            ActionSystem.Instance.AddReaction(enemy.PlayEnemyAction());

        }
        yield return null;
    
    }

    private void effectdecrease(EnemyView enemy)
    {
        int burnStacks = enemy.GetStatusEffectStacks(StatusEffectType.BURN);
        if (burnStacks > 0)
        {
            ApplyBurnGA applyBurnGA = new (burnStacks, enemy);
            ActionSystem.Instance.AddReaction(applyBurnGA);
        }//damageystem extension
        int vunlerStacks = enemy.GetStatusEffectStacks(StatusEffectType.VULNERABLE);
        if (vunlerStacks > 0)
        {
            ApplyVulnerableGA applyVulnerableGA  = new (vunlerStacks, enemy);
            ActionSystem.Instance.AddReaction(applyVulnerableGA);
        }//damageystem extension
        int weaknessStacks = enemy.GetStatusEffectStacks(StatusEffectType.WEAKNESS);
        if (vunlerStacks > 0)
        {
            ApplyWeaknessGA ga    = new (weaknessStacks, enemy);
            ActionSystem.Instance.AddReaction(ga);
        }//damageystem extension
    }
    private IEnumerator AttackHeroPerformer(AttackHeroGA attackHeroGA)
    {
       EnemyView attacker = attackHeroGA.Attacker;
    //    Tween tween = attacker.transform.DOMoveX(attacker.transform.position.x-1f, 0.15f);
       attacker.animChange(EnemyState.Attack);
       Debug.Log("Enemy Attacking");
       yield return new WaitForSeconds(0.5f);
    //    attacker.transform.DOMoveX(attacker.transform.position.x+1f, 0.25f);
       yield return new WaitForSeconds(0.1f);
       DealDamageGA dealDamageGA = new DealDamageGA(attacker.AttackPower, new () {HeroSystem.Instance.HeroView}, attackHeroGA.Caster);
       ActionSystem.Instance.AddReaction(dealDamageGA);
    
    }
    private IEnumerator KillEnemyPerformer(KillEnemyGA killEnemyGA)
    {
        yield return new WaitForSeconds(0.1f);
        if(killEnemyGA.enemyView == null)
        {
            // Debug.LogError("EnemyView is null in KillEnemoyPerformer");
            yield break;
        }
        else
        {
            yield return enemyBoardView.RemoveEnemy(killEnemyGA.enemyView);
            Debug.Log("killGA"+Enemies.Count);
            if (Enemies.Count==0)
            {

                CanGetReward(curRoomType);
            }
        }
    }


}
