using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Consumables/New Potion")]
public class Potion : Consumables
{
    public int addHealth;
   
    public override bool ApplyConsumable(Unit unit)
    {
        unit.Heal(addHealth);
        unit.HealEffect();
        
        return true;
    }

    public override bool ApplyConsumable(Tile t, BattleController battleController)
    {
        if(t.content != null)
        {
            if(t.content.GetComponent<Unit>() != null)
            {
                Unit unit = t.content.GetComponent<Unit>();

                unit.Heal(addHealth);

                unit.HealEffect();

                
                if (unit.GetComponent<PlayerUnit>() != null)
                {
                    PlayerUnit p = unit.GetComponent<PlayerUnit>();
                    p.animations.SetInject();

                    if (p != battleController.currentUnit)
                    {
                        battleController.currentUnit.animations.SetThrow();
                    }

                    if (p.isNearDeath)
                    {
                        p.Revive(battleController);
                    }

                }
                return true;
            }

            return false;
            
        }

        return false;
        
    }

}
