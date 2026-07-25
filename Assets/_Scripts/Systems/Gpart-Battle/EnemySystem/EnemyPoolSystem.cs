using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolSystem : Singleton<EnemyPoolSystem>
{
    public EnemySystem  enemySystem;

    [SerializeField]public LayerOfEnemy NormalEnemy;

    [SerializeField]public LayerOfEnemy  EliteEnemy;
    [SerializeField]public LayerOfEnemy  BossEnemy;

    public void Setup(RoomType roomType)
    {
        switch(roomType)
        {
            case RoomType.Normal:
                enemySystem.Setup(NormalEnemy.enemyGroup[0].enemydatas,roomType);
                break;
            case RoomType.Elite:
                enemySystem.Setup(EliteEnemy.enemyGroup[0].enemydatas,roomType);
                break;   
            case RoomType.Boss:
                enemySystem.Setup(BossEnemy.enemyGroup[0].enemydatas,roomType);
                break;
            default:
                Debug.Log("WarnOfUnknowroomType" + roomType);
                 break; 
        }
        ;
    }

    
}
