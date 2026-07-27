using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;

[Serializable] 
public abstract class GameAction
{
    public List<GameAction> PreReactions {get; private set; } =new ();
    public List<GameAction> PerformReactions {get; private set; }= new();
    public List<GameAction> PostReactions { get; private set; } = new();
    


}
