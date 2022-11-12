using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit  
{
    public UnitProfile profile;
    [HideInInspector] public Sprite unitPortrait;
    [HideInInspector] public Sprite fullUnitPortrait;
    public bool didNotMove;
    public Weapons weapon;


    public bool changing;

    [HideInInspector] public UnitStatus status;

    public PlayerUnitUI playerUI;


    //Sprites

    [HideInInspector] public Sprite idleSprite;
    [HideInInspector] public Sprite weaponSprite;
    [HideInInspector] public Sprite combatSprite;
    [HideInInspector] public Sprite attackSprite;
    [HideInInspector] public Sprite damageSprite;
    [HideInInspector] public Sprite pushSprite;
    [HideInInspector] public Sprite nearDeathSprite;
    [HideInInspector] public Sprite deathSprite;

    [Header("VFX")]
    [SerializeField] Animator movementEffect;

    [Header("Unit Death")]
    public PlayerUnitDeath nearDeathElement;
    PlayerUnitDeath deathElement;

    [HideInInspector] public bool isNearDeath;
    protected override void Start()
    {
        base.Start();

        playerUI.unitUI.worldCamera = Camera.main;
        playerUI.unitUI.planeDistance = 0.01f;

        didNotMove = true;
        timelineFill = Random.Range(70, 90);
        //ESTO DEBERÍA DE ESTAR EN UNIT

        timelineTypes = TimeLineTypes.PlayerUnit;
        EquipAllItems();
    }


    //ESTA FUNCION HAY QUE REVISARLA
    public void EquipAllItems()
    {
        if (weapon == null) { return; }
        health.baseValue = 100;

        
        weapon.EquipItem(this);
        maxHealth.baseValue = health.Value;

    }

    public bool CanMove()
    {
        if(didNotMove && actionsPerTurn >= 2)
        {
            return true;
        }
        else
        {
            return false;
        }        
    }


    public bool CanDoAbility()
    {
        foreach (Abilities a in weapon.Abilities)
        {
            if (actionsPerTurn >= a.actionCost)
            {
                return true;
            }
        }

        return false;
    }

    public void NearDeathSprite()
    {
        unitSprite.sprite = nearDeathSprite;
    }

    public void DeathSprite()
    {
        unitSprite.sprite = deathSprite;
    }
    public override void Damage()
    {
        base.Damage();
        unitSprite.sprite = damageSprite;
        Invoke("Default", 1.5f);
    }
    public override void Attack()
    {

        unitSprite.sprite = attackSprite;
    }

    public override void Default()
    {
        unitSprite.sprite = idleSprite;
    }

    public void Push()
    {
        unitSprite.sprite = pushSprite;
    }

    public void MovementEffect()
    {
        movementEffect.SetTrigger("move");
    }
    public void WeaponOut()
    {
        unitSprite.sprite = weaponSprite;
        Invoke("Combat", 0.1f);
    }
    public void DeathsDoor()
    {
        unitSprite.sprite = nearDeathSprite;
    }
    public void WeaponIn()
    {
        unitSprite.sprite = weaponSprite;
        Invoke("Default", 0.1f);
    }
    public void Combat()
    {
        unitSprite.sprite = combatSprite;
    }

    public override void NearDeath(BattleController battleController)
    {
        NearDeathSprite();
        PlayerUnitDeath element = Instantiate(nearDeathElement);
        isNearDeath = true;
        deathElement = element;
        deathElement.unit = this;
        battleController.timelineElements.Add(element);
        battleController.timelineElements.Remove(this);
    }

    public void Revive(BattleController battleController)
    {
        battleController.timelineElements.Add(this);
        battleController.timelineElements.Remove(deathElement);
        Destroy(deathElement);
        playerUI.gameObject.SetActive(true);

    }
    public override void Die(BattleController battleController)
    {
        base.Die(battleController);
        DeathSprite();
        status.gameObject.SetActive(false);
        Destroy(this);
    }

    public override void Stun()
    {
        base.Stun();
        iconTimeline.EnableStun();
        playerUI.EnableStun();
        Push();
        Invoke("Default", 1f);
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

            if (timeStunned <= 0)
            {
                timelineVelocity = previousVelocity;
                SetCurrentVelocity();
                stunned = false;
                timeStunned = originalTimeStunned;
                playerUI.DisableStun();
                iconTimeline.DisableStun();
            }

            return false;
        }

    }
}
