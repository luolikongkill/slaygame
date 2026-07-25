using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyGroup", menuName = "Game/Enemy Group")]
public class EnemyGroup : ScriptableObject
{

    public List<EnemyData> enemydatas;
}
