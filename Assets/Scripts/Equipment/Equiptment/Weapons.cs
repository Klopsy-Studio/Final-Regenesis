using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/New weapon")]
public class Weapons : Equipment
{
    int criticalPercentage;
    public int CriticalPercentage { get { return criticalPercentage; } }

    [SerializeField] E_Effectiveness elements_Effectiveness;
    public E_Effectiveness Elements_Effectiveness { get { return elements_Effectiveness; } }

    [SerializeField] private int power;
    public int Power { get { return power; } }

    [SerializeField] private int elementPower;
    public int ElementPower { get { return elementPower; } }

    [SerializeField] private int defense;
    public int Defense { get { return defense; } }

    [SerializeField] private int staminaCost;
    public int StaminaCost { get { return staminaCost; } }


    public int originalRange;
    [HideInInspector] public int range;

    public Abilities[] Abilities;

    public Sprite weaponSprite;
    public Sprite weaponCombat;
    
    public Sprite weaponIcon;

    public override void EquipItem(Unit c)
    {
        c.damage = power;
    }

    private void OnEnable()
    {
        range = originalRange;
    }


}
