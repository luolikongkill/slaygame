using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : Singleton<Interactions>
{
    public bool PlayerIsDragging { get;  set; } = false;
    public bool GameIsOver { get; set; } = false;

    public bool PlayerCanInteract()
    {

        if (!ActionSystem.Instance.IsPerforming&&!GameIsOver) return true;
        else return false;
    }

    public bool PlayerCanHover()
    {
        if (PlayerIsDragging&&!GameIsOver) return false;
        return true;
    }
    
}
