using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/New Armor")]
public class Armor : Equipment
{
    [SerializeField] private float health;
    public float Health { get { return health; } }
    public override void EquipItem(Unit c)
    {
        c.health.AddModifier(new StatsModifier(health, StatModType.Flat, this));
    }

   
}
