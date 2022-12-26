using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/New weapon")]
public class Weapons : Equipment
{
    public int criticalPercentage;
    public int CriticalPercentage { get { return criticalPercentage; } }

    [SerializeField] WeaponElement weaponAttackElement;
    public WeaponElement WeaponAttackElement { get { return weaponAttackElement; } }

    [SerializeField] WeaponElement weaponDefenseElement;
    public WeaponElement WeaponDefenseElement { get { return weaponDefenseElement; } }

    [SerializeField] private int power;
    public int Power { get { return power; } }

    [SerializeField] private int elementPower;
    public int ElementPower { get { return elementPower; } }

    [SerializeField] private int defense;
    public int Defense { get { return defense; } }




    public int originalRange;
    [HideInInspector] public int range;

    public Abilities[] Abilities;

    public Sprite weaponSprite;
    public Sprite weaponCombat;

    //Extra sprites if necessary
    public Sprite bowTensed;
    
    public Sprite weaponIcon;

    public override void EquipItem(PlayerUnit c)
    {
        c.power = power;
        c.criticalPercentage = criticalPercentage;
        c.attackElement = WeaponAttackElement;
        c.defenseElement = WeaponDefenseElement;
        c.elementPower = elementPower;
        c.defense = defense;

        foreach(Abilities a in Abilities)
        {
            if(a.sequence != null)
            {
                a.sequence.user = c;
                a.weapon = this;
                a.sequence.ability = a;
            }
        }

        switch (EquipmentType)
        {
            case KitType.Hammer:
                c.hammerSprite.sprite = weaponSprite;
                break;
            case KitType.Bow:
                c.bowSprite.sprite = weaponSprite;
                c.bowTensedSprite.sprite = bowTensed;
                break;
            case KitType.Gunblade:
                c.gunbladeSprite.sprite = weaponSprite;
                break;
            default:
                break;
        }
    }

    private void OnEnable()
    {
        range = originalRange;
    }


}
