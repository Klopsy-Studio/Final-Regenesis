using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Equipment/New weapon")]
public class Weapons : Equipment
{
    const int planticidaFull = 100;
    [HideInInspector ]public int planticidaPoints;
  

    [SerializeField] private int damage;
    [SerializeField] private int sharpness;
    [SerializeField] private int staminaCost;
    public int range;
    
    public int Damage { get { return damage; } }
    public int Sharpness { get { return sharpness; } }

    public int StaminaCost { get { return staminaCost; } }

    public Abilities[] Abilities;

    public Sprite weaponIcon;
    public override void EquipItem(Unit c)
    {
        planticidaPoints = planticidaFull;
        c.damage.AddModifier(new StatsModifier(damage, StatModType.Flat, this));
    }

    public void PlanticidaLost(int i)
    {
        planticidaPoints -= i;
        if (planticidaPoints <= 0)
        {
            planticidaPoints = 0;
        }
    }

  
}
