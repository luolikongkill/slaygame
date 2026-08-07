using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolSystem : Singleton<EnemyPoolSystem>
{
    public EnemySystem  enemySystem;
    public NodeEventManager eventmanager;

    [SerializeField]public LayerOfEnemy NormalEnemy;
    [SerializeField]public LayerOfEnemy  EliteEnemy;
    [SerializeField]public LayerOfEnemy  BossEnemy;


    [SerializeField]public EventData[] eventDatas;


    public void Setup(RoomType roomType)
    {
        switch(roomType)
        {
            case RoomType.Normal:
                enemySystem.Setup(NormalEnemy.enemyGroup[RandomData(NormalEnemy.enemyGroup.Count)],roomType);
                break;
            case RoomType.Elite:
                enemySystem.Setup(EliteEnemy.enemyGroup[RandomData(EliteEnemy.enemyGroup.Count)],roomType);
                break;   
            case RoomType.Boss:
                enemySystem.Setup(BossEnemy.enemyGroup[RandomData(BossEnemy.enemyGroup.Count)],roomType);
                break;

            case RoomType.Event:
                eventmanager.Setup(eventDatas[0]);
                break;
            default:
                enemySystem.Setup(BossEnemy.enemyGroup[0],roomType);
                Debug.Log("WarnOfUnknowroomType, Setup bossroom" + roomType);
                 break; 
        }
        ;
    }


    private int RandomData(int Count)
    {
        int index = Random.Range(0,Count);
        return index;
    }



    // public void Setup(RoomType roomType)
    // {
    //     switch(roomType)
    //     {
    //         case RoomType.Normal:
    //             enemySystem.Setup(NormalEnemy.enemyGroup[0].enemydatas,roomType);
    //             break;
    //         case RoomType.Elite:
    //             enemySystem.Setup(EliteEnemy.enemyGroup[0].enemydatas,roomType);
    //             break;   
    //         case RoomType.Boss:
    //             enemySystem.Setup(BossEnemy.enemyGroup[0].enemydatas,roomType);
    //             break;

    //         case RoomType.Event:
    //             eventmanager.Setup(eventDatas[0]);
    //             break;
    //         default:
    //             enemySystem.Setup(BossEnemy.enemyGroup[0].enemydatas,roomType);
    //             Debug.Log("WarnOfUnknowroomType, Setup bossroom" + roomType);
    //              break; 
    //     }
    //     ;
    // }

    
}
