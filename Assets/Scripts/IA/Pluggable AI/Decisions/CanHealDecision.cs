using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Can Heal")]

public class CanHealDecision : Decision
{
    public override bool Decide(MonsterController controller)
    {
        if(controller.obstaclesInGame.Count != 0)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
