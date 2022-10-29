using System.Collections;
using System.Collections.Generic;
using UnityEngine;




//Right now, this script track if the unit is on the board and what direction is facing
//In the future, we should add stats of the unit
public class Unit : TimelineElements
{


    [Space]
    public Directions dir;
    public Tile tile { get; protected set; }
    public float unitSpeed;

    public TimelineElements element;

    public bool stunned;
    public float timeStunned;
    protected float originalTimeStunned;
    protected TimelineVelocity previousVelocity;

    [HideInInspector] public TimelineIconUI timelineIconUI;
    
    public override TimelineVelocity TimelineVelocity
    {
        get { return timelineVelocity; }

        set { timelineVelocity = value;/* SetCurrentVelocity(); */}
    }
    public override int ActionsPerTurn
    {
        get { return actionsPerTurn; }
        set { actionsPerTurn = value; }
    }


    //When unit uses its action, the turn goes to the next unit
    public bool turnEnded;
    public bool actionDone;
    public Point currentPoint;


    //Variables que se comparten entre unidades del jugador y del enemigo
    public Stats maxHealth;
    public Stats health;
    public Stats damage;
    public int stamina;

    public AnimationClip hurtAnimation;

    public string unitName;

    public GameObject particleHitPrefab;

    public bool isInAction = false;

    [HideInInspector] public bool isDead;

    [Header("Particles")]
    [SerializeField] GameObject healEffect;
    [SerializeField] GameObject hitEffect;


    [SerializeField] float stunThreshold;
    [SerializeField] float stunLimit;

    public SpriteRenderer unitSprite;
    protected virtual void Start()
    {
        Match();
        SetInitVelocity();
        stamina = 100;
        damage.baseValue = 10;
        originalTimeStunned = timeStunned;
    }

  

    public void Place(Tile target)
    {
        // Make sure old tile location is not still pointing to this unit
        if (tile != null && tile.content == gameObject)
            tile.content = null;
        // Link unit and tile references
        tile = target;

        if (target != null)
            target.content = gameObject;
    }

    public void Match()
    {
        transform.localPosition = tile.center;
        currentPoint = tile.pos;
    }

    public void ApplyStunValue(float value)
    {
        stunThreshold += value;

        if(stunThreshold >= stunLimit)
        {
            stunThreshold = 0;
            FallBackOnTimeline();
        }
    }

    public void FallBackOnTimeline()
    {
        timelineFill -= 50;

        if(timelineFill <= 0)
        {
            timelineFill = 0;
        }
    }
    public virtual bool ReceiveDamage(float damage)
    {
        health.AddModifier(new StatsModifier(-damage, StatModType.Flat, this));

        if (health.Value <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual bool ReceiveDamageStun(float damage, float StunDMG)
    {
        health.AddModifier(new StatsModifier(-damage, StatModType.Flat, this));

        if (health.Value <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void Heal(float heal)
    {
        health.AddModifier(new StatsModifier(heal, StatModType.Flat, this));

        if(health.Value >= maxHealth.Value)
        {
            health.baseValue = maxHealth.Value;
        }
    }

    public void SetInitVelocity()
    {
        switch (timelineVelocity)
        {
            case TimelineVelocity.VerySlow:
                fTimelineVelocity = 5;
                break;
            case TimelineVelocity.Slow:
                fTimelineVelocity = 10f;
                break;
            case TimelineVelocity.Normal:
                fTimelineVelocity = 15;
                break;
            case TimelineVelocity.Quick:
                fTimelineVelocity = 20f;
                break;
            case TimelineVelocity.VeryQuick:
                fTimelineVelocity = 25;
                break;
            case TimelineVelocity.TurboFast:
                fTimelineVelocity = 30f;
                break;
            default:
                break;
        }
    }

    public virtual void Die(BattleController battleController)
    {
        battleController.timelineElements.Remove(this);
        battleController.unitsInGame.Remove(this);
        
        gameObject.SetActive(false);
    }

    public virtual void Stun()
    {
        fTimelineVelocity = 0;
        previousVelocity = timelineVelocity;
        stunned = true;
    }
    public void SetCurrentVelocity()
    {
        timelineVelocity += (int)actionsPerTurn;
        //Debug.Log("TimelineVelocitydespues " + timelineVelocity +" valor" + (int)timelineVelocity);
        switch (timelineVelocity)
        {
            case TimelineVelocity.VerySlow:
                fTimelineVelocity = 5;
                break;
            case TimelineVelocity.Slow:
                fTimelineVelocity = 10f;
                break;
            case TimelineVelocity.Normal:
                fTimelineVelocity = 15;
                break;
            case TimelineVelocity.Quick:
                fTimelineVelocity = 20f;
                break;
            case TimelineVelocity.VeryQuick:
                fTimelineVelocity = 25;
                break;
            case TimelineVelocity.TurboFast:
                fTimelineVelocity = 30f;
                break;
            default:
                break;
        }
        //Debug.Log("CURRENT VELOCITY TYPE " + currentVelocity);

    }

    public override bool UpdateTimeLine()
    {
        if (!stunned)
        {
            if (timelineFill >= timelineFull)
            {
                return true;
            }

            timelineFill += fTimelineVelocity * Time.deltaTime;
            //Debug.Log(gameObject.name + "timelineFill " + timelineFill);

            return false;
        }

        else
        {
            Debug.Log("stunned");
            timeStunned -= Time.deltaTime;

            if(timeStunned <= 0)
            {
                timelineVelocity = previousVelocity;
                SetCurrentVelocity();
                stunned = false;
                timeStunned = originalTimeStunned;
            }

            return false;
        }
        
    }

    //public void DebugThings()
    //{
    //    Debug.Log( this.unitName + "current velocity: " + timelineVelocity);
    //}


    public virtual void Damage()
    {

    }

    public virtual void Default()
    {

    }

    public virtual void Attack()
    {

    }


    public void DamageEffect()
    {
        GameObject temp = Instantiate(hitEffect, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), hitEffect.transform.rotation);
        Destroy(temp, 0.8f);
    }

    public void HealEffect()
    {
        GameObject temp = Instantiate(healEffect, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), healEffect.transform.rotation);
        Destroy(temp, 0.8f);
    }

    
}
