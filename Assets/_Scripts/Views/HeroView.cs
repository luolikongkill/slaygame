using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HeroView : CombatantView
{
    public Slider Hpslider;
    public void Setup(HeroData herodata)
    {
        SetupBase(herodata.CurrentHealth, herodata.Image);
    }

    public override void UpdateHealthText()
    {
        healthText.text = "HP:"+ CurHealth;
        Hpslider.maxValue = MaxHealth;
        Hpslider.value = CurHealth;
    }


        public override void Damage(int damageAmount)
    {
        damageAmount = singleEffectSystem.DamageSet(damageAmount);
        int reaminingDamage = damageAmount;
        int currentArmor = GetStatusEffectStacks(StatusEffectType.ARMOR);
        if (currentArmor > 0)
        {
            if(currentArmor >= reaminingDamage)
            {
                RemoveStatusEffect(StatusEffectType.ARMOR, reaminingDamage);
                reaminingDamage = 0;
            }
            else
            {
                RemoveStatusEffect(StatusEffectType.ARMOR, currentArmor);
                reaminingDamage -= currentArmor;
            }
        }
        if(reaminingDamage > 0)
        {
            CurHealth -= reaminingDamage;
            Debug.Log("FineDamage" + reaminingDamage);
            if (CurHealth < 0) 
            {
                CurHealth = 0;
                MatchSetupSystem.Instance.isPlayerDied = true;
                Interactions.Instance.GameIsOver =true;
            }
        }
        
        UpdateHealthText();
        if(CurHealth>0)transform.DOShakePosition(0.5f, 0.5f);
    }
    public override void EffectDamage(int damageAmount)
    {
        int reaminingDamage = damageAmount;
        if(reaminingDamage > 0)
        {
            CurHealth -= reaminingDamage;
            if (CurHealth < 0) 
            {
                CurHealth = 0;
                MatchSetupSystem.Instance.isPlayerDied = true;
                Interactions.Instance.GameIsOver =true;
            }
        }
        
        UpdateHealthText();
        if(CurHealth>0)transform.DOShakePosition(0.5f, 0.5f);
    }
}
