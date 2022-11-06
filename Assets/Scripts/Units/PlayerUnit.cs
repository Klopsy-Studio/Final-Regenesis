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

    public void WeaponOut()
    {
        unitSprite.sprite = weaponSprite;
        Invoke("Combat", 0.1f);
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

    public override void Die(BattleController battleController)
    {
        status.gameObject.SetActive(false);
        playerUI.gameObject.SetActive(false);
        base.Die(battleController);
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
