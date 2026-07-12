using UnityEngine;

public class EndTurnButtonUI : MonoBehaviour
{
    
    public void OnClick()
    {
        if(!Interactions.Instance.PlayerCanInteract())
        {
            return;
        }
        EnemyTurnGA enemyTurnGA = new ();
        ActionSystem.Instance.Perform(enemyTurnGA);
    }
    void Update()
    {
        if(Input.GetButtonDown("EndTurn")&&Interactions.Instance.PlayerCanInteract())
        {
        EnemyTurnGA enemyTurnGA = new ();
        ActionSystem.Instance.Perform(enemyTurnGA); 
        }
    }
}
