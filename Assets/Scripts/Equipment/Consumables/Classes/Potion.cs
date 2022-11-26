using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Consumables/New Potion")]
public class Potion : Consumables
{
    public int addHealth;
   
    public override bool ApplyConsumable(Unit unit)
    {

        unit.health += addHealth;

        if(unit.health> unit.maxHealth)
        {
            unit.health = unit.maxHealth;
        }

        unit.HealEffect();
        
        if(unit.GetComponent<PlayerUnit>() != null)
        {
            PlayerUnit p = unit.GetComponent<PlayerUnit>();
            p.status.HealthAnimation(p.health);
        }
        return true;

    }

    public override bool ApplyConsumable(Tile t, BattleController battleController)
    {
        throw new System.NotImplementedException();
    }
}
