using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("you lost");
    }

    IEnumerator DefeatCor()
    {
        owner.turnStatusUI.ActivateGameOverTurn();
        yield return new WaitForSeconds(2);
        owner.turnStatusUI.DeactivateEnemyTurn();
    }
}
