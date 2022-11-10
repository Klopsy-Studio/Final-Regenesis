using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonsterController : MonsterController
{
    public override MonsterAbility ChooseRandomAttack()
    {
        MonsterAbility attack = validAbilities[Random.Range(0, validAbilities.Count)];
        possibleTargets = attack.ReturnPossibleTargets(this);

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
                target.Clear();
                target.Add(lowestHealthUnit);
            }

            else if(random == 1)
            {
                target.Add(possibleTargets[Random.Range(0, possibleTargets.Count)]);
            }
        }
        else
        {
            target.Add(possibleTargets[0]);
        }

        return attack;
    }

    public override MonsterAbility ChooseSpecificAttack()
    {
        MonsterAbility attack = validAttack;
        possibleTargets = attack.ReturnPossibleTargets(this);

        if (possibleTargets.Count > 1)
        {
            int random = Random.Range(0, 2);

            if (random == 0)
            {
                PlayerUnit lowestHealthUnit = null;

                foreach (PlayerUnit unit in possibleTargets)
                {
                    if (lowestHealthUnit != null)
                    {
                        if (unit.health.Value < lowestHealthUnit.health.Value)
                        {
                            lowestHealthUnit = unit;
                        }
                    }
                    else
                    {
                        lowestHealthUnit = unit;
                    }
                }

                target.Add(lowestHealthUnit);
            }

            else if (random == 1)
            {
                target.Add(possibleTargets[Random.Range(0, possibleTargets.Count)]);
            }
        }
        else
        {
            target.Add(possibleTargets[0]);
        }

        return attack;
    }

}
