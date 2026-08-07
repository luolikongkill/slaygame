
public enum EnemyActionType 
{
   Attack,
   Effect,
   Defense,
   Stuned,
   Burn,
   Weakness,
   Vulnerable

}
[System.Serializable]
public class EnemyAction
{
    public EnemyActionType type;
    public int count;
    public CombatantView target;
}

public enum EnemyState
{
    Idle,
    Attack,
    Stun,
    Skill,
    Die
}
