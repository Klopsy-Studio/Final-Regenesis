using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : Action
{
    public override void Act(MonsterController controller)
    {
        Attack(controller);
    }

    private void Attack(MonsterController controller)
    {

    }
}
