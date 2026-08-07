using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class EnemyView : CombatantView
{
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private TMP_Text NameText;

    [SerializeField]private BoxCollider boxCollider;


    public Slider Hpslider;
    public int AttackPower { get;  set; }

    public EnemyData enemydata;
    public EnemyAction curaction;
    
    public Animator anim;

    private int actioncount = 0;
    public void Setup(EnemyData enemyData)
    {
        this.enemydata = enemyData;
        actioncount = 0;

        NameText.text = enemydata.EnemyName;

        UpdateEnemyIntention();
        SetupBase(enemyData.Health, enemyData.Image);
        if(MaxHealth > 300)
        {
            float size = Mathf.Min(1 + Mathf.Log((float)MaxHealth/300f), 3);
            Hpslider.GetComponent<RectTransform>().sizeDelta = new Vector2(400 * size,100 * (size + 1)/2.1f);
        }

        ColliderSet();
    }

    private void ColliderSet()
    {
        if(spriteRenderer == null || boxCollider == null) return;

        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        boxCollider.size = new Vector3 (spriteSize.x, spriteSize.y, 0.01f);

        boxCollider.size = new Vector3(boxCollider.size.x/transform.lossyScale.x,
        boxCollider.size.y/transform.lossyScale.y,boxCollider.size.z/transform.lossyScale.z);

        Vector3 centerOffset = spriteRenderer.bounds.center - transform.position;
        boxCollider.center = new Vector3(centerOffset.x/transform.lossyScale.x,centerOffset.y/transform.lossyScale.y,3);
    }

    public override void UpdateHealthText()
    {
        healthText.text = "HP:"+ CurHealth;
        Hpslider.maxValue = MaxHealth;
        Hpslider.value = CurHealth; 

    }

    public void UpdateEnemyIntention()
    {
        curaction = enemydata.enemyActions[actioncount++%enemydata.enemyActions.Count];
        attackText.text = curaction.type.ToString() + curaction.count;
        AttackPower = curaction.count;
    }


    public GameAction PlayEnemyAction()
    {
        AttackPower = singleEffectSystem.AttackSet(AttackPower);
        switch (curaction.type)
        {
            case EnemyActionType.Attack:
                return new AttackHeroGA(this,this);
            case EnemyActionType.Defense:         
                return new AddStatusEffectGA(StatusEffectType.ARMOR,this, AttackPower,this);
            case EnemyActionType.Burn:         
                return new AddStatusEffectGA(StatusEffectType.BURN, this,AttackPower,HeroSystem.Instance.HeroView);    
            case EnemyActionType.Vulnerable:         
                return new AddStatusEffectGA(StatusEffectType.VULNERABLE, this,AttackPower,HeroSystem.Instance.HeroView);
            case EnemyActionType.Weakness:         
                return new AddStatusEffectGA(StatusEffectType.WEAKNESS, this,AttackPower,HeroSystem.Instance.HeroView); 
            default:
                break;
        }

         return null;

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
                KillEnemyGA killEnemyGA = new(this);
                ActionSystem.Instance.AddReaction(killEnemyGA);
                if(anim != null) animChange(EnemyState.Die);
            }
        }
        if(anim != null)
        animChange(EnemyState.Stun);
        UpdateHealthText();
        // if(CurHealth>0)transform.DOShakePosition(0.5f, 0.5f);
    }
    public override void EffectDamage(int damageAmount)
    {
        int reaminingDamage = damageAmount;
        Debug.Log("effectDamage" + damageAmount);
        if(reaminingDamage > 0)
        {
            CurHealth -= reaminingDamage;
            if (CurHealth < 0) 
            {
                CurHealth = 10;
                if(anim != null) animChange(EnemyState.Die);
            }
        }
        
        if(anim != null)
        animChange(EnemyState.Stun);
        UpdateHealthText();
        if(CurHealth>0)transform.DOShakePosition(0.5f, 0.5f);
    }

    public override void animChange(EnemyState state)
    {
        if(anim == null) return;
        if(state == EnemyState.Attack) anim.Play("Enemy_Attack");
        else if(state == EnemyState.Stun) anim.Play("Enemy_Stun");
        else if(state == EnemyState.Skill) anim.Play("Enemy_Skill");
        else if(state == EnemyState.Die) anim.Play("Enemy_Die");
    }

    void OnMouseEnter()
    {
        NameText.color = Color.red;
    }
    void OnMouseExit()
    {
        NameText.color = Color.white;
    }
}
