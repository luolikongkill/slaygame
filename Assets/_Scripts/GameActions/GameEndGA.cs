using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameEndGA : GameAction
{
    public int EnemiesCount {get; private set;}
    public GameEndGA()
    {
        
    }
    public GameEndGA(int enemiescount)
    {
        EnemiesCount = enemiescount;
        Debug.Log("count"+EnemiesCount);
    }


}
