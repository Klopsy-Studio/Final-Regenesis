using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Consumables/New Potion")]
public class Potion : Consumables
{
    public int addHealth;
   
    public override bool ApplyConsumable(Unit unit)
    {
        
    
        //if(unit.health.Value == unit.maxHealth.Value || unit.health.Value <0)
        //{
        //    Debug.Log("can't use item because unit has max health");
        //    return false;
        //}

        unit.health.AddModifier(new StatsModifier(addHealth, StatModType.Flat, this));
        Debug.Log("unit se ha sanado. Su vida actual es " + unit.health.Value);
        return true ;
        //unit.health.AddModifier(new StatsModifier(addHealth, StatModType.Flat, this));

    }

    public override bool ApplyConsumable(Tile t, BattleController battleController)
    {
        throw new System.NotImplementedException();
    }
}
