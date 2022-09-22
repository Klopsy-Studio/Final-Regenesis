using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityVelocityCost
{
    Quick,
    Normal,
    Slow,
    VerySlow,
}

public enum TypeOfAbilityRange
{
    Cone,
    Constant,
    Infinite,
    LineAbility,
    SelfAbility,
    SquareAbility,
    Side,
    Cross,
    Normal,
};

public enum EffectType
{
    Damage,
    Heal,
    Buff,
    Debuff
};

[CreateAssetMenu(menuName = "Ability/New Ability")]
public class Abilities : ScriptableObject
{
    public AbilityVelocityCost abilityVelocityCost;
    public TypeOfAbilityRange rangeType;

    [Header("Effect parameters")]
    [SerializeField] public float cameraSize = 3f;
    [SerializeField] public float effectDuration = 0.5f;
    [SerializeField] public float shakeIntensity = 0.01f;
    [SerializeField] public float shakeDuration = 0.05f;


    public AbilityRange rangeScript
    {
        get
        {
            switch (rangeType)
            {
                case TypeOfAbilityRange.Cone:
                    return new ConeAbilityRange();
                case TypeOfAbilityRange.Constant:
                    return new ConstantAbilityRange();
                case TypeOfAbilityRange.Infinite:
                    return new InfiniteAbilityRange();
                case TypeOfAbilityRange.LineAbility:
                    return new LineAbilityRange();
                case TypeOfAbilityRange.SelfAbility:
                    return new SelfAbilityRange();
                case TypeOfAbilityRange.SquareAbility:
                    return new SquareAbilityRange();
                case TypeOfAbilityRange.Side:
                    return new SideAbilityRange();
                case TypeOfAbilityRange.Normal:
                    return new MovementRange();
                default:
                    return null;
            }
        }

        set
        {
            _rangeScript = value;
        }
    }

    AbilityRange _rangeScript;
    float velocityCost = 0;

    public int range;
    [Header("Ability Variables")]
    [SerializeField] int planticidaCost;
    public string abilityName;
    [SerializeField] EffectType abilityEffect;
    float planticidaBuffDmg;

    [Header("Damage")]
    //Variables relacionado con daño
    public float initialDamage;
    float finalDamage;
    

    [Header("Heal")]
    //Si la habilidad es de curación, se utilizan estas variables
    public float initialHeal;
    float finalHeal;

    [Header("Buff")]
    //Si la habilidad es un bufo, se usará esto
    public float initialBuff;

    [Header("Debuff")]
    //Si la habilidad es de debuffo, se usará esto
    public float initialDebuff;

    
    [Space]
    [Header("Others")]
    public int staminaCost;
    public int stunDamage;
    public string animationName;
    [HideInInspector] public Unit lastTarget;
    public Weapons weapon;

    public string[] description;

  
    public void SetUnitTimelineVelocity(Unit u)
    {

        u.TimelineVelocity += (int)abilityVelocityCost+1;
        Debug.Log("CURRENT VELOCITY ES " + u.TimelineVelocity);
    }

    public bool CheckUnitInRange(Board board)
    {
        List<Tile> tiles = rangeScript.GetTilesInRange(board);

        foreach(Tile t in tiles)
        {
            if(t.content != null)
            {
                if (t.content.GetComponent<PlayerUnit>())
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void UseAbilityAgainstPlayerUnit(Unit target)
    {
        CalculateDmg();
        target.ReceiveDamage(finalDamage);
    }

    void CalculateDmg()
    {
        if (weapon.planticidaPoints >= 75)
        {
            planticidaBuffDmg = 1.5f;
        }
        else if(weapon.planticidaPoints >= 40)
        {
            planticidaBuffDmg = 1.25f;
        }
        else if(weapon.planticidaPoints >= 0)
        {
            planticidaBuffDmg = 1f;
        }

        //desgaste planticidaPoints;
        weapon.PlanticidaLost(planticidaCost);
        Debug.Log("planticidaBuff: " + planticidaBuffDmg + "initial damage es " + initialDamage);
        //COMPROBAR CÓMO REDONDEA
        finalDamage = Mathf.Round(initialDamage * planticidaBuffDmg);
        Debug.Log("finaldamage: " + finalDamage + "planticidaPoints es " + weapon.planticidaPoints);
    }

    void CalculateHeal()
    {
        //Fill with calculate heal code
        finalHeal = initialHeal;
    }

    public void UseAbility(Unit target)
    {
        switch (abilityEffect)
        {
            case EffectType.Damage:

                target.DamageEffect();
                if (target.GetComponent<EnemyUnit>())
                {
                    CalculateDmg();
                    target.ReceiveDamageStun(finalDamage, stunDamage);     
                }
                else
                {
                    CalculateDmg();
                    target.ReceiveDamage(finalDamage);

                    if(target.GetComponent<PlayerUnit>() != null)
                    {
                        PlayerUnit u = target.GetComponent<PlayerUnit>();
                        u.status.HealthAnimation((int)target.health.Value);
                    }
                }

                break;
                
            case EffectType.Heal:
                CalculateHeal();
                target.HealEffect();
                target.Heal(finalHeal);

                if (target.GetComponent<PlayerUnit>() != null)
                {
                    PlayerUnit u = target.GetComponent<PlayerUnit>();
                    u.status.HealthAnimation((int)target.health.Value);
                }
                break;
            case EffectType.Buff:
                //Fill with buff code
                break;
            case EffectType.Debuff:
                //Fill with debuff code
                break;
            default:
                break;
        }
    }

}
