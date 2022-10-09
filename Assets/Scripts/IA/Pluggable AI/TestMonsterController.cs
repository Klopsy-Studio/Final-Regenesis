using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonsterController : MonsterController
{
    public GameObject Obstacle;
    public Tile tileToPlaceObstacle;

    public override MonsterAbility ChooseAttack()
    {
        MonsterAbility randomAttack = validAbilities[Random.Range(0, validAbilities.Count)];
        possibleTargets = randomAttack.ReturnPossibleTargets(this);

        if(possibleTargets.Count > 1)
        {
            int random = Random.Range(0, 2);

            if(random == 0)
            {
                PlayerUnit lowestHealthUnit = null;
                
                foreach(PlayerUnit unit in possibleTargets)
                {
                    if(lowestHealthUnit != null)
                    {
                        if(unit.health.Value < lowestHealthUnit.health.Value)
                        {
                            lowestHealthUnit = unit;
                        }
                    }
                    else
                    {
                        lowestHealthUnit = unit;
                    }
                }

                target = lowestHealthUnit;
            }

            else if(random == 1)
            {
                target = possibleTargets[Random.Range(0, possibleTargets.Count)];
            }
        }
        else
        {
            target = possibleTargets[0];
        }

        return randomAttack;
    }

}
