using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LayerOfEnemy", menuName = "Game/LayerOfEnemy")]
public class LayerOfEnemy : ScriptableObject
{
    public int layer;
    public RoomType roomType;
    public List<EnemyGroup> enemyGroup;
}
