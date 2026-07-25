using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroView : CombatantView
{
    public Slider Hpslider;
    public void Setup(HeroData herodata)
    {
        SetupBase(herodata.CurrentHealth, herodata.Image);
    }
    public void Setupv1(int health, Sprite image)
    {
        SetupBase(health, image);
    }

    public override void UpdateHealthText()
    {
        healthText.text = "HP:"+ CurHealth;
        Hpslider.maxValue = MaxHealth;
        Hpslider.value = CurHealth;
    }
}
