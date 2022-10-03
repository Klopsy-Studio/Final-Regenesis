using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/StartTurnAction")]
public class StartTurnAction : Action
{

    public override void Act(MonsterController controller)
    {
        Debug.Log("ACTIONS monstruo");
        controller.battleController.SelectTile(controller.currentEnemy.tile.pos);
    }

  


}

