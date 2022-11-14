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
    public PlayerUnitDeath deathElement;

    [HideInInspector] public bool isNearDeath;

    [Header("Weapons")]
    public WeaponSpriteData hammerData;
    public WeaponOffset hammerOffset;
    public WeaponSpriteData slingShotData;
    public WeaponOffset slingshotOffset;
    protected override void Start()
    {
        base.Start();

        playerUI.unitUI.worldCamera = Camera.main;
        playerUI.unitUI.planeDistance = 0.01f;

        didNotMove = true;
        timelineFill = Random.Range(10, 30);
        //ESTO DEBERÍA DE ESTAR EN UNIT

        timelineTypes = TimeLineTypes.PlayerUnit;

        switch (weapon.EquipmentType)
        {
            case EquipmentType.Hammer:
                hammerData.savedWeaponSprite.gameObject.SetActive(true);
                hammerData.idleCombatSprite.gameObject.SetActive(true);
                hammerData.attackSprite.gameObject.SetActive(true);
                ApplyOffSetToWeapon(hammerData, hammerOffset);
                hammerData.savedWeaponSprite.gameObject.SetActive(true);
                hammerData.idleCombatSprite.gameObject.SetActive(false);
                hammerData.attackSprite.gameObject.SetActive(false);
                break;
            case EquipmentType.Slingshot:
                slingShotData.savedWeaponSprite.gameObject.SetActive(true);
                slingShotData.idleCombatSprite.gameObject.SetActive(true);
                slingShotData.attackSprite.gameObject.SetActive(true);

                ApplyOffSetToWeapon(slingShotData, slingshotOffset);

                slingShotData.savedWeaponSprite.gameObject.SetActive(true);
                slingShotData.idleCombatSprite.gameObject.SetActive(false);
                slingShotData.attackSprite.gameObject.SetActive(false);
                break;
            default:
                break;
        }
        EquipAllItems();
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
        if (weapon == null) { return; }
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
        switch (weapon.EquipmentType)
        {
            case EquipmentType.Hammer:
                hammerData.idleCombatSprite.gameObject.SetActive(false);
                hammerData.attackSprite.gameObject.SetActive(true);

                break;
            case EquipmentType.Slingshot:
                slingShotData.idleCombatSprite.gameObject.SetActive(false);
                slingShotData.attackSprite.gameObject.SetActive(true);
                break;
            default:
                break;
        }
        unitSprite.sprite = attackSprite;
    }

    public void DefaultCombat()
    {
        switch (weapon.EquipmentType)
        {
            case EquipmentType.Hammer:
                hammerData.savedWeaponSprite.gameObject.SetActive(true);
                hammerData.idleCombatSprite.gameObject.SetActive(false);
                break;
            case EquipmentType.Slingshot:
                slingShotData.savedWeaponSprite.gameObject.SetActive(true);
                slingShotData.idleCombatSprite.gameObject.SetActive(false);
                break;
            default:
                break;
        }

        unitSprite.sprite = idleSprite;
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
        Invoke("DefaultCombat", 0.1f);
    }
    public void Combat()
    {
        switch (weapon.EquipmentType)
        {
            case EquipmentType.Hammer:
                hammerData.savedWeaponSprite.gameObject.SetActive(false);
                hammerData.idleCombatSprite.gameObject.SetActive(true);
                break;
            case EquipmentType.Slingshot:
                slingShotData.savedWeaponSprite.gameObject.SetActive(false);
                slingShotData.idleCombatSprite.gameObject.SetActive(true);
                break;
            default:
                break;
        }

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
        iconTimeline.gameObject.SetActive(false);
        timelineTypes = TimeLineTypes.Null;
    }

    public void Revive(BattleController battleController)
    {
        deathElement.DisableDeath(battleController);
        playerUI.gameObject.SetActive(true);
        timelineFill = 0;
        iconTimeline.gameObject.SetActive(true);
        isNearDeath = false;
        timelineTypes = TimeLineTypes.PlayerUnit;
    }
    public override void Die(BattleController battleController)
    {
        base.Die(battleController);
        DeathSprite();
        status.gameObject.SetActive(false);
        isDead = true;
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

        else
        {
            return false;
        }
        

    }
}
