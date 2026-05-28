using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCardsGA : GameAction
{
    public int Amount{ get; private set;}
    public DrawCardsGA(int amount)
    {
        Amount = amount;
    }
 
}
