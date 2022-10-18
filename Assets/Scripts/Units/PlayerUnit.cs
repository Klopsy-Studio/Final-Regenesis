using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit  
{
    public UnitProfile profile;
    public SpriteRenderer unitSprite;
    [HideInInspector] public Sprite unitPortrait;
    [HideInInspector] public Sprite fullUnitPortrait;
    public bool didNotMove;
    public Weapons weapon;


    public bool changing;

    [HideInInspector] public UnitStatus status;

    public PlayerUnitUI playerUI;
    protected override void Start()
    {
        base.Start();

        playerUI.unitUI.worldCamera = Camera.main;
        playerUI.unitUI.planeDistance = 0.01f;
        playerUI.unitUI.gameObject.SetActive(false);

        didNotMove = true;
        timelineFill = Random.Range(0, 50);
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
       
        return didNotMove;
        
    }


    public bool CanDoAbility()
    {
       

        foreach (Abilities a in weapon.Abilities)
        {
            if (actionsPerTurn >= (int)a.abilityVelocityCost)
            {
                return true;
            }
        }

        return false;
    }

    public override void React()
    {
        base.React();
        unitSprite.sprite = profile.unitReaction;
    }
    public override void Attack()
    {
        unitSprite.sprite = profile.unitAction;
    }

    public override void Default()
    {
        unitSprite.sprite = profile.unitSprite;
    }




}
