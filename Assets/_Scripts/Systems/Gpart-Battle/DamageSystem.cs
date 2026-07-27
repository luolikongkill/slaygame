using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    [SerializeField] private GameObject damageVFX;
    void OnEnable()
    {
        ActionSystem.AttachPerformer<DealDamageGA>(DealDamagePerformer);
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<DealDamageGA>();
    }
    private IEnumerator DealDamagePerformer(DealDamageGA dealDamageGA)
    {
        if (dealDamageGA.Targets == null || dealDamageGA.Targets.Count == 0)
        {
            Debug.LogWarning("DealDamageGA has no targets.");
            yield break;
        }
        foreach (var target in dealDamageGA.Targets)
        {
            if(target.CurHealth <= 0)
            {
                if(target is EnemyView enemyView)
                {
                    KillEnemyGA killEnemyGA = new(enemyView);
                    ActionSystem.Instance.AddReaction(killEnemyGA);
                }
                else if(MatchSetupSystem.Instance.isPlayerDied == false)
                {
                    MatchSetupSystem.Instance.isPlayerDied = true;
                    Interactions.Instance.GameIsOver =true;
                    UIChangeSet.Instance.UIChange(RoomType.Empty);
                    Debug.Log("Player Died");
                    break;
                }
                continue;
            }
            else
            {
                 if (dealDamageGA.Targets == null)
                {
                    Debug.LogWarning("DealDamageGA has no targets or is died.");
                    break;
                }
                target.Damage(dealDamageGA.Amount);
                Instantiate(damageVFX, target.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
                if(target.CurHealth <= 0)
                {
                    if(target is EnemyView enemyView)
                    {
                        KillEnemyGA killEnemyGA = new(enemyView);
                        ActionSystem.Instance.AddReaction(killEnemyGA);
                    }
                    else if(MatchSetupSystem.Instance.isPlayerDied == false)
                    {
                        MatchSetupSystem.Instance.isPlayerDied = true;
                        Interactions.Instance.GameIsOver =true;
                        UIChangeSet.Instance.UIChange(RoomType.Empty);
                        Debug.Log("Player Died");
                        break;
                    }

                }
            }
           
        }
    }
}
