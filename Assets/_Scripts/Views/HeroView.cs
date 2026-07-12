using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroView : CombatantView
{
    public void Setup(HeroData herodata)
    {
        SetupBase(herodata.CurrentHealth, herodata.Image);
    }
    public void Setupv1(int health, Sprite image)
    {
        SetupBase(health, image);
    }
}
