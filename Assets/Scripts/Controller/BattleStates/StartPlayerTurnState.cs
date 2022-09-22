using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlayerTurnState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        owner.turnStatusUI.ActivateBanner();
        owner.turnStatusUI.PlayerTurn();
        //StartCoroutine(BeginPlayerTurn());
        StartCoroutine(SetStats());
    }

    IEnumerator SetStats()
    {
        owner.currentUnit.TimelineVelocity = TimelineVelocity.None;
      
        owner.currentUnit.stamina += 50;
        yield return null;
        owner.ChangeState<SelectActionState>();
    }

    //version anterior
    //IEnumerator BeginPlayerTurn()
    //{
    //    foreach (Unit u in playerUnits)
    //    {
    //        if(u.turnEnded)
    //        {
    //            u.stamina += 50;
    //        }
    //        u.EnableUnit();
    //        unitsWithActions.Add(u);
    //    }

    //    tileSelectionIndicator.gameObject.SetActive(true);

    //    owner.turnStatusUI.ActivatePlayerTurn();

    //    yield return new WaitForSeconds(2);

    //    owner.turnStatusUI.DeactivatePlayerTurn();
    //    yield return null;
    //    owner.ChangeState<SelectUnitState>();
    //}
}
