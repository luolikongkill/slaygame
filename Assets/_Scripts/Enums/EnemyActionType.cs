
public enum EnemyActionType 
{
   Attack,
   Effect,
   Defense,
   Stuned,
   Burn,

}
[System.Serializable]
public class EnemyAction
{
    public EnemyActionType type;
    public int count;
    public CombatantView target;
}
