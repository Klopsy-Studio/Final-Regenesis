using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("you win");
        StartCoroutine(VictoryCor());
    }

    IEnumerator VictoryCor()
    {
        owner.turnStatusUI.ActivateVictoryTurn();
        yield return new WaitForSeconds(2);
        owner.turnStatusUI.DeactivatePlayerTurn();
    }
}
