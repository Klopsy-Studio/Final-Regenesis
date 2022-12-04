using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weasel.Utils;

public class PlayerUnit : Unit  
{
    public UnitProfile profile;
    [HideInInspector] public Sprite unitPortrait;
    [HideInInspector] public Sprite fullUnitPortrait;
    public bool didNotMove;
    public Weapons weapon;
    [Header("PlayerStats")]
    public int playerPower;
    public int playerCriticalPercentage;
    public WeaponElement playerElement;
    public int playerElementPower;
    public int playerDefense;

    public bool changing;

    [HideInInspector] public UnitStatus status;

    public PlayerUnitUI playerUI;


    [Header("Animations")]
    public UnitAnimations animations;

    [HideInInspector] public Sprite idleSprite;
    [HideInInspector] public Sprite weaponSprite;
    [HideInInspector] public Sprite combatSprite;
    [HideInInspector] public Sprite attackSprite;
    [HideInInspector] public Sprite damageSprite;
    [HideInInspector] public Sprite pushSprite;
    [HideInInspector] public Sprite nearDeathSprite;
    [HideInInspector] public Sprite deathSprite;
    [HideInInspector] public Sprite deathTimelineSprite;

    
    [Header("VFX")]
    [SerializeField] Animator movementEffect;

    [Header("Unit Death")]
    public PlayerUnitDeath nearDeathElement;
    public PlayerUnitDeath deathElement;

    [HideInInspector] public bool isNearDeath;

    [Header("Weapons")]
    public SpriteRenderer hammerSprite;
    public SpriteRenderer slingshotSprite;

    
    protected override void Start()
    {
        base.Start();

        playerUI.unitUI.worldCamera = Camera.main;
        playerUI.unitUI.planeDistance = 0.01f;

        didNotMove = true;
        timelineFill = Random.Range(50, 90);
        //ESTO DEBERÍA DE ESTAR EN UNIT

        timelineTypes = TimeLineTypes.PlayerUnit;

       
        EquipAllItems();

        switch (weapon.EquipmentType)
        {
            case EquipmentType.Hammer:
                animations.SetAnimation("hammer");
                break;
            case EquipmentType.Slingshot:
                animations.SetAnimation("slingshot");
                break;
            default:
                break;
        }
    }

    public void ApplyOffset(GameObject data, Vector3 offset)
    {
        if(offset != Vector3.zero)
        {
            data.transform.localPosition = new Vector3(offset.x, offset.y, offset.z);
        }
    }

    public void ApplyOffSetToWeapon(WeaponSpriteData data, WeaponOffset offset)
    {
        ApplyOffset(data.savedWeaponSprite.gameObject, offset.savedWeaponSpriteOffset);
        ApplyOffset(data.idleCombatSprite.gameObject, offset.idleCombatSpriteOffset);
        ApplyOffset(data.attackSprite.gameObject, offset.attackSpriteOffset);
    }
    
    //ESTA FUNCION HAY QUE REVISARLA
    public void EquipAllItems()
    {
        if (weapon == null) { Debug.Log("No hay weapon"); return; }
        health = 100;

        
        weapon.EquipItem(this);

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
        animations.SetNearDeath();
    }

    public override void Damage()
    {
        animations.SetDamage();
        Invoke("Default", 0.5f);
    }
    public override void Attack()
    {
        switch (weapon.EquipmentType)
        {
            case EquipmentType.Hammer:
                animations.SetAttackHammer();
                break;
            case EquipmentType.Slingshot:
                animations.SetAttackSlingshot();
                break;
            default:
                break;
        }
    }

    public void DefaultCombat()
    {
        switch (weapon.EquipmentType)
        {
            case EquipmentType.Hammer:
                animations.SetCombatIdle();
                break;
            case EquipmentType.Slingshot:
                animations.SetCombatIdle();
                break;
            default:
                break;
        }

        unitSprite.sprite = idleSprite;
    }
    public override void Default()
    {
        animations.SetIdle();
    }

    public void Push()
    {
        animations.SetPush();
    }

    public void MovementEffect()
    {
        movementEffect.SetTrigger("move");
    }
    public void WeaponOut()
    {
        animations.SetCombatIdle();

        //For later, to take out each weapon depending on type

        //switch (weapon.EquipmentType)
        //{
        //    case EquipmentType.Hammer:
        //        hammerData.savedWeaponSprite.gameObject.SetActive(false);
        //        hammerData.idleCombatSprite.gameObject.SetActive(true);
        //        break;
        //    case EquipmentType.Slingshot:
        //        slingShotData.savedWeaponSprite.gameObject.SetActive(false);
        //        slingShotData.idleCombatSprite.gameObject.SetActive(true);
        //        break;
        //    default:
        //        break;
        //}
    }



    public override void NearDeath(BattleController battleController)
    {
        NearDeathSprite();
        PlayerUnitDeath element = Instantiate(nearDeathElement);
        element.timelineIcon = deathTimelineSprite;
        status.unitPortrait.sprite = deathTimelineSprite;
        isNearDeath = true;
        deathElement = element;
        deathElement.unit = this;
        battleController.timelineElements.Add(element);
        iconTimeline.gameObject.SetActive(false);
        timelineTypes = TimeLineTypes.Null;
    }

    public void Revive(BattleController battleController)
    {
        deathElement.DisableDeath(battleController);
        status.unitPortrait.sprite = timelineIcon;
        Default();
        playerUI.gameObject.SetActive(true);
        timelineFill = 0;
        iconTimeline.gameObject.SetActive(true);
        isNearDeath = false;
        timelineTypes = TimeLineTypes.PlayerUnit;
    }
    public override void Die(BattleController battleController)
    {
        battleController.playerUnits.Remove(this);

        animations.SetDeath();
        AudioManager.instance.Play("HunterDeath");
        status.gameObject.SetActive(false);
        isDead = true;
        battleController.CheckAllUnits();
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
        if (!isNearDeath)
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

        else
        {
            return false;
        }
        

    }
}
