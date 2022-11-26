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
    //Variables relacionado con daño
    public float initialDamage;
    float finalDamage;
    
    [Range(0.1f, 1f)]
    [SerializeField] float abilityModifier; //CAMBIAR ESTA VARIABLE A PUBLICA Y HACER QUE SEA UN SLIDE ENTRE 0 A 1 

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
 
    public int stunDamage;
    public string animationName;
    [HideInInspector] public Unit lastTarget;
    public Weapons weapon;

    public string[] description;

    [Header("AbilityEffects")]
    public List<Effect> inAbilityEffects;
    public List<Effect> postAbilityEffect;

    [Header("Sound Parameters")]
    public string soundString;

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


    public bool CanDoAbility(int actionPoints)
    {
        if(actionPoints < actionCost)
        {
            return false;
        }
        else
        {
            return true;
        }
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
    void CalculateDmg(PlayerUnit player, EnemyUnit target)
    {
        float criticalDmg = 1f;
        if (Random.value * 100 <= player.playerCriticalPercentage) criticalDmg = 1.5f;
        float elementEffectivenessNumber = ElementsEffectiveness.GetEffectiveness(player.playerElement, target.monsterElement);


        finalDamage = (((player.playerPower * criticalDmg) + (player.playerPower * player.playerElementPower) * elementEffectivenessNumber) * abilityModifier);
        
    }


    //When playerUnit does dmg to another playerUnit
    void CalculateDmg(PlayerUnit player,PlayerUnit target)
    {
        float criticalDmg = 1f;
        if (Random.value * 100 <= player.playerCriticalPercentage) criticalDmg = 1.5f;
        float elementDmg = ElementsEffectiveness.GetEffectiveness(player.playerElement, target.playerElement);


        finalDamage = (((player.playerPower * criticalDmg) + (player.playerPower * player.playerElementPower) * elementDmg) * abilityModifier) - target.playerDefense;
        
    }
    void CalculateHeal()
    {
        //Fill with calculate heal code
        finalHeal = initialHeal;
    }

    public void UseAbility(Unit target, BattleController controller)
    {
        //AQUI ES DONDE SE HACE EL ACTION COST
        //target.ActionsPerTurn -= ActionCost;
        controller.currentUnit.actionsPerTurn -= ActionCost;

        switch (abilityEffect)
        {
            case EffectType.Damage:
                AudioManager.instance.Play(soundString);
                target.DamageEffect();
                if (target.GetComponent<EnemyUnit>())
                {
                    CalculateDmg(controller.currentUnit,target.GetComponent<EnemyUnit>());
                    if (target.ReceiveDamage(finalDamage))
                    {
                        target.GetComponent<EnemyUnit>().Die(controller);
                    }
                    Debug.Log(target.health);
                    target.GetComponent<UnitUI>().CreatePopUpText(target.transform.position, (int)finalDamage);

                }
                else
                {
                    PlayerUnit u = target.GetComponent<PlayerUnit>();

                    CalculateDmg(controller.currentUnit,u);
                    if (u.ReceiveDamage(finalDamage))
                    {
                        u.NearDeath(controller);
                    }

                    u.status.HealthAnimation((int)target.health);
                }

                break;
                
            case EffectType.Heal:
                CalculateHeal();
                target.HealEffect();
                target.Heal(finalHeal);

                if (target.GetComponent<PlayerUnit>() != null)
                {
                    PlayerUnit u = target.GetComponent<PlayerUnit>();

                    if (u.isNearDeath)
                    {
                        u.Revive(controller);
                        u.Default();
                    }
                    u.status.HealthAnimation((int)target.health);
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
