using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventActiveState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        owner.isTimeLineActive = false;
        owner.turnStatusUI.ActivateBanner();
        owner.turnStatusUI.EventTurn();
        StartCoroutine(EventCoroutine());
    }

    IEnumerator EventCoroutine()
    {
        owner.environmentEvent.ApplyEffect();
        yield return new WaitForSeconds(1);
        owner.ChangeState<TimeLineState>();

    }

    public override void Exit()
    {
        base.Exit();
        owner.isTimeLineActive = true;
    }
}
