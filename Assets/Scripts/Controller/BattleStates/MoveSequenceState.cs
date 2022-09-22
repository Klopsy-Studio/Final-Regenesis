using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSequenceState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        owner.currentUnit.isInAction = true;
        owner.isTimeLineActive = false;
        StartCoroutine(Sequence());
    }

    public override void Exit()
    {
        base.Exit();
        owner.currentUnit.isInAction = false;
    }

    IEnumerator Sequence()
    { 
        Movement m = owner.currentUnit.GetComponent<Movement>();

        owner.currentUnit.currentPoint = owner.currentTile.pos;
        yield return StartCoroutine(m.SimpleTraverse(owner.currentTile));

        while (m.moving)
        {
            yield return null;
        }

        owner.currentUnit.actionDone = true;

        owner.ChangeState<SelectActionState>();

    }





}
