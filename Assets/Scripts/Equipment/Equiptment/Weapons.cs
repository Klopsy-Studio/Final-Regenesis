using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/New weapon")]
public class Weapons : Equipment
{
    int criticalPercentage;
    public int CriticalPercentage { get { return criticalPercentage; } }

    [SerializeField] WeaponElement weaponPowerElement;
    public WeaponElement WeaponPowerElement { get { return weaponPowerElement; } }

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
    
    public Sprite weaponIcon;

    public override void EquipItem(PlayerUnit c)
    {
        c.playerPower = power;
        c.playerCriticalPercentage = criticalPercentage;
        c.playerAttackElement = WeaponPowerElement;
        c.playerDefenseElement = WeaponDefenseElement;
        c.playerElementPower = elementPower;
        c.playerDefense = defense;

    }

    private void OnEnable()
    {
        range = originalRange;
    }


}
