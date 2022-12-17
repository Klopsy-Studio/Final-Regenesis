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
    public TimelineVelocity previousVelocity;

    
  
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
    public int maxHealth;
    public int health;
 
  
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

    [Header("Testing")]
    [SerializeField] bool thisIsMyFuckingTurn;
    protected virtual void Start()
    {
        Match();
        UpdateCurrentVelocity();
       
        originalTimeStunned = timeStunned;
    }


    private void Update()
    {
        if (thisIsMyFuckingTurn)
        {
            fTimelineVelocity = 100000f;
        }
        
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
        
        health -= (int)damage;
        
        if (health <= 0)
        {
            health = 0;
            return true;
        }
        else
        {
            return false;
        }
      
    }

    public virtual bool ReceiveDamageStun(float damage, float StunDMG)
    {
        health-=(int)damage;

        if (health <= 0)
        {
            health = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void Heal(float heal)
    {
        AudioManager.instance.Play("HunterHeal");

        health += (int)heal;

        if(health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    public void UpdateCurrentVelocity()
    {
        //SetTimelineVelocityText();
        switch (timelineVelocity)
        {
            case TimelineVelocity.VerySlow:
                fTimelineVelocity = 9;
                break;
            case TimelineVelocity.Slow:
                fTimelineVelocity = 12;
                break;
            case TimelineVelocity.Normal:
                fTimelineVelocity = 15;
                break;
            case TimelineVelocity.Quick:
                fTimelineVelocity = 18;
                break;
            case TimelineVelocity.VeryQuick:
                fTimelineVelocity = 21;
                break;
            case TimelineVelocity.TurboFast:
                fTimelineVelocity = 24;
                break;
            default:
                break;
        }
    }

    public virtual void NearDeath(BattleController battleController)
    {

    }
    public virtual void Die(BattleController battleController)
    {
        battleController.unitsInGame.Remove(this);
        
    }

    public virtual void Stun()
    {
        if (!stunned)
        {
            //fTimelineVelocity = 0;
            previousVelocity = timelineVelocity;
            timelineVelocity = TimelineVelocity.Stun;
            Debug.Log(gameObject.name + "previousVelocity es " + previousVelocity);
            stunned = true;
            iconTimeline.velocityText.gameObject.SetActive(false);
            UpdateCurrentVelocity();
        }
    }
    public void SetVelocityWhenTurnIsFinished()
    {
        timelineVelocity += (int)actionsPerTurn;

        switch (timelineVelocity)
        {
            case TimelineVelocity.VerySlow:
                fTimelineVelocity = 9;
                break;
            case TimelineVelocity.Slow:
                fTimelineVelocity = 12;
                break;
            case TimelineVelocity.Normal:
                fTimelineVelocity = 15;
                break;
            case TimelineVelocity.Quick:
                fTimelineVelocity = 18;
                break;
            case TimelineVelocity.VeryQuick:
                fTimelineVelocity = 21;
                break;
            case TimelineVelocity.TurboFast:
                fTimelineVelocity = 24;
                break;
            //case TimelineVelocity.Stun:
            //    fTimelineVelocity = 0;
            //    break;
            default:
                break;
        }
    }

    public void DecreaseTimelineVelocity(int decrease)
    {
        timelineVelocity -= decrease;

        switch (timelineVelocity)
        {
            case TimelineVelocity.VerySlow:
                fTimelineVelocity = 9;
                break;
            case TimelineVelocity.Slow:
                fTimelineVelocity = 12f;
                break;
            case TimelineVelocity.Normal:
                fTimelineVelocity = 15;
                break;
            case TimelineVelocity.Quick:
                fTimelineVelocity = 18f;
                break;
            case TimelineVelocity.VeryQuick:
                fTimelineVelocity = 21;
                break;
            case TimelineVelocity.TurboFast:
                fTimelineVelocity = 24f;
                break;
            default:
                break;
        }
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
                TimelineVelocity = previousVelocity;
                UpdateCurrentVelocity();
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
        Debug.Log("Heal");
        GameObject temp = Instantiate(healEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), healEffect.transform.rotation);
        Destroy(temp, 0.8f);
    }

    
}
