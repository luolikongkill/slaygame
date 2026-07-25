using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadomEnemyTM : TargetMode
{
    public override List<CombatantView> GetTargets()
    {
        CombatantView target = EnemyPoolSystem.Instance.enemySystem.Enemies[Random.Range(0, EnemyPoolSystem.Instance.enemySystem.Enemies.Count)];
        return new() { target };
     }

}
