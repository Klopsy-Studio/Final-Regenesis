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
    AlternateSide,
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
    [Range (1,5)]
    public int actionCost;
    public int ActionCost
    {
        get { return actionCost; }
    }
    public AbilityVelocityCost abilityVelocityCost;


    public RangeData rangeData;
    [Header("Effect parameters")]
    [SerializeField] public float cameraSize = 3f;
    [SerializeField] public float effectDuration = 0.5f;
    [SerializeField] public float shakeIntensity = 0.01f;
    [SerializeField] public float shakeDuration = 0.05f;

    [Header("")]
    public AbilityRange rangeScript;
   
    [Header("Ability Variables")]
 
    public string abilityName;
    [SerializeField] EffectType abilityEffect;
  

    [Header("Damage")]
    //Variables relacionado con da�o
    public float initialDamage;
    float finalDamage;
    
    [Range(0.1f, 1f)]
    [SerializeField] float abilityModifier; //CAMBIAR ESTA VARIABLE A PUBLICA Y HACER QUE SEA UN SLIDE ENTRE 0 A 1 

    [Header("Heal")]
    //Si la habilidad es de curaci�n, se utilizan estas variables
    public float initialHeal;
    float finalHeal;

    [Header("Buff")]
    //Si la habilidad es un bufo, se usar� esto
    public float initialBuff;

    [Header("Debuff")]
    //Si la habilidad es de debuffo, se usar� esto
    public float initialDebuff;

    
    [Space]
    [Header("Others")]
 
    public int stunDamage;
    public string animationName;
    [HideInInspector] public Unit lastTarget;
    public Weapons weapon;

    public string[] description;


    private void Awake()
    {
        GetRangeScript();
    }

    public void SetUnitTimelineVelocityAndActionCost(Unit u)
    {
        u.ActionsPerTurn -= actionCost;
        u.TimelineVelocity += (int)abilityVelocityCost+1;
        Debug.Log("CURRENT VELOCITY ES " + u.TimelineVelocity + u.gameObject.name + "CURRENT UNIT ACTIONS " + u.ActionsPerTurn);
    }

    public bool CheckUnitInRange(Board board)
    {
        rangeScript.AssignVariables(rangeData);
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

    //public void UseAbilityAgainstPlayerUnit(Unit target)
    //{
    //    CalculateDmg();
    //    target.ReceiveDamage(finalDamage);
    //}
    public void GetRangeScript()
    {
        switch (rangeData.range)
        {
            case TypeOfAbilityRange.Cone:
                rangeScript = new ConeAbilityRange();
                break;
            case TypeOfAbilityRange.Constant:
                rangeScript = new ConstantAbilityRange();
                break;
            case TypeOfAbilityRange.Infinite:
                rangeScript = new InfiniteAbilityRange();
                break;
            case TypeOfAbilityRange.LineAbility:
                rangeScript = new LineAbilityRange();
                break;
            case TypeOfAbilityRange.SelfAbility:
                rangeScript = new SelfAbilityRange();
                break;
            case TypeOfAbilityRange.SquareAbility:
                rangeScript = new SquareAbilityRange();
                break;
            case TypeOfAbilityRange.Side:
                rangeScript = new SideAbilityRange();
                break;
            case TypeOfAbilityRange.Normal:
                rangeScript = new MovementRange();
                break;
            default:
                break;
        }
    }
    void CalculateDmg(EnemyUnit enemy)
    {

        float criticalDmg = 1f;
        if (Random.value * 100 <= weapon.CriticalPercentage) criticalDmg = 1.5f;
        float elementDmg = ElementsEffectiveness.GetEffectiveness(weapon.Elements_Effectiveness, enemy.Elements_Effectiveness);


        finalDamage = (((weapon.Power * criticalDmg) + (weapon.Power * weapon.ElementPower) * elementDmg) * abilityModifier) - weapon.Defense;
      
        //if (weapon.planticidaPoints >= 75)
        //{
        //    planticidaBuffDmg = 1.5f;
        //}
        //else if(weapon.planticidaPoints >= 40)
        //{
        //    planticidaBuffDmg = 1.25f;
        //}
        //else if(weapon.planticidaPoints >= 0)
        //{
        //    planticidaBuffDmg = 1f;
        //}

        ////desgaste planticidaPoints;
        //weapon.PlanticidaLost(planticidaCost);
        //Debug.Log("planticidaBuff: " + planticidaBuffDmg + "initial damage es " + initialDamage);
        ////COMPROBAR C�MO REDONDEA
        //finalDamage = Mathf.Round(initialDamage * planticidaBuffDmg);
        //Debug.Log("finaldamage: " + finalDamage + "planticidaPoints es " + weapon.planticidaPoints);
    }

    void CalculateHeal()
    {
        //Fill with calculate heal code
        finalHeal = initialHeal;
    }

    public void UseAbility(Unit target)
    {
        //AQUI ES DONDE SE HACE EL ACTION COST
        target.ActionsPerTurn -= ActionCost;

        switch (abilityEffect)
        {
            case EffectType.Damage:

                target.DamageEffect();
                if (target.GetComponent<EnemyUnit>())
                {
                    CalculateDmg(target.GetComponent<EnemyUnit>());
                    target.ReceiveDamageStun(finalDamage, stunDamage);
                  
                }
                else
                {
                    //CalculateDmg();
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
