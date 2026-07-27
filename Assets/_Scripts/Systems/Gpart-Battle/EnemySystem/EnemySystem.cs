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
        ActionSystem.AttachPerformer<KillEnemyGA>(KillEnemoyPerformer);
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
    public void Reset()
    {
        enemyBoardView.RemoveEnemy(enemyBoardView.EnemyViews);
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
            int burnStacks = enemy.GetStatusEffectStacks(StatusEffectType.BURN);
                if (burnStacks > 0)
                {
                    ApplyBurnGA applyBurnGA = new (burnStacks, enemy);
                    ActionSystem.Instance.AddReaction(applyBurnGA);
                }//damageystem extension
            if(MatchSetupSystem.Instance.isPlayerDied||Interactions.Instance.GameIsOver)
                break;
            
            ActionSystem.Instance.AddReaction(enemy.PlayEnemyAction());

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
        yield return new WaitForSeconds(0.1f);
        if(killEnemyGA.enemyView == null)
        {
            Debug.LogError("EnemyView is null in KillEnemoyPerformer");
            yield break;
        }
        else
        {
            yield return enemyBoardView.RemoveEnemy(killEnemyGA.enemyView);
            Debug.Log("killGA"+Enemies.Count);
            if (Enemies.Count==0)
            {   //敌人全部死亡创造游戏结束ga
                // GameEndGA gameEndGA = new GameEndGA (Enemies.Count);
                // ActionSystem.Instance.AddReaction(gameEndGA);
                CanGetReward(curRoomType);
            }
        }
    }


}
