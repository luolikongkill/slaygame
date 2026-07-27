using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class KillEnemyGA : GameAction
{
    public EnemyView enemyView{ get; private set; }
    public KillEnemyGA(EnemyView enemyView)
    {
        if(enemyView == null)
        {
            Debug.LogWarning("EnemyView is null in KillEnemyGA");
            return;
        }
        this.enemyView = enemyView;
    }
}
    
